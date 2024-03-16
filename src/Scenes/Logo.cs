using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetPractice.Tools;

namespace TargetPractice.Scenes;

public class LogoScene : DrawableGameComponent
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _logo;
    private Texture2D _fadeToBlack;
    private float _logoTimeElapsed = 0f;
    private float _logoDuration = 3f;
    private float _overlayAlpha = 0f;
    private float _screenAspect, _logoAspect;
    private int _scaledWidth, _scaledHeight;
    private Rectangle _logoDestRect, _fullScreenRect;
    private ResourceManager _resourceManager;
    private Vector2 _screenSize;
    public LogoScene(Game game, ResourceManager resourceManager) : base(game)
    {
        _resourceManager = resourceManager;
    }

    public override void Initialize()
    {
        Console.WriteLine("ONe");
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Console.WriteLine("Two");
        _logo = _resourceManager.LoadTexture("images/logo");
        _logoAspect = _logo.Width / (float)_logo.Height;
        _fadeToBlack = _resourceManager.CreateOverlayTexture();
        _fadeToBlack.SetData(new Color[] { Color.Black });
        _resourceManager.LoadSpriteAtlas("images/spritesheet_hud");
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        OnResize();
        base.LoadContent();


    }

    public override void Update(GameTime gameTime)
    {
        Console.WriteLine("Three");
        _logoTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_logoTimeElapsed > 1f && _logoTimeElapsed <= _logoDuration)
        {
            _overlayAlpha = MathHelper.Lerp(0f, 1f, (_logoTimeElapsed - 1f) / 0.5f);
            _overlayAlpha = MathHelper.Clamp(_overlayAlpha, 0f, 1f);
        }

        if (_logoTimeElapsed > _logoDuration)
        {
            //TODO: Change to Main Menu
        }
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Console.WriteLine("Four");
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        //_resourceManager.DrawSprite(_spriteBatch, "images/spritesheet_hud", "crosshair_blue_large.png", new Vector2(100, 100), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
        _spriteBatch.Draw(
            _logo
        , _logoDestRect
        , null
        , Color.White
        , 0f
        , Vector2.Zero
        , SpriteEffects.None
        , 0f);
        _spriteBatch.Draw(_fadeToBlack
        , _fullScreenRect
        , null, new Color(0f, 0f, 0f, _overlayAlpha), 0f, Vector2.Zero, SpriteEffects.None, 1.0f);

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    public void OnResize()
    {
        _screenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        _screenAspect = _screenSize.X / (float)_screenSize.Y;
        if (_logoAspect > _screenAspect)
        {
            _scaledWidth = (int)_screenSize.X;
            _scaledHeight = (int)(_scaledWidth / _logoAspect);
        }
        else
        {
            _scaledHeight = (int)_screenSize.Y;
            _scaledWidth = (int)(_scaledHeight * _logoAspect);

        }
        _logoDestRect = new Rectangle(((int)_screenSize.X - _scaledWidth) / 2, ((int)_screenSize.Y - _scaledHeight) / 2, _scaledWidth, _scaledHeight);
        _fullScreenRect = new Rectangle(0, 0, (int)_screenSize.X, (int)_screenSize.Y);
    }
}