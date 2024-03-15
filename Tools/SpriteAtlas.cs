using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TargetPractice.Tools;

public class SpriteAtlas
{
    private Texture2D _spriteSheet;
    private Dictionary<string, Rectangle> _spriteCoordinates = new Dictionary<string, Rectangle>();

    public SpriteAtlas(Texture2D spriteSheet, XDocument doc)
    {
        _spriteSheet = spriteSheet;
        ParseXML(doc);
    }

    private void ParseXML(XDocument doc)
    {
        foreach (var spriteElement in doc.Descendants("SubTexture"))
        {
            string name = spriteElement.Attribute("name").Value;
            int x = int.Parse(spriteElement.Attribute("x").Value);
            int y = int.Parse(spriteElement.Attribute("y").Value);
            int width = int.Parse(spriteElement.Attribute("width").Value);
            int height = int.Parse(spriteElement.Attribute("height").Value);

            _spriteCoordinates[name] = new Rectangle(x, y, width, height);
        }
    }

    public void Draw(SpriteBatch spriteBatch, string spriteName, Vector2 position, Color color)
    {
        if (_spriteCoordinates.ContainsKey(spriteName))
        {
            Rectangle sourceRectangle = _spriteCoordinates[spriteName];
            spriteBatch.Draw(_spriteSheet, position, sourceRectangle, color);
        }
    }
}