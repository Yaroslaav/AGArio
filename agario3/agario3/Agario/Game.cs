using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Game
{
    public static Game instance { get; private set; }
    
    public Camera mainCamera { get; private set; }
    public Window window { get; private set; }
    
    public List<Food> foodList { get; private set; } = new ();
    Sound sound { get; set; }
    SoundBuffer soundBuffer { get; set; }

    public Player ownPlayer { get; private set; }
    public List<Player> players = new List<Player>();

    private string playerTexturesFolderPath = "PlayerAnimations/";
    private string foodTexturesFolderPath = "FoodAnimations/";
    
    public Vector2f ownPlayerPosition
    {
        get => ownPlayer.shape.Position;
    }
    
    public void Start()
    {
        if(instance == null)
            instance = this;

        //soundBuffer = new("");
        sound = new(soundBuffer);
        //sound.Status == SoundStatus.Stopped
        
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
    
    private void SetBackgroundAudio()
    {
        
    }
    
    private void SpawnPlayer()
    {

        string[] textureFiles = Directory.GetFiles(playerTexturesFolderPath, "*.png", SearchOption.AllDirectories);

        Texture playerTexture = new Texture(textureFiles[Rand.Next(textureFiles.Length)]);

        Player spawnedPlayer = this.CreateActor<Player>(new Vector2f(60, 60), playerTexture,
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
        string[] textureFiles = Directory.GetFiles(foodTexturesFolderPath, "*.png", SearchOption.AllDirectories);

        Texture foodTexture = new Texture(textureFiles[Rand.Next(textureFiles.Length)]);


        Vector2f foodPosition = window.GetRandomPosition();
        Food food = this.CreateActor<Food>(new Vector2f(30, 30), foodTexture, foodPosition, Color.Red, Color.White);
        food.Start();
        foodList.Add(food);
    }

    private void SwapMainPlayer()
    {
        Player randomPlayer = players[Rand.Next(players.Count)];
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
        BindKey changeSoulKey = Input.AddNewBind(Keyboard.Key.F, "ChangeSoul");
        changeSoulKey.OnKeyPress += SwapMainPlayer;
    }
}