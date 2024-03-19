using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetPractice.Tools;

namespace TargetPractice.Objects;
public class Background : DrawableGameComponent
{
    private SpriteBatch _spriteBatch;
    private Texture2D stall_sheet;

    public Background(Game game) : base(game)
    {
        game.Components.Add(this);
    }

    public override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteAtlas.Instance.RegisterSpriteSheet("spritesheet_stall");
        stall_sheet = SpriteAtlas.Instance.GetSpriteSheet("spritesheet_stall");
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        var backgroundImage = Settings.Instance.GetSetting("Background");
        var background = SpriteAtlas.Instance.GetSpriteRectangle("spritesheet_stall", backgroundImage);
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
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}