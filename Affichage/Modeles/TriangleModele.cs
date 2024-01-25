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
using QuadTree_OpenTK.Affichage.Shader;
using QuadTree_OpenTK.GrilleCollision.Items;
using GrilleCollision;

namespace QuadTree_OpenTK.Affichage.Modeles
{
    internal class TriangleModele : Modele
    {
        protected static Shader.Shader shader;
        protected static int nbTriangleModele = 0;

        protected int vertexBufferHandle;
        protected int vertexArrayHandle;

        protected float[] vertices;

        protected static bool ShaderChargé = false;
        protected bool DonnéesGraphiquesChargés = false;

        public TriangleModele( Vec2[] points )
        {
            vertices = new float[9];

            points = Modele.ConversionCoordonnéesAffichage( points );

            for(int i=0; i<3; i++)
            {
                this.vertices[i*3    ] = points[i].X();
                this.vertices[i*3 + 1] = points[i].Y();
                this.vertices[i*3 + 2] = 0;
            }

            //Chargement des données graphiques
            if (!DonnéesGraphiquesChargés)
            {
                shader = new Shader.ShaderFeuille();
                ShaderChargé = true;
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

            nbTriangleModele++;

        }

        public void Dessiner()
        {
            //GL.UseProgram(shader.getShaderProgramHandle());

            GL.BindVertexArray(vertexArrayHandle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

        }

        public static Shader.Shader GetShader()
        {
            return shader;
        }

        public void Dispose()
        {
            nbTriangleModele--;

            if(nbTriangleModele == 0 )
            {
                shader.Dispose();
            }
        }
    }
}
