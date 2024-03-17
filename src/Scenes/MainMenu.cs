using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TargetPractice.Scenes;

public class MainMenuScene : DrawableGameComponent
{
    private SpriteBatch _spriteBatch;
    private ContentManager _sceneContent;
    private SpriteAtlas _spriteAtlas;
    private float _logoTimeElapsed = 0f;
    private float _logoDuration = 3f;
    private float _overlayAlpha = 0f;
    private float _screenAspect, _logoAspect;
    private int _scaledWidth, _scaledHeight;
    private Rectangle _logoDestRect, _fullScreenRect;
    private Vector2 _screenSize;
    public MainMenuScene(Game game) : base(game)
    {
        _sceneContent = new ContentManager(Game.Services, Game.Content.RootDirectory);
        _spriteAtlas = new SpriteAtlas(_sceneContent);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteAtlas.LoadSheet("spritesheet_hud");
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        SetWindowSize();
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
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        _spriteAtlas.Draw(_spriteBatch, "spritesheet_hud", "crosshair_blue_large.png", new Vector2(100, 100), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    public void SetWindowSize()
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

    protected override void UnloadContent()
    {
        _sceneContent.Unload();
        base.UnloadContent();
    }
}