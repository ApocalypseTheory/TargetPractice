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
        _fadeToBlack = new Texture2D(GraphicsDevice, 1, 1);
        _fadeToBlack.SetData(new Color[] { Color.Black });
        _spriteBatch = new SpriteBatch(GraphicsDevice);
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
        GraphicsDevice.Clear(Color.Black);

        float scale = ScaleManager.GlobalScale;
        int scaledLogoWidth = (int)(_logo.Width * scale);
        int scaledLogoHeight = (int)(_logo.Height * scale);
        var screenCenter = new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f);
        var logoPosition = new Vector2(screenCenter.X - scaledLogoWidth / 2, screenCenter.Y - scaledLogoHeight / 2);
        var _fullScreenRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        _spriteBatch.Draw(_fadeToBlack, _fullScreenRect, null, new Color(0f, 0f, 0f, _overlayAlpha), 0f, Vector2.Zero, SpriteEffects.None, 1.0f);
        _spriteBatch.Draw(_logo, logoPosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }



    protected override void UnloadContent()
    {
        _sceneContent.Unload();
        base.UnloadContent();
    }
}
