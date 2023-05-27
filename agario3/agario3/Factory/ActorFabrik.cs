using SFML.System;
using SFML.Graphics;
public static class GameObjectFabrik
{
    public static T CreateActor<T>(this GameLoop game, Shape shape, IntRect rect, Texture texture, Vector2f position, Color fillColor, Color borderColor) where T:GameObject,new()
    {
        T t = CreateActor_Internal<T>(shape, rect, texture, position, fillColor, borderColor);
        if (t == null)
            return null;

        game.RegisterGameObject(t);
        return t;
    }
    private static T CreateActor_Internal<T>(Shape shape, IntRect rect, Texture texture, Vector2f position, Color fillColor, Color borderColor) where T : GameObject, new()
    {
        GameObjArgs args;
        args.Position = position;
        args.Rect = rect;
        args.Shape = shape;
        args.texture = texture;
        args.borderColor = borderColor;
        args.fillColor = fillColor;
        T t = new T();
        t.PostCreate(args);
        return t;
    }
}