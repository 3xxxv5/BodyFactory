#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class FogVolumePrimitiveCreator : Editor {


	[UnityEditor.MenuItem("GameObject/Create Other/Fog Volume Primitive")]

    
	static public void CreateFogVolumePrimitive()
	{
		GameObject FogVolumePrimitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //Icon stuff
        //Texture Icon = Resources.Load("FogVolumePrimitiveIcon") as Texture;
        //Icon.hideFlags = HideFlags.HideAndDontSave;
        // var editorGUI = typeof(EditorGUIUtility);
        // var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
        //var args = new object[] { FogVolumePrimitive, Icon };
        //editorGUI.InvokeMember("SetIconForObject", bindingFlags, null, null, args);

        FogVolumePrimitive.name = "Fog Volume Primitive";
	
        FogVolumePrimitive.GetComponent<BoxCollider>().isTrigger=true;
		
		FogVolumePrimitive.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		FogVolumePrimitive.GetComponent<Renderer>().receiveShadows = false;
        FogVolumePrimitive.GetComponent<Renderer>().reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
        FogVolumePrimitive.GetComponent<Renderer>().lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        Selection.activeObject = FogVolumePrimitive;
		if (UnityEditor.SceneView.currentDrawingSceneView) UnityEditor.SceneView.currentDrawingSceneView.MoveToView(FogVolumePrimitive.transform);
        FogVolumePrimitive.AddComponent<FogVolumePrimitive>();
    }

	

}
#endif