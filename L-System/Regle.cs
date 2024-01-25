using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree_OpenTK.L_System
{
    internal class Regle
    {
        private char carac;
        private string chaineRemplacement;
        public Regle( char carac, string chaineRemplacement )
        {
            this.carac = carac;
            this.chaineRemplacement = chaineRemplacement;
        }

        public char Carac()
        {
            return this.carac;
        }

        public void Carac(char carac )
        {
            this.carac = carac;
        }

        public string ChaineRemplacement()
        {
            return this.chaineRemplacement;
        }

        public void ChaineRemplacement(string chaineRemplacement)
        {
            this.chaineRemplacement= chaineRemplacement;
        }

        public override string ToString()
        {
            return this.carac + " -> " + this.chaineRemplacement + " \r\n ";
        }
    }
}
