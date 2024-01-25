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

namespace QuadTree_OpenTK.Affichage.Shader
{
    internal class ShaderGrilleCollision : Shader
    {
        protected string p_vertexShaderCode =
            @"
            #version 330 core
            
            in vec3 aPosition;
            
            void main()
            {
                gl_Position = vec4(aPosition, 1f);
            }
            ";

        protected string p_pixelShaderCode =
            @"
            #version 330 core
            
            layout (location = 0) out vec4 pixelColor;
            
            void main()
            {
                pixelColor = vec4(1.0f, 0.0f, 0.0f, 1.0f);
            }
            ";

        protected int p_vertexShaderHandle;
        protected int p_shaderProgramHandle;

        string Shader.vertexShaderCode
        {
            get => p_vertexShaderCode;
            set => p_vertexShaderCode = value;
        }

        string Shader.pixelShaderCode
        {
            get => p_pixelShaderCode;
            set => p_pixelShaderCode = value;
        }

        int Shader.vertexShaderHandle
        {
            get => p_vertexShaderHandle;
            set => p_vertexShaderHandle = value;
        }
        int Shader.shaderProgramHandle
        {
            get => p_shaderProgramHandle;
            set => p_shaderProgramHandle = value;
        }

        public ShaderGrilleCollision()
        {
            // Création du vertex Shader
            p_vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(p_vertexShaderHandle, p_vertexShaderCode);
            GL.CompileShader(p_vertexShaderHandle);

            // Création du pixel Shader
            int pixelShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(pixelShaderHandle, p_pixelShaderCode);
            GL.CompileShader(pixelShaderHandle);

            // Création du programme shader
            p_shaderProgramHandle = GL.CreateProgram();
            GL.AttachShader(p_shaderProgramHandle, p_vertexShaderHandle);
            GL.AttachShader(p_shaderProgramHandle, pixelShaderHandle);

            GL.LinkProgram(p_shaderProgramHandle);


            GL.DetachShader(p_shaderProgramHandle, p_vertexShaderHandle);
            GL.DetachShader(p_shaderProgramHandle, pixelShaderHandle);

            GL.DeleteShader(p_vertexShaderHandle);
            GL.DeleteShader(pixelShaderHandle);
        }

        public void Dispose()
        {
            Console.WriteLine("Suppression Shader");

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(p_vertexShaderHandle);

            GL.UseProgram(0);
            GL.DeleteProgram(p_shaderProgramHandle);
        }

    }
}
