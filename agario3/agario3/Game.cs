using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Game
{
    private  Random random = new ();
    
    private bool isPlaying;
    
    private List<Food> foodList = new ();

    //private List<Player> bots = new();
    private Player ownPlayer;

    private List<IDrawable> drawableObjects = new();
    private List<IUpdatable> updatableObjects = new();



    public void SpawnPlayer()
    {
        Player spawnedPlayer = this.CreateActor<Player>(new CircleShape(), new IntRect(0, 0, 30, 30), null,
            new Vector2f(Window.GetWindowCenter().X, Window.GetWindowCenter().Y), Color.Green, Color.Black);
        if (ownPlayer == null)
        {
            ownPlayer = spawnedPlayer;
        }
        else
        {
            //bots.Add(spawnedPlayer);
        }
    }

    public void SpawnFood()
    {
        Vector2f foodPosition = new Vector2f(random.Next(GameSettings.FIELD_WIDTH), random.Next(GameSettings.FIELD_HEIGHT));
        Food food = this.CreateActor<Food>(new CircleShape(), new IntRect(0, 0, 10, 10), null, foodPosition, Color.Red, 
            Color.White);
        foodList.Add(food);
    }
   public void Start()
   {
            Window.SetWindow(); 
            Window.renderWindow.Closed += (_, _) => isPlaying = false;

            SpawnPlayer();
            for (int i = 0; i < 500; i++)
            {
                SpawnFood();
            }

            MainCamera.SetupCamera();
            Time.Start();
            isPlaying = true;
            
            GameLoop();
   }

   private void GameLoop()
   {
       while (isPlaying)
       {
           Time.UpdateTimer();
           Window.DispatchEvents();

           ownPlayer.Update();

           ownPlayer.CheckCollisionWithFood(foodList.ToArray());

           MainCamera.MoveCamera(ownPlayer.shape.Position);
           
           Window.Draw(drawableObjects);
       }

   }

    public void RegisterDrawableActor(IDrawable drawable)
    {
        if (!drawableObjects.Contains(drawable))
            drawableObjects.Add(drawable);
    }
    public void RegisterActor(IUpdatable actor)
    {
        if (!updatableObjects.Contains(actor))
            updatableObjects.Add(actor as IUpdatable);
    }
    public void UnregisterActor(IUpdatable actor)
    {
        if (updatableObjects.Contains(actor))
            updatableObjects.Remove(actor);
    }
    public void UnregisterDrawableActor(IDrawable drawable)
    {
        if (drawableObjects.Contains(drawable))
            drawableObjects.Remove(drawable);
    }
}