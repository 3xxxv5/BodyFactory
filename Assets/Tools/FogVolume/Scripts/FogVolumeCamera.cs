
using UnityEngine;
using UnityEngine.Profiling;
using FogVolumeUtilities;
using UnityEngine.VR;

[ExecuteInEditMode]

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(FogVolumeRenderManager))]

public class FogVolumeCamera : MonoBehaviour
{
    //[HideInInspector]
    //public bool _GenerateDepth;
    [HideInInspector]
    public string FogVolumeResolution;
    //[HideInInspector]
    public RenderTexture RT_FogVolume, RT_FogVolumeR;
    [HideInInspector]
    public int _Downsample;
    FogVolumeRenderer _FogVolumeRenderer;
    [HideInInspector]
    public Camera ThisCamera;
    [HideInInspector]
    public Camera SceneCamera;
    public LayerMask DepthMask = 0;
    public int screenX
    {
        get
        {
            return SceneCamera.pixelWidth;
        }
    }

    public int screenY
    {
        get
        {
            return SceneCamera.pixelHeight;
        }
    }
    //  FogVolumeRenderer Combiner;
    public RenderTexture RT_Depth, RT_DepthR;
    Shader depthShader = null;

    // float AspectRatio = 1;
    [HideInInspector]
    //[Range(0,1f)]
    public float upsampleDepthThreshold = .02f;

    #region BILATERAL_UPSAMPLING_DATA

    [System.Serializable]
    public enum UpsampleMode
    {
        DOWNSAMPLE_MIN,
        DOWNSAMPLE_MAX,
        DOWNSAMPLE_CHESSBOARD
    };

    // [SerializeField]
    UpsampleMode _upsampleMode;
    public UpsampleMode upsampleMode
    {

        set
        {
            if (value != _upsampleMode)
                SetUpsampleMode(value);
        }
        get
        {
            return _upsampleMode;
        }
    }
    void SetUpsampleMode(UpsampleMode value)
    {

        _upsampleMode = value;
        UpdateBilateralDownsampleModeSwitch();

    }

    public static bool BilateralUpsamplingEnabled()
    {
        return (SystemInfo.graphicsShaderLevel >= 40);
    }

    // Downsampled depthBuffer
    public RenderTexture[] lowProfileDepthRT;
    void ReleaseLowProfileDepthRT()
    {
        if (lowProfileDepthRT != null)
        {
            for (int i = 0; i < lowProfileDepthRT.Length; i++)
                RenderTexture.ReleaseTemporary(lowProfileDepthRT[i]);

            lowProfileDepthRT = null;
        }
    }

    //  [SerializeField]
    bool _useBilateralUpsampling = false;
    public bool useBilateralUpsampling
    {
        get { return _useBilateralUpsampling; }
        set
        {
            if (_useBilateralUpsampling != value)
                SetUseBilateralUpsampling(value);
        }
    }

    public enum UpsampleMaterialPass
    {
        DEPTH_DOWNSAMPLE = 0,
        BILATERAL_UPSAMPLE = 1
    };
    // [SerializeField]
    Material bilateralMaterial;
    void SetUseBilateralUpsampling(bool b)
    {

        _useBilateralUpsampling = b /*&& BilateralUpsamplingEnabled()*/;//works on dx9
        if (_useBilateralUpsampling)
        {
            if (bilateralMaterial == null)
            {
                bilateralMaterial = new Material(Shader.Find("Hidden/Upsample"));
                if (bilateralMaterial == null)
                    Debug.Log("#ERROR# Hidden/Upsample");

                // refresh keywords
                UpdateBilateralDownsampleModeSwitch();
                ShowBilateralEdge(_showBilateralEdge);
            }
        }
        else
        {
            // release resources
            bilateralMaterial = null;
        }
    }

    void UpdateBilateralDownsampleModeSwitch()
    {
        if (bilateralMaterial != null)
        {
            switch (_upsampleMode)
            {
                case UpsampleMode.DOWNSAMPLE_MIN:
                    bilateralMaterial.EnableKeyword("DOWNSAMPLE_DEPTH_MODE_MIN");
                    bilateralMaterial.DisableKeyword("DOWNSAMPLE_DEPTH_MODE_MAX");
                    bilateralMaterial.DisableKeyword("DOWNSAMPLE_DEPTH_MODE_CHESSBOARD");
                    break;
                case UpsampleMode.DOWNSAMPLE_MAX:
                    bilateralMaterial.DisableKeyword("DOWNSAMPLE_DEPTH_MODE_MIN");
                    bilateralMaterial.EnableKeyword("DOWNSAMPLE_DEPTH_MODE_MAX");
                    bilateralMaterial.DisableKeyword("DOWNSAMPLE_DEPTH_MODE_CHESSBOARD");
                    break;
                case UpsampleMode.DOWNSAMPLE_CHESSBOARD:
                    bilateralMaterial.DisableKeyword("DOWNSAMPLE_DEPTH_MODE_MIN");
                    bilateralMaterial.DisableKeyword("DOWNSAMPLE_DEPTH_MODE_MAX");
                    bilateralMaterial.EnableKeyword("DOWNSAMPLE_DEPTH_MODE_CHESSBOARD");
                    break;
                default:
                    break;
            }
        }
    }

    // Debug option: shows the bilateral upsamping edge
    //[SerializeField]
    bool _showBilateralEdge = false;


    public bool showBilateralEdge
    {
        set
        {
            if (value != _showBilateralEdge)
                ShowBilateralEdge(value);
        }
        get
        {
            return _showBilateralEdge;
        }
    }
    public void ShowBilateralEdge(bool b)
    {

        _showBilateralEdge = b;
        if (bilateralMaterial)
        {

            if (showBilateralEdge)
                bilateralMaterial.EnableKeyword("VISUALIZE_EDGE");
            else
                bilateralMaterial.DisableKeyword("VISUALIZE_EDGE");
        }
    }

    #endregion
    private Texture2D MakeTex(Color col)
    {
        Color[] pix = new Color[1];

        //   for (int i = 0; i < pix.Length; i++)
        pix[0] = col;

        Texture2D result = new Texture2D(1, 1);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }
    void ShaderLoad()
    {

        depthShader = Shader.Find("Hidden/Fog Volume/Depth");

        if (depthShader == null) print("Hidden/Fog Volume/Depth #SHADER ERROR#");
        //   depthShader = Shader.Find("Hidden/Internal-DepthNormalsTexture");

        //  if (depthShader == null) print("Hidden/Internal-DepthNormalsTexture #SHADER ERROR#");

    }

    public RenderTextureReadWrite GetRTReadWrite()
    {
        //return RenderTextureReadWrite.Default;
#if UNITY_5_6_OR_NEWER
        return (SceneCamera.allowHDR) ? RenderTextureReadWrite.Default : RenderTextureReadWrite.Linear;
#else
        return (SceneCamera.hdr) ? RenderTextureReadWrite.Default : RenderTextureReadWrite.Linear;
#endif
    }
    public RenderTextureFormat GetRTFormat()
    {
        //   return RenderTextureFormat.DefaultHDR;
        if (!_FogVolumeRenderer.TAA)
        {
#if UNITY_5_6_OR_NEWER
        return (SceneCamera.allowHDR == true) ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
#else
            return (SceneCamera.hdr == true) ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.ARGBHalf;
#endif
        }
        else
            return RenderTextureFormat.ARGBHalf;
    }
    protected void GetRT(ref RenderTexture rt, int2 size, string name)
    {

        // Release existing one
        ReleaseRT(rt);
        rt = RenderTexture.GetTemporary(size.x, size.y, 0, GetRTFormat(), GetRTReadWrite());
        rt.filterMode = FilterMode.Bilinear;
        rt.name = name;
        rt.wrapMode = TextureWrapMode.Clamp;

    }
    public void ReleaseRT(RenderTexture rt)
    {
        if (rt != null)
        {
            RenderTexture.ReleaseTemporary(rt);
            rt = null;
        }
    }

    FogVolumeRenderManager _CameraRender;
    RenderTextureFormat rt_DepthFormat;
    FogVolumeData _FogVolumeData;
    GameObject _FogVolumeDataGO;
    Texture2D nullTex;

    void OnEnable()
    {

        nullTex = MakeTex(Color.black);
        if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RFloat))
            rt_DepthFormat = RenderTextureFormat.RFloat;
        else
            rt_DepthFormat = RenderTextureFormat.DefaultHDR;

        // SceneCamera = Camera.main;
        _FogVolumeDataGO = GameObject.Find("Fog Volume Data");
        if (_FogVolumeDataGO)
            _FogVolumeData = _FogVolumeDataGO.GetComponent<FogVolumeData>();


        if (_FogVolumeData)
            SceneCamera = _FogVolumeData.GetFogVolumeCamera;

        if (SceneCamera == null)
        {
            //Debug.Log("FogVolumeCamera.cs can't get a valid camera from 'Fog Volume Data'\n Assigning Camera.main");
            SceneCamera = Camera.main;
        }

        ShaderLoad();
        SetUpsampleMode(_upsampleMode);
        ShowBilateralEdge(_showBilateralEdge);
        _FogVolumeRenderer = SceneCamera.GetComponent<FogVolumeRenderer>();

        if (_FogVolumeRenderer == null)
        {
            //    Combiner = SceneCamera.gameObject.GetComponent<FogVolumeRenderer>();
            //else
            if (!_FogVolumeData.ForceNoRenderer)
            {
                var renderer = SceneCamera.gameObject.AddComponent<FogVolumeRenderer>();
                renderer.enabled = true;
            }
        }


        ThisCamera = GetComponent<Camera>();        
        ThisCamera.enabled = false;

        ThisCamera.clearFlags = CameraClearFlags.SolidColor;
        ThisCamera.backgroundColor = new Color(0, 0, 0, 0);
        ThisCamera.farClipPlane = SceneCamera.farClipPlane;

        _CameraRender = GetComponent<FogVolumeRenderManager>();

        if (_CameraRender == null)
            _CameraRender = gameObject.AddComponent<FogVolumeRenderManager>();

        _CameraRender.SceneCamera = SceneCamera;
        _CameraRender.SecondaryCamera = ThisCamera;


    }


    void CameraUpdate()
    {

        //AspectRatio = (float)screenX / screenY;

        if (SceneCamera)
        {

            ThisCamera.aspect = SceneCamera.aspect;
            ThisCamera.farClipPlane = SceneCamera.farClipPlane;
            ThisCamera.nearClipPlane = SceneCamera.nearClipPlane;
#if UNITY_5_6_OR_NEWER
                        ThisCamera.allowHDR = SceneCamera.allowHDR;
                        ThisCamera.projectionMatrix = SceneCamera.projectionMatrix;
#else
            ThisCamera.hdr = SceneCamera.hdr;
            ThisCamera.fieldOfView = SceneCamera.fieldOfView;
#endif


        }
    }

    //   bool test = true;

    protected void Get_RT_Depth(ref RenderTexture rt, int2 size, string name)
    {
        // Release existing one
        ReleaseRT(rt);
        rt = RenderTexture.GetTemporary(size.x, size.y, 16, rt_DepthFormat);
        rt.filterMode = FilterMode.Bilinear;
        rt.name = name;
        rt.wrapMode = TextureWrapMode.Clamp;

    }

    public void RenderDepth()
    {
        if (_FogVolumeRenderer.GenerateDepth)
        {

            Profiler.BeginSample("FogVolume Depth");
            //Gimme scene depth

            // ThisCamera.cullingMask = SceneCamera.cullingMask;//Render the same than scene camera
            //3.2.1p2
           
            ThisCamera.cullingMask = _FogVolumeRenderer.DepthLayer2;
            #region Stereo
            if (SceneCamera.stereoEnabled)
            {
                Shader.EnableKeyword("FOG_VOLUME_STEREO_ON");
                //Left eye            

                if (SceneCamera.stereoTargetEye == StereoTargetEyeMask.Both || SceneCamera.stereoTargetEye == StereoTargetEyeMask.Left)
                {
                    Vector3 eyePos = SceneCamera.transform.parent.TransformPoint(UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.LeftEye));
                    Quaternion eyeRot = SceneCamera.transform.rotation;
                    Matrix4x4 projectionMatrix = SceneCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);

                    Get_RT_Depth(ref RT_Depth, new int2(screenX, screenY), "RT_DepthLeft");

                    _CameraRender.RenderEye(RT_Depth, eyePos, eyeRot, projectionMatrix, depthShader);

                    Shader.SetGlobalTexture("RT_Depth", RT_Depth);
                }
                //Right eye            

                if (SceneCamera.stereoTargetEye == StereoTargetEyeMask.Both || SceneCamera.stereoTargetEye == StereoTargetEyeMask.Right)
                {
                    Vector3 eyePos = SceneCamera.transform.parent.TransformPoint(UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.RightEye));
                    Quaternion eyeRot = SceneCamera.transform.rotation;
                    Matrix4x4 projectionMatrix = SceneCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);

                    Get_RT_Depth(ref RT_DepthR, new int2(screenX, screenY), "RT_DepthRight");

                    _CameraRender.RenderEye(RT_DepthR, eyePos, eyeRot, projectionMatrix, depthShader);

                    Shader.SetGlobalTexture("RT_DepthR", RT_DepthR);
                }
            }
            #endregion

            else
            {
                Shader.DisableKeyword("FOG_VOLUME_STEREO_ON");
                Get_RT_Depth(ref RT_Depth, new int2(screenX, screenY), "RT_Depth");
                _CameraRender.RenderEye(RT_Depth, SceneCamera.transform.position, SceneCamera.transform.rotation, SceneCamera.projectionMatrix, depthShader);
                Shader.SetGlobalTexture("RT_Depth", RT_Depth);
            }

            Profiler.EndSample();
        }
        else
        {
            Shader.SetGlobalTexture("RT_Depth", nullTex);
            Shader.SetGlobalTexture("RT_DepthR", nullTex);
            //Debug.Log("Not Generating Depth");
            // Debug.Log("Depth value: "+ _GenerateDepth);
        }
    }

    public void Render()
    {

        CameraUpdate();

        if (_Downsample > 0)
        {
            RenderDepth();

            //Textured Fog
            ThisCamera.cullingMask = 1 << LayerMask.NameToLayer("FogVolume");//show Fog volume
            ThisCamera.cullingMask |= 1 << LayerMask.NameToLayer("FogVolumeShadowCaster");//show FogVolumeShadowCaster
            int2 resolution = new int2(screenX / _Downsample, screenY / _Downsample);
            FogVolumeResolution = resolution.x + " X " + resolution.y;

            if (SceneCamera.stereoEnabled)
            {
                Profiler.BeginSample("FogVolume Render Stereo");
                Shader.EnableKeyword("FOG_VOLUME_STEREO_ON");
                //Left eye            

                if (SceneCamera.stereoTargetEye == StereoTargetEyeMask.Both || SceneCamera.stereoTargetEye == StereoTargetEyeMask.Left)
                {
                    Vector3 eyePos = SceneCamera.transform.parent.TransformPoint(UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.LeftEye));
                    Quaternion eyeRot = SceneCamera.transform.rotation;
                    Matrix4x4 projectionMatrix = SceneCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);

                    GetRT(ref RT_FogVolume, resolution, "RT_FogVolumeLeft");

                    _CameraRender.RenderEye(RT_FogVolume, eyePos, eyeRot, projectionMatrix, null);
                }

                //Right eye            

                if (SceneCamera.stereoTargetEye == StereoTargetEyeMask.Both || SceneCamera.stereoTargetEye == StereoTargetEyeMask.Right)
                {
                    Vector3 eyePos = SceneCamera.transform.parent.TransformPoint(UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.RightEye));
                    Quaternion eyeRot = SceneCamera.transform.rotation;

                    Matrix4x4 projectionMatrix = SceneCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);

                    GetRT(ref RT_FogVolumeR, resolution, "RT_FogVolumeRight");
                    _CameraRender.RenderEye(RT_FogVolumeR, eyePos, eyeRot, projectionMatrix, null);
                }
                Profiler.EndSample();
            }
            else
            {
                Profiler.BeginSample("FogVolume Render");
                Shader.DisableKeyword("FOG_VOLUME_STEREO_ON");
                GetRT(ref RT_FogVolume, resolution, "RT_FogVolume");
                _CameraRender.RenderEye(RT_FogVolume, SceneCamera.transform.position, SceneCamera.transform.rotation, SceneCamera.projectionMatrix, null);
                Profiler.EndSample();
            }


            // print("RT_FogVolume size: (" + RT_FogVolume.width + ", " + RT_FogVolume.height + ")");
            if (RT_FogVolume)
            {
                if (_FogVolumeRenderer.TAA && Application.isPlaying)
                    _FogVolumeRenderer._TAA.TAA(ref RT_FogVolume);
                Shader.SetGlobalTexture("RT_FogVolume", RT_FogVolume);
            }
            if (RT_FogVolumeR)
            {
                if (_FogVolumeRenderer.TAA && Application.isPlaying)
                    _FogVolumeRenderer._TAA.TAA(ref RT_FogVolumeR);
                Shader.SetGlobalTexture("RT_FogVolumeR", RT_FogVolumeR);
            }

            if (useBilateralUpsampling && _FogVolumeRenderer.GenerateDepth)
            {
                #region BILATERAL_DEPTH_DOWNSAMPLE
                Profiler.BeginSample("FogVolume Upsample");
                // Compute downsampled depth-buffer for bilateral upsampling
                if (bilateralMaterial)
                {
                    ReleaseLowProfileDepthRT();
                    lowProfileDepthRT = new RenderTexture[_Downsample];

                    for (int downsampleStep = 0; downsampleStep < _Downsample; downsampleStep++)
                    {
                        int targetWidth = screenX / (downsampleStep + 1);
                        int targetHeight = screenY / (downsampleStep + 1);

                        int stepWidth = screenX / Mathf.Max(downsampleStep, 1);
                        int stepHeight = screenY / Mathf.Max(downsampleStep, 1);
                        Vector4 texelSize = new Vector4(1.0f / stepWidth, 1.0f / stepHeight, 0.0f, 0.0f);
                        bilateralMaterial.SetFloat("_UpsampleDepthThreshold", upsampleDepthThreshold);
                        bilateralMaterial.SetVector("_TexelSize", texelSize);

                        bilateralMaterial.SetTexture("_HiResDepthBuffer", RT_Depth);

                        lowProfileDepthRT[downsampleStep] =
                            RenderTexture.GetTemporary(targetWidth, targetHeight, 0, rt_DepthFormat, GetRTReadWrite());
                        lowProfileDepthRT[downsampleStep].name = "lowProfileDepthRT_" + downsampleStep;
                        Graphics.Blit(null, lowProfileDepthRT[downsampleStep], bilateralMaterial, (int)UpsampleMaterialPass.DEPTH_DOWNSAMPLE);
                    }
                    Shader.SetGlobalTexture("RT_Depth", lowProfileDepthRT[lowProfileDepthRT.Length - 1]);

                }


                #endregion

                #region BILATERAL_UPSAMPLE

                // Upsample convolution RT
                if (bilateralMaterial)
                {
                    for (int downsampleStep = _Downsample - 1; downsampleStep >= 0; downsampleStep--)
                    {
                        int targetWidth = screenX / Mathf.Max(downsampleStep, 1);
                        int targetHeight = screenY / Mathf.Max(downsampleStep, 1);

                        // compute Low-res texel size
                        int stepWidth = screenX / (downsampleStep + 1);
                        int stepHeight = screenY / (downsampleStep + 1);
                        Vector4 texelSize = new Vector4(1.0f / stepWidth, 1.0f / stepHeight, 0.0f, 0.0f);
                        bilateralMaterial.SetVector("_TexelSize", texelSize);
                        bilateralMaterial.SetVector("_InvdUV", new Vector4(RT_FogVolume.width, RT_FogVolume.height, 0, 0));
                        // High-res depth texture

                        bilateralMaterial.SetTexture("_HiResDepthBuffer", RT_Depth);
                        bilateralMaterial.SetTexture("_LowResDepthBuffer", lowProfileDepthRT[downsampleStep]);

                        bilateralMaterial.SetTexture("_LowResColor", RT_FogVolume);
                        RenderTexture newRT = RenderTexture.GetTemporary(targetWidth, targetHeight, 0, GetRTFormat(), GetRTReadWrite());
                        newRT.filterMode = FilterMode.Bilinear;
                        Graphics.Blit(null, newRT, bilateralMaterial, (int)UpsampleMaterialPass.BILATERAL_UPSAMPLE);

                        // Swap and release
                        RenderTexture swapRT = RT_FogVolume;
                        RT_FogVolume = newRT;
                        RenderTexture.ReleaseTemporary(swapRT);
                    }
                }
                ReleaseLowProfileDepthRT();
                #endregion
                Profiler.EndSample();
            }





            Shader.SetGlobalTexture("RT_FogVolume", RT_FogVolume);
        }
        else
        {
            ReleaseRT(RT_FogVolume);
            FogVolumeResolution = Screen.width + " X " + Screen.height;
        }

    }



    void OnDisable()
    {
        if (ThisCamera)
            ThisCamera.targetTexture = null;
        DestroyImmediate(RT_FogVolume);
        ReleaseRT(RT_FogVolume);
        DestroyImmediate(RT_FogVolumeR);
        ReleaseRT(RT_FogVolumeR);
        ReleaseRT(RT_Depth);
        DestroyImmediate(RT_Depth);
        ReleaseRT(RT_DepthR);
        DestroyImmediate(RT_DepthR);

    }
    //GUIStyle labelStyle = new GUIStyle();
    //void OnGUI()
    //{
    //    labelStyle.normal.textColor = Color.white;
    //    labelStyle.fontSize = 50;
    //    GUI.Label(new Rect(10, 100, 100, 20), "RT_Depth format: "+RT_Depth.format.ToString(), labelStyle);
    //}
}
