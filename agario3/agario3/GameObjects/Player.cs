using SFML.Graphics;
using SFML.System;

public class Player : GameObject
{
    
    
    private float moveSpeed = .5f;
    private float maxMass = 70;
    public Vector2f Position;
    public CircleShape shape = new ();

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
        UpdateMovement();
    }
    private void UpdateMovement()
    {
        Vector2f targetPosition = Input.GetMouseInput();
        Vector2f direction = targetPosition - Position;

        if (direction != new Vector2f(0, 0))
        {
            float magnitude = MathF.Sqrt((direction.X * direction.X) + (direction.Y * direction.Y));
            direction /= magnitude;

            Position += direction * moveSpeed * Time.deltaTime;
        }
		
        CheckMovement();

        shape.Position = Position;
    }
    private void CheckMovement()
    {
        if (Position.X < shape.Radius)
            Position.X = shape.Radius;
        else if (Position.X > GameSettings.FIELD_WIDTH - shape.Radius)
            Position.X = GameSettings.FIELD_WIDTH - shape.Radius;

        if (Position.Y < shape.Radius)
            Position.Y = shape.Radius;
        else if (Position.Y > GameSettings.FIELD_HEIGHT - shape.Radius)
            Position.Y = GameSettings.FIELD_HEIGHT - shape.Radius;
    }

    public void OnEat(float mass)
    {
        if (this.mass < maxMass)
        {
            Console.WriteLine("mass" + mass);
            Console.WriteLine("max mass" + maxMass);
            Console.WriteLine("radius" + shape.Radius);
            shape.Radius += mass;
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
            this.mass = (int)shape.Radius / 10;
        }
            
    }



    
}