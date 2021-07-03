using System;

namespace UnityEngine.Rendering.Universal
{

    [Serializable, VolumeComponentMenu("Addition-Post-Processing/Invert-Color")]
    public class InvertColor : VolumeComponent, IPostProcessComponent
    {
        public BoolParameter invert = new BoolParameter(false);
        
        public bool IsActive()
        {
            return (bool)invert;
        }
        public bool IsTileCompatible()
        {
            return false;
        }
    }
}