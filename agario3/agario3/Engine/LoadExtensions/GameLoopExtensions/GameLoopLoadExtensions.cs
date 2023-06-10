
using System.Reflection;

public static class GameLoopLoadExtensions
{
    public static GameLoop LoadGameLoopInfo(this GameLoop gameLoop)
    {
        List<(string, string, string)> savableItems = new();
       
        StreamReader sr = new StreamReader(gameLoop.pathToSavedFile);
        while (!sr.EndOfStream)
        {
            var info = sr.ReadLine().Split(' ');
            if(info.Length < 3)
                continue;
            
            string variableType = info[0];
            string variableName = info[1];
            string value = info[2];
            
            if (Type.GetType(variableType) == null)
            {
                continue;
            }

            object type = Activator.CreateInstance(Type.GetType(variableType));
            if(type == null)
                continue;
            
            savableItems.Add((variableType,variableName,value));

            FieldInfo fieldInfo = typeof(GameLoop).GetField(variableName);
            switch (type)
            {
                case int:
                    if (int.TryParse(value, out int _value))
                    {
                        fieldInfo?.SetValue(gameLoop, _value);
                    }
                    break;
                case string:
                    fieldInfo?.SetValue(gameLoop, value);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        sr.Close();
        Console.WriteLine(gameLoop.test + "    variable");
        gameLoop.SetSavableItems(savableItems);
        return gameLoop;
    }
}