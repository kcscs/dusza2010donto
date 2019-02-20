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
        public List<Oldal> oldalak = new List<Oldal>();
        public List<string> linkek = new List<string>();
        public string fajlnev;
        public DOM dom;

        public Weboldal(string fajl) {
            fajlnev = fajl;
            dom = new DOM(fajl);
            List<Oldalelem> oldalelemek = new List<Oldalelem>();

            StringBuilder sb = new StringBuilder();
            string tabPrefix = "";
            foreach (DOM.Node n in dom.nodes) {
                Oldalelem o;
                switch (n.type) {
                    case DOM.NodeType.TEXT:
                        sb.Append(n.szov);
                        break;
                    case DOM.NodeType.REF:
                        int veg = n.szov.LastIndexOf(" [");
                        linkek.Add(n.szov.Substring(veg + 2, n.szov.Length - veg - 3));
                        sb.Append(n.szov.Substring(0, veg) + "[L"+linkek.Count+"]");
                        break;
                    case DOM.NodeType.NL:
                        o = new Oldalelem();
                        sb.Insert(0, tabPrefix);
                        o.szoveg = sb.ToString();
                        sb.Clear();
                        oldalelemek.Add(o);
                        tabPrefix = "";
                        break;
                    case DOM.NodeType.NUM:
                        tabPrefix = n.szov;
                        break;
                    case DOM.NodeType.LIST:
                        tabPrefix = n.szov;
                        break;
                    case DOM.NodeType.IMG:
                        o = new Oldalelem();
                        sb.Insert(0, tabPrefix);
                        o.szoveg = sb.ToString();
                        sb.Clear();
                        oldalelemek.Add(o);
                        tabPrefix = "";
                        oldalelemek.Add(new Abra(n.szov.Substring(0, n.szov.Length)));
                        break;
                    default:
                        break;
                }
            }

            /*using (StreamReader sr = new StreamReader(fajl)) {  //Fájlok beolvasása

                fajlnev = fajl;
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
                    /*if (sor.Length > 0) {
                        if (sor[0] == '*')    // Ha a sor csillaggal kezdődik
                        {
                            StringBuilder strB = new StringBuilder(sor);
                            strB[0] = '-'; // Az első elem legyen -
                            while (sor.Contains("*")) // Ameddig van benne *, addig cserélje őket 2 szóközre
                            {
                                strB.Replace("*", "  ");
                                sor = strB.ToString();
                            }
                        }

                        int kulso = 0;
                        int belso = 0;
                        int legbelsobb = 0;
                        String s = "";

                        if (sor[0] == '#') // Ha a sor #-el kezdődik
                        {
                            kulso++;
                            StringBuilder strB = new StringBuilder(sor);
                            for (int i = 0; i < sor.LastIndexOf('#'); i++)  // # Számaszor fusson le
                            {
                                belso = 0;
                                strB[i] = kulso.ToString()[0];
                                if (sor.LastIndexOf('#') == 1) // Belső index 
                                {
                                    belso++;
                                    s = new StringBuilder().Append(strB[i]).Append(".").Append(belso).Append(".").ToString();
                                    legbelsobb = 0;
                                }

                                if (sor.LastIndexOf('#') == 2) // Legbelsőbb index 
                                {
                                    legbelsobb++;
                                    s = new StringBuilder().Append(strB[i]).Append(".").Append(belso).Append(".").Append(legbelsobb).Append(".").ToString();
                                }

                            }

                            sor.Remove(0, sor.LastIndexOf('#')); // Sorok átalakítása
                            sor = s + strB.ToString();

                        }
                    } else {
                        Oldalelem o = new Oldalelem();
                        o.szoveg = "";
                        oldalelemek.Add(o);
                    }


                    


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
            }*/

            Oldal old = new Oldal();
            old.oldalelemek = oldalelemek;
            oldalak.Add(old);
        }
    }

}

