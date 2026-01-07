using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nraya
{
    class JuegoEnRaya
    {
        static char[,] Tablero;
        static char Turno = 'X';
        static int Tamaño;
        static bool Resultado = true;

        static void Main()
        {
            Partida();
            Console.ReadKey();
        }

        static void Partida()
        {
            do
            {
                Console.Write("Dime el tamaño del tablero (3-9): ");
                bool valido = int.TryParse(Console.ReadLine(), out Tamaño);

                if (!valido || Tamaño < 3 || Tamaño > 9)
                {
                    Console.WriteLine("Número incorrecto. Inténtalo de nuevo.");
                    Tamaño = 0;
                }
            }
            while (Tamaño < 3 || Tamaño > 9);

            Tablero = new char[Tamaño, Tamaño];
            LlenarTablero();

            while (Resultado)
            {
                MostrarTablero();
                PedirJugada();

                if (HayGanador())
                {
                    MostrarTablero();
                    Console.WriteLine("Jugador " + Turno + " ha ganado.");
                    Resultado = false;
                }
                else if (TableroLleno())
                {
                    MostrarTablero();
                    Console.WriteLine("La partida ha quedado empate.");
                    Resultado = false;
                }
                else
                {
                    CambiarTurno();
                }
            }
        }

        static void LlenarTablero()
        {
            for (int i = 0; i < Tamaño; i++)
                for (int j = 0; j < Tamaño; j++)
                    Tablero[i, j] = '.';
        }

        static void MostrarTablero()
        {
            Console.Clear();
            Console.WriteLine("JUEGO DE " + Tamaño + " EN RAYA");
            Console.Write("   ");
            for (int i = 0; i < Tamaño; i++)
                Console.Write(i % 10 + " ");
            Console.WriteLine();

            for (int j = 0; j < Tamaño; j++)
            {
                Console.Write(j % 10 + "  ");
                for (int k = 0; k < Tamaño; k++)
                    Console.Write(Tablero[j, k] + " ");
                Console.WriteLine();
            }
        }

        static void PedirJugada()
        {
            int fila, columna;
            while (true)
            {
                Console.WriteLine("Turno del jugador " + Turno);
                Console.Write("Fila: ");
                bool okFila = int.TryParse(Console.ReadLine(), out fila);
                Console.Write("Columna: ");
                bool okCol = int.TryParse(Console.ReadLine(), out columna);

                if (!okFila || !okCol || fila < 0 || fila >= Tamaño || columna < 0 || columna >= Tamaño)
                    Console.WriteLine("Posición incorrecta.");
                else if (Tablero[fila, columna] != '.')
                    Console.WriteLine("Casilla ocupada.");
                else
                    break;
            }
            Tablero[fila, columna] = Turno;
        }

        static bool HayGanador()
        {
            for (int i = 0; i < Tamaño; i++)
                for (int j = 0; j < Tamaño; j++)
                    if (Tablero[i, j] != '.' &&
                        (EnLinea(i, j, 1, 0) ||
                         EnLinea(i, j, 0, 1) ||
                         EnLinea(i, j, 1, 1) ||
                         EnLinea(i, j, 1, -1)))
                        return true;
            return false;
        }

        static bool EnLinea(int fila, int columna, int pasoFila, int pasoColumna)
        {
            char simbolo = Tablero[fila, columna];
            for (int i = 1; i < Tamaño; i++)
            {
                int nuevaFila = fila + i * pasoFila;
                int nuevaColumna = columna + i * pasoColumna;

                if (nuevaFila < 0 || nuevaFila >= Tamaño ||
                    nuevaColumna < 0 || nuevaColumna >= Tamaño ||
                    Tablero[nuevaFila, nuevaColumna] != simbolo)
                {
                    return false;
                }
            }
            return true;
        }

        static bool TableroLleno()
        {
            foreach (char celda in Tablero)
                if (celda == '.')
                    return false;
            return true;
        }

        static void CambiarTurno()
        {
            Turno = (Turno == 'X') ? 'O' : 'X';
        }
    }
}

