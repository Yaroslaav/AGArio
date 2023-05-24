using SFML.Graphics;
using SFML.System;

public static class MainCamera
{
    public static View camera;
    public static float ZoomIncrement = .2f;
    public static float maxZoom = 1.03f;


    public static void SetupCamera()
    {
        camera = new (new FloatRect(Window.GetWindowCenter().X, Window.GetWindowCenter().Y, GameSettings.WINDOW_WIDTH, GameSettings.WINDOW_HEIGHT));
        camera.Zoom(0.5f);
    }

    public static void Zoom(float zoom)
    {
        if(zoom<maxZoom)
            camera.Zoom(zoom);
    }

    public static void MoveCamera(Vector2f newPosition)
    {
        camera.Center = newPosition;
    }

}