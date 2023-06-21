using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Input
{
    
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
        Vector2f targetPosition = window.MapPixelToCoords(window.GetMousePosition());
        Vector2f direction = targetPosition;

        lastPlayerDirection = direction;
    }
    
    public static Vector2f GetRandomBotDirection()
    {
        Vector2f direction = new (0,0);
        
        direction.X = Rand.Next(-GameSettings.fieldWidth, GameSettings.fieldWidth);
        direction.Y = Rand.Next(-GameSettings.fieldHeight, GameSettings.fieldHeight);

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