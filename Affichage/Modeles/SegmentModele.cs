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
using QuadTree_OpenTK.GrilleCollision.Items;
using GrilleCollision;
using QuadTree_OpenTK.Affichage.Shader;

namespace QuadTree_OpenTK.Affichage.Modeles
{
    internal class SegmentModele : Modele
    {
        protected static Shader.Shader shader;
        protected static int nbSegmentModele = 0;

        protected int vertexBufferHandle;
        protected int vertexArrayHandle;

        protected float[] vertices;

        protected static bool ShaderChargés = false;
        protected bool DonnéesGraphiquesChargés = false;

        public SegmentModele(Vec2[] points)
        {
            vertices = new float[6];

            points = Modele.ConversionCoordonnéesAffichage(points);

            int compt = 0;

            foreach (var point in points)
            {
                vertices[compt * 3] = point.X();
                vertices[compt * 3 + 1] = point.Y();
                vertices[compt * 3 + 2] = 0;

                compt++;
            }

            //Chargement des données graphiques
            if (!ShaderChargés)
            {
                shader = new Shader.ShaderTronc();

                ShaderChargés = true;
            }

            if (!DonnéesGraphiquesChargés)
            {
                vertexBufferHandle = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                vertexArrayHandle = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayHandle);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                GL.BindVertexArray(0);

                DonnéesGraphiquesChargés = true;
            }

            nbSegmentModele++;

        }

        public void Dessiner()
        {
            GL.BindVertexArray(vertexArrayHandle);
            GL.DrawArrays(PrimitiveType.Lines, 0, 2);
        }

        public static Shader.Shader GetShader()
        {
            return shader;
        }

        public void Dispose()
        {
            nbSegmentModele--;

            if(nbSegmentModele == 0)
            {
                shader.Dispose();
            }
        }
    }
}