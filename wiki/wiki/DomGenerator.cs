using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 

namespace wiki
{
    static class DomGenerator
    {
        const string fajlKi = "domnev.txt";

        public static void General(string weboldal) {

        }
    }

    class DOM
    {
        public enum NodeType { TEXT, REF, NL, NUM, LIST, IMG }
        public class Node
        {
            public NodeType type;
            public string szov;
        }
    }
}
