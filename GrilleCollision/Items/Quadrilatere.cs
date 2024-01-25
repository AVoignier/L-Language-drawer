using GrilleCollision;
using QuadTree_OpenTK.Affichage.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree_OpenTK.GrilleCollision.Items
{
    internal class Quadrilatere : IItem
    {
        Vec2[] points;
        Vec2 p_position;
        Modele? p_modele;

        public Quadrilatere(Vec2[] points, bool genererModele)
        {
            this.points = points;
            p_position = position;

            // Calcul de la position
            float sumX = 0;
            float sumY = 0;
            foreach (Vec2 point in points)
            {
                sumX += point.X();
                sumY += point.Y();
            }
            p_position = new Vec2(sumX / 4, sumY / 4);

            if(genererModele)
            {
                GenererModele();
            }

        }

        public Vec2 position 
        { 
            get => p_position;
            set
            {
                foreach (Vec2 point in points)
                {
                    point.X(point.X() - p_position.X() + value.X());
                    point.Y(point.Y() - p_position.Y() + value.Y());
                }
            }
        }
        public Modele? modele
        {
            get => p_modele; 
            set => p_modele = value;
        }

        public void GenererModele()
        {
            modele = new QuadrilatereModele(points);
        }

        public Vec2 PointPlusADroite()
        {
            float valMaxX = points[0].X();
            Vec2 pointMaxX = points[0];

            for (int i = 1; i < 4; i++)
            {
                if (points[i].X() > valMaxX)
                {
                    valMaxX = points[i].X();
                    pointMaxX = points[i];
                }
            }

            return pointMaxX;

        }

        public Vec2 PointPlusAGauche()
        {
            float valMinX = points[0].X();
            Vec2 pointMinX = points[0];

            for (int i = 1; i < 4; i++)
            {
                if (points[i].X() < valMinX)
                {
                    valMinX = points[i].X();
                    pointMinX = points[i];
                }
            }

            return pointMinX;
        }

        public Vec2 PointPlusBas()
        {
            float valMinY = points[0].Y();
            Vec2 pointMinY = points[0];

            for (int i = 1; i < 4; i++)
            {
                if (points[i].Y() < valMinY)
                {
                    valMinY = points[i].Y();
                    pointMinY = points[i];
                }
            }

            return pointMinY;
        }

        public Vec2 PointPlusHaut()
        {
            float valMaxY = points[0].Y();
            Vec2 pointMaxY = points[0];

            for (int i = 1; i < 4; i++)
            {
                if (points[i].Y() > valMaxY)
                {
                    valMaxY = points[i].Y();
                    pointMaxY = points[i];
                }
            }

            return pointMaxY;
        }
    }
}
