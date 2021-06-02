using System;

namespace PlayField
{
    enum Direction
    {
        North = 1,
        East,
        South,
        West
    }

    public class EnumEx
    {    
        public static void Main(string[] args)
        {
            int result = (int) Direction.East;
            Console.WriteLine($"Number of East: {result}");

            Direction direction;
            Enum.TryParse("South", out direction);

            result = (int)direction;
            Console.WriteLine($"Number of new direction = {result}");

            Console.ReadLine();
        }
    }
}
