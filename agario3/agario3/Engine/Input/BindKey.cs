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
        GetKeyUp();
        GetKeyPress();
        GetKeyDown();
    }
    private bool GetKeyUp()
    {
        if (_wasPressed && !GetKeyDown())
        {
            _wasPressed = false;
            OnKeyUp?.Invoke();
            return true;
        }
        return false;
    }
    private bool GetKeyDown()
    {
        if (Keyboard.IsKeyPressed(key))
        {
            _wasPressed = true;
            OnKeyDown?.Invoke();
            return true;
        }

        return false;
    } 
    
    private bool GetKeyPress()
    {
        bool isPresses = Keyboard.IsKeyPressed(key);
        if (!_wasPressed)
        {
            _wasPressed = isPresses;
            if(isPresses)
                OnKeyPress?.Invoke();
            return isPresses;
        }
        _wasPressed = isPresses;

        return false;

    }





}