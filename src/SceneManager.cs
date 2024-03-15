
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetPractice.Scenes;
public class SceneManager
{
    private IScene _currentScene;

    public void ChangeScene(IScene scene)
    {
        _currentScene = scene;
        _currentScene.Initialize();
    }

    public void Update(GameTime gameTime)
    {
        _currentScene?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _currentScene?.Draw(spriteBatch);
    }
}