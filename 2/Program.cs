namespace _2
{
    using static System.Math;
    using static _2.Program;

    internal class Program
    {
        interface Shape
        {
            double calculate(params double[] numbers);
            bool islegal(params double[] numbers);
        }
        public class rectangle : Shape
        {
            public double calculate(params double[] numbers)
            {
                return numbers[0] * numbers[1];
            }
            public bool islegal(params double[] numbers)
            {
                if (numbers[0] > 0 && numbers[1] > 0)
                    return true;
                else
                    return false;
            }
        }
        public class circle : Shape
        {
            public double calculate(params double[] numbers)
            {
                return Pow(numbers[0],2)*PI;
            }
            public bool islegal(params double[] numbers)
            {
                if (numbers[0] > 0 )
                    return true;
                else
                    return false;
            }
        }
        public class square: Shape
        {
            public double calculate(params double[] numbers)
            {
                return Pow(numbers[0],2);
            }
            public bool islegal(params double[] numbers)
            {
                if (numbers[0] > 0)
                    return true;
                else
                    return false;
            }
        }
        static void Main(string[] args)
        {
            Random rand = new Random();
            List<Shape> shapes = new List<Shape>();
            double totalArea = 0;

            for (int i = 0; i < 10; i++)
            {
                int type = rand.Next(0, 3); // 随机产生 0, 1, 2

                switch (type)
                {
                    case 0: // 长方形
                        shapes.Add(new rectangle());
                        // 随机生成长宽 1.0 - 10.0
                        double w = rand.NextDouble() * 9 + 1;
                        double h = rand.NextDouble() * 9 + 1;
                        totalArea += shapes[i].calculate(w, h);
                        Console.WriteLine($"创建了长方形: 宽={w:F2}, 高={h:F2}");
                        break;

                    case 1: // 圆形
                        shapes.Add(new circle());
                        double r = rand.NextDouble() * 5 + 1;
                        totalArea += shapes[i].calculate(r);
                        Console.WriteLine($"创建了圆形: 半径={r:F2}");
                        break;

                    case 2: // 正方形
                        shapes.Add(new square());
                        double side = rand.NextDouble() * 8 + 1;
                        totalArea += shapes[i].calculate(side);
                        Console.WriteLine($"创建了正方形: 边长={side:F2}");
                        break;
                }
            }

            Console.WriteLine("------------------------------");
            Console.WriteLine($"10个形状的总面积之和为: {totalArea:F2}");
        }

    }
    
}
