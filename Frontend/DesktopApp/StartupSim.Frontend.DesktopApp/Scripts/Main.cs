using System;
using Godot;
using StartupSim.Frontend.Domain.Interfaces;
using StartupSim.Frontend.Domain.Languages.English;
using StartupSim.Frontend.Domain.Languages.Russian;
using StartupSim.Frontend.DesktopApp.Scripts.Enums;
using StartupSim.Frontend.Domain;

public class Main : Node {

	private ILanguageProvider[] _languageProviders { get; }
		= {
			new EnglishLanguageProvider(),
			new RussianLanguageProvider()
		};

	private IStartupSimDomain _multiDomain;
	private readonly IStartupSimDomain _singleDomain = new StartupSimDomain();

	private readonly BaseScreen[] _screens = {
			(BaseScreen) ((PackedScene) GD.Load("res://Scenes/TitleScreen.tscn")).Instance(),
			(BaseScreen) ((PackedScene) GD.Load("res://Scenes/OptionsScreen.tscn")).Instance(),
			(BaseScreen) ((PackedScene) GD.Load("res://Scenes/GameSettingsScreen.tscn")).Instance(),
			(BaseScreen) ((PackedScene) GD.Load("res://Scenes/GamesListScreen.tscn")).Instance(),
			(BaseScreen) ((PackedScene) GD.Load("res://Scenes/SignUpScreen.tscn")).Instance(),
			(BaseScreen) ((PackedScene) GD.Load("res://Scenes/SignInScreen.tscn")).Instance(),
			(BaseScreen) ((PackedScene) GD.Load("res://Scenes/EmailConfirmationScreen.tscn")).Instance(),
			(BaseScreen) ((PackedScene) GD.Load("res://Scenes/SingleSignInScreen.tscn")).Instance(),
		};
	
	public ILanguageProvider LanguageProvider { get; private set; }

	public bool IsSingleGame { get; set; }

	public IStartupSimDomain Domain
	{
		get => IsSingleGame ? _singleDomain : _multiDomain;

		set
		{
			if (!IsSingleGame)
			{
				_multiDomain = value;
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var screen in _screens)
		{
			screen.Core = this;
		}
		LanguageProvider = _languageProviders[0];
		ChangeScreen(Screens.Title);
	}

	public void ChangeScreen(Screens screen)
	{
		ChangeScreen(_screens[(int)screen]);
	}

	public async void StartGame(int gameMapNumber)
	{
		var gameId = await Domain.CreateNewGameAsync(1, 5, 0);
		try
		{
			await Domain.ConnectToGame(gameId);
			await Domain.UpdateStatusAsync();
		}
		catch (Exception e)
		{
			GD.Print(e);
		}

		var screen = (BaseScreen)((PackedScene)GD.Load("res://Scenes/GameScreen.tscn")).Instance();
		screen.Core = this;
		ChangeScreen(screen);
		((GameScreen) GetChild(0)).GameMapNumber = gameMapNumber;
	}

	public void ChangeLanguage(Languages language)
	{
		LanguageProvider = _languageProviders[(int)language];
	}

	private void ChangeScreen(BaseScreen screen)
	{
		RemoveChild(GetChild(0));
		AddChild(screen);
		((BaseScreen)GetChild(0)).UpdateText();
		//GetTree().ChangeSceneTo(titleScreen);
	}
}