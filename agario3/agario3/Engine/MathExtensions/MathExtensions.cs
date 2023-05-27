using SFML.System;

public static class MathExtensions
{
    public static Vector2f ClampByWindowSize(this Vector2f position, Vector2f size)
    {
        size /= 2;
        if (position.X < size.X)
            position.X = size.X;
        else if (position.X > GameSettings.FIELD_WIDTH - size.X)
            position.X = GameSettings.FIELD_WIDTH - size.X;

        if (position.Y < size.Y)
            position.Y = size.Y;
        else if (position.Y > GameSettings.FIELD_HEIGHT - size.Y)
            position.Y = GameSettings.FIELD_HEIGHT - size.Y;
        return position;
    }

    public static Vector2f Normalize(this Vector2f direction)
    {
        return direction / MathF.Sqrt((direction.X * direction.X) + (direction.Y * direction.Y));
    }
    
}