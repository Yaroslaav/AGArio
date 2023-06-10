public class GameLoop : ISavable
{
    public static GameLoop Instance { get; private set; }
    public int test;

    private Game _game;
    
    private bool isPlaying;
    
    private List<IDrawable> drawableObjects = new();
    private List<IUpdatable> updatableObjects = new();
    private List<ISavable> savableObjects = new();

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
        
        savableObjects.Add(this);
        
        _game = new ();
        _game.Start();
        
        _game.window.renderWindow.Closed += (_, _) => isPlaying = false;

        isPlaying = true;
        //AddSavableItem(test);
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
            if(test<int.MaxValue)
                test++;
        }
    }

    private void Update()
    {
        for (int i = 0; i < updatableObjects.Count; i++)
        {
            updatableObjects[i].Update();
        }

        for (int i = 0; i < savableObjects.Count; i++)
        {
            savableObjects[i].Save();
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
        if (gameObject is ISavable)
        {
            if (!savableObjects.Contains(gameObject as ISavable))
            {
                savableObjects.Add(gameObject as ISavable);
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
        newGameLoop.LoadGameLoopInfo();
        return newGameLoop;
    }
    public void RemoveSavableItem(string name)
    {
        foreach (var item in savableItems)
        {
            if (item.Item2 == name)
            {
                savableItems.Remove(item);
            }

        }
    }
    private void UpdateSavableItemsValues()
    {
        for (int i = 0; i < savableItems.Count; i++)
        {
            (string type, string name, string value) = savableItems[i];
            value = typeof(GameLoop).GetField(savableItems[i].Item2).GetValue(this).ToString();
            savableItems[i] = (type, name, value);
        }
    }

    public void Save()
    {
        UpdateSavableItemsValues();
        StreamWriter sw = new StreamWriter(pathToSavedFile);

        foreach (var item in savableItems)
        {
            sw.WriteLine($"{item.Item1} {item.Item2} {item.Item3}");
        }
        sw.Close();
    }

    public void SetSavableItems(List<(string,string,string)> items)
    {
        savableItems = new(items);
    }

}