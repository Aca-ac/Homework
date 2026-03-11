namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("你好，我是何向玺");
            string inputLine = Console.ReadLine();
            string[] inputParts = inputLine.Split(' ');
            int downlimit = int.Parse(inputParts[0]);
            int uplimit=int.Parse(inputParts[1]);
            int count = 0;
            for (int i = downlimit; i <= uplimit; i++)
            {
                bool judge = true;
                for (int j = 2; j * j <= i; j++)
                {
                   if(i%j==0)
                    {
                        judge = false;
                        break;
                    }
                }
                if (judge)
                {
                    Console.Write(i+" ");
                    count++;
                }
                if (count == 10)
                {
                    Console.WriteLine();
                    count = 0;
                }
            }

        }
    }
}

