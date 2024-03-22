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