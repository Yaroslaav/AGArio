using SFML.Graphics;
using SFML.System;
using SFML.Window;

public static class Window
{
    public const int WindowWidth = 800;
    public const int WindowHeight = 600;

    public static RenderWindow renderWindow;
    
    public static void DrawScene(GameObject[] items)
    {
        Clear();

        foreach (GameObject item in items)
        {
            if (item is Drawable)
            {
                
            }
                //renderWindow.Draw((Drawable)item.transformable);
        }
        renderWindow.Display();
    }

    public static void Draw(IDrawable drawableObject)
    {
        renderWindow.Draw(drawableObject.GetShape());
    }
    public static void Draw(List<IDrawable> drawableObjects, View currentCamera)
    {
        DispatchEvents();
        Clear();
        renderWindow.SetView(currentCamera);
        RectangleShape bak = new RectangleShape();
        bak.Size = new Vector2f(GameSettings.FIELD_WIDTH, GameSettings.FIELD_HEIGHT);
        bak.Texture = new Texture("D:/AGArio/AGArio/AGA_Rio/Agario/texture.png");
        renderWindow.Draw(bak);
        
        for (int i = 0; i < drawableObjects.Count; i++)
        {
            renderWindow.Draw(drawableObjects[i].GetShape());
        }
        renderWindow.Display();
    }
    public static void SetWindow()
    {
        renderWindow = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), "Aero Hockey");
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
    public static Vector2u GetWindowCenter() => new (WindowWidth / 2, WindowHeight / 2);
}