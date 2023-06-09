
public static class GameLoopLoadExtensions
{
    public static bool TrySetIntVariable(this GameLoop obj, string variableName, int value)
    {
        typeof(GameLoop).GetField(variableName)?.SetValue(obj, (object) value);
        return typeof(GameLoop).GetField(variableName) != null;
    }
}