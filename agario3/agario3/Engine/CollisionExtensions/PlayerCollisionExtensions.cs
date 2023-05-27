using SFML.Graphics;

public static class PlayerCollisionExtensions
{
    public static void CheckCollisionWithFood(this Player player, Food[] foodArr)
    {
        for (int i = 0; i < foodArr.Length; i++)
        {
            Food food = foodArr[i];
            if (player.shape.CheckCollision(food.shape))
            {
                player.OnEat(food.mass);
                   
                food.OnWasEaten();
            }
        }

    }
    public static void CheckCollisionWithPlayers(this Player player, Player[] enemiesArr)
    {
        for (int i = 0; i < enemiesArr.Length; i++)
        {
            Player enemy = enemiesArr[i];
            if (player.shape.CheckCollision(enemy.shape))
            {
                player.OnEat(enemy.mass);
                   
                enemy.OnWasEaten();
            }
        }

    }

}