using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrilleCollision;
using QuadTree_OpenTK.Affichage.Modeles;

namespace QuadTree_OpenTK.GrilleCollision.Items
{
    internal interface IItem
    {
        Vec2 position
        {
            get;
            set;
        }

        Modele? modele
        {
            get;
            set;
        }

        public Vec2 Position() { return position; }
        public void Position(Vec2 position) { this.position = position; }
        public void GenererModele();
        public Vec2 PointPlusHaut();
        public Vec2 PointPlusBas();
        public Vec2 PointPlusADroite();
        public Vec2 PointPlusAGauche();


    }
}
