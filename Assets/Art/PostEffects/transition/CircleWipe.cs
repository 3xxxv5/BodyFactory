/// <summary>
/// by 李红伟
/// </summary>

using UnityEngine;
using System.Collections;

namespace taecg.tools
{
    [AddComponentMenu("Taecg/Transition/CircleWipe")]
    public class CircleWipe : PostEffectsBase
    {
        [Header("混合度")]
        [Range(0f, 1f)]
        public float Value;

        void OnEnable()
        {
            shader=Shader.Find("Hidden/CircleWipe");
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            material.SetFloat("_Value", Value);
            Graphics.Blit(src, dest, material);
        }
    }
}
