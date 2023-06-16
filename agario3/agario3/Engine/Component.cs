using SFML.System;

public abstract class Component
{
    protected GameObject gameObject;
    public string Name { get; protected set; }
    public Component(string name, GameObject gameObject)
    {
        this.gameObject = gameObject;
        Name = name;
        Awake();
        Start();
    }
    public virtual void Awake()
    {
        
    }

    public virtual void Start()
    {
        
    }
    public virtual void Update()
    {
        
    }
    public GameObject GetGameObject() => gameObject;
}   