public class GameLoop : ISavable
{
    public static GameLoop Instance { get; private set; }
    public int test;

    private Game _game;
    
    private bool isPlaying;
    
    private List<IDrawable> drawableObjects = new();
    private List<IUpdatable> updatableObjects = new();

    public string pathToSavedFile { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents") + "/Agario/Saves/GameSettings.cfg";
    public string pathToDefaultFile { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents") + "/Agario/DefaultSaves/GameSettings.cfg";
    public List<(string, string, string)> savableItems { get; set; } = new ();

    
    private Camera _camera
    {
        get => _game.mainCamera;
    }

    public void Start()
    {
        if (Instance == null)
            Instance = this;
        
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
            if (!drawableObjects.Contains(gameObject))
            {
                drawableObjects.Add(gameObject);
            }
            
        }
        if (gameObject is IUpdatable)
        {
            if (!updatableObjects.Contains(gameObject))
            {
                updatableObjects.Add(gameObject);
            }
        }
    }

    public void UnRegisterGameObject(GameObject gameObject)
    {
        if (updatableObjects.Contains(gameObject))
        {
            updatableObjects.Remove(gameObject);
        }
        if (drawableObjects.Contains(gameObject))
        {
            drawableObjects.Remove(gameObject);
        }
    }

    public static GameLoop CreateNew()
    {
        GameLoop newGameLoop = new GameLoop();
        
        
        StreamReader sr = new StreamReader(pathToSavedFile);
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(' ');
            if(info.Length < 3)
                continue;
            
            string valueType = info[0];
            string variableName = info[1];
            string value = info[2];
            
            if (Type.GetType(valueType) == null)
            {
                continue;
            }

            object type = Activator.CreateInstance(Type.GetType(valueType));
            
            switch (type)
            {
                case int:
                    if (int.TryParse(value, out int _value))
                    {
                        newGameLoop.TrySetIntVariable(variableName, _value);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            
            
        }
        Console.WriteLine(newGameLoop.test + "    variable");
        return newGameLoop;
    }

    private void LoadInfo()
    {
        
    }

    public void AddSavableItem(string type, string name, string value)
    {
        savableItems.Add((type,name,value));
    }
    public void RemoveSavableItem(string name)
    {
        for (int i = 0; i < savableItems.Count; i++)
        {
            if (savableItems[i].Item2 == name)
            {
                savableItems.Remove(savableItems[i]);
            }
        }
    }
    public (string, string, string) GetSavableItem(string name)
    {
        foreach (var item in savableItems)
        {
            if (item.Item2 == name)
            {
                return item;
            }
        }
        return (null, null, null);
    }
}