using SFML.Graphics;
using SFML.System;
using SFML.Window;

public static class Input
{
    private static Random rand = new ();
    public static Vector2f lastPlayerDirection;
    public static Action swapMainPlayer;
    private static bool soulSwapKeyActive;
    private static Window window
    {
        get => Game.instance.window;
    }

    public static void CheckInput()
    {
        CheckMouseInput();
        CheckPlayerSwapInput();
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
        
        direction.X = rand.Next(-GameSettings.FIELD_WIDTH, GameSettings.FIELD_WIDTH);
        direction.Y = rand.Next(-GameSettings.FIELD_HEIGHT, GameSettings.FIELD_HEIGHT);

        if (direction.X == 0 || direction.Y == 0)
        {
            direction.X += 1;
            direction.Y += 1;
        }

        return direction;
    }
    private static void CheckPlayerSwapInput()
    {
        if(GetKeyboardPlayerSwap())
            swapMainPlayer?.Invoke();
    }

    private static bool GetKeyboardPlayerSwap()
    {
        if (!soulSwapKeyActive)
        {
            soulSwapKeyActive = Keyboard.IsKeyPressed(Keyboard.Key.F);
            return Keyboard.IsKeyPressed(Keyboard.Key.F);
        }
        soulSwapKeyActive = Keyboard.IsKeyPressed(Keyboard.Key.F);

        return false;
    }
}