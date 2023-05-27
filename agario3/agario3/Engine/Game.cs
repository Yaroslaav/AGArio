using SFML.Graphics;
using SFML.System;

public class Game
{
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

        for (int i = 0; i < 5; i++)
        {
            SpawnPlayer();
        }
        for (int i = 0; i < GameSettings.maxFoodAmount; i++)
        {
            SpawnFood();
        }
 
        mainCamera.SetupCamera();
        Time.Start();
    }
    public void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].CheckCollisionWithFood(foodList.ToArray());
        }
    }
    
    private void SpawnPlayer()
    {
        Player spawnedPlayer = this.CreateActor<Player>(new CircleShape(), new IntRect(0, 0, 30, 30), null,
            new Vector2f(window.GetRandomPosition().X, window.GetRandomPosition().Y), Color.Green, Color.Black);
        if (ownPlayer == null)
        {
            spawnedPlayer.isBot = false;
            ownPlayer = spawnedPlayer;
        }
        else
        {
            spawnedPlayer.isBot = true;
        }
        players.Add(spawnedPlayer);

    }
    private void SpawnFood()
    {
        Vector2f foodPosition = window.GetRandomPosition();
        Food food = this.CreateActor<Food>(new CircleShape(), new IntRect(0, 0, 10, 10), null, foodPosition, Color.Red, 
            Color.White);
        foodList.Add(food);
    }
    
}