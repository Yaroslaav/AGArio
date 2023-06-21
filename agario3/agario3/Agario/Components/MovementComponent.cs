using SFML.System;
using SFML.Window;

public enum MovementType
{
    RandomDirection,
    MousePosition,
}
public class MovementComponent : Component
{
    private int lastMoveTime = 0;
    private int timeBetweenMoves = 0;
    private Vector2f lastBotDirection = new Vector2f(0, 0);
    public Vector2f position;
    private float moveSpeed = .1f;


    private MovementType movementType;

    private Window window
    {
        get => Game.instance.window;
    }

    private Vector2f size
    {
        get => gameObject.size;
    }


    public void Setup(GameObject gameObject, MovementType movementType)
    {
        Awake();
        this.gameObject = gameObject;
        this.movementType = movementType;
        
        position = window.GetRandomPosition();
        position = position.ClampByWindowSize(size);
        this.gameObject.GetShape().Position = position;
        //SetPosition(window.GetRandomPosition());
        Start();
    }
    private void SetPosition(Vector2f position)
    {
        position = position.ClampByWindowSize(size);
        gameObject.GetShape().Position = position;
    }

    public override void Update()
    {
        base.Update();
        TryMove();
    }

    private void TryMove()
    {
        if (Time.totalSeconds >= lastMoveTime + timeBetweenMoves)
        {
            lastBotDirection = Input.GetRandomBotDirection();
            timeBetweenMoves = Rand.Next(10);
            lastMoveTime = Time.totalSeconds;
        }

        switch (movementType)
        {
            case MovementType.MousePosition:
                UpdateMovement(Input.lastPlayerDirection);
                break;
            case MovementType.RandomDirection:
                UpdateMovement(lastBotDirection);
                break;
        }
    }
    private void UpdateMovement(Vector2f newDirection)
    {
        Vector2f direction = newDirection - position;

        if (direction.X != 0 || direction.Y != 0)
        {
            direction = direction.Normalize();

            position += direction * moveSpeed * Time.deltaTime;
        }
        
        SetPosition();
    }

    private void SetPosition()
    {
        position = position.ClampByWindowSize(size);
        gameObject.GetShape().Position = position;
    }

    public void ChangeMovementType(MovementType _type)
    {
        movementType = _type;
    }

}