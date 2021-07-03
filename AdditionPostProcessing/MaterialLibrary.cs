using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering.Universal
{
    public class MaterialLibrary
    {
        public readonly Material invertColor;

        public MaterialLibrary(AdditionPostProcessData data)
        {
            invertColor = Load(data.shaders.invertColor);
        }

        private Material Load(Shader shader)
        {
            if (shader == null)
            {
                Debug.LogErrorFormat($"Missing shader. {GetType().DeclaringType.Name} render pass will not execute. Check for missing reference in the renderer resources.");
                return null;
            }
            return shader.isSupported ? CoreUtils.CreateEngineMaterial(shader) : null;
        }
        internal void Cleanup()
        {
            CoreUtils.Destroy(invertColor);
        }
    }
}