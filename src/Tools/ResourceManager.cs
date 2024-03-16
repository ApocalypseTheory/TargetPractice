using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml.Linq;


namespace TargetPractice.Tools;

public class ResourceManager
{
    private readonly ContentManager _content;
    private readonly GraphicsDevice _graphicsDevice;
    private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
    private Dictionary<string, SpriteAtlas> _spriteAtlases = new Dictionary<string, SpriteAtlas>();
    private Dictionary<string, HashSet<string>> _sceneResources = new Dictionary<string, HashSet<string>>();


    public ResourceManager(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _content = content;
        _graphicsDevice = graphicsDevice;
    }

    public Texture2D LoadTexture(string path)
    {
        if (!_textures.ContainsKey(path))
        {
            var texture = _content.Load<Texture2D>(path);
            _textures[path] = texture;
        }
        return _textures[path];
    }

    public Texture2D CreateOverlayTexture()
    {
        return new Texture2D(_graphicsDevice, 1, 1);
    }

    public void LoadSpriteAtlas(string assetName, string sceneName = null)
    {
        if (!_spriteAtlases.ContainsKey(assetName))
        {
            Texture2D spriteSheet = LoadTexture(assetName);
            XDocument doc = XDocument.Load($"Content/{assetName}.xml");
            SpriteAtlas atlas = new SpriteAtlas(spriteSheet, doc);
            _spriteAtlases[assetName] = atlas;

            if (sceneName != null)
            {
                RegisterResourceForScene(sceneName, assetName, isAtlas: true);
            }
        }
    }

    public void DrawSprite(SpriteBatch spriteBatch, string atlasName, string spriteName, Vector2 position, Color color, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0f)
    {
        if (_spriteAtlases.ContainsKey(atlasName))
        {
            SpriteAtlas atlas = _spriteAtlases[atlasName];
            atlas.Draw(spriteBatch, spriteName, position, color, rotation, origin, scale, effects, layerDepth);
        }
    }

    public void RegisterResourceForScene(string sceneName, string resourceName, bool isAtlas = false)
    {
        if (!_sceneResources.ContainsKey(sceneName))
        {
            _sceneResources[sceneName] = new HashSet<string>();
        }
        _sceneResources[sceneName].Add(resourceName + (isAtlas ? "|atlas" : "|texture"));
    }

    public void UnloadSceneResources(string sceneName)
    {
        if (_sceneResources.ContainsKey(sceneName))
        {
            HashSet<string> resources = _sceneResources[sceneName];
            foreach (string resource in resources)
            {
                var parts = resource.Split('|');
                var name = parts[0];
                var type = parts[1];

                if (type == "texture")
                {
                    if (_textures.ContainsKey(name))
                    {
                        _textures[name]?.Dispose();
                        _textures.Remove(name);
                    }
                }
                else if (type == "atlas")
                {
                    _spriteAtlases.Remove(name);
                }
            }
            _sceneResources.Remove(sceneName);
        }
    }

}