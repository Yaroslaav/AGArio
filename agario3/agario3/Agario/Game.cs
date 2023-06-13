using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Game
{
    private Random rand = new Random();
    public static Game instance { get; private set; }
    
    public Camera mainCamera { get; private set; }
    public Window window { get; private set; }
    
    public List<Food> foodList { get; private set; } = new ();

    public Player ownPlayer { get; private set; }
    public List<Player> players = new List<Player>();
    
    public Vector2f ownPlayerPosition
    {
        get => ownPlayer.shape.Position;
    }
    
    public void Start()
    {
        if(instance == null)
            instance = this;
        
        
        window = new ();
        mainCamera = new ();

        for (int i = 0; i < GameSettings.maxPlayersAmount; i++)
        {
            SpawnPlayer();
        }
        for (int i = 0; i < GameSettings.maxFoodAmount; i++)
        {
            SpawnFood();
        }
 
        mainCamera.SetupCamera();
        Time.Start();

        CreateBindings();
    }
    public void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].CheckCollisionWithFood(foodList.ToArray());
            players[i].CheckCollisionWithPlayers(players.ToArray());
        }
    }
    
    private void SpawnPlayer()
    {
        Player spawnedPlayer = this.CreateActor<Player>(new Vector2f(60,60), new IntRect(0, 0, 60, 60), null,
            new Vector2f(window.GetRandomPosition().X, window.GetRandomPosition().Y), Color.Green, Color.Black);
        spawnedPlayer.Start(ownPlayer != null);
        
        if (ownPlayer == null)
        {
            ownPlayer = spawnedPlayer;
        }
        
        players.Add(spawnedPlayer);

    }
    private void SpawnFood()
    {
        Vector2f foodPosition = window.GetRandomPosition();
        Food food = this.CreateActor<Food>(new Vector2f(20,20), new IntRect(0, 0, 20, 20), null, foodPosition, Color.Red, 
            Color.White);
        foodList.Add(food);
    }

    private void SwapMainPlayer()
    {
        Player randomPlayer = players[rand.Next(players.Count)];
        ownPlayer.OnSwitchSoul();
        randomPlayer.OnSwitchSoul();
        ownPlayer = randomPlayer;
    }


    public void DestroyGameObject(GameObject gameObject)
    {
        if (gameObject is Player)
        {
            if (ownPlayer == gameObject as Player) 
                ownPlayer = null;

            if (players.Contains(gameObject as Player))
                players.Remove(gameObject as Player);
        }
        else if(gameObject is Food)
        {
            if (foodList.Contains(gameObject as Food))
            {
                foodList.Remove(gameObject as Food);
            }
        }
        GameLoop.Instance.UnRegisterGameObject(gameObject);
    }

    private void CreateBindings()
    {
        BindKey changeSoulKey = Input.AddNewBind(Keyboard.Key.F, "ChangeSoul", PressType.Press);
        changeSoulKey.onActivate += SwapMainPlayer;
    }
}