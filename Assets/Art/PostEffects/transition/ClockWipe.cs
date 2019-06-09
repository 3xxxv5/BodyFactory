/// <summary>
/// by 李红伟
/// </summary>

using UnityEngine;
using System.Collections;

namespace taecg.tools
{
    [AddComponentMenu("Taecg/Transition/ClockWipe")]
    public class ClockWipe : PostEffectsBase
    {
        
        public enum Direction
        {
            Clockwise,CounterClockwise,
        }
        [Header("方向")]
        public Direction theDirection=Direction.Clockwise;

        [Header("混合度")]
        [Range(0f, 1f)]
        public float Value;

        void OnEnable()
        {
            shader=Shader.Find("Hidden/ClockWipe");
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            switch (theDirection)
            {
                case Direction.Clockwise:
                    material.SetFloat("_Direction",0);
                    break;
                case Direction.CounterClockwise:
                    material.SetFloat("_Direction",1);
                    break;
            }
            material.SetFloat("_Value", Value);
            Graphics.Blit(src, dest, material);
        }
    }
}
