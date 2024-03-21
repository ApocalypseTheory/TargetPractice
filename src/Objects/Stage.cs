using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetPractice.Tools;

public class Stage : DrawableGameComponent
{
    private SpriteBatch _spriteBatch;
    private List<string> _assets = new List<string>();
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
        LoadAssets("spritesheet_stall");
        base.LoadContent();
    }

    private void LoadAssets(string asset)
    {
        _assets.Add(asset);
        SpriteAtlas.Instance.RegisterSpriteSheet(asset);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        var stall_sheet = SpriteAtlas.Instance.GetSpriteSheet("spritesheet_stall");

        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        var topRect = SpriteAtlas.Instance.GetSpriteRectangle("spritesheet_stall", "curtain_straight");
        var topBackRect = SpriteAtlas.Instance.GetSpriteRectangle("spritesheet_stall", "curtain_top");
        var curtainSSPos = SpriteAtlas.Instance.GetSpriteRectangle("spritesheet_stall", "curtain");
        var rope = SpriteAtlas.Instance.GetSpriteRectangle("spritesheet_stall", "curtain_rope");
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

        // var lRope = new Vector2(-10 * scale, 260 * scale);
        // var rRope = new Vector2(-GraphicsDevice.Viewport.Width - rope.Value.Width * scale, 260 * scale);
        // _spriteBatch.Draw(stall_sheet, lRope, rope, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.95f);
        // _spriteBatch.Draw(stall_sheet, rRope, rope, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0.95f);


        _spriteBatch.End();
        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {
        foreach (var asset in _assets)
        {
            SpriteAtlas.Instance.DeregisterSpriteSheet(asset);
        }

        base.UnloadContent();
        Game.Components.Remove(this);
    }
}
