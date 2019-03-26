using UnityEngine;

[ExecuteInEditMode]
public class FogVolumeRenderer : MonoBehaviour
{    
    public string FogVolumeResolution;
    //	public bool RenderableInSceneView = true;

    public enum BlendMode
    {
        PremultipliedTransparency = (int)UnityEngine.Rendering.BlendMode.One,
        TraditionalTransparency = (int)UnityEngine.Rendering.BlendMode.SrcAlpha,
    };
    public BlendMode _BlendMode = BlendMode.PremultipliedTransparency;
    public bool GenerateDepth = true;
    [SerializeField]
    [Range(0, 8)]
    public int _Downsample = 1;
    public void setDownsample(int val) { _Downsample = val; }
    //public bool useRectangularStereoRT = false;
    public bool BilateralUpsampling = false;
    public bool ShowBilateralEdge = false;
    [SerializeField]
    public FogVolumeCamera.UpsampleMode USMode = FogVolumeCamera.UpsampleMode.DOWNSAMPLE_CHESSBOARD;
    public Camera ThisCamera = null;

    [HideInInspector]
    public FogVolumeCamera _FogVolumeCamera;
    // [SerializeField]
    GameObject _FogVolumeCameraGO;
    [SerializeField]
    [Range(0, .01f)]
    public float upsampleDepthThreshold = 0.00187f;
    public bool HDR;
    public bool TAA = false;
    public FogVolumeTAA _TAA = null;
    private FogVolumePlaydeadTAA.VelocityBuffer _TAAvelocity = null;
    //[HideInInspector]
    //public LayerMask DepthLayer = -1;
    [SerializeField]
   // [HideInInspector]
    //string _DepthLayersName = "Water";
    public int DepthLayer2=0;
    //public string DepthLayersName
    //{
    //    get { return _DepthLayersName; }
    //    set
    //    {
    //        if (_DepthLayersName != value)
    //            SetDepthLayer(value);
    //    }
    //}

    //void SetDepthLayer(string NewDepthLayersName)
    //{
    //    _DepthLayersName = NewDepthLayersName;
    //    DepthLayer = ThisCamera.cullingMask;
    //    DepthLayer &= ~(1 << LayerMask.NameToLayer(_DepthLayersName));
    //    //DepthLayer = LayerMask.NameToLayer(_DepthLayersName);
    //}

    //void OnValidate()
    //{
    //    SetDepthLayer(_DepthLayersName);
    //}
    void TAASetup()
	{
        if (_Downsample > 1 && TAA)
        {
            if (_FogVolumeCameraGO.GetComponent<FogVolumeTAA>() == null)
                _FogVolumeCameraGO.AddComponent<FogVolumeTAA>();
            _TAA = _FogVolumeCameraGO.GetComponent<FogVolumeTAA>();
            _TAAvelocity = _FogVolumeCameraGO.GetComponent<FogVolumePlaydeadTAA.VelocityBuffer>();
        }
    }

    void CreateFogCamera()
    {
        if (_Downsample > 1)
        {
            _FogVolumeCameraGO = new GameObject();
            _FogVolumeCameraGO.name = "FogVolumeCamera";
            _FogVolumeCamera = _FogVolumeCameraGO.AddComponent<FogVolumeCamera>();
            _FogVolumeCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
            _FogVolumeCamera.GetComponent<Camera>().backgroundColor = new Color(0, 0, 0, 0);
            _FogVolumeCameraGO.hideFlags = HideFlags.HideInHierarchy;
            // _FogVolumeCameraGO.hideFlags = HideFlags.None;
            _FogVolumeCamera.GetComponent<Camera>().renderingPath = RenderingPath.Forward;
#if UNITY_5_6_OR_NEWER
			_FogVolumeCamera.GetComponent<Camera>().allowMSAA = false;
#endif
        }
    }

    void FindFogCamera()
    {
        _FogVolumeCameraGO = GameObject.Find("FogVolumeCamera");
        if (_FogVolumeCameraGO)
			DestroyImmediate(_FogVolumeCameraGO);//the RT is not created in VR on start. Resetting here for now

        CreateFogCamera();

        TAASetup();
    }
    Vector4 TexelSize = Vector4.zero;
    void TexelUpdate()
    {
        if (_FogVolumeCamera.RT_FogVolume)
        {
            TexelSize.x = 1.0f / _FogVolumeCamera.RT_FogVolume.width;
            TexelSize.y = 1.0f / _FogVolumeCamera.RT_FogVolume.height;
            TexelSize.z = _FogVolumeCamera.RT_FogVolume.width;
            TexelSize.w = _FogVolumeCamera.RT_FogVolume.height;
            Shader.SetGlobalVector("RT_FogVolume_TexelSize", TexelSize);
        }
        //  print(TexelSize);
    }
    //#region instance
    //private static FogVolumeRenderer _instance;
    //public static FogVolumeRenderer instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<FogVolumeRenderer>();

    //        }

    //        return _instance;
    //    }
    //}
    //#endregion
    void OnEnable()
    {
        ThisCamera = gameObject.GetComponent<Camera>();

        FindFogCamera();
        //3.2.2 clean meshes. Some users pretends to add FogVolume.cs here ¬¬
        if (GetComponent<FogVolume>())
        {
            print("Don't add FogVolume here. Create a new one using the menu buttons and follow the instructions");
            DestroyImmediate(GetComponent<FogVolume>());
        }

        if (ThisCamera.GetComponent<MeshFilter>())
            DestroyImmediate(ThisCamera.GetComponent<MeshFilter>());

        if (ThisCamera.GetComponent<MeshRenderer>())
            DestroyImmediate(ThisCamera.GetComponent<MeshRenderer>());

        SurrogateMaterial = (Material)Resources.Load("Fog Volume Surrogate");
        UpdateParams();

        if (DepthLayer2 == 0)
            DepthLayer2 = 1;
        //DepthLayer2 = ThisCamera.cullingMask;
    }
    void UpdateParams()
    {
		if (_FogVolumeCamera && _Downsample > 1)
        {
            _FogVolumeCamera.useBilateralUpsampling = BilateralUpsampling;
            // _FogVolumeCamera._GenerateDepth = GenerateDepth;
            if (BilateralUpsampling && GenerateDepth)
            {
                _FogVolumeCamera.upsampleMode = USMode;

                _FogVolumeCamera.showBilateralEdge = ShowBilateralEdge;
                _FogVolumeCamera.upsampleDepthThreshold = upsampleDepthThreshold;
            }

            if (GenerateDepth)
            {
                SurrogateMaterial.SetInt("_ztest", (int)UnityEngine.Rendering.CompareFunction.Always);
                //_FogVolumeCamera.DepthMask = instance.DepthLayer;
                // _FogVolumeCamera.DepthMask = ThisCamera.cullingMask;
                //_FogVolumeCamera.DepthMask &= ~(1 << DepthLayer2);
                DepthLayer2 &= ~(1 << LayerMask.NameToLayer("FogVolume"));//hide FogVolume
                DepthLayer2 &= ~(1 << LayerMask.NameToLayer("FogVolumeShadowCaster"));//hide FogVolumeShadowCaster
                DepthLayer2 &= ~(1 << LayerMask.NameToLayer("FogVolumeSurrogate"));//hide FogVolumeSurrogate
                DepthLayer2 &= ~(1 << LayerMask.NameToLayer("FogVolumeUniform"));//hide FogVolumeUniform
                DepthLayer2 &= ~(1 << LayerMask.NameToLayer("UI"));//hide UI
                _FogVolumeCamera.DepthMask = DepthLayer2;
            }
            else
                SurrogateMaterial.SetInt("_ztest", (int)UnityEngine.Rendering.CompareFunction.LessEqual);

            if (!_TAA)
				TAASetup();

            if (_TAA && _TAA.enabled != TAA)
            {
                _TAA.enabled = TAA;
                _TAAvelocity.enabled = TAA;
            }

#if UNITY_5_6_OR_NEWER
		HDR= ThisCamera.allowHDR;
#else
        HDR = ThisCamera.hdr;
#endif
        }
    }
    Material SurrogateMaterial;
   
    public bool SceneBlur = true;

    void OnPreRender()
    {
		int InitialpixelLights = QualitySettings.pixelLightCount;
		ShadowQuality InitialShadows = QualitySettings.shadows;
        QualitySettings.pixelLightCount = 0;
        QualitySettings.shadows = ShadowQuality.Disable;

#if UNITY_EDITOR
        UpdateParams();
        if (ThisCamera == null)
            ThisCamera = gameObject.GetComponent<Camera>();
#endif
        if (_Downsample > 1 && _FogVolumeCamera)
        {
            SurrogateMaterial.SetInt("_SrcBlend", (int)_BlendMode);
            Shader.EnableKeyword("_FOG_LOWRES_RENDERER");

            // Profiler.BeginSample("FogVolume Render");
            _FogVolumeCamera.Render();
            // Profiler.EndSample();

            //  TexelUpdate();

            Shader.DisableKeyword("_FOG_LOWRES_RENDERER");
        }
        else
        {
            Shader.DisableKeyword("_FOG_LOWRES_RENDERER");
        }

        QualitySettings.pixelLightCount = InitialpixelLights;
        QualitySettings.shadows = InitialShadows;
    }

    private int m_screenWidth = 0;
    private int m_screenHeight = 0;

    void Update()
	{
#if UNITY_EDITOR
        if (ThisCamera == null)
			ThisCamera = gameObject.GetComponent<Camera>();
#endif
        //#if UNITY_EDITOR
        //        // if destroyed...
        //        FindFogCamera();
        //#endif
        if (_FogVolumeCamera == null
            && _Downsample > 1)//3.2.1
            FindFogCamera();

        if (_Downsample == 1) DestroyFogCamera();

        if (_Downsample > 1 && _FogVolumeCameraGO && this.isActiveAndEnabled)
        {

            ThisCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("FogVolume"));//hide FogVolume
            ThisCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("FogVolumeShadowCaster"));//hide FogVolumeShadowCaster
            FogVolumeResolution = _FogVolumeCamera.FogVolumeResolution;
            ThisCamera.cullingMask |= 1 << LayerMask.NameToLayer("FogVolumeSurrogate");//show surrogate
        }
        else
        {
            ThisCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("FogVolumeSurrogate"));//hide surrogate

            ThisCamera.cullingMask |= 1 << LayerMask.NameToLayer("FogVolume");//show FogVolume
            ThisCamera.cullingMask |= 1 << LayerMask.NameToLayer("FogVolumeShadowCaster");//show FogVolumeShadowCaster

            if ((Screen.width != m_screenWidth) ||
                (Screen.height != m_screenHeight))
            {
                m_screenWidth = Screen.width;
                m_screenHeight = Screen.height;
                FogVolumeResolution = m_screenWidth + " X " + m_screenHeight;
            }
        }

        if (_FogVolumeCameraGO)
            _FogVolumeCamera._Downsample = _Downsample;
        else
            CreateFogCamera();

       
    }
    void DestroyFogCamera()
    {
        if (_FogVolumeCameraGO
          // &&            _FogVolumeCameraGO.activeInHierarchy
          )//3.1.6
        {
            DestroyImmediate(_FogVolumeCameraGO);
        }
    }

    void OnDisable()
    {
        //		Shader.DisableKeyword("RENDER_SCENE_VIEW");
        Shader.DisableKeyword("_FOG_LOWRES_RENDERER");
        DestroyFogCamera();
        ThisCamera.cullingMask |= (1 << LayerMask.NameToLayer("FogVolume"));
        ThisCamera.cullingMask |= 1 << LayerMask.NameToLayer("FogVolumeShadowCaster");
		ThisCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("FogVolumeSurrogate"));//hide surrogate
	}
}
