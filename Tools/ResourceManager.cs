using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TargetPractice.Tools;

public class ResourceManager
{
    private readonly ContentManager _content;
    private readonly GraphicsDevice _graphicsDevice;

    public ResourceManager(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _content = content;
        _graphicsDevice = graphicsDevice;
    }

    public Texture2D LoadTexture(string path)
    {
        return _content.Load<Texture2D>(path);
    }

    public Texture2D CreateOverlayTexture()
    {
        return new Texture2D(_graphicsDevice, 1, 1);
    }
}