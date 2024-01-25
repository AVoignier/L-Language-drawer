using GrilleCollision.GrilleCollision;
using QuadTree_OpenTK.Affichage.Modeles;
using QuadTree_OpenTK.GrilleCollision.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrilleCollision
{
    internal abstract class Case
    {
        protected Case? caseParent;

        protected Vec2 position;

        protected float largeur;

        protected float hauteur;

        protected int profondeur;

        protected List<IItem> items;

        protected const int PROFONDEUR_MAX = 10;


        public Case( Vec2 position, float hauteur, float largeur, Case? parent)
        {

            this.position = position;
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.caseParent = parent;

            if (parent == null)
                profondeur = 0;
            else
            {
                profondeur = parent.Profondeur() + 1;
            }
        }

        public abstract Case AjouterItem(IItem item);

        public Vec2 Position()
        { return position; }
        public float Largeur() 
        { return largeur; }
        public float Hauteur()
        { return hauteur; }
        public int Profondeur()
        { return profondeur; }

        public Vec2[] getForme()
        {
            Vec2[] points = new Vec2[4];

            points[0] = new Vec2(position.X(),           position.Y());
            points[1] = new Vec2(position.X() + largeur, position.Y());
            points[2] = new Vec2(position.X() + largeur, position.Y() + hauteur);
            points[3] = new Vec2(position.X()          , position.Y() + hauteur);

            return points;
        }

        public abstract List<Vec2[] > getAllFormes();

        public override string ToString()
        {
            string texte = "position = " + position.ToString() + " hauteur = " + hauteur + " largeur = " + largeur + " profondeur = " + profondeur;

            return texte;
        }

        public abstract List<Modele?> getAllModeles();

    }
}
