using SFML.Graphics;
using SFML.System;
using SFML.Window;

public static class Input
{
    private static Random rand = new ();
    
    public static Vector2f lastPlayerDirection = new (0,0);
    
    private static Dictionary<string, BindKey> keys = new(0);

    private static Window window
    {
        get => Game.instance.window;
    }

    public static void CheckInput()
    {
        CheckMouseInput();
        CheckKeys();
    }
    private static void CheckMouseInput()
    {
        Vector2i mousePosition = Mouse.GetPosition(window.renderWindow);
        Vector2f targetPosition = window.MapPixelToCoords(mousePosition);
        Vector2f direction = targetPosition;

        lastPlayerDirection = direction;
    }
    
    public static Vector2f GetRandomBotDirection()
    {
        Vector2f direction = new (0,0);
        
        direction.X = rand.Next(-GameSettings.fieldWidth, GameSettings.fieldWidth);
        direction.Y = rand.Next(-GameSettings.fieldHeiHeight, GameSettings.fieldHeiHeight);

        if (direction.X == 0 || direction.Y == 0)
        {
            direction.X += 1;
            direction.Y += 1;
        }

        return direction;
    }

    private static void CheckKeys()
    {
        foreach (BindKey key in keys.Values)
        {
            key.CheckInput();
        }
    }

    public static BindKey AddNewBind(Keyboard.Key key, string name)
    {
        BindKey bindKey = new BindKey(key);
        keys.Add(name, bindKey);
        return bindKey;
    }
}