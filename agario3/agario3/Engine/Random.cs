public class Rand
{
    public static int Next(int min, int max)
    {
        return new Random().Next(min, max);
    }
    public static int Next(int max)
    {
        return new Random().Next(0, max);
    }
}
