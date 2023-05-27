using System.Diagnostics;

public static class Time
{
    public static int totalFrames = 0;
    public static int totalMiliSeconds = 0;
    public static int totalSeconds
    {
        get => totalMiliSeconds / 1000;
    }
    public static int deltaTime { get; private set; } = 0;
    private static Stopwatch timer;

    public static void Start()
    {
        timer = new();
        timer.Start();
        deltaTime = 0;
    }

    public static void UpdateTimer()
    {
        totalFrames++;
        deltaTime = timer.Elapsed.Milliseconds;
        totalMiliSeconds += deltaTime;
        timer.Restart();
    }

}