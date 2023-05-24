using SFML.Graphics;
using SFML.System;

public class Game
{
    private GameLoop _gameLoop;

    private List<Food> _food = new ();
    private List<Player> _players = new();
    
    private Player _currentPlayer;


    public void Run()
    {
        _gameLoop = new GameLoop();
        _gameLoop.Run();
        
        Window.SetWindow();

        //SpawnMaxFoodAmount(true);
        //SpawnMaxPlayersAmount(true);

        SpawnPlayer();
        
        Time.Start();
        
        _gameLoop.isPlaying = true;
        GameLoop();
    }

    private void GameLoop()
    {
        while (_gameLoop.isPlaying || Window.renderWindow.IsOpen)
        {
            Console.WriteLine("Player position" + _currentPlayer.Position);
            Console.WriteLine("Camera position" + _gameLoop._camera.Center);

            _gameLoop.Update();
            _gameLoop.SetCameraPosition(_currentPlayer.Position);
            
        }
    }
    
    private Food SpawnFood()
    {
        return null;
    }
    private List<Food> SpawnMaxFoodAmount(bool needRespawnAllFood)
    {
        if(needRespawnAllFood)
            _food.Clear();
        if (_food.Count > GameSettings.maxFoodAmount)
        {
            #if DEBUG
                throw new Exception("There is more food on the field than there should be");
            #else
                return;
            #endif
        }
        while(_food.Count < GameSettings.maxFoodAmount)
        {
            _food.Add(SpawnFood());
        }
        return _food;
    }
    private void SpawnPlayer()
    {
        _currentPlayer = this.CreateActor<Player>(new CircleShape(100), new IntRect(0, 0, 1979, 1974), null,
            new Vector2f(800f, 450f));
    }
    private List<Player> SpawnMaxPlayersAmount(bool needRespawnAllPlayers)
    {
        if(needRespawnAllPlayers)
            _players.Clear();
        if (_players.Count > GameSettings.maxFoodAmount)
        {
            #if DEBUG
                throw new Exception("There is more players than there should be");
            #else
                return;
            #endif
        }
        while(_players.Count < GameSettings.maxFoodAmount)
        {
            //_players.Add(SpawnPlayer());
        }
        return _players;
    }

    public void RegisterDrawableActor(IDrawable drawable)
    {
        if (!_gameLoop.drawableObjects.Contains(drawable))
            _gameLoop.drawableObjects.Add(drawable);
    }

    public void RegisterActor(IUpdatable actor)
    {
        if (!_gameLoop.updatableObjects.Contains(actor))
            _gameLoop.updatableObjects.Add(actor as IUpdatable);
    }

    public void UnregisterActor(IUpdatable actor)
    {
        if (_gameLoop.updatableObjects.Contains(actor))
            _gameLoop.updatableObjects.Remove(actor);
    }

    public void UnregisterDrawableActor(IDrawable drawable)
    {
        if (_gameLoop.drawableObjects.Contains(drawable))
            _gameLoop.drawableObjects.Remove(drawable);
    }

    


}