/// <summary>
/// by 李红伟
/// </summary>

using UnityEngine;
using System.Collections;

namespace taecg.tools
{
    [AddComponentMenu("Taecg/Transition/ShutterWipe")]
    public class ShutterWipe : PostEffectsBase
    {
        public enum Direction
        {
           Horizontal,Verital,
        }
        [Header("百叶窗方向")]
        public Direction theDirection=Direction.Horizontal;

        [Header("百叶窗格数")]
        public int Amount=10;

        [Header("转场")]
        [Range(0f, 1f)]
        public float Value;

        void OnEnable()
        {
            shader=Shader.Find("Hidden/ShutterWipe");
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            switch (theDirection)
            {
                case Direction.Horizontal:
                    material.SetInt("_Direction", 0);
                    break;
                case Direction.Verital:
                    material.SetInt("_Direction", 1);
                    break;
            }
            material.SetInt("_Amount", Amount);
            material.SetFloat("_Value", Value);
            Graphics.Blit(src, dest, material);
        }
    }
}
