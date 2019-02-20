using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace wiki
{
    static class DomGenerator
    {
        const string fajlKi = "domnev.txt";
        

        public static void General(string weboldal) {
            DOM d = new DOM(weboldal);
            using(StreamWriter sw = new StreamWriter(fajlKi)) {
                sw.Write(d.ToString());
            }
        }
    }

    class DOM
    {
        public enum NodeType { TEXT, REF, NL, NUM, LIST, IMG }
        const char listChar = '*';
        const char numbChar = '#';
        const int listTabLen = 2;
        const int tabLen = 4;
        public class Node
        {
            public NodeType type;
            public string szov;

            public Node(NodeType _type, string _szov) {
                type = _type;
                szov = _szov;
            }
        }

        public List<Node> nodes = new List<Node>();
        
        public DOM(string weboldal) {
            using (StreamReader sr = new StreamReader(weboldal)) {
                int szamSzint = 0;
                int listaSzint = 0;
                List<int> szamozas = new List<int>();
                while (!sr.EndOfStream) {
                    string sor = sr.ReadLine();
                    int kezd, egy, veg;
                    int sorKezd = 0;

                    if (sor.Length > 0 && sor[sorKezd] == numbChar) {
                        while (sor[sorKezd] == numbChar)
                            ++sorKezd;
                        if(szamSzint < sorKezd) {
                            szamozas.Add(1);
                        } else if(szamSzint == sorKezd) {
                            szamozas[szamozas.Count - 1]++;
                        } else {
                            for (int i = 0; i < szamSzint-sorKezd; i++) {
                                szamozas.RemoveAt(szamozas.Count - 1);
                            }
                            szamozas[szamozas.Count - 1]++;
                        }
                        string str = "";
                        for (int i = 0; i < szamozas.Count; i++) {
                            str += szamozas[i]+".";
                        }
                        str += " ";

                        //if(szamSzint != sorKezd)
                            nodes.Add(new Node(NodeType.NUM, str));
                        szamSzint = sorKezd;
                    } else {
                        szamozas.Clear();
                        szamSzint = 0;
                    }

                    if(sor.Length > 0 && sor[sorKezd] == listChar) {
                        while (sor[sorKezd] == listChar)
                            ++sorKezd;
                        if(sorKezd > listaSzint) {
                            nodes.Add(new Node(NodeType.LIST, new string(' ', (sorKezd-1) * listTabLen)+"- "));
                        }
                    }

                    for (int i = sorKezd; i < sor.Length; i++) {
                        if (stringMatch(sor, "[a", i)) {
                            kezd = i;
                            nodes.Add(new Node(NodeType.TEXT, sor.Substring(sorKezd, kezd - sorKezd)));
                            while (sor[i] != '=')
                                ++i;
                            egy = i;
                            while (sor[i] != ']')
                                ++i;
                            veg = i;
                            string fajl = sor.Substring(egy + 1, veg - egy - 1);
                            while (!stringMatch(sor, "[/a]", i))
                                ++i;
                            nodes.Add(new Node(NodeType.REF, sor.Substring(veg + 1, i - veg - 1) + " [" + fajl + "]"));
                            i += 3;
                            sorKezd = i + 1;
                        }
                        if (stringMatch(sor, "[img", i)) {
                            kezd = i;
                            nodes.Add(new Node(NodeType.TEXT, sor.Substring(sorKezd, kezd - sorKezd)));
                            while (sor[i] != '=')
                                ++i;
                            egy = i;
                            while (sor[i] != '/')
                                ++i;
                            veg = i + 1;
                            string fajl = sor.Substring(egy+1, i - egy - 1);
                            ++i;
                            sorKezd = veg + 1;
                            nodes.Add(new Node(NodeType.IMG, fajl));
                        }
                    }
                    if (sorKezd < sor.Length) {
                        nodes.Add(new Node(NodeType.TEXT, sor.Substring(sorKezd, sor.Length - sorKezd)));
                    }
                    nodes.Add(new Node(NodeType.NL, ""));
                }
            }
        }

        /// <summary>
        /// Az "a" string i. helyetol megnezi h egyezik-e "b" string-el
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        private bool stringMatch(string a, string b, int start) {
            if (start < 0 || start+b.Length-1 >= a.Length)
                return false;
            for (int j = 0; j < b.Length; j++) {
                if (a[start + j] != b[j])
                    return false;
            }
            return true;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();

            sb.Append("page\n");
            int tab = 0;
            foreach (Node n in nodes) {
                sb.Append('|');
                for (int i = 0; i < tab; i++) {
                    sb.Append(new string(' ', tabLen));
                    sb.Append('|');
                }
                sb.Append('\n');

                sb.Append('|');
                for (int i = 0; i < tab; i++) {
                    sb.Append(new string(' ', tabLen));
                    sb.Append('|');
                }
                sb.Append('-');
                switch (n.type) {
                    case NodeType.TEXT:
                        sb.Append("text: ");
                        sb.AppendLine(n.szov);
                        break;
                    case NodeType.REF:
                        sb.Append("a ref: ");
                        sb.AppendLine(n.szov);
                        break;
                    case NodeType.NL:
                        sb.AppendLine("newline");
                        break;
                    case NodeType.NUM:
                        sb.AppendLine("numbered");
                        tab = n.szov.Count((x) => x == '.');
                        break;
                    case NodeType.LIST:
                        sb.AppendLine("list");
                        tab = n.szov.Length / 2;
                        tab++;
                        break;
                    case NodeType.IMG:
                        sb.Append("image: [");
                        sb.Append(n.szov);
                        sb.AppendLine("]");
                        break;
                    default:
                        break;
                }
            }

            sb.Append('|');
            for (int i = 0; i < tab; i++) {
                sb.Append(new string(' ', tabLen));
                sb.Append('|');
            }
            sb.Append('\n');
            sb.AppendLine("end page");

            return sb.ToString();
        }
    }
}
