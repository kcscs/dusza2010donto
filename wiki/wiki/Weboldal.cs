using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace wiki
{
    class Weboldal
    {
       public List<Oldal> oldalak;
       public List<string> linkek;

        Weboldal(string fajl)
        {
            List<Oldalelem> oldalelemek = new List<Oldalelem>();
            using (StreamReader sr = new StreamReader(fajl))
            {
                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    Oldalelem oldalelem = new Oldalelem();
                    oldalelem.szoveg = sor;
                    int indexkep = 0;
                    int indexref = 0;
                    /*
                    if (sor.Contains("[img") == true)
                    {
                        for (int j = 0; j < oldalelemek[i].szoveg.Length; j++)
                        {
                            if (oldalelemek[i].szoveg[j - 1] == '[' && oldalelemek[i].szoveg[j] == 'i')
                            {
                                index = j-1;
                                break;
                            }
                        }

                        for (int j = 0; j < oldalelemek[i].szoveg.Length; j++)
                        {
                            if (oldalelemek[i].szoveg[j] == ']')
                            {
                                hossz = j;
                                break;
                            }
                        } 
                        oldalelemek[i].szoveg.Substring(index, oldalelemek[i].szoveg.Length - hossz);
                    }
                    oldalelemek.Add(oldalelem); */

                while(true)
                    {   
                        indexkep = sor.IndexOf("[img");
                        indexref = sor.IndexOf("[ref");
                        if(indexkep>indexref)
                        {

                        }
                        
                    }
                }
            }


           /* for (int i = 0; i < oldalelemek.Count; i++)
            {
                if (oldalelemek[i].szoveg.Contains("[a ref") == true)
                {
                    for (int j = 0; j < oldalelemek[i].szoveg.Length; j++)
                    {
                        if (oldalelemek[i].szoveg[j - 1] == '[' && oldalelemek[i].szoveg[j] == 'a')
                        {
                            index = j;
                            break;
                        }
                    }

                    for (int j = 0; j < oldalelemek[i].szoveg.Length; j++)
                    {
                        if (oldalelemek[i].szoveg[j] == ']')
                        {
                            hossz = j;
                            break;
                        }
                    }
                    oldalelemek[i].szoveg.Substring(index, oldalelemek[i].szoveg.Length - hossz);
                } */

                
            }
        }

    }
}
