using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrilleCollision;
using QuadTree_OpenTK.Affichage.Modeles;
using QuadTree_OpenTK.GrilleCollision.Items;

namespace QuadTree_OpenTK.GrilleCollision.Items
{
    internal class Point : IItem
    {
        protected Vec2 position;

        Vec2 IItem.position 
        { 
            get => position; 
            set => position = value; 
        }

        public Modele? modele 
        { 
            get => modele; 
            set => modele = value; 
        }

        public Point(Vec2 position, Modele? modele)
        {
            this.position = position;
            this.modele = modele;
        }

        public override string ToString()
        {
            return position.ToString();
        }

        public Vec2 Position()
        { return position; }
        public void Position(Vec2 position)
        { this.position = position; }

        public void GenererModele()
        {
            throw new NotImplementedException();
        }
        public Vec2 PointPlusHaut()
        {
            return this.position;
        }

        public Vec2 PointPlusBas()
        {
            return this.position;
        }

        public Vec2 PointPlusADroite()
        {
            return this.position;
        }

        public Vec2 PointPlusAGauche()
        {
            return this.position;
        }

        public void Dessiner()
        {
            throw new NotImplementedException();
        }

    }
}
