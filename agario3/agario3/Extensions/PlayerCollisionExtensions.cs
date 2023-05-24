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
                float zoomLevel = 1 + player.shape.Radius / 1000 * MainCamera.ZoomIncrement;
                Console.WriteLine("Zoom " + zoomLevel);
                MainCamera.Zoom(zoomLevel);
                   
                food.OnWasEaten();
            }
        }

    }

}