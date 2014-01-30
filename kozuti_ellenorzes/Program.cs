using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace kozuti_ellenorzes
{
    class Program
    {
        static void Main(string[] args)
        {
            #region beolvas
            Console.WriteLine("1. feladat:");
            string[][] adattomb = new string[1000][];
            StreamReader be = new StreamReader("jarmu.txt");
            int iterator = 0;
            while (!be.EndOfStream)
            {
                string beolvasott = be.ReadLine();
                adattomb[iterator] = beolvasott.Split(' ');
                iterator++;
            }
            be.Close();
            Console.WriteLine("beolvasva, összesen {0} elem", iterator);
            #endregion
            #region óraszám
            Console.WriteLine("2. feladat:");
            int ora1 = Convert.ToInt16(adattomb[0][0]);
            int ora2 = Convert.ToInt16(adattomb[iterator-1][0]);

            if (adattomb[0][1] != "00")
                ora2++;
            Console.WriteLine("{0}", ora2-ora1);
            #endregion
            #region elsőautó
            Console.WriteLine("3. feladat:");
            int ora = -1;
            for (int i = 0; i < iterator; i++)
            {
                int mostora = Convert.ToInt16(adattomb[i][0]);
                if(ora != mostora)
                {
                    Console.WriteLine("{0} óra: {1}", mostora, adattomb[i][3]);
                    ora = mostora;
                }
            }
            #endregion
            #region típusszám
            Console.WriteLine("4. feladat:");
            int busz = 0;
            int kamion = 0;
            int motor = 0;
            for (int i = 0; i < iterator; i++)
            {
                string mostrendszam = adattomb[i][3];
                switch (mostrendszam[0])
                { 
                    case 'K':
                        kamion++;
                        break;
                    case 'B':
                        busz++;
                        break;
                    case 'M':
                        motor++;
                        break;
                }
            }
            Console.WriteLine("Busz: {0} db\nKamion: {1} db\nMotor: {2} db", busz, kamion, motor);
            #endregion
            #region forgalommentes
            Console.WriteLine("5. feladat:");
            int maxido = 0;
            int maxhely = 0;
            for (int i = 1; i < iterator; i++)
            {
                ora1 = Convert.ToInt16(adattomb[i - 1][0]) * 3600 + Convert.ToInt16(adattomb[i - 1][1]) * 60 + Convert.ToInt16(adattomb[i - 1][2]);
                ora2 = Convert.ToInt16(adattomb[i][0]) * 3600 + Convert.ToInt16(adattomb[i][1]) * 60 + Convert.ToInt16(adattomb[i][2]);
                if (maxido < (ora2 - 1))
                {
                    maxhely = i;
                }
            }
            Console.WriteLine("{0}:{1}:{2} - {3}:{4}:{5}", Convert.ToInt16(adattomb[maxhely - 1][0]), Convert.ToInt16(adattomb[maxhely - 1][1]), Convert.ToInt16(adattomb[maxhely - 1][2]),
                                                           Convert.ToInt16(adattomb[maxhely][0]), Convert.ToInt16(adattomb[maxhely][1]), Convert.ToInt16(adattomb[maxhely][2]));
            #endregion
            #region kivalasztos
            Console.WriteLine("6. feladat:");
            string rendszam = Console.ReadLine();
            for (int i = 0; i < iterator; i++)
            {
                if ((rendszam == "") || (rendszam.Length != 7))    
                {
                    Console.WriteLine("Nem megfelelő formátum.");
                    break;
                }
                bool jo = true;
                for (int j = 0; j < 7; j++)
                {
                    string mostanirendszam = adattomb[i][3];
                    if (rendszam[j] != '*' && rendszam[j] != mostanirendszam[j])
                        jo = false;
                }
                if (jo)
                    Console.WriteLine(adattomb[i][3]);
            }
            #endregion
            #region ellenorzes
            Console.WriteLine("7. feladat:");
            int[] ellenorzottek = new int[iterator];
            ellenorzottek[0] = 0;
            int auto_kezd = 0;
            bool ellenorzott = false;
            int ora_kezd = 0;
            int ora_veg = 0;
            int iterator2 = 1;
            for (int i = 1; i < iterator; i++)
            {
                if (!ellenorzott)
                {
                    ora_kezd = Convert.ToInt16(adattomb[auto_kezd][0]) * 3600 + Convert.ToInt16(adattomb[auto_kezd][1]) * 60 + Convert.ToInt16(adattomb[auto_kezd][2]);
                    ellenorzott = true;
                }
                ora_veg = Convert.ToInt16(adattomb[i][0]) * 3600 + Convert.ToInt16(adattomb[i][1]) * 60 + Convert.ToInt16(adattomb[i][2]);
                if((ora_kezd + 300) <= ora_veg)
                {
                    ellenorzottek[iterator2] = i;
                    auto_kezd = i;
                    ellenorzott = false;
                    iterator2++;
                }
            }
            StreamWriter kiiro = new StreamWriter("vizsgalt.txt");
            for (int i = 0; i < iterator2; i++)
            {

                kiiro.WriteLine("{0} {1} {2} {3}", adattomb[ellenorzottek[i]][0], adattomb[ellenorzottek[i]][1], adattomb[ellenorzottek[i]][2], adattomb[ellenorzottek[i]][3]);
            }
            kiiro.Close();
            #endregion
            Console.ReadLine();

        }
    }
}
