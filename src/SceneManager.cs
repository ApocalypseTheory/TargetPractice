
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TargetPractice.Scenes;
public class SceneManager
{
    private IScene _currentScene;
    Vector2 _currentScreenSize;

    public void ChangeScene(IScene scene)
    {
        _currentScene = scene;
        _currentScene.Initialize();
        _currentScene?.OnResize(_currentScreenSize);


    }

    public void LoadContent()
    {
        _currentScene?.LoadContent();
    }

    public void Update(GameTime gameTime)
    {
        _currentScene?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _currentScene?.Draw(spriteBatch);
    }

    public void OnResize(Vector2 newScreenSize)
    {
        _currentScreenSize = newScreenSize;
        _currentScene?.OnResize(newScreenSize);
    }
}