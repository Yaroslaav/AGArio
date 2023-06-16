public class GameLoop
{
    public static GameLoop Instance { get; private set; }

    private Game _game;
    
    private bool isPlaying;
    
    private List<IDrawable> drawableObjects = new();
    private List<IUpdatable> updatableObjects = new();
    
    private Camera _camera
    {
        get => _game.mainCamera;
    }

    public void Start()
    {
        if (Instance == null)
            Instance = this;
        new GameSettings();
        _game = new ();
        _game.Start();
        
        _game.window.renderWindow.Closed += (_, _) => isPlaying = false;

        isPlaying = true;
        Loop();
    }
    private void Loop()
    {
        while (isPlaying)
        {
            Time.UpdateTimer();
            _game.window.DispatchEvents();
            
            Input.CheckInput();
            
            _game.ownPlayer.Update();
            Update();
            _camera.MoveCamera(_game.ownPlayerPosition);
            _game.Update();
               
            _game.window.Draw(drawableObjects);
        }
    }

    private void Update()
    {
        for (int i = 0; i < updatableObjects.Count; i++)
        {
            updatableObjects[i].Update();
        }
    }
    
    public void RegisterGameObject(GameObject gameObject)
    {
        if (gameObject is IDrawable)
        {
            if (!drawableObjects.Contains(gameObject as IDrawable))
            {
                drawableObjects.Add(gameObject as IDrawable);
            }
        }
        if (gameObject is IUpdatable)
        {
            if (!updatableObjects.Contains(gameObject as IUpdatable))
            {
                updatableObjects.Add(gameObject as IUpdatable);
            }
        }
    }

    public void UnRegisterGameObject(GameObject gameObject)
    {
        if (updatableObjects.Contains(gameObject as IUpdatable))
        {
            updatableObjects.Remove(gameObject as IUpdatable);
        }
        if (drawableObjects.Contains(gameObject as IDrawable))
        {
            drawableObjects.Remove(gameObject as IDrawable);
        }
    }
}