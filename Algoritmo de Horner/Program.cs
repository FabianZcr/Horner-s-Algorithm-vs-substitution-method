/*  Horner-s-Algorithm-vs-substitution-method.
    A brief analysis of two algorithms to determine which is better 
    to carry out the evaluation of a polynomial where x takes a specific value.
    By: Fabián Mauricio Zamora Rivera
*/
using System;
using System.Collections.Generic;


namespace Algoritmo_de_Horner
{
    class Program
    {
        static int ms = 0; // multiplicacioenes efectuadas por el método de horner
        static int mh = 0; // multiplicaciones efectuadas por el método de sustitución
        static int ss = 0; // sumas realizadas en el método por sustitución
        static int sh = 0; // sumas realizadas en el método de horner

        static int a = 0; // asignaciones
        static int c = 0; // comparaciones

        static void Main(string[] args)
        {
            // x^5+ 4x^4 + 5x^3  - 6x^2 + 0x + 8 = 1, 4, 5, -6, 0, 8 }; 
            // 21x^5+ 4x^4 + -9x^3  - 1x^2 + 5x + 43 = { 21, 4, -9, -1, 5, 43 }; 
            List<int> coeficientes = new List<int> { 53, 1, 7, -6, 2, 1 };           // 53x^5+ x^4 + 7x^3  + 2x^2 + 17x + 1
            int x = 12;
            llamarMetodos(x, coeficientes);
            while (true)
            {
                Console.Write("¿Desea probar los algoritmos con otro polinomio? S/N: ");
                char a = Console.ReadKey().KeyChar;
                if (a == 's' | a == 'S')
                {
                    obtenerPolinomioInteractivo();
                }
                else if(a == 'n' | a == 'N')
                {
                    Console.WriteLine("Programa finalizado.");
                    break;
                }
                else
                {
                    Console.WriteLine("Digite un caracter válido.");
                }
            }
        }

        private static String ObtenerPolinomio(List<int> coeficientes)
        {
            int grado = coeficientes.Count - 1;
            String notacionPolinomio = coeficientes[0] + "x^" + (grado);
            grado--;
            for (int i = 1; i < coeficientes.Count; i++)
            {
                notacionPolinomio += " + " + coeficientes[i] + "x^" + (grado);
                grado--;
            }
            return notacionPolinomio;
        }

        private static int AlgoritmoSustitucion(int x, List<int> polinomio)
        {
            int grado = polinomio.Count - 1; a++;
            int resultado = polinomio[0] * multiplicatoria(x, grado); grado--; a += 3;
            a++;
            for (int i = 1; i < polinomio.Count; i++)
            {
                c++;
                resultado += multiplicatoria(x, grado) * polinomio[i];
                ms++;
                ss++;
                grado--;
                a++;
            }
            c++;
            a++;
            return resultado;
        }

        private static int multiplicatoria(int x, int grado)
        {
            int multiplicatoria = x;
            a += 3;
            if (grado == 0)
            {
                c++;
                a++;
                return 1;
            }
            else if(grado == 1)
            {
                c += 2;
                a++;
                return x;
            }
            else
            {
                c += 2;
                a++;
                for (int j = 1; j < grado; j++)
                {
                    c++;
                    multiplicatoria *= x;
                    ms++;
                    a++;
                }
                c++;
                a++;
                return multiplicatoria;
            }         
        }

        private static int AlgoritmoHorner(int x, List<int> polinomio)
        {
            int resultado = polinomio[0]; a++;
            a++;
            for (int i = 1; i < (polinomio.Count); i++)
            {
                c++;
                resultado = (resultado * x) + polinomio[i];
                a += 2;
                mh++;
                sh++;
            }
            a++;
            c++;
            return resultado;
        }

        private static void llamarMetodos(int x, List<int> coeficientes)
        {
            Console.WriteLine("*************************************************************************");
            Console.WriteLine("Polinomio evaluado: " + ObtenerPolinomio(coeficientes));
            Console.WriteLine("Valor de X: " + x);
            Console.WriteLine("\nPor sustitución: \n"
                              + "Resultado: " + AlgoritmoSustitucion(x, coeficientes) + "\n"
                              + "Multiplicaciones efectuadas: " + ms + "\n"
                              + "Sumas efectuadas: " + ss + "\n"
                              + "Asignaciones: " + a + "\n"
                              + "Comparaciones: " + c);
            a = 0;
            c = 0;
            ms = 0;
            mh = 0;
            sh = 0;
            ss = 0;

            Console.WriteLine("\nPor Horner: \n"
                              + "Resultado: " + AlgoritmoHorner(x, coeficientes) + "\n"
                              + "Multiplicaciones efectuadas: " + mh + "\n"
                              + "Sumas efectuadas: " + sh + "\n"
                              + "Asignaciones: " + a + "\n"
                              + "Comparaciones: " + c + "\n");
            Console.WriteLine("*************************************************************************");
            a = 0;
            c = 0;
            ms = 0;
            mh = 0;
            sh = 0;
            ss = 0;
        }

        private static void obtenerPolinomioInteractivo()
        {
            String polinomio = "";
            List<int> coeficientes = new List<int>();
            Console.Write("\n\nDigite el grado del polinomio: ");
            int grado = int.Parse(Console.ReadLine());
            for (int i = 0; i < (grado + 1); i++)
            {
                Console.Write("Digite el valor del coeficiente de x^" + (grado - i) + ": ");
                coeficientes.Add(int.Parse(Console.ReadLine()));
                if (i == 0)
                {
                    polinomio = coeficientes[i] + "x^" + (grado - i);
                }
                else
                {
                    polinomio += " + " + coeficientes[i] + "x^" + (grado - i);
                }
            }
            Console.Write("Digite el valor por el cual desea evaluar x: ");
            int x = int.Parse(Console.ReadLine()); 
            llamarMetodos(x, coeficientes);
        }
    }
}
