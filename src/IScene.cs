using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetPractice.Scenes;

public interface IScene
{
    void Initialize();
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}

