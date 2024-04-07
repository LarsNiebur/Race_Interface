using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race_Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Race my_race = new Race();

            Race_Element m1 = new Mensch("Klaus");
            my_race.addElemetToRace(m1);

            Race_Element my_auto = new Auto( "BrumBrum" );
            my_race.addElemetToRace(my_auto);

            Race_Element my_auto_2 = new Auto("TüTüt");
            my_race.addElemetToRace(my_auto_2);

            Race_Element m2 = new Mensch("Kai-Uwe");
            my_race.addElemetToRace(m2);

            my_race.init();

            my_race.start();

            Console.ReadKey();
        }
    }
}
