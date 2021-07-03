using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
#endif
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace UnityEngine.Rendering.Universal
{
    [Serializable]
    public class AdditionPostProcessData : ScriptableObject
    {
#if UNITY_EDITOR
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812")]

        [MenuItem("Assets/Create/Rendering/Universal Render Pipeline/Additional Post-process Data", priority = CoreUtils.assetCreateMenuPriority3 + 1)]
        static void CreateAdditionalPostProcessData()
        {
            var instance = CreateInstance<AdditionPostProcessData>();
            AssetDatabase.CreateAsset(instance, $"Assets/Settings/{nameof(AdditionPostProcessData)}.asset");
            Selection.activeObject = instance;
        }
#endif
        [Serializable]
        public sealed class Shaders
        {
            public Shader invertColor;
        }

        private void OnEnable()
        {
            shaders.invertColor = Shader.Find("Post Process/Invert Color");
        }

        public Shaders shaders;
    }
}