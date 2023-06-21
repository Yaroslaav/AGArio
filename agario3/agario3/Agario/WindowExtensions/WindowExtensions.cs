
using SFML.System;

public static class WindowExtensions
{
    public static Vector2u GetWindowCenter(this Window window) => new (GameSettings.windowWidth / 2, GameSettings.windowHeight / 2);

    public static Vector2f GetRandomPosition(this Window window) => new Vector2f(Rand.Next(GameSettings.fieldWidth),
        Rand.Next(GameSettings.fieldHeight));
    
}