using System;
using Microsoft.Xna.Framework;

public static class ScaleManager
{
    private static Vector2 _baseResolution = new Vector2(800, 480); // Adjust this to your base resolution
    private static float _globalScale;

    public static void UpdateResolution(int currentWidth, int currentHeight)
    {
        // Assuming uniform scaling for simplicity
        float scaleX = currentWidth / _baseResolution.X;
        float scaleY = currentHeight / _baseResolution.Y;

        // Use the smaller scale factor to ensure content fits on screen
        _globalScale = Math.Min(scaleX, scaleY);
    }

    public static float GlobalScale => _globalScale;
}
