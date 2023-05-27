public static class Probability
{
    private static Random rand = new();
    public static bool Next(int procent)
    {
        if (procent < 0)
        {
            procent = 0;
        }
        else if (procent > 100)
        {
            procent = 100;
        }
        return rand.Next(1, 101) <= procent;
    }

}