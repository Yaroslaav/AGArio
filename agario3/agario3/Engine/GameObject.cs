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

    protected Texture texture { get; set; }

    private List<Component> _components = new ();


    public Shape GetShape() => (GetOriginalShape());

    protected virtual Shape GetOriginalShape()
    {
        return null;
    }

    public virtual void Awake(GameObjArgs args)
    {
        
    }

    public virtual void Update()
    {
        UpdateComponents();
    }

    public Component AddComponent(Component component)
    {
        foreach (Component _component in _components)
        {
            if(_component.Name == component.Name)
                return _component;
        }
        _components.Add(component);
        return component;
    }

    private void UpdateComponents()
    {
        foreach (var component in _components)
        {
            component.Update();
        }
    }
}