using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree_OpenTK.L_System
{
    internal class L_System
    {
        string axiome;
        Dictionary<char, Regle> regleDict;

        public L_System()
        {
            axiome = "";

            regleDict = new Dictionary<char, Regle>();
        }

        public L_System(string phraseDepart, List<Regle> regles  )
        {
            this.axiome = phraseDepart;
            regleDict= new Dictionary<char, Regle>();
            
            foreach(Regle regle in regles)
            {
                this.AjouterRegle(regle);
            }
        }

        public void AjouterRegle( Regle regle )
        {
            regleDict.Add(regle.Carac(), regle);
        }

        public void AjouterRegle( char carac, string chaineRemplacement )
        {
            Regle regle = new Regle(carac, chaineRemplacement);
            AjouterRegle(regle);
        }

        public void NouvelleGeneration()
        {
            string nouvellePhrase = "";

            foreach( char c in this.axiome )
            {
                if( regleDict.ContainsKey(c) )
                {
                    nouvellePhrase += regleDict[c].ChaineRemplacement();
                }
                else
                {
                    nouvellePhrase += c;
                }
            }

            this.axiome = nouvellePhrase;
        }

        public string Axiome()
        {
            return axiome;
        }

        public void Axiome(string axiome)
        {
            this.axiome = axiome;
        }

        public override string ToString()
        {
            string stringReturn = "Axiome : " + this.axiome + " \r\n ";

            foreach( KeyValuePair<char,Regle> k in regleDict)
            {
                stringReturn += k.Value.ToString();
            }

            return stringReturn;

        }

    }
}
