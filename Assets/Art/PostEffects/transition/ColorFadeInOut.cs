/// <summary>
/// by 李红伟
/// </summary>

using UnityEngine;
using System.Collections;

namespace taecg.tools
{
    [AddComponentMenu("Taecg/Transition/ColorFadeInOut")]
    public class ColorFadeInOut : PostEffectsBase
    {
        [Header("转场颜色")]
        public Color TransitionColor = Color.black;

        [Header("混合度")]
        [Range(0f, 1f)]
        public float Value;

        void OnEnable()
        {
            shader=Shader.Find("Hidden/ColorFadeInOut");
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            material.SetColor("_MainColor", TransitionColor);
            material.SetFloat("_Value", Value);
            Graphics.Blit(src, dest, material);
        }
    }
}
