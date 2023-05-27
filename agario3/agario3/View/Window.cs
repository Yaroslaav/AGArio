using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Window
{
    private Random random = new();
    public RenderWindow renderWindow;
    private static View _camera
    {
        get => Game.instance.mainCamera.camera;
    }

    public Window()
    {
        SetWindow();
    }
    
    public void Draw(IDrawable drawableObject)
    {
        renderWindow.Draw(drawableObject.GetShape());
    }
    public void Draw(List<IDrawable> drawableObjects)
    {
        DispatchEvents();
        Clear();
        renderWindow.SetView(_camera);
        
        for (int i = 0; i < drawableObjects.Count; i++)
        {
            renderWindow.Draw(drawableObjects[i].GetShape());
        }
        renderWindow.Display();
    }
    public void SetWindow()
    {
        renderWindow = new RenderWindow(new VideoMode(GameSettings.WINDOW_WIDTH, GameSettings.WINDOW_HEIGHT), "Aero Hockey");
        renderWindow.SetFramerateLimit(600);
        Clear();
    }

    public void DispatchEvents() => renderWindow.DispatchEvents();
    public void Clear() => renderWindow.Clear(Color.White);
    public void Close()
    {
        Clear();
        renderWindow.Close();
    }
    public Vector2u GetWindowCenter() => new (GameSettings.WINDOW_WIDTH / 2, GameSettings.WINDOW_HEIGHT / 2);
    public Vector2f MapPixelToCoords(Vector2i position) => renderWindow.MapPixelToCoords(position);

    public Vector2f GetRandomPosition() => new Vector2f(random.Next(GameSettings.FIELD_WIDTH),
        random.Next(GameSettings.FIELD_HEIGHT));
}