// See https://aka.ms/new-console-template for more information

namespace QuadTree_OpenTK
{
    class Program
    {
        static void Main(String[] args)
        {
            
            using( Game game = new Game() ) 
            {
                game.Run();
            }

        }

    }
}


