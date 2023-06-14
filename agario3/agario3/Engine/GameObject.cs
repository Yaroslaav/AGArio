using SFML.Graphics;
using SFML.System;

public struct GameObjArgs
{
    public Vector2f size;
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


    public (Shape, Sprite) GetShape() => (GetOriginalShape(), GetSprite());

    protected virtual Shape GetOriginalShape()
    {
        return null;
    }

    protected virtual Sprite GetSprite() => null;
    public virtual void Awake(GameObjArgs args)
    {
        
    }

    public virtual void Update()
    {
        
    }

    
}