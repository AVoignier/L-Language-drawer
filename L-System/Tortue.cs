using GrilleCollision;
using QuadTree_OpenTK.Affichage.Modeles;
using QuadTree_OpenTK.GrilleCollision.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree_OpenTK.L_System
{
    internal class Tortue
    {
        private Vec2 position;
        private float directionRad;

        private List<Vec2> positionSave;
        private List<float> directionSave;

        private float vitesse;
        private float angleRotationRad;

        private Dictionary<char, ActionDel> actionsDict;

        private L_System l_system;

        private bool genererModele;

        private float tailleFeuille;

        //private Dictionary<char, delegate > actionsDict;

        /* ordre :
         * A -> Avancer
         * B -> Reculer
         * + -> TournerADroite
         * - -> TournerAGauche
         * [ -> SauvegarderPosition
         * ] -> ChargerDernierePosition
        */

        public Tortue( Vec2 position, float directionDeg, float vitesse , float angleRotationDeg, L_System l_system)
        {
            this.position = position;
            this.directionRad = 1.62f + directionDeg/-180*(float)Math.PI;

            positionSave = new List<Vec2>();
            directionSave = new List<float>();

            this.vitesse = vitesse;
            this.angleRotationRad = angleRotationDeg/-180*(float)Math.PI;
            this.l_system = l_system;

            genererModele = true;

            tailleFeuille = vitesse / 50;

            //Initialisation actionsDict
            actionsDict = new Dictionary<char, ActionDel>();
            actionsDict.Add('A', Avancer);
            actionsDict.Add('B', Reculer);
            actionsDict.Add('-', TournerAGauche);
            actionsDict.Add('+', TournerADroite);
            actionsDict.Add('[', Sauvegarder);
            actionsDict.Add(']', Charger);
            actionsDict.Add('F', CréerFeuille);

        }

        private delegate IItem? ActionDel();

        private IItem? Avancer()
        {
            Vec2[] points = new Vec2[2];
            points[0] = position;

            float X = position.X() + (float)Math.Cos(this.directionRad) * this.vitesse;
            float Y = position.Y() + (float)Math.Sin(this.directionRad) * this.vitesse;

            this.position = new Vec2(X, Y);

            points[1] = position;

            if (genererModele)
            {
                return new Segment(points,true);
            }

            return null;

        }

        private IItem? Reculer()
        {
            Vec2[] points = new Vec2[2];
            points[0] = position;

            float X = position.X() - (float)Math.Cos(directionRad) * vitesse;
            float Y = position.Y() - (float)Math.Sin(directionRad) * vitesse;

            this.position = new Vec2(X, Y);

            points[1] = position;

            if (genererModele)
            {
                return new Segment(points, true);
            }

            return null;
        }

        private IItem? TournerADroite()
        {
            this.directionRad += this.angleRotationRad;

            return null;
        }

        private IItem? TournerAGauche()
        {
            this.directionRad -= this.angleRotationRad;

            return null;

        }

        private IItem? Sauvegarder()
        {
            positionSave.Add(this.position);
            directionSave.Add(this.directionRad);

            return null;
        }

        private IItem? Charger()
        {
            this.position = positionSave.Last();
            this.directionRad = directionSave.Last();

            positionSave.RemoveAt(positionSave.Count - 1);
            directionSave.RemoveAt(directionSave.Count - 1);

            return null;
        }

        private IItem? CréerFeuille()
        {
            if (genererModele)
            {
                Vec2[] points = new Vec2[3];

                points[0] = (this.position + new Vec2(0, tailleFeuille).RotateRad(directionRad));
                points[1] = (this.position + new Vec2(tailleFeuille / 2, 0).RotateRad(directionRad));
                points[2] = (this.position + new Vec2(-tailleFeuille / 2, 0).RotateRad(directionRad));



                return new Triangle(points, true);
            }

            return null;
        }

        public void NouvelleGénération()
        {
            this.l_system.NouvelleGeneration();
        }

        public List<IItem> GénérerItems()
        {
            string phrase = l_system.Axiome();
            List<IItem> items = new List<IItem>();


            foreach(char c in phrase)
            {
                if( actionsDict.ContainsKey(c))
                {
                    items.Add(actionsDict[c]());
                }
            }

            return items;
        }

        public float Vitesse()
        {
            return this.vitesse;
        }

        public void Vitesse(float vitesse)
        {
            this.vitesse = vitesse;
            this.tailleFeuille = vitesse * 2;
        }

        public L_System L_system()
        {
            return this.l_system;
        }



    }
}
