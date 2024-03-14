using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TargetPractie.Logo;

namespace TargetPractie.Logo;

public class Logo
{
    Texture2D _logo;
    float _logoTimeElapsed = 0f;
    float _logoDuration = 3f;
    Texture2D _fadeToBlack;
    float _overlayAlpha = 0f;
    float _screenAspect, _logoAspect;
    int _scaledWidth, _scaledHeight;
    Rectangle _logoDestRect, _fullScreenRect;

    public Logo()
    {
    }

    public void LoadContent(Texture2D logo, Texture2D fadeToBlack)
    {
        _logo = logo;
        _fadeToBlack = fadeToBlack;
        _fadeToBlack.SetData(new Color[] { Color.Black });
    }

    public void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
    {
        if (_logoTimeElapsed > _logoDuration)
        {
            return;
        }
        _logoTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_logoTimeElapsed > 1f && _logoTimeElapsed <= _logoDuration)
        {
            _overlayAlpha = MathHelper.Lerp(0f, 1f, (_logoTimeElapsed - 1f) / 0.5f);
            _overlayAlpha = MathHelper.Clamp(_overlayAlpha, 0f, 1f);
        }
        _screenAspect = graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;
        _logoAspect = _logo.Width / (float)_logo.Height;
        if (_logoAspect > _screenAspect)
        {
            _scaledWidth = graphicsDevice.Viewport.Width;
            _scaledHeight = (int)(_scaledWidth / _logoAspect);
        }
        else
        {
            _scaledHeight = graphicsDevice.Viewport.Height;
            _scaledWidth = (int)(_scaledHeight * _logoAspect);

        }

        _logoDestRect = new Rectangle((graphicsDevice.Viewport.Width - _scaledWidth) / 2, (graphicsDevice.Viewport.Height - _scaledHeight) / 2, _scaledWidth, _scaledHeight);
        _fullScreenRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_logoTimeElapsed > _logoDuration)
        {
            return;
        }

        spriteBatch.Draw(_logo, _logoDestRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);


        spriteBatch.Draw(_fadeToBlack, _fullScreenRect, null, new Color(0f, 0f, 0f, _overlayAlpha), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

    }
}