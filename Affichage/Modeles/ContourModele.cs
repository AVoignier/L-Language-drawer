using GrilleCollision;
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

namespace QuadTree_OpenTK.Affichage.Modeles
{
    internal class ContourModele : Modele
    {
        protected static Shader.Shader shader;
        protected static int nbContourModele = 0;

        protected int vertexBufferHandle;
        protected int vertexArrayHandle;

        protected float[] vertices;

        protected static bool ShaderChargés = false;
        protected bool DonnéesGraphiquesChargés = false;

        public ContourModele(Vec2[] points)
        {
            vertices = new float[ points.Length *3 +3 ];

            points = Modele.ConversionCoordonnéesAffichage(points);

            for (int i = 0; i < points.Length; i++)
            {
                vertices[i * 3    ] = points[i].X();
                vertices[i * 3 + 1] = points[i].Y();
                vertices[i * 3 + 2] = 0;
            }

            vertices[points.Length * 3    ] = points[0].X();
            vertices[points.Length * 3 + 1] = points[0].Y();
            vertices[points.Length * 3 + 2] = 0;

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

            nbContourModele++;
        }

        public void Dessiner()
        {
            GL.BindVertexArray(vertexArrayHandle);
            GL.DrawArrays(PrimitiveType.LineStrip, 0, vertices.Length/3);

        }

        public static Shader.Shader GetShader()
        {
            return shader;
        }

        public void Dispose()
        {
            nbContourModele--;

            if(nbContourModele == 0)
            {
                shader.Dispose();
            }
        }
    }
}