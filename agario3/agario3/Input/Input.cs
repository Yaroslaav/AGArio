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
    public static Vector2f lastDirection = new ();
    
    public static void CheckMouseInput()
    {
        Vector2i mousePosition = Mouse.GetPosition(Window.renderWindow);
        Vector2f targetPosition = Window.MapPixelToCoords(mousePosition);
        Vector2f direction = targetPosition;

        lastDirection = direction;
    }
}