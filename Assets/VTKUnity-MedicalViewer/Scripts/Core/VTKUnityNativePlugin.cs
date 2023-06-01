using System;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// VTKUnityNativePlugin: C# interface to the VTK native plugin for unity 3D.
/// </summary>
public class VTKUnityNativePlugin
{
  // Define dll to import
#if (UNITY_IPHONE || UNITY_WEBGL) && !UNITY_EDITOR
  const string pluginDll = "__Internal";
#else
  const string pluginDll = "VTKNativePlugin";
#endif

  // Return VTK rendering callback function
  [DllImport(pluginDll)]
  public static extern IntPtr GetRenderCallback();

  // Set camera projection matrix.
  // The input matrix is transposed before being passed to VTK to match the coordinate system.
  [DllImport(pluginDll)]
  public static extern void SetCameraProjectionMatrix(Float16 projectionMatrix);
  public static void SetCameraProjectionMatrix(Matrix4x4 matrix)
  {
    SetCameraProjectionMatrix(Matrix4x4ToFloat16ColMajor(matrix));
  }

  // Set camera view matrix.
  // The input matrix is transposed before being passed to VTK to match the coordinate system.
  [DllImport(pluginDll)]
  public static extern void SetCameraViewMatrix(Float16 viewMatrix);
  public static void SetCameraViewMatrix(Matrix4x4 matrix)
  {
    SetCameraViewMatrix(Matrix4x4ToFloat16ColMajor(matrix));
  }

  [DllImport(pluginDll)]
  public static extern void SetLightTransform(int index, Float16 tMatrix);
  public static void SetLightTransform(int index, Matrix4x4 tMatrix)
  {
    SetLightTransform(index, Matrix4x4ToFloat16(tMatrix));
  }

  // Set camera clipping range.
  [DllImport(pluginDll)]
  public static extern void SetCameraClippingRange(float dNear, float dFar);

  // Add Light.
  [DllImport(pluginDll)]
  public static extern int AddLight();
  
  // Remove Light.
  [DllImport(pluginDll)]
  public static extern void RemoveLight(int index);

  // Set light type.
  [DllImport(pluginDll)]
  public static extern void SetLightType(int index, LightType type);

  // Set light type cone angle.
  [DllImport(pluginDll)]
  public static extern void SetLightConeAngle(int index, double angle);

  // Set light range.
  [DllImport(pluginDll)]
  public static extern void SetLightRange(int index, double range);

  // Set light range.
  [DllImport(pluginDll)]
  public static extern void SetLightIntensity(int index, double intensity);

  // Set volume file name.
  [DllImport(pluginDll)]
  public static extern void SetVolumeFileName(string filename);

  // Set volume sample distance when AutoAdjustSampleDistance is off
  [DllImport(pluginDll)]
  public static extern void SetVolumeSampleDistance(double sample);

  // Adjust volume sample distance to match an interactive framerate
  [DllImport(pluginDll)]
  public static extern void SetVolumeAutoAdjustSampleDistance(bool autoAdjust);

  // Set volume visibility
  [DllImport(pluginDll)]
  public static extern void SetVolumeVisibility(bool visible);

  // Add HSV point to the volume color transfer function for the specified data value
  [DllImport(pluginDll)]
  public static extern void AddVolumeColorTransferFunctionPoint(double value, double h, double s, double v);

  // Add scalar opacity point to the volume transfer function for the specified data value
  [DllImport(pluginDll)]
  public static extern void AddVolumeScalarOpacityFunctionPoint(double value, double opacity);

  // Add gradient opacity point to the volume transfer function for the specified data value
  [DllImport(pluginDll)]
  public static extern void AddVolumeGradientOpacityFunctionPoint(double value, double opacity);

  // Reset volume color transfer function
  [DllImport(pluginDll)]
  public static extern void ResetVolumeColorTransferFunction();

  // Reset volume scalar opacity function
  [DllImport(pluginDll)]
  public static extern void ResetVolumeScalarOpacityFunction();

  // Reset volume gradient opacity function
  [DllImport(pluginDll)]
  public static extern void ResetVolumeGradientOpacityFunction();

  // Set the axial slice number
  [DllImport(pluginDll)]
  public static extern void SetAxialSlice(int slice);

  // Set the sagittal slice number
  [DllImport(pluginDll)]
  public static extern void SetSagittalSlice(int slice);

  // Set the coronal slice number
  [DllImport(pluginDll)]
  public static extern void SetCoronalSlice(int slice);

  // Set the axial slice visibility
  [DllImport(pluginDll)]
  public static extern void SetAxialSliceVisibility(bool visible);

  // Set the sagittal slice visibility
  [DllImport(pluginDll)]
  public static extern void SetSagittalSliceVisibility(bool visible);

  // Set the coronal slice visibility
  [DllImport(pluginDll)]
  public static extern void SetCoronalSliceVisibility(bool visible);

  // Set isosurface value
  [DllImport(pluginDll)]
  public static extern void SetIsoSurfaceValue(int index, double value);

  // Set isosurface visibility
  [DllImport(pluginDll)]
  public static extern void SetIsoSurfaceVisibility(bool visible);

  // Set the histogram visibility
  [DllImport(pluginDll)]
  public static extern void SetHistogramVisibility(bool visible);

  [DllImport(pluginDll)]
  public static extern int GetNumberOfActors();

  // Float16 (float[16]) helper structure for matrix conversion
  [StructLayout(LayoutKind.Sequential)]
  public struct Float16
  {
    [MarshalAsAttribute(UnmanagedType.LPArray, SizeConst = 16)]
    public float[] elements;
  }

  // Convert unity Matrix4x4 to Float16
  public static Float16 Matrix4x4ToFloat16(Matrix4x4 unityMatrix)
  {
    Float16 pluginMatrix = new Float16()
    {
      elements = new float[16]
    };

    for (int row = 0; row < 4; row++)
    {
      for (int col = 0; col < 4; col++)
      {
        pluginMatrix.elements[(row * 4) + col] = unityMatrix[row, col];
      }
    }

    return pluginMatrix;
  }

  // Transpose and convert unity Matrix4x4 to Float16
  public static Float16 Matrix4x4ToFloat16ColMajor(Matrix4x4 unityMatrix)
  {
    Float16 pluginMatrix = new Float16()
    {
      elements = new float[16]
    };

    for (int row = 0; row < 4; row++)
    {
      for (int col = 0; col < 4; col++)
      {
        pluginMatrix.elements[(col * 4) + row] = unityMatrix[row, col];
      }
    }

    return pluginMatrix;
  }
}

public enum LightType
{
  Spot,
  Directional,
  Point
};
