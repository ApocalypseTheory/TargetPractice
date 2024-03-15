
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TargetPractice.Logo;

namespace TargetPractice;

public class TargetPractice : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Logo _logo;


    public TargetPractice()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        ConfigureBorderlessFullScreen();
    }

    protected override void Initialize()
    {
        _logo = new Logo();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        var logo = Content.Load<Texture2D>("images/logo");
        var overlay = new Texture2D(GraphicsDevice, 1, 1);
        _logo.LoadContent(logo, overlay);
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _logo.Update(gameTime, GraphicsDevice);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        _logo.Draw(_spriteBatch);
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
