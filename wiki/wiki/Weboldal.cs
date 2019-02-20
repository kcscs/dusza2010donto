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

        Weboldal(string fajl) {
            List<Oldalelem> oldalelemek = new List<Oldalelem>();
            using (StreamReader sr = new StreamReader(fajl)) {
                while (!sr.EndOfStream) {
                    string sor = sr.ReadLine();
                    Oldalelem oldalelem = new Oldalelem();
                    oldalelem.szoveg = sor;
                    int indexkep = 0;
                    int indexref = 0;
                    int indexVeg = 0;
                    int indexEgyenlo = 0;
                    int indexRefVeg = 0;
                    string utolsoLink = "";
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

                    while (true) {
                        indexkep = sor.IndexOf("[img");
                        indexref = sor.IndexOf("[ref");
                        indexRefVeg = sor.IndexOf("[/a]");

                        if (indexref == indexkep && indexkep == -1)
                            break;

                        if (indexref < 0)
                            indexref = int.MaxValue;
                        if (indexkep < 0)
                            indexkep = int.MaxValue;
                        if (indexRefVeg < 0)
                            indexRefVeg = int.MaxValue;

                        if (indexref < indexkep) {
                            if (indexRefVeg < indexref) {
                                sor = sor.Replace("[/a]", "L" + (linkek.Count + 1).ToString());
                            } else {
                                indexEgyenlo = indexref;
                                while (sor[indexEgyenlo] != '=')
                                    ++indexEgyenlo;
                                indexVeg = indexref;
                                while (sor[indexVeg] != ']')
                                    ++indexVeg;
                                utolsoLink = sor.Substring(indexEgyenlo + 1, indexVeg - indexref - 1);
                                linkek.Add(utolsoLink);
                                sor = sor.Remove(indexref, indexVeg - indexref + 1);
                            }
                        } else {
                            if (indexRefVeg < indexkep) {
                                sor = sor.Replace("[/a]", "L" + (linkek.Count + 1).ToString());
                            } else {
                                indexEgyenlo = indexkep;
                                while (sor[indexEgyenlo] != '=')
                                    ++indexEgyenlo;
                                indexVeg = indexkep;
                                while (sor[indexVeg] != ']')
                                    ++indexVeg;

                                if (indexkep > 0) {
                                    Oldalelem o = new Oldalelem();
                                    o.szoveg = sor.Substring(0, indexkep);
                                    oldalelemek.Add(o);
                                }

                                Abra abra = new Abra(sor.Substring(indexEgyenlo + 1, indexVeg - indexEgyenlo - 1));
                                oldalelemek.Add(abra);
                                sor = sor.Remove(0, indexVeg + 1);
                            }
                        }

                    }
                    if (sor.Length > 0) {
                        Oldalelem o = new Oldalelem();
                        o.szoveg = sor;
                        oldalelemek.Add(o);
                    }
                }
            }

            Oldal old = new Oldal();
            old.oldalelemek = oldalelemek;
            oldalak.Add(old);
        }
    }

}

