
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TargetPractice.Scenes;

namespace TargetPractice.Tools;
public class SceneManager
{
    private IScene _currentScene;
    private Vector2 _currentScreenSize;
    private ResourceManager _resourceManager;

    public void ChangeScene(IScene scene)
    {
        if (_currentScene != null)
        {
            _currentScene.RequestSceneChange -= HandleSceneChange;
        }
        _currentScene = scene;
        _currentScene.Initialize();
        _currentScene?.OnResize(_currentScreenSize);
        _currentScene.RequestSceneChange += HandleSceneChange;


    }

    private void HandleSceneChange(SceneTypes sceneType)
    {
        switch (sceneType)
        {
            case SceneTypes.Logo:
                ChangeScene(new LogoScene(_resourceManager));
                break;
            case SceneTypes.MainMenu:
                //ChangeScene(new MainMenuScene(_resourceManager));
                break;
            case SceneTypes.Game:
                //ChangeScene(new GameScene(_resourceManager));
                break;
            case SceneTypes.GameOver:
                //ChangeScene(new GameOverScene(_resourceManager));
                break;
            case SceneTypes.Credits:
                //ChangeScene(new CreditsScene(_resourceManager));
                break;
        }
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