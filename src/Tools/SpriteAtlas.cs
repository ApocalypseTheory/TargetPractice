using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TargetPractice.Tools;

public class SpriteAtlas
{
    private Dictionary<string, Texture2D> _sheets = new Dictionary<string, Texture2D>();
    private Dictionary<string, Dictionary<string, Rectangle>> _sprites = new Dictionary<string, Dictionary<string, Rectangle>>();
    private ContentManager _content;

    public SpriteAtlas(ContentManager content)
    {
        _content = content;
    }

    public void LoadSheet(string spriteSheet)
    {
        Texture2D sheet = _content.Load<Texture2D>($"spritesheets/{spriteSheet}");
        _sheets[spriteSheet] = sheet;
        LoadSprites(spriteSheet, $"Content/xml/{spriteSheet}.xml");
    }

    private void LoadSprites(string spriteSheet, string xmlPath)
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
        _sprites[spriteSheet] = spriteMap;
    }

    public Texture2D GetSpriteSheet(string spriteSheet)
    {
        if (_sheets.ContainsKey(spriteSheet))
        {
            return _sheets[spriteSheet];
        }
        return null;
    }

    public Rectangle? GetSpriteRectangle(string spriteSheet, string spriteName)
    {
        if (_sprites.ContainsKey(spriteSheet) && _sprites[spriteSheet].ContainsKey(spriteName))
        {
            return _sprites[spriteSheet][spriteName];
        }
        return null;
    }
}
