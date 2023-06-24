using SFML.Graphics;

public class GameSettings
{
    public static uint windowWidth = 1600;
    public static uint windowHeight = 900;
    public static int fieldWidth = 1600;
    public static int fieldHeight = 900;
    public static string windowTitle = "Agario!";
    
    public static int maxFoodAmount = 100;
    public static int maxPlayersAmount = 30;
    
    private static string pathToCFG = "CFGs/engine.cfg";

    static GameSettings()
    {
        Load();
    }
    public static void Save()
    {
        var variables = typeof(GameSettings).GetFields();

        StreamWriter sw = new StreamWriter(pathToCFG);
        
        foreach (var variable in variables)
        {
            sw.WriteLine($"{variable.Name} {variable.GetValue(null)}");
        }
        sw.Close();
    }

    private static void Load()
    {
        if (!File.Exists(pathToCFG))
        {
            Save();
            return;
        }
        StreamReader sr = new (pathToCFG);

        while (!sr.EndOfStream)
        {
            string[] info = sr.ReadLine().Split(' ');
            if (info.Length < 2)
                continue;
            
            var foundVariable = typeof(GameSettings).GetField(info[0]);
            if (foundVariable == null)
                continue;

            switch (Type.GetTypeCode(foundVariable.FieldType))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    foundVariable.SetValue(null, int.Parse(info[1]));
                    break;
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    foundVariable.SetValue(null, uint.Parse(info[1]));
                    break;
                case TypeCode.String:
                    foundVariable.SetValue(null, info[1]);
                    break;
                case TypeCode.Boolean:
                    foundVariable.SetValue(null, Convert.ToBoolean(info[1]));
                    break;
            }
        }
        sr.Close();
    }
}