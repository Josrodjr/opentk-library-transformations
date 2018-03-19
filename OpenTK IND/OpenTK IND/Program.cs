using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace OpenTK_IND
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindow window = new GameWindow(750, 750);
            game gm = new game(window);
        }
    }
}
