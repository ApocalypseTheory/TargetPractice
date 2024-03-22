using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TargetPractice.Objects;
using System.Collections.Generic;

namespace TargetPractice.Scenes;

public class MainMenuScene : DrawableGameComponent
{
    private SpriteBatch _spriteBatch;
    private Dictionary<string, DrawableGameComponent> _components = new Dictionary<string, DrawableGameComponent>();

    public MainMenuScene(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _components.Add("Background", new Background(Game));
        _components.Add("Stage", new Stage(Game));
        _components.Add("StartMenu", new StartMenu(Game));
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    protected override void UnloadContent()
    {
        foreach (var component in _components)
        {
            component.Value.Dispose();
        }
        base.UnloadContent();
    }
}
