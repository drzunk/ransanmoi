using System;
using System.Collections.Generic;

namespace SnakeGame
{
    class Program
    {
        private const int Left = 30;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White; // đặt màu cho rắn
            Console.BackgroundColor = ConsoleColor.DarkBlue; // đặt màu cho mồi
            Console.SetWindowSize(60, 30);
            Console.CursorVisible = false;

            int width = 60;
            int height = 30;

            Random random = new Random();
            Point food = new Point(random.Next(0, width), random.Next(0, height));

            Snake snake = new Snake(new Point(width / 2, height / 2));

            bool gameOver = false;

            while (!gameOver)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            snake.Direction = Direction.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            snake.Direction = Direction.Right;
                            break;
                        case ConsoleKey.UpArrow:
                            snake.Direction = Direction.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            snake.Direction = Direction.Down;
                            break;
                    }
                }

                Console.Clear();

                snake.Move();

                if (snake.Head.Equals(food))
                {
                    snake.Grow();
                    food = new Point(random.Next(0, width), random.Next(0, height));
                }

                if (snake.Head.X < 0 || snake.Head.X >= width || snake.Head.Y < 0 || snake.Head.Y >= height)
                {
                    gameOver = true;
                }

                for (int i = 1; i < snake.Body.Count; i++)
                {
                    if (snake.Head.Equals(snake.Body[i]))
                    {
                        gameOver = true;
                    }
                }


                Console.SetCursorPosition(food.X, food.Y);
                Console.Write("X");

                foreach (Point point in snake.Body)
                {
                    Console.SetCursorPosition(point.X, point.Y);
                    Console.Write("0");
                }

                System.Threading.Thread.Sleep(100);
            }

            Console.SetCursorPosition(Left, height);
            Console.WriteLine("Game Over!");
            

        }
    }

    enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }
    }

    class Snake
    {
        public List<Point> Body { get; set; }
        public Direction Direction { get; set; }

        public Point Head
        {
            get { return Body[0]; }
        }

        public Snake(Point position)
        {
            
            Body = new List<Point>();
            Body.Add(position);
            Direction = Direction.Right;

        }

        public void Move()
        {
            Point newHead = new Point(Head.X, Head.Y);

            switch (Direction)
            {
                case Direction.Left:
                    newHead.X--;
                    break;
                case Direction.Right:
                    newHead.X++;
                    break;
                case Direction.Up:
                    newHead.Y--;
                    break;
                case Direction.Down:
                    newHead.Y++;
                    break;
            }

            Body.Insert(0, newHead);
            Body.RemoveAt(Body.Count - 1);
        }

        public void Grow()
        {
            Body.Add(new Point(Body[Body.Count - 1].X, Body[Body.Count - 1].Y));
        }
    }
}


