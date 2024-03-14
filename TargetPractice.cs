using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TargetPractice;

public class TargetPractice : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D _logo;
    float _logoTimeElapsed = 0f;
    float _logoDuration = 3f;
    Texture2D _fadeToBlack;
    float _overlayAlpha = 0f;

    public TargetPractice()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        ConfigureBorderlessFullScreen();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _logo = Content.Load<Texture2D>("images/logo");
        _fadeToBlack = new Texture2D(GraphicsDevice, 1, 1);
        _fadeToBlack.SetData(new Color[] { Color.Black });
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _logoTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_logoTimeElapsed > 1f && _logoTimeElapsed <= _logoDuration)
        {
            _overlayAlpha = MathHelper.Lerp(0f, 1f, (_logoTimeElapsed - 1f) / 0.5f);
            _overlayAlpha = MathHelper.Clamp(_overlayAlpha, 0f, 1f);


        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        DrawLogo();

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

    private void DrawLogo()
    {
        if (_logoTimeElapsed > _logoDuration)
        {
            return;
        }

        float screenAspect = GraphicsDevice.Viewport.Width / (float)GraphicsDevice.Viewport.Height;
        float logoAspect = _logo.Width / (float)_logo.Height;
        int scaledWidth, scaledHeight;

        if (logoAspect > screenAspect)
        {
            scaledWidth = GraphicsDevice.Viewport.Width;
            scaledHeight = (int)(scaledWidth / logoAspect);
        }
        else
        {
            scaledHeight = GraphicsDevice.Viewport.Height;
            scaledWidth = (int)(scaledHeight * logoAspect);
        }

        Rectangle logoDestRect = new Rectangle((GraphicsDevice.Viewport.Width - scaledWidth) / 2, (GraphicsDevice.Viewport.Height - scaledHeight) / 2, scaledWidth, scaledHeight);
        _spriteBatch.Draw(_logo, logoDestRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

        Rectangle fullScreenRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        _spriteBatch.Draw(_fadeToBlack, fullScreenRect, null, new Color(0f, 0f, 0f, _overlayAlpha), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
    }


}
