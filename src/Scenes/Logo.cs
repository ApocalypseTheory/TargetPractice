using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TargetPractice.Scenes;

public class LogoScene : DrawableGameComponent
{
    private SpriteBatch _spriteBatch;
    private ContentManager _sceneContent;
    private Texture2D _logo;
    private Texture2D _fadeToBlack;
    private float _logoTimeElapsed = 0f;
    private float _logoDuration = 3f;
    private float _overlayAlpha = 0f;
    private float _screenAspect, _logoAspect;
    private int _scaledWidth, _scaledHeight;
    private Rectangle _logoDestRect, _fullScreenRect;
    private Vector2 _screenSize;
    private Vector2 _currentScreenSize;
    public LogoScene(Game game) : base(game)
    {
        _sceneContent = new ContentManager(Game.Services, Game.Content.RootDirectory);
        Game.Components.Add(this);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _logo = _sceneContent.Load<Texture2D>("images/logo");
        _logoAspect = _logo.Width / (float)_logo.Height;
        _fadeToBlack = new Texture2D(GraphicsDevice, 1, 1);
        _fadeToBlack.SetData(new Color[] { Color.Black });
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        CheckForWindowResize();
        base.LoadContent();


    }

    public override void Update(GameTime gameTime)
    {
        _logoTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_logoTimeElapsed > 1f && _logoTimeElapsed <= _logoDuration)
        {
            _overlayAlpha = MathHelper.Lerp(0f, 1f, (_logoTimeElapsed - 1f) / 0.5f);
            _overlayAlpha = MathHelper.Clamp(_overlayAlpha, 0f, 1f);
        }

        if (_logoTimeElapsed > _logoDuration)
        {
            Game.Components.Add(new MainMenuScene(Game));
            Game.Components.Remove(this);
        }
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        _spriteBatch.Draw(_logo, _logoDestRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f); _spriteBatch.Draw(_fadeToBlack, _fullScreenRect, null, new Color(0f, 0f, 0f, _overlayAlpha), 0f, Vector2.Zero, SpriteEffects.None, 1.0f);
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    public void CheckForWindowResize()
    {
        if (_currentScreenSize == new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)) return;
        _currentScreenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
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

    protected override void UnloadContent()
    {
        _sceneContent.Unload();
        base.UnloadContent();
    }
}