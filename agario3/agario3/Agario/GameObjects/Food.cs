using SFML.Graphics;
using SFML.System;

public class Food : GameObject
{

    public CircleShape shape = new ();

    public override Vector2f size
    {
        get => new(shape.Radius * 2, shape.Radius * 2);  
    }

    public override void Awake(GameObjArgs args)
    {
        mass = 1;
        shape.Radius = args.size.X/2;
        shape.Position = args.Position;
        shape.Texture = texture;
        shape.FillColor = args.fillColor;

    }

    public void Start()
    {
        AddComponents();
    }

    private void AddComponents()
    {
        AnimationArgs animArgs = new AnimationArgs();
        animArgs.spriteSize = new Vector2i(31, 31);
        animArgs.milliSecondsBetweenAnimation = 100;
        AddComponent<AnimationComponent>().Setup(this, animArgs);
    }

    public void OnWasEaten()
    {
        shape.Position = new Vector2f(Rand.Next((int)GameSettings.windowWidth), Rand.Next((int)GameSettings.windowHeight));
    }
    protected override Shape GetOriginalShape()
    {
        return shape;
    }

    
}