using SFML.Graphics;
using SFML.System;

public class Shield : GameObject
{
    public CircleShape shape = new ();
    private int lifeTimeInSeconds;
    private int spawnTime;

    public Action onDestroy;

   public void SetPosition(Vector2f position)
    {   
        shape.Position = position;
    }

    public override void Update()
    {
        base.Update();
        if (spawnTime + lifeTimeInSeconds <= Time.totalSeconds)
        {
            
        }
    }

    public override void Awake(GameObjArgs args)
    {
        spawnTime = Time.totalSeconds;
        shape.Radius = args.size.X/2;
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);
        shape.Position = args.Position;
        Position = args.Position;
        shape.Texture = texture;
        shape.FillColor = args.fillColor;
    }

    public void Start(int lifeTimeInSeconds)
    {
        this.lifeTimeInSeconds = lifeTimeInSeconds;
    }

    private void Destroy()
    { 
        onDestroy?.Invoke();
        Game.instance.DestroyGameObject(this);
    }

}