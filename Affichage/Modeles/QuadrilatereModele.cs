using QuadTree_OpenTK.Affichage.Shader;
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

namespace QuadTree_OpenTK.Affichage.Modeles
{
    internal class QuadrilatereModele : Modele
    {
        protected static Shader.Shader shader;
        protected static int nbQuadrilatereModele = 0;

        protected int vertexBufferHandle;
        protected int vertexArrayHandle;

        protected float[] vertices;

        protected static bool ShaderChargés = false;
        protected bool DonnéesGraphiquesChargés = false;

        public QuadrilatereModele(Vec2[] points)
        {
            vertices = new float[18];

            points = Modele.ConversionCoordonnéesAffichage(points);

            // Triangle 1
            vertices[ 0] = points[0].X();
            vertices[ 1] = points[0].Y();
            vertices[ 2] = 0;

            vertices[ 3] = points[1].X();
            vertices[ 4] = points[1].Y();
            vertices[ 5] = 0;

            vertices[ 6] = points[2].X();
            vertices[ 7] = points[2].Y();
            vertices[ 8] = 0;

            // Triangle 2
            vertices[ 9] = points[0].X();
            vertices[10] = points[0].Y();
            vertices[11] = 0;

            vertices[12] = points[2].X();
            vertices[13] = points[2].Y();
            vertices[14] = 0;

            vertices[15] = points[3].X();
            vertices[16] = points[3].Y();
            vertices[17] = 0;

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

            nbQuadrilatereModele++;

        }

        public void Dessiner()
        {
            GL.BindVertexArray(vertexArrayHandle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

        }

        public static Shader.Shader GetShader()
        {
            return shader;
        }

        public void Dispose()
        {
            nbQuadrilatereModele--;

            if(nbQuadrilatereModele== 0 )
            {
                shader.Dispose();
            }
        }
    }
}
