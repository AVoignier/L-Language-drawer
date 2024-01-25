using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using QuadTree_OpenTK;
using QuadTree_OpenTK.GrilleCollision.Items;
using GrilleCollision;
using QuadTree_OpenTK.Affichage.Modeles;
using QuadTree_OpenTK.Affichage;
using QuadTree_OpenTK.L_System;

namespace QuadTree_OpenTK
{
    internal class Game : GameWindow
    {
        GestionnaireAffichage afficheur;
        GrilleCollision.GrilleCollision grilleCollision;

        private bool AfficherGrilleCollion = true;

        public const int largeurEcran = 800;
        public const int hauteurEcran = 800;

        public Game() : base( GameWindowSettings.Default, NativeWindowSettings.Default ) 
        {
            this.CenterWindow( new Vector2i(largeurEcran,hauteurEcran) );
        }

        protected override void OnLoad()
        {
            GL.ClearColor(new Color4(0.3f, 0.4f, 0.5f, 1f));

            //Création de l'afficheur
            afficheur = GestionnaireAffichage.getInstance();

            //Création du gestionnaire de collision
            GrilleCollision.GrilleCollision.SetupGrilleCollision(largeurEcran,hauteurEcran);
            grilleCollision = GrilleCollision.GrilleCollision.getInstance();

            // Création Quadrilateres

            /*for(int i=0; i<100; i++)
            {
                Vec2[] pointsSegment = new Vec2[2];

                Random rand = new Random();

                Vec2 p1 = new Vec2(rand.Next(800), rand.Next(800));
                Vec2 p2 = new Vec2( p1.X() + rand.Next(-10,10), p1.Y() + rand.Next(-10, 10));

                pointsSegment[0] = new Vec2(p1.X(), p1.Y());
                pointsSegment[1] = new Vec2(p2.X(), p2.Y());

                Segment segment = new Segment(pointsSegment, true);
                afficheur.AjouterModele(segment.modele);

                grilleCollision.AjouterItem(segment);
            }
            */

            int nbgénérétion = 5;

            L_System.L_System l_system = new L_System.L_System();
            l_system.Axiome("X");
            l_system.AjouterRegle('A', "AA");
            l_system.AjouterRegle('X', "A-[[X]+X]+A[+AXF]-X");

            Console.WriteLine(l_system.ToString());

            Tortue tortue = new Tortue(new Vec2(400, 0), 0, 600, 30f, l_system);

            for(int i=0; i<nbgénérétion; i++)
            {
                tortue.NouvelleGénération();
            }

            tortue.Vitesse(tortue.Vitesse() / (float)Math.Pow(2, nbgénérétion+1));
            grilleCollision.AjouterItems(tortue.GénérerItems());
            afficheur.AjouterModeles(grilleCollision.getAllModele());

            Console.WriteLine( tortue.L_system().Axiome() );

            if (AfficherGrilleCollion)
            {
                List<Vec2[]> pointsGrille = new List<Vec2[]>();
                pointsGrille = grilleCollision.getAllFormes();

                ContourModele contour;

                foreach(Vec2[] pointsContour in pointsGrille)
                {
                    contour = new ContourModele(pointsContour);

                    afficheur.AjouterModele(contour);
                }

            }

            // Console.WriteLine( grilleCollision.ToString() ); 

            base.OnLoad();
        }

        protected override void OnUnload()
        {
            Console.WriteLine("Unload");
            afficheur.Dispose();

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0,0,e.Width,e.Height);

            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            
            GL.Clear(ClearBufferMask.ColorBufferBit);

            afficheur.Dessiner();

            this.Context.SwapBuffers();

            base.OnRenderFrame(args);
        }


    }
}
