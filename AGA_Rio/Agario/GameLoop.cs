using SFML.Graphics;
using SFML.System;

public class GameLoop
{
    public bool isPlaying;
    
    public List<IUpdatable> updatableObjects;
    public List<IDrawable> drawableObjects;
    
    public View _camera;


    public void Run()
    {
        updatableObjects = new();
        drawableObjects = new();
        SetPlayerCamera();
        Input.SetCurrentCamera(_camera);
    }
    
    public void Update()
    {
        UpdateObjects();
        
        SortDrawableObjects();
        Window.Draw(drawableObjects, _camera);
    }

    private void UpdateObjects()
    {
        Time.UpdateTimer();
        for (int i = 0; i < updatableObjects.Count; i++)
        {
            updatableObjects[i].Update();
        }
    }

    private void SortDrawableObjects()
    {
        drawableObjects.Sort((x, y) => x.ZPosition.CompareTo(y.ZPosition));
    }
    private void SetPlayerCamera()
    {
        _camera = new View(new FloatRect(0f, 0f, GameSettings.WINDOW_WIDTH, GameSettings.WINDOW_HEIGHT));
        
    }

    public void SetCameraPosition(Vector2f newPosition)
    {
        _camera.Center = newPosition;
    }
    

}