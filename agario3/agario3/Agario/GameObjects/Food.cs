using SFML.Graphics;
using SFML.System;

public class Food : GameObject
{
    Random random = new ();

    public CircleShape shape = new ();

    
    public override void Awake(GameObjArgs args)
    {
        mass = 1;
        shape.Radius = args.size.X/2;
        texture = args.texture;
        shape.Position = args.Position;
        shape.Texture = texture;
        shape.TextureRect = args.Rect;
        shape.FillColor = args.fillColor;

    }

    public void OnWasEaten()
    {
        shape.Position = new Vector2f(random.Next(GameSettings.WINDOW_WIDTH), random.Next(GameSettings.WINDOW_HEIGHT));
    }
    protected override Shape GetOriginalShape()
    {
        return shape;
    }

    
}