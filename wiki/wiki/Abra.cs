using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace wiki
{
    class Abra : Oldalelem
    {
        readonly char[] splitChar = new char[] { ' ' };
        private string str;
        public int szelesseg, magassag;

        public Abra(string fajl) {
            List<byte> szamok = new List<byte>();
            using (StreamReader sr = new StreamReader(fajl)) {
                string sor = sr.ReadLine();
                string[] darab = sor.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
                magassag = int.Parse(darab[0]);
                szelesseg = int.Parse(darab[1]);
                
                for (int i = 2; i < darab.Length; i++) {
                    szamok.Add(byte.Parse(darab[i]));
                }
                while (!sr.EndOfStream) {
                    sor = sr.ReadLine();
                    darab = sor.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < darab.Length; i++) {
                        szamok.Add(byte.Parse(darab[i]));
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            int db = 0;
            int hossz = szelesseg * magassag;
            for (int i = 0; i < szamok.Count; i++) {
                for (int b = 7; b >= 0 && hossz > 0; b--) {
                    if(((1 << b)&szamok[i]) > 0) {
                        sb.Append('*');
                    } else {
                        sb.Append(' ');
                    }
                    ++db;
                    --hossz;
                    if (db == szelesseg && hossz != 0) {
                        db = 0;
                        sb.Append('\n');
                    }
                }
            }

            str = sb.ToString();
        }

        public override string ToString() {
            return str;
        }
    }
}
