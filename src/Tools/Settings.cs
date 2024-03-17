using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetPractice.Tools;

public class Settings : DrawableGameComponent
{

    GraphicsDeviceManager _graphics;
    DisplayMode _displayMode;

    public Settings(Game game) : base(game)
    {
        _graphics = (GraphicsDeviceManager)game.Services.GetService(typeof(IGraphicsDeviceManager));
        _displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
    }

    public override void Initialize()
    {
        Console.WriteLine("Initializing settings");
        var settings = ReadSettings();
        Console.WriteLine(settings);
        ApplySettings(settings);
        base.Initialize();
    }

    public static Dictionary<string, string> ReadSettings()
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
        return settings;
    }

    public void ApplySettings(Dictionary<string, string> settings)
    {
        Console.WriteLine(settings["Mode"]);

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

    public void ConfigureGraphicsFullScreen()
    {
        Console.WriteLine("Configuring full screen");
        Game.Window.AllowUserResizing = false;
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = _displayMode.Width;
        _graphics.PreferredBackBufferHeight = _displayMode.Height;
        _graphics.ApplyChanges();
    }
    public void ConfigureBorderlessFullScreen()
    {
        Console.WriteLine("Configuring borderless full screen");
        Game.Window.AllowUserResizing = false;
        Game.Window.IsBorderless = true;
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = _displayMode.Width;
        _graphics.PreferredBackBufferHeight = _displayMode.Height;
        _graphics.ApplyChanges();
    }

    public void ConfigureGraphicsWindowed(int width = 800, int height = 480)
    {
        Console.WriteLine("Configuring windowed");
        Game.Window.AllowUserResizing = true;
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = width;
        _graphics.PreferredBackBufferHeight = height;
        _graphics.ApplyChanges();
    }
}