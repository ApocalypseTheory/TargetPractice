using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml.Linq;

public class SpriteAtlas
{
    private Dictionary<string, Texture2D> _sheets = new Dictionary<string, Texture2D>();
    private Dictionary<string, Dictionary<string, Rectangle>> _sprites = new Dictionary<string, Dictionary<string, Rectangle>>();
    private ContentManager _content;

    public SpriteAtlas(ContentManager content)
    {
        _content = content;
    }

    public void LoadSheet(string assetName)
    {
        Texture2D sheet = _content.Load<Texture2D>($"spritesheets/{assetName}");
        _sheets[assetName] = sheet;
        LoadSprites(assetName, $"Content/xml/{assetName}.xml");
    }

    private void LoadSprites(string assetName, string xmlPath)
    {
        XDocument doc = XDocument.Load(xmlPath);
        Dictionary<string, Rectangle> spriteMap = new Dictionary<string, Rectangle>();
        foreach (var spriteElement in doc.Descendants("SubTexture"))
        {
            string name = spriteElement.Attribute("name").Value;
            int x = int.Parse(spriteElement.Attribute("x").Value);
            int y = int.Parse(spriteElement.Attribute("y").Value);
            int width = int.Parse(spriteElement.Attribute("width").Value);
            int height = int.Parse(spriteElement.Attribute("height").Value);

            spriteMap[name] = new Rectangle(x, y, width, height);
        }
        _sprites[assetName] = spriteMap;
    }

    public void Draw(SpriteBatch spriteBatch, string assetName, string spriteName, Vector2 position, Color color, float rotation = 0f, Vector2 origin = default, float scale = 1f, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0f)
    {
        if (_sprites.ContainsKey(assetName) && _sprites[assetName].ContainsKey(spriteName))
        {
            Texture2D sheet = _sheets[assetName];
            Rectangle sourceRectangle = _sprites[assetName][spriteName];
            spriteBatch.Draw(sheet, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }
    }
}
