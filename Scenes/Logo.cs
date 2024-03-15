using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetPractice.Tools;

namespace TargetPractice.Scenes;

public class LogoScene : IScene
{
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
    public LogoScene(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
    }

    public event Action<SceneTypes> RequestSceneChange;
    public void Initialize()
    {
    }

    public void LoadContent()
    {
        _logo = _resourceManager.LoadTexture("images/logo");
        _logoAspect = _logo.Width / (float)_logo.Height;
        _fadeToBlack = _resourceManager.CreateOverlayTexture();
        _fadeToBlack.SetData(new Color[] { Color.Black });
    }

    public void Update(GameTime gameTime)
    {
        _logoTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_logoTimeElapsed > 1f && _logoTimeElapsed <= _logoDuration)
        {
            _overlayAlpha = MathHelper.Lerp(0f, 1f, (_logoTimeElapsed - 1f) / 0.5f);
            _overlayAlpha = MathHelper.Clamp(_overlayAlpha, 0f, 1f);
        }

        if (_logoTimeElapsed > _logoDuration)
        {
            RequestSceneChange?.Invoke(SceneTypes.MainMenu);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_logo, _logoDestRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        spriteBatch.Draw(_fadeToBlack, _fullScreenRect, null, new Color(0f, 0f, 0f, _overlayAlpha), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
    }

    public void OnResize(Vector2 newScreenSize)
    {
        _screenSize = newScreenSize;
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