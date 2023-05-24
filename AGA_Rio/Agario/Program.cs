using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Program
{
    static void Main(string[] args)
        {
            const int WindowWidth = 800;
            const int WindowHeight = 600;
            const float PlayerSpeed = .1f;
            const float InitialZoom = 1f;
            const float ZoomIncrement = 0.2f;

            RenderWindow window = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), "Agario");
            window.Closed += (s, e) => window.Close();

            CircleShape player = new CircleShape(20);
            player.Origin = new Vector2f(player.Radius, player.Radius);
            player.FillColor = Color.Green;
            player.Position = new Vector2f(WindowWidth / 2, WindowHeight / 2);

            List<CircleShape> foodList = new List<CircleShape>();
            Random random = new Random();

            for (int i = 0; i < 50; i++)
            {
                CircleShape food = new CircleShape(10);
                food.Origin = new Vector2f(10, 10);
                food.FillColor = Color.Red;
                food.Position = new Vector2f(random.Next(WindowWidth), random.Next(WindowHeight));
                foodList.Add(food);
            }

            View camera = new View(new FloatRect(0, 0, WindowWidth, WindowHeight));
            camera.Zoom(0.5f);
                float curr = .5f;

            while (window.IsOpen)
            {
                window.DispatchEvents();

                Vector2i mousePosition = Mouse.GetPosition(window);
                Vector2f targetPosition = window.MapPixelToCoords(mousePosition);
                Vector2f direction = targetPosition - player.Position;
                float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
                if (length != 0)
                {
                    direction.X /= length;
                    direction.Y /= length;
                }
                player.Position += direction * PlayerSpeed;

                List<CircleShape> eatenFood = new List<CircleShape>();
                foreach (CircleShape food in foodList)
                {
                    if (CheckCollision(player, food))
                    {
                        float newRadius = (float)Math.Sqrt((player.Radius * player.Radius) + (food.Radius * food.Radius));
                        player.Radius = newRadius;
                        float zoomLevel = InitialZoom + (player.Radius / 1000) * ZoomIncrement;
                        //curr += zoomLevel/2;
                        camera.Zoom(zoomLevel);
                        Console.WriteLine(zoomLevel);
                        eatenFood.Add(food);
                    }
                }

                foreach (CircleShape food in eatenFood)
                {
                    foodList.Remove(food);
                }

                camera.Center = player.Position;
                window.SetView(camera);

                window.Clear();

                window.Draw(player);

                foreach (CircleShape food in foodList)
                    window.Draw(food);

                window.Display();
            }
        }

        static bool CheckCollision(CircleShape circle1, CircleShape circle2)
        {
            FloatRect rect1 = circle1.GetGlobalBounds();
            FloatRect rect2 = circle2.GetGlobalBounds();
		
            return rect1.Intersects(rect2);
        }
    }
