using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Player : GameObject
{
    public bool isBot;

    private float maxMass = 70;
    public CircleShape shape = new();

    public bool canBeEaten = true;

    private Shield _shield;
    private int shieldCooldown = 10;


    public float diameter
    {
        get => shape.Radius * 2;
    }

    public override Vector2f size
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
        
    }

    private void AddComponents()
    {
        AnimationArgs animArgs = new AnimationArgs();
        animArgs.spriteSize = new Vector2i(31, 31);
        animArgs.milliSecondsBetweenAnimation = 100;
        AddComponent<AnimationComponent>().Setup(this, animArgs);

        AddComponent<MovementComponent>().Setup(this,
            isBot ? MovementType.RandomDirection : MovementType.MousePosition);

        AddComponent<AudioSource>();
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
        AddComponents();
    }

    protected override Shape GetOriginalShape()
    {
        return shape;
    }
    public void OnEat(float mass)
    {
        if (this.mass < maxMass)
        {
            shape.Radius += mass;
            shape.Origin = new Vector2f(shape.Radius, shape.Radius);
            this.mass = (int)shape.Radius / 10;
            
            if (!isBot)
            {
                AudioSource source = GetComponent<AudioSource>();
                source.loop = false;
                source.SetClip("Eat.ogg");
                source.PlayClip();
            }
        }
    }


    public void OnSwitchSoul()
    {
        isBot = !isBot;
        GetComponent<MovementComponent>()
            .ChangeMovementType(isBot ? MovementType.RandomDirection : MovementType.MousePosition);
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