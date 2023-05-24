using SFML.Graphics;
using SFML.System;

public class Player : GameObject
{
    private float moveSpeed = 2f;
    CircleShape shape = new CircleShape();
    protected override void Start()
    {
        base.Start();
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);
        SetNewShape(shape);
    }

    public override void Update()
    {
        base.Update();
        UpdateMovement();
    }
    private void UpdateMovement()
    {
        Vector2f targetPosition = Input.GetMouseInput();
        Vector2f direction = (targetPosition - shape.Position);

        if (direction != new Vector2f(0, 0))
        {
            float magnitude = MathF.Sqrt((direction.X * direction.X) + (direction.Y * direction.Y));
            direction /= magnitude;

            Position += direction * moveSpeed * Time.deltaTime;
        }
		
        ClampMovement();

        shape.Position = Position;
    }
    private void ClampMovement()
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



    
}