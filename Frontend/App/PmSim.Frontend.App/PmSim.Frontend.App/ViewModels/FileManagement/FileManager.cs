using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PmSim.Frontend.App.ViewModels.FileManagement;

/// <summary>
/// Serializes and saves objects to files.
/// Loads and deserializes objects from files.
/// </summary>
public class FileManager
{
    /// <summary>
    /// Gets the full name of the object configuration file by type name.
    /// </summary>
    public static string GetConfigurationFileName(MemberInfo type)
        => Constants.ConfigurationDirectory + type.Name.ToLower() 
            + Constants.ConfigurationFileExtension;

    /// <summary>
    /// Serializes the object to the specified file.
    /// </summary>
    public static void SaveConfiguration(object configuration, string filename, 
        string? exceptionMessage = null)
    {
        try
        {
            var file = CreateFile(filename);
            var json = Serialize(configuration);
            File.WriteAllText(file.FullName, json);
        }
        catch (Exception exception)
        {
            if (exceptionMessage is null)
            {
                exceptionMessage = exception.Message;
            }
            else
            {
                exceptionMessage += filename;
            }

            throw new Exception(exceptionMessage);
        }
    }

    /// <summary>
    /// Serializes and saves the object. The save file is determined programmatically.
    /// </summary>
    public static void SaveConfiguration(object configuration) 
        => SaveConfiguration(configuration, 
            GetConfigurationFileName(configuration.GetType()));

    /// <summary>
    /// Deserializes an object from a file.
    /// </summary>
    public static T LoadConfiguration<T>(string filename, string deserializationExceptionMessage = "")
    {
        var data = File.ReadAllText(filename);
        try
        {
            return Deserialize<T>(data);
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException(deserializationExceptionMessage + filename);
        }
    }

    private static FileInfo CreateFile(string path)
    {
        var file = new FileInfo(path);
        if (file.Directory is null)
        {
            throw new IOException("File access error: " + path);
        }

        file.Directory.Create();
        return file;
    }

    private static string Serialize(object serializable)
    {
        var json = new JsonObject();
        var value = JsonValue.Create(serializable);
        json.Add(serializable.GetType().Name, value);
        return json.ToJsonString();
    }

    private static T Deserialize<T>(string data)
    {
        var node = JsonNode.Parse(data);
        var value = node?[typeof(T).Name]?.ToString() ?? throw new NullReferenceException();
        var instance = JsonSerializer.Deserialize<T>(value);
        return instance ?? throw new NullReferenceException();
    }
}