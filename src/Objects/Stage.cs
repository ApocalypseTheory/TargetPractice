using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetPractice.Tools;

public class Stage : DrawableGameComponent
{
    private SpriteBatch _spriteBatch;
    private Texture2D stall_sheet;
    public Stage(Game game) : base(game)
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
        var topRect = SpriteAtlas.Instance.GetSpriteRectangle("spritesheet_stall", "curtain_straight");
        var topBackRect = SpriteAtlas.Instance.GetSpriteRectangle("spritesheet_stall", "curtain_top");
        var curtainSSPos = SpriteAtlas.Instance.GetSpriteRectangle("spritesheet_stall", "curtain");
        var alternate = 0;
        for (float x = 0; x < GraphicsDevice.Viewport.Width; x += (float)topRect.Value.Width / 1.5f)
        {
            float layer = (alternate % 2 == 0) ? 0.5f : 0.6f;
            alternate++;
            _spriteBatch.Draw(stall_sheet, new Vector2(x, (topRect.Value.Height / 1.5f)), topBackRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);
        }
        for (var x = 0; x < GraphicsDevice.Viewport.Width; x += topRect.Value.Width)
        {
            _spriteBatch.Draw(stall_sheet, new Vector2(x, 0), topRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }




        float scale = ScaleManager.GlobalScale;


        var lPos = new Vector2(0, topRect.Value.Height - 25);
        var rPos = new Vector2(GraphicsDevice.Viewport.Width - curtainSSPos.Value.Width * scale, topRect.Value.Height - 25);

        _spriteBatch.Draw(stall_sheet, lPos, curtainSSPos, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.9f);
        _spriteBatch.Draw(stall_sheet, rPos, curtainSSPos, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0.9f);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
