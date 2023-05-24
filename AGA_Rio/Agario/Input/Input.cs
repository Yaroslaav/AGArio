using SFML.Graphics;
using SFML.System;
using SFML.Window;

public enum InputType
{
    Mouse,
    KeyBoard,
    Ai,
}
public static class Input
{
    private static View _currentCamera;

    public static void SetCurrentCamera(View currentCamera)
    {
        _currentCamera = currentCamera;
    }
    
    public static Vector2f GetMouseInput()
    {
        if (_currentCamera == null)
        {
            #if DEBUG
                throw new Exception("There is no current camera");
            #else
                return;
            #endif

        }
        Vector2u windowCenter = Window.GetWindowCenter();
        Vector2i mousePosition = Mouse.GetPosition(Window.renderWindow);
        Vector2f mouseDirection = new (mousePosition.X - windowCenter.X, mousePosition.Y - windowCenter.Y);

        return mouseDirection ;
    }
}