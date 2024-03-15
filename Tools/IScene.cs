using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetPractice.Tools;

public enum SceneTypes
{
    Logo,
    MainMenu,
    Game,
    GameOver,
    Credits
}

public interface IScene
{
    event Action<SceneTypes> RequestSceneChange;
    void Initialize();
    void LoadContent();
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void OnResize(Vector2 newScreenSize);
}

