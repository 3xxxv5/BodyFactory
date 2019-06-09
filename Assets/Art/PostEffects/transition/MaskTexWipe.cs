/// <summary>
/// by 李红伟
/// </summary>

using UnityEngine;
using System.Collections;

namespace taecg.tools
{
    [AddComponentMenu("Taecg/Transition/MaskTexWipe")]
    public class MaskTexWipe : PostEffectsBase
    {
        [Header("混合度")]
        [Range(0f, 1f)]
        public float Value;

        [Header("溶解贴图")]
        public Texture MaskTex;

        void OnEnable()
        {
            shader=Shader.Find("Hidden/MaskTexWipe");
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            material.SetFloat("_Value", Value);
            material.SetTexture("_MaskTex",MaskTex);
            Graphics.Blit(src, dest, material);
        }
    }
}
