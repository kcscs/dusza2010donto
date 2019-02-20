using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace wiki
{
    class Program
    {
        static void Main(string[] args) {
            Console.SetWindowSize(80, 22);
            string nev = "kkzs";
            string parancs;

            Weboldal woldal = null;


            if (File.Exists("index.txt")) {
                woldal = new Weboldal("index.txt");
            }
            do {
                Console.Clear();
                Console.WriteLine(nev);
                if (woldal == null) {


                } else {
                    for (int i = 0; i < woldal.oldalak.Count; i++) {
                        for (int j = 0; j < woldal.oldalak[i].oldalelemek.Count; j++) {
                            Console.WriteLine(woldal.oldalak[i].oldalelemek[j].ToString());
                        }
                        Console.CursorTop = 21;
                        if (i != woldal.oldalak.Count - 1) {
                            Console.Write("Az oldal nem ért véget, nyomjon meg egy gombot a folytatáshoz.");
                            Console.ReadKey();
                        }
                    }
                }

                Console.CursorTop = 21;





                parancs = Console.ReadLine();
                switch (parancs[0]) {
                    case 'S':
                        woldal = new Weboldal(parancs.Substring(1) + ".txt");//megnyitja a megadott weboldalt
                        break;
                    case 'L':
                        int n;
                        n = int.Parse(parancs.Substring(1)) - 1;
                        woldal = new Weboldal(woldal.linkek[n]);//megnyitja a jelenlegi lap n. linkjét
                        break;
                    case 'D':
                        DomGenerator.General(woldal.fajlnev);//generál egy Dom fát
                        break;

                }
                //Console.CursorTop--;
            } while (parancs != "q");
        }
    }
}