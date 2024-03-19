using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetPractice.Tools;

public class Settings
{
    private static Settings _instance;
    private GraphicsDeviceManager _graphics;
    private DisplayMode _displayMode;
    private Game _game;
    private Dictionary<string, string> GlobalSettings;

    private Settings(Game game)
    {
        _game = game;
        var settings = ReadSettings();
        _graphics = (GraphicsDeviceManager)game.Services.GetService(typeof(IGraphicsDeviceManager));
        _displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
        ApplyVideoSettings(settings);
    }
    public static Settings Instance
    {

        get
        {
            if (_instance == null)
            {
                throw new Exception("Settings not initialized");
            }
            return _instance;
        }
    }

    public static void Initialize(Game game)
    {
        if (_instance == null)
        {
            _instance = new Settings(game);
        }
        else
        {
            throw new InvalidOperationException("Settings already initialized");
        }
    }

    private Dictionary<string, string> ReadSettings()
    {
        var settings = new Dictionary<string, string>();
        var lines = File.ReadAllLines("Content/settings.ini");
        foreach (var rawLine in lines)
        {
            var line = rawLine.Split('#', 2)[0].Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;
            var keyValue = line.Split('=', 2);
            if (keyValue.Length == 2)
            {
                settings[keyValue[0].Trim()] = keyValue[1].Trim();
            }
        }
        GlobalSettings = settings;
        return settings;
    }

    private void ApplyVideoSettings(Dictionary<string, string> settings)
    {
        switch (settings["Mode"])
        {
            case "FullScreen":
                ConfigureGraphicsFullScreen();
                break;
            case "BorderlessFullScreen":
                ConfigureBorderlessFullScreen();
                break;
            case "Windowed":
                ConfigureGraphicsWindowed(int.Parse(settings["Width"]), int.Parse(settings["Height"]));
                break;
            default:
                ConfigureGraphicsWindowed();
                break;
        }
    }

    private void ConfigureGraphicsFullScreen()
    {
        Console.WriteLine("Configuring full screen");
        _game.Window.AllowUserResizing = false;
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = _displayMode.Width;
        _graphics.PreferredBackBufferHeight = _displayMode.Height;
        _graphics.ApplyChanges();
    }

    private void ConfigureBorderlessFullScreen()
    {
        Console.WriteLine("Configuring borderless full screen");
        _game.Window.AllowUserResizing = false;
        _game.Window.IsBorderless = true;
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = _displayMode.Width;
        _graphics.PreferredBackBufferHeight = _displayMode.Height;
        _graphics.ApplyChanges();
    }

    private void ConfigureGraphicsWindowed(int width = 800, int height = 480)
    {
        Console.WriteLine("Configuring windowed");
        _game.Window.AllowUserResizing = true;
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = width;
        _graphics.PreferredBackBufferHeight = height;
        _graphics.ApplyChanges();
    }

    public string GetSetting(string key, string defaultValue = "")
    {
        if (GlobalSettings.TryGetValue(key, out string value))
        {
            return value;
        }
        return defaultValue;
    }

    public void UpdateSetting(string key, string newValue)
    {
        GlobalSettings[key] = newValue;
        var lines = File.ReadAllLines("Content/settings.ini");
        bool keyFound = false;
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Split('#', 2);
            var keyValue = line[0].Trim().Split('=', 2);

            if (keyValue.Length == 2 && keyValue[0].Trim() == key)
            {
                lines[i] = $"{keyValue[0].Trim()}={newValue}" + (line.Length > 1 ? $" # {line[1].Trim()}" : "");
                keyFound = true;
                break;
            }
        }

        if (keyFound)
        {
            File.WriteAllLines("Content/settings.ini", lines);
        }

    }

}
