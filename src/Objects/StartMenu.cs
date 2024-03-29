using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetPractice.Tools;

namespace TargetPractice.Objects;

public class StartMenu : DrawableGameComponent
{
    private SpriteBatch _spriteBatch;
    List<string> _assets = new List<string>();

    public StartMenu(Game game) : base(game)
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
        LoadAssets("blueSheet");
        LoadAssets("greySheet");
        LoadAssets("yellowSheet");
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
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
        var blueButton = SpriteAtlas.Instance.GetSpriteSheet("blueSheet");
        var blueButtonRect = SpriteAtlas.Instance.GetSpriteRectangle("blueSheet", "blue_button00");
        var screenWidth = GraphicsDevice.Viewport.Width;
        var screenHeight = GraphicsDevice.Viewport.Height;

        _spriteBatch.Draw(blueButton, new Vector2((screenWidth - blueButtonRect.Value.Width) / 2, screenHeight / 3), blueButtonRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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