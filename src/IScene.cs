using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetPractice.Scenes;

public interface IScene
{
    void Initialize();
    void LoadContent();
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void OnResize(Vector2 newScreenSize);
}

