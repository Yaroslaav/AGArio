using SFML.Graphics;
using SFML.System;

public struct GameObjArgs
{
    public Shape Shape;
    public IntRect Rect;
    public Vector2f Position;
    public Texture texture;
}
public class GameObject : Transformable, IDrawable, IUpdatable
{
    public int ZPosition { get; set; } = 2;
    public Shape baseShape;
    public Vector2f Position;


    private Texture texture;


    public Shape GetShape() => baseShape;
    public void PostCreate(GameObjArgs args)
    {
        texture = args.texture;
        baseShape = args.Shape;
        baseShape.Position = args.Position;
        Position = args.Position;
        baseShape.Texture = texture;
        baseShape.TextureRect = args.Rect;
        baseShape.FillColor = Color.Black;
        Start();
    }

    protected virtual void Start()
    {
        
    }
    public virtual void Update()
    {
        
    }

    protected void SetNewShape(Shape newShape)
    {
        GameObjArgs lastArgs = new();
        lastArgs.Position = baseShape.Position;
        lastArgs.texture = baseShape.Texture;
        lastArgs.Shape = null;
        lastArgs.Rect = baseShape.TextureRect;

        baseShape = newShape;
        baseShape.Scale = new (10,10);
        baseShape.Position = lastArgs.Position;
        baseShape.Texture = lastArgs.texture;
        baseShape.TextureRect = lastArgs.Rect;
        baseShape.FillColor = Color.Black;
    }

    
}