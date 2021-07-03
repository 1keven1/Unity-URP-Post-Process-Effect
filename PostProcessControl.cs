using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessControl : MonoBehaviour
{
    private UnityEngine.Rendering.Volume _ppv = null;
    
    private UnityEngine.Rendering.Universal.InvertColor _invertColor = null;

    private bool _invert = false;
    void Start()
    {
        _ppv = GetComponent<UnityEngine.Rendering.Volume>();
        
        if (!_ppv.profile.TryGet(out _invertColor))
        {
            _invertColor = ScriptableObject.CreateInstance<UnityEngine.Rendering.Universal.InvertColor>();
            _ppv.profile.components.Add(_invertColor);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _invert = !_invert;
        }
        
        ChangeOverride();
    }

    private void ChangeOverride()
    {
        _invertColor.invert.Override(_invert);
    }
}
