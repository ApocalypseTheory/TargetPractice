
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TargetPractice.Scenes;


namespace TargetPractice;

public class TargetPractice : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;


    public TargetPractice()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        ConfigureBorderlessFullScreen();
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
        // OnResize();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        base.Draw(gameTime);
        _spriteBatch.End();
    }

    private void ConfigureBorderlessFullScreen()
    {
        Window.AllowUserResizing = false;
        Window.IsBorderless = true;
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
    }
}
