using UnityEngine;
using UnityEngine.VR;

public class FogVolumeRenderManager : MonoBehaviour
{

    public Camera SceneCamera;
    public Camera SecondaryCamera;
   // public RenderTexture targetTexture;
    //public Shader CameraShader;   


    //public void Render()
    //{

       

    //    if (SceneCamera.stereoEnabled)
    //    {           

    //        if (SceneCamera.stereoTargetEye == StereoTargetEyeMask.Both || SceneCamera.stereoTargetEye == StereoTargetEyeMask.Left)
    //        {
    //            Vector3 eyePos = SceneCamera.transform.parent.TransformPoint(InputTracking.GetLocalPosition(VRNode.LeftEye));
    //            Quaternion eyeRot = SceneCamera.transform.rotation;
              
    //            Matrix4x4 projectionMatrix = SceneCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);

    //            RenderEye(targetTexture, eyePos, eyeRot, projectionMatrix);
    //        }
    //        if (SceneCamera.stereoTargetEye == StereoTargetEyeMask.Both || SceneCamera.stereoTargetEye == StereoTargetEyeMask.Right)
    //        {
    //            Vector3 eyePos = SceneCamera.transform.parent.TransformPoint(InputTracking.GetLocalPosition(VRNode.RightEye));
    //            Quaternion eyeRot = SceneCamera.transform.rotation;
                
    //            Matrix4x4 projectionMatrix = SceneCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
               
    //            RenderEye(targetTexture, eyePos, eyeRot, projectionMatrix);
    //        }
          
    //    }
    //    else
    //    {
    //        RenderEye(targetTexture, SceneCamera.transform.position, SceneCamera.transform.rotation, SceneCamera.projectionMatrix);
    //    }

      
    //}

   public void RenderEye(RenderTexture targetTexture, Vector3 camPosition, Quaternion camRotation, Matrix4x4 camProjectionMatrix, Shader CameraShader)
    {
        //SecondaryCamera.ResetWorldToCameraMatrix();
        SecondaryCamera.transform.position = camPosition;
        SecondaryCamera.transform.rotation = camRotation;
        SecondaryCamera.projectionMatrix = camProjectionMatrix;
        SecondaryCamera.targetTexture = targetTexture;
       // SecondaryCamera.rect = camViewport;

        if (CameraShader!=null)
            SecondaryCamera.RenderWithShader(CameraShader, "RenderType");
        else

            SecondaryCamera.Render();

    }






}