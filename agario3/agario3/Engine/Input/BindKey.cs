using SFML.Window;

public enum PressType
{
    KeyUp,
    KeyDown,
    Press,
}
public class BindKey
{
    public Action onActivate;

    private Keyboard.Key key;
    
    private PressType _pressType;
    private bool _activationKeyActive;

    public BindKey(Keyboard.Key key, PressType pressType)
    {
        this.key = key;
        _pressType = pressType;
    }
    
    public void CheckInput()
    {
        GetKeyDown();
        
        if(GetKeyboardInput())
            onActivate?.Invoke();
    }
    private bool GetKeyboardInput() => _pressType switch
    {
        PressType.Press => GetKeyPress(),
        PressType.KeyDown => GetKeyDown(),
        PressType.KeyUp => GetKeyUp(),
        _ => false,
    };
    private bool GetKeyUp()
    {
        if (Input.lastKeyboardKey == key && !GetKeyDown())
        {
            Input.lastKeyboardKey = Keyboard.Key.Unknown;
            return true;
        }
        return false;
    }
    private bool GetKeyDown()
    {
        if (Keyboard.IsKeyPressed(key))
        {
            Input.lastKeyboardKey = key;
            return true;
        }

        return false;
    } 
    
    private bool GetKeyPress()
    {
        if (!_activationKeyActive)
        {
            _activationKeyActive = Keyboard.IsKeyPressed(key);
            return Keyboard.IsKeyPressed(key);
        }
        _activationKeyActive = Keyboard.IsKeyPressed(key);

        return false;

    }





}