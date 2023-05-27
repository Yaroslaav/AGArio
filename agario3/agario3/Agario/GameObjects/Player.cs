using SFML.Graphics;
using SFML.System;

public class Player : GameObject
{
    private Random rand = new ();
    
    public bool isBot;
    private Vector2f lastBotDirection = new Vector2f(0, 0);

    private int lastMoveTime = 0;
    private int timeBetweenMoves = 0;
    
    private float moveSpeed = .1f;
    private float maxMass = 70;
    public Vector2f Position;
    public CircleShape shape = new ();
    public float diameter
    {
        get => shape.Radius * 2;
    }

    public Vector2f size
    {
        get => new (diameter, diameter);
    }

    public override void PostCreate(GameObjArgs args)
    {
        shape.Radius = 30;
        mass = (int)shape.Radius / 10;
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);
        texture = args.texture;
        shape.Position = args.Position;
        Position = args.Position;
        shape.Texture = texture;
        shape.TextureRect = args.Rect;
        shape.FillColor = args.fillColor;
    }

    protected override Shape GetOriginalShape()
    {
        return shape;
    }


    public override void Update()
    {
        base.Update();
        TryMove();
    }
    private void UpdateMovement(Vector2f newDirection)
    {
        Vector2f direction = newDirection - Position;

        if (direction.X != 0 || direction.Y != 0)
        {
            direction = direction.Normalize();

            Position += direction * moveSpeed * Time.deltaTime;
        }
		
        Position = Position.ClampByWindowSize(size);
        shape.Position = Position;
    }
    public void OnEat(float mass)
    {
        if (this.mass < maxMass)
        {
            shape.Radius += mass;
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
            this.mass = (int)shape.Radius / 10;
        }
    }

    public void OnWasEaten()
    {
        
    }

    private void TryMove()
    {
        if (Time.totalSeconds >= lastMoveTime + timeBetweenMoves)
        {
            lastBotDirection = Input.GetRandomBotDirection();
            timeBetweenMoves = rand.Next(10);
            lastMoveTime = Time.totalSeconds;
            //Console.WriteLine($"bot Position {shape.Position} ");
        }
        if (!isBot)
        {
            UpdateMovement(Input.lastDirection);
            //Console.WriteLine($"player Position {shape.Position} ");
        }
        else
        {
            UpdateMovement(lastBotDirection);
        }

    }
}