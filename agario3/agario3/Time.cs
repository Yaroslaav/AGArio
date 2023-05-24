using System.Diagnostics;

public static class Time
{
    public static int deltaTime { get; private set; } = 0;
    private static Stopwatch timer = new ();

    public static void Start()
    {
        timer.Start();
        deltaTime = 0;
    }

    public static void UpdateTimer()
    {
        deltaTime = timer.Elapsed.Milliseconds;
        timer.Restart();
    }

}