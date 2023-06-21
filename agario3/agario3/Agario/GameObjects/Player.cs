using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Player : GameObject
{
    public bool isBot;
    private Vector2f lastBotDirection = new Vector2f(0, 0);

    private int lastMoveTime = 0;
    private int timeBetweenMoves = 0;

    private float moveSpeed = .1f;
    private float maxMass = 70;
    public Vector2f Position;
    public CircleShape shape = new();

    public bool canBeEaten = true;

    private Shield _shield;
    private int shieldCooldown = 10;


    public float diameter
    {
        get => shape.Radius * 2;
    }

    public Vector2f size
    {
        get => new(diameter, diameter);
    }

    public Action OnWasEaten;

    public override void Awake(GameObjArgs args)
    {
        shape.Radius = args.size.X / 2;
        mass = (int)shape.Radius / 10;
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);

        texture = args.texture;

        shape.Position = args.Position;
        Position = args.Position;

        OnWasEaten += () => Game.instance.DestroyGameObject(this);
        shape.Texture = args.texture;
        
        AnimationArgs animArgs = new AnimationArgs();
        animArgs.spriteSize = new Vector2i(60, 30);
        animArgs.milliSecondsBetweenAnimation = 100;
        AddComponent(new AnimationComponent("AnimationComponent", this, animArgs));
    }

    private void CreateBindings()
    {
        if (!isBot)
        {
            /*BindKey shieldActivationKey = Input.AddNewBind( Keyboard.Key.S, "ActivateShield");
            shieldActivationKey.OnKeyPress += ActivateShield;*/
        }
    }

    public void Start(bool isBot)
    {
        this.isBot = isBot;

        CreateBindings();
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

    private void TryMove()
    {
        if (Time.totalSeconds >= lastMoveTime + timeBetweenMoves)
        {
            lastBotDirection = Input.GetRandomBotDirection();
            timeBetweenMoves = Rand.Next(10);
            lastMoveTime = Time.totalSeconds;
        }

        if (!isBot)
        {
            UpdateMovement(Input.lastPlayerDirection);
        }
        else
        {
            UpdateMovement(lastBotDirection);
        }
    }

    public void OnSwitchSoul()
    {
        isBot = !isBot;
    }

    private void ActivateShield()
    {
        float shieldDiameter = diameter * 1.2f;
        _shield = Game.instance.CreateActor<Shield>(new Vector2f(shieldDiameter, shieldDiameter),
            null, shape.Position, new Color(23, 190, 187, 50), Color.Black);

        canBeEaten = false;
        _shield.onDestroy += () => canBeEaten = true;
    }

}