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

using QuadTree_OpenTK.Affichage.Modeles;
using QuadTree_OpenTK.Affichage.Shader;

namespace QuadTree_OpenTK.Affichage
{
    internal class GestionnaireAffichage : IDisposable
    {
        List<TriangleModele> triangles;
        List<QuadrilatereModele> quadrilateres;
        List<ContourModele> contours;
        List<SegmentModele> segments;

        private static GestionnaireAffichage instance;

        public static GestionnaireAffichage getInstance()
        {
            if( instance == null)
            {
                instance = new GestionnaireAffichage();
            }

            return instance;
        }

        private GestionnaireAffichage()
        {
            triangles = new List<TriangleModele>();
            quadrilateres = new List<QuadrilatereModele>();
            contours = new List<ContourModele>();
            segments = new List<SegmentModele>();
        }

        public bool AjouterModele(Modele modele)
        {
            if( modele is TriangleModele)
            {
                triangles.Add( (TriangleModele)modele );
                return true;
            }
            else if(modele is QuadrilatereModele)
            {
                quadrilateres.Add((QuadrilatereModele)modele);
                return true;
            }
            else if( modele is ContourModele)
            {
                contours.Add((ContourModele)modele);
            }
            else if( modele is SegmentModele)
            {
                segments.Add((SegmentModele)modele);
            }

            return false;
        }

        public List<bool> AjouterModeles( List<Modele> modeles)
        {
            List<bool> result = new List<bool>();

            foreach( Modele modele in modeles)
            {
                result.Add( this.AjouterModele(modele));
            }

            return result;
        }

        public void Dessiner()
        {
            if(triangles.Count > 0)
                GL.UseProgram(TriangleModele.GetShader().getShaderProgramHandle());
            foreach (TriangleModele triangle in triangles)
            {
                triangle.Dessiner();
            }

            if(quadrilateres.Count > 0)
                GL.UseProgram(QuadrilatereModele.GetShader().getShaderProgramHandle());
            foreach (QuadrilatereModele quadrilatere in quadrilateres)
            {
                quadrilatere.Dessiner();
            }

            if(contours.Count > 0)
                GL.UseProgram(ContourModele.GetShader().getShaderProgramHandle());
            foreach (ContourModele contour in contours)
            {
                contour.Dessiner();
            }

            if(segments.Count > 0)
                GL.UseProgram(SegmentModele.GetShader().getShaderProgramHandle());
            foreach (SegmentModele segment in segments)
            {
                segment.Dessiner();
            }

            GL.UseProgram(0);
        }

        public void Dispose()
        {
            foreach(TriangleModele triangleModele in triangles)
            {
                triangleModele.Dispose();
            }

            foreach(QuadrilatereModele quadrilatereModele in quadrilateres)
            {
                quadrilatereModele.Dispose();
            }

            foreach(ContourModele contourModele in contours)
            {
                contourModele.Dispose();
            }

            foreach(SegmentModele segmentModele in segments)
            {
                segmentModele.Dispose();
            }
        }
    }
}
