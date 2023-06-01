using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// VTKCamera.cs: This MonoBehaviour script should be added to a Unity camera
/// to synchronize render calls and update the corresponding VTK camera.
/// </summary>
public class VTKCamera : MonoBehaviour
{
  Camera UnityCamera;

  void Start()
  {
    UnityCamera = GetComponent<Camera>();
    UnityCamera.nearClipPlane = 0.05f;
    UnityCamera.farClipPlane = 1000.0f;

    // Setup render command buffer on AfterForwardAlpha events.
    // VTK rendering is performed after the translucent pass.
    CommandBuffer renderCommandBuffer = new CommandBuffer();
    renderCommandBuffer.name = "Render VTK";
    renderCommandBuffer.IssuePluginEvent(VTKUnityNativePlugin.GetRenderCallback(), 1);
    UnityCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, renderCommandBuffer);
  }

  void OnPreRender()
  {
    // Update VTK camera matrices
    Matrix4x4 viewMatrix = UnityCamera.worldToCameraMatrix;
    Matrix4x4 projectionMatrix = UnityCamera.projectionMatrix;

    if (UnityCamera.stereoEnabled)
    {
      Camera.MonoOrStereoscopicEye activeEye = UnityCamera.stereoActiveEye;

      if (!(Camera.MonoOrStereoscopicEye.Mono == activeEye))
      {
        Camera.StereoscopicEye stereoEye = Camera.StereoscopicEye.Left;

        if (Camera.MonoOrStereoscopicEye.Right == activeEye)
        {
          stereoEye = Camera.StereoscopicEye.Right;
        }

        viewMatrix = UnityCamera.GetStereoViewMatrix(stereoEye);
        projectionMatrix = UnityCamera.GetStereoProjectionMatrix(stereoEye);
      }
    }

    VTKUnityNativePlugin.SetCameraViewMatrix(viewMatrix);
    VTKUnityNativePlugin.SetCameraProjectionMatrix(projectionMatrix);

    // vtkOpenGLGPUVolumeRayCastMapper moves the near clipping plane by
    // a "fraction of the near-far distance". Set dummy values to work around
    // this. The following has no influence on the camera projection matrix
    // because UseExplicitProjectionMatrix is on.
    VTKUnityNativePlugin.SetCameraClippingRange(
      UnityCamera.nearClipPlane, UnityCamera.nearClipPlane + 1);
  }

  void Update()
  {
    //// Rotate camera in example scene
    //UnityCamera.transform.RotateAround(
    //  new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime);
  }
}
