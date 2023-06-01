using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// VTKLight: This MonoBehaviour script should be added to a Unity light component
/// to setup the corresponding VTK scene light.
/// </summary>
public class VTKLight : MonoBehaviour
{
  int VTKLightIndex;

  void Start()
  {
    this.VTKLightIndex = VTKUnityNativePlugin.AddLight();
  }

  void Update()
  {
    Light unityLight = gameObject.GetComponent<Light>();

    if (unityLight == null)
    {
      return;
    }

    VTKUnityNativePlugin.SetLightType(this.VTKLightIndex, (LightType)unityLight.type);
    VTKUnityNativePlugin.SetLightTransform(this.VTKLightIndex, unityLight.transform.localToWorldMatrix);
    VTKUnityNativePlugin.SetLightConeAngle(this.VTKLightIndex, unityLight.spotAngle / 2.0);
    VTKUnityNativePlugin.SetLightRange(this.VTKLightIndex, unityLight.range);
    VTKUnityNativePlugin.SetLightIntensity(this.VTKLightIndex, unityLight.intensity);
  }

  void OnDestroy()
  {
    VTKUnityNativePlugin.RemoveLight(this.VTKLightIndex);
  }
}
