using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Race_Interface
{
    interface Race_Element
    {
        char   getSymbol();
        int    getPos_X();
        string getName();
        void   move();
    }

    internal class Race
    {
        private int            game_speed      = 1000 / 10;
        private int            anzahl          = 8;
        private Race_Element[] array_elemets   = null; // new Lebewesen[anzahl];
        private bool           running         = false;

        private int            ziel_x_pos      = 80;
        private int            elem_count      = 0;

        public Race() 
        {
            this.anzahl = 8;
            this.erzeugeLebewesen();
        }
        public void init()
        {      
            Console.Clear();
            Console.CursorVisible = false;
            
            this.zeichneSpielfeld();            
        }
        private void zeichneSpielfeld()
        {
            // Grezen zwischen den Bahnen
            for (int i = 0; i < this.elem_count; i++)
            {
                Console.SetCursorPosition(0, (i * 2) + 1);
                Console.Write(new string('_', this.ziel_x_pos + 2));
            }

            // Ziellinie am Ende
            for (int i = 0; i < this.elem_count * 2; i++)
            {
                Console.SetCursorPosition(this.ziel_x_pos - 1 , i);
                Console.Write( '|');
            }
        }
        public void start()
        {
            this.running = true;
            int ziel_counter = 0;
            while (this.running)
            {
                ziel_counter = 0;
                for (int i = 0; i < this.elem_count; i++)
                {                       
                    Console.SetCursorPosition( this.array_elemets[i].getPos_X() , i * 2 );
                    Console.Write( this.array_elemets[i].getSymbol() );

                    // Überschreibem der alten 
                    if( this.array_elemets[i].getPos_X() > 0 ) 
                    {
                        Console.SetCursorPosition(this.array_elemets[i].getPos_X() - 1, i * 2);
                        Console.Write( " " );
                    }                    

                    if ( this.array_elemets[i].getPos_X() >= this.ziel_x_pos )
                    {
                        Console.Beep();
                        ziel_counter++;
                    }
                    else
                    {
                        this.array_elemets[i].move();
                    }
                }

                if( ziel_counter >= this.elem_count )
                {
                    Console.WriteLine("Ende");
                    this.running = false;
                }

                this.Leaderboard();
                Thread.Sleep( this.game_speed );
            }
        }
        private void erzeugeLebewesen()
        {
            this.array_elemets = new Race_Element[this.anzahl];            
        }
        public void addElemetToRace( Race_Element in_element)
        {
            for(int x = 0;  x < this.array_elemets.Length; x++)
            {
                if( this.array_elemets[x] == null )
                {
                    this.array_elemets[x] = in_element;
                    this.elem_count = x + 1;
                    return;
                }
            }

            Console.WriteLine( " Das Rennen ist schon voll belegt !!! " );
        }
        private void Leaderboard()
        {
            Race_Element[] lb_array = sort_Lebewesen_Array_By_XPos();
            for ( int i = 0; i < this.elem_count; i++ )
            {
                Console.SetCursorPosition(0, 0 + ( this.elem_count * 2 ) + i );
                Console.Write("#" + (i + 1) + ": " + lb_array[i].getName() + "    ");
            }
        }
        private Race_Element[] sort_Lebewesen_Array_By_XPos()
        {
            Race_Element[] new_lebewesen_array = new Race_Element[this.array_elemets.Length];

            this.array_elemets.CopyTo(new_lebewesen_array, 0);

            // bubble sort
            for (int x = 0; x < this.elem_count; x++)
            {
                for (int y = 0; y < this.elem_count - 1 - x; y++)
                {
                    if (new_lebewesen_array[y + 1].getPos_X() > new_lebewesen_array[y].getPos_X())
                    {
                        Race_Element tmp_lw        = new_lebewesen_array[y];
                        new_lebewesen_array[y]     = new_lebewesen_array[y + 1];
                        new_lebewesen_array[y + 1] = tmp_lw;
                    }
                }
            }
            return new_lebewesen_array;
        }
    }
}
