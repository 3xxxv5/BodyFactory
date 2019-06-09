/// <summary>
/// by 李红伟
/// </summary>

using UnityEngine;
using System.Collections;

namespace taecg.tools
{
    [AddComponentMenu("Taecg/Transition/LinearWipe")]
    public class LinearWipe : PostEffectsBase
    {
        [Header("线性角度")]
        [Range(0f,1f)]
        public float Angle;

        [Header("混合度")]
        [Range(0f, 1f)]
        public float Value;

        void OnEnable()
        {
            shader=Shader.Find("Hidden/LinearWipe");
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            material.SetFloat("_Angle",Angle);
            material.SetFloat("_Value", Value);
            Graphics.Blit(src, dest, material);
        }
    }
}
