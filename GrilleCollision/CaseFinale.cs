using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuadTree_OpenTK.Affichage.Modeles;
using QuadTree_OpenTK.GrilleCollision.Items;

namespace GrilleCollision.GrilleCollision
{
    internal class CaseFinale : Case
    {

        public CaseFinale(Vec2 position, float largeur, float hauteur, Case parent) : base(position, largeur, hauteur, parent)
        {
            items = new List<IItem>();
        }

        public override Case AjouterItem(IItem item)
        {
            items.Add(item);
            return this;
        }

        public override List<Vec2[]> getAllFormes()
        {
            List<Vec2[] > points = new List< Vec2[]>();

            points.Add(this.getForme());

            return points;
        }

        public override string ToString()
        {
            string texte = "";

            for (int i = 0; i < profondeur; i++)
            {
                texte += " ";
            }

            return texte + "Case Finale " + base.ToString() + " \r\n ";
        }

        public override List<Modele?> getAllModeles()
        {
            List<Modele?> modeles = new List<Modele?>();

            foreach(IItem item in items)
            {
                if(item.modele != null)
                    modeles.Add(item.modele);
            }

            return modeles;

        }

    }
}
