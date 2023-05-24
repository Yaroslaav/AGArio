using SFML.Graphics;
using SFML.System;

public struct GameObjArgs
{
    public Shape Shape;
    public IntRect Rect;
    public Vector2f Position;
    public Texture texture;
    public Color fillColor;
    public Color borderColor;
}
public class GameObject : Transformable, IDrawable, IUpdatable
{
    public int mass = 0;
    public int ZPosition { get; set; } = 0;

    protected Texture texture;


    public Shape GetShape() => GetOriginalShape();

    protected virtual Shape GetOriginalShape()
    {
        return null;
    }
    public virtual void PostCreate(GameObjArgs args)
    {
        
    }

    public virtual void Update()
    {
        
    }

    
}