using GrilleCollision;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree_OpenTK.Affichage.Modeles
{
    internal interface Modele : IDisposable
    {
        public void Dessiner();

        public static Vec2 ConversionCoordonnéeAffichage(Vec2 point)
        {
            Vec2 nouveauPoint = new Vec2((point.X() - (Game.largeurEcran/2)) / Game.largeurEcran*2, (point.Y() - (Game.hauteurEcran/2)) / Game.hauteurEcran*2);

            return nouveauPoint;
        }

        public static Vec2[] ConversionCoordonnéesAffichage(Vec2[] points )
        {
            Vec2[] nouveauPoints = new Vec2[points.Length];

            int compt = 0;
            foreach( Vec2 point in points )
            {
                nouveauPoints[compt] = ConversionCoordonnéeAffichage(point);
                compt++;
            }

            return nouveauPoints;

        }

        public static Shader.ShaderFeuille GetShader()
        {
            return null;
        }

    }
}
