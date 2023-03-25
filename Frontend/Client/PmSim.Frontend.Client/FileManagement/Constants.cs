namespace PmSim.Frontend.Client.FileManagement;

/// <summary>
/// Contains immutable values used when working with files.
/// </summary>
public static class Constants
{
    /// <summary>
    /// The directory of the program files. Stores configurations and logs.
    /// </summary>
    private static readonly string AppDataDirectory
        = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
          + Path.DirectorySeparatorChar + "PmSimApp" + Path.DirectorySeparatorChar;

    /// <summary>
    /// The extension of the files in which configuration data is written.
    /// </summary>
    public const string ConfigurationFileExtension = ".json";

    /// <summary>
    /// The directory stores configurations.
    /// </summary>
    public static string ConfigurationDirectory { get; } 
        = AppDataDirectory + "Config" + Path.DirectorySeparatorChar;

    /// <summary>
    /// The full name of the file in which logs are written.
    /// </summary>
    public static string LogFileFullName { get; }
        = AppDataDirectory + "pm_sim_app_log.txt";
}