using SFML.Window;

class Program
{
    static void Main(string[] args)
    {
        /*Type type = Type.GetType("System.Int32");
        Console.WriteLine(type != null);
        Console.WriteLine(Type.GetType("System.Int32") != null);*/
        GameLoop gameLoop = GameLoop.CreateNew();
        gameLoop.Start();
    }
}

