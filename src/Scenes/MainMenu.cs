using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TargetPractice.Tools;

namespace TargetPractice.Scenes;

public class MainMenuScene : DrawableGameComponent
{
    private SpriteBatch _spriteBatch;
    private ContentManager _sceneContent;
    private SpriteAtlas _spriteAtlas;
    private Texture2D stall_sheet;

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
        _spriteAtlas.LoadSheet("spritesheet_stall");
        stall_sheet = _spriteAtlas.GetSpriteSheet("spritesheet_stall");
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        base.LoadContent();


    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        DrawBackground();
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {
        _sceneContent.Unload();
        base.UnloadContent();
    }

    private void DrawBackground()
    {
        var background = _spriteAtlas.GetSpriteRectangle("spritesheet_stall", "bg_wood");
        var bgWidth = background.Value.Width;
        var bgHeight = background.Value.Height;
        var screenWidth = GraphicsDevice.Viewport.Width;
        var screenHeight = GraphicsDevice.Viewport.Height;
        for (var x = 0; x < screenWidth; x += bgWidth)
        {
            for (var y = 0; y < screenHeight; y += bgHeight)
            {
                _spriteBatch.Draw(stall_sheet, new Vector2(x, y), background, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}