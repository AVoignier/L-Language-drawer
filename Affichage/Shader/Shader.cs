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
    internal interface Shader : IDisposable
    {
        protected string vertexShaderCode
        {
            get; set;
        }

        protected string pixelShaderCode
        { 
            get; 
            set; 
        }

        protected int vertexShaderHandle 
        { 
            get; 
            set; 
        }
        protected int shaderProgramHandle 
        { 
            get; 
            set; 
        }

        public int getShaderProgramHandle()
        {
            return shaderProgramHandle;
        }



    }
}
