
using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TargetPractice.Scenes;
using TargetPractice.Tools;


namespace TargetPractice;

public class TargetPractice : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly Settings _settings;
    private SpriteBatch _spriteBatch;




    public TargetPractice()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _settings = new Settings(this);
        Components.Add(_settings);
        Console.WriteLine("TargetPractice constructor");
    }

    protected override void Initialize()
    {
        var scene = new LogoScene(this);
        Components.Add(scene);
        base.Initialize();

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        base.Draw(gameTime);
        _spriteBatch.End();
    }



    public void ChangeScene(DrawableGameComponent currentScene, string nextScene)
    {
        Console.WriteLine($"Changing scene to {nextScene}");
        DrawableGameComponent scene;
        Components.Remove(currentScene);
        switch (nextScene)
        {
            case "MainMenu":
                scene = new MainMenuScene(this);
                break;
            case "Logo":
                scene = new LogoScene(this);
                break;
            default:
                throw new InvalidEnumArgumentException();
        }
        Components.Add(scene);
    }


}