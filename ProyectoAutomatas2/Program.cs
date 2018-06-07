using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAutomatas2
{
    class Program
    {

        static void PrintTable(int NumEntradas, List<List<String>> Tabla)
        {
            int n = Tabla.Count();
            for (int i = 0; i < n; i++)
            {
                Console.Write("||"); 
                for(int j = 0; j <= NumEntradas; j++)
                {
                    Console.Write(" " + Tabla[i][j] + " ||");
                }
                Console.Write("\n");
            }
        }

        static string Revisar(String estado, String entrada, List<List<String>> Tabla)
        {
            String salida = "";
            String[] Division;
            int Elementos;
            List<String> Resultados = new List<string>();            
            if (estado.Contains(","))
            {
                Division = estado.Split(',');
                Elementos = Division.Count();
                for(int i = 0; i < Elementos; i++)
                {
                    Resultados.Add(Revisar(Division[i], entrada, Tabla));
                }
                Resultados = Resultados.Distinct().ToList();
                string empty = "";
                while (Resultados.Contains(""))
                {
                    int temp = Resultados.IndexOf("");
                    Resultados.RemoveAt(temp);
                }
                for(int i = 0; i<Resultados.Count; i++)
                {
                    if (i == Resultados.Count - 1)
                        salida += Resultados[i];
                    else
                        salida += Resultados[i] + ",";
                }
            }
            else
            {
                int n = Tabla[0].IndexOf(entrada);
                for(int i = 0; i<Tabla.Count(); i++)
                {
                    if(Tabla[i][0].Contains("-->")|| Tabla[i][0].Contains("*"))
                    {
                        string temp;
                        if (Tabla[i][0].Contains("-->"))
                        {
                            temp = Tabla[i][0].Remove(0, 3);
                        }else
                        {
                            temp = Tabla[i][0].Remove(0, 1);
                        }
                        if (temp == estado)
                        {
                            salida = Tabla[i][n];
                            break;
                        }
                    }
                    else
                    {
                        if (Tabla[i][0] == estado)
                        {
                            salida = Tabla[i][n];
                            break;
                        }
                    }
                }
                if (salida == "t")
                    salida = "*t";
            }
            return salida;
        }

        static void Main(string[] args)
        {
            bool loop = false;
            char loopverif;
            int NumOfEstados, NumOfEntradas;
            int NewStates = 0;
            int NewStatesCompleted = 0;
            List<String> NuevosEstados = new List<string>();
            List<List<String>> Tabla = new List<List<string>>();
            List<String> Entradas = new List<string>();
            List<String> Estados = new List<string>();
            do
            {
                Console.WriteLine("Proyecto 2");
                Console.WriteLine("");
                Console.WriteLine("Ingrese el numero de estados: ");
                NumOfEstados = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese el numero de entradas: ");
                NumOfEntradas = Int32.Parse(Console.ReadLine());
                String Salida;
                Entradas.Add("");
                for (int i = 1; i <= NumOfEntradas; i++)
                {
                    Console.WriteLine("Ingresa el nombre de la entrada numero " + (i).ToString() + ": ");
                    Entradas.Add(Console.ReadLine());
                }
                Tabla.Add(new List<string>(Entradas));
                for (int i = 0; i < NumOfEstados; i++)
                {
                    Console.WriteLine("Ingresa el nombre del estado numero " + (i + 1).ToString() + " (--> para indicar inicial, * para indicar final): ");
                    Estados.Add(Console.ReadLine());
                    
                   
                    for(int j=1; j<NumOfEntradas+1; j++)
                    {
                        Console.WriteLine("Ingresa la salida con la entrada " + Entradas[j] + " separada por comas y con el nombre de los estados como se ingreso (A,B): ");
                        Salida = Console.ReadLine();
                        if (Salida.Contains(","))
                        {
                            NewStates++;
                            NuevosEstados.Add(Salida);
                        }
                        Estados.Add(Salida);
                        Salida = "";
                    }
                    Tabla.Add(new List<String>(Estados));
                    Estados.Clear();
                }
                String Return;
                Estados.Clear();
                while (NewStatesCompleted<NewStates)
                {
                    Estados.Add(NuevosEstados[NewStatesCompleted]);
                    for(int i=1; i<=NumOfEntradas; i++)
                    {
                        Return = Revisar(NuevosEstados[NewStatesCompleted], Tabla[0][i], Tabla);
                        Estados.Add(Return);
                        if (Return.Contains(","))
                        {
                            if (!NuevosEstados.Contains(Return))
                            {
                                NuevosEstados.Add(Return);
                                NewStates++;
                            }
                        }
                    }
                    Tabla.Add(new List<string>(Estados));
                    Estados.Clear();
                    NewStatesCompleted++;
                }
                PrintTable(NumOfEntradas, Tabla);
                Console.WriteLine("Desea ingresar otro cadena para verificar (Y/N): ");
                loopverif = Char.Parse(Console.ReadLine());
                if (loopverif == 'Y' || loopverif == 'y')
                {
                    loop = true;
                    Tabla.Clear();
                    Estados.Clear();
                    Entradas.Clear();
                    NuevosEstados.Clear();
                    NewStates = 0;
                    NewStatesCompleted = 0;
                    NumOfEstados = 0;
                    NumOfEntradas = 0;
                }
                else if (loopverif == 'N' || loopverif == 'n')
                    loop = false;
            } while (loop);
        }
    }
}
