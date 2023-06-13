using SFML.Window;

public class BindKey
{
    public Action OnKeyPress;
    public Action OnKeyDown;
    public Action OnKeyUp;

    private Keyboard.Key key;
    
    private bool _wasPressed;

    public BindKey(Keyboard.Key key)
    {
        this.key = key;
    }
    
    public void CheckInput()
    {
        CheckKeyboardInput();
    }

    private void CheckKeyboardInput()
    {
        if(GetKeyUp())
            OnKeyUp?.Invoke();
        if(GetKeyPress())
            OnKeyPress?.Invoke();
        if(GetKeyDown())
            OnKeyDown?.Invoke();
    }
    private bool GetKeyUp()
    {
        if (_wasPressed && !GetKeyDown())
        {
            _wasPressed = false;
            return true;
        }
        return false;
    }
    private bool GetKeyDown()
    {
        if (Keyboard.IsKeyPressed(key))
        {
            _wasPressed = true;
            return true;
        }

        return false;
    } 
    
    private bool GetKeyPress()
    {
        if (!_wasPressed)
        {
            _wasPressed = Keyboard.IsKeyPressed(key);
            return Keyboard.IsKeyPressed(key);
        }
        _wasPressed = Keyboard.IsKeyPressed(key);

        return false;

    }





}