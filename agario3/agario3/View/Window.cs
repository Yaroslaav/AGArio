using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public static class Window
{
    public static RenderWindow renderWindow;
    
    public static void Draw(IDrawable drawableObject)
    {
        renderWindow.Draw(drawableObject.GetShape());
    }
    public static void Draw(List<IDrawable> drawableObjects)
    {
        DispatchEvents();
        Clear();
        renderWindow.SetView(MainCamera.camera);
        
        for (int i = 0; i < drawableObjects.Count; i++)
        {
            renderWindow.Draw(drawableObjects[i].GetShape());
        }
        renderWindow.Display();
    }
    public static void SetWindow()
    {
        renderWindow = new RenderWindow(new VideoMode(GameSettings.WINDOW_WIDTH, GameSettings.WINDOW_HEIGHT), GameSettings.WINDOW_TITLE);
        renderWindow.SetFramerateLimit(600);
        Clear();
    }

    public static void DispatchEvents() => renderWindow.DispatchEvents();
    public static void Clear() => renderWindow.Clear(Color.White);
    public static void Close()
    {
        Clear();
        renderWindow.Close();
    }
    public static Vector2u GetWindowCenter() => new (GameSettings.WINDOW_WIDTH / 2, GameSettings.WINDOW_HEIGHT / 2);
    public static Vector2f MapPixelToCoords(Vector2i position) => renderWindow.MapPixelToCoords(position);
}