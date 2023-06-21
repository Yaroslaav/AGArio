public abstract class Component
{
    protected GameObject gameObject;
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