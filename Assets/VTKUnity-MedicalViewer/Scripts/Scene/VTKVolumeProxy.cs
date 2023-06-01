using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// VTKVolumeProxy
/// </summary>
public class VTKVolumeProxy : MonoBehaviour
{
  [Header("Volume Rendering")]
  public string VolumeFileName = "";
  public bool VolumeVisibility = true;
  public bool AutoAdjustSampleDistance = false;
  [Range(0.0001f, 1f)]
  public double SampleDistance = 0.001;

  [Header("Volume Slices")]
  public bool AxialSliceVisibility = true;
  public int AxialSlice = 0;
  private int lastAxialSlice = -1;

  public bool SagittalSliceVisibility = true;
  public int SagittalSlice = 0;
  private int lastSagittalSlice = -1;

  public bool CoronalSliceVisibility = true;
  public int CoronalSlice = 0;
  private int lastCoronalSlice = -1;

  [Header("Isosurface")]
  public bool IsosurfaceVisibility = false;
  public double IsosurfaceValue = 90;
  private double lastIsosurfaceValue = -1;

  [Header("Histogram")]
  public bool HistogramVisibility = true;

  void Start()
  {
    if (this.VolumeFileName == "")
    {
      this.VolumeFileName = "head.vti";
    }

    string fullVolumeFileName = Path.Combine(Application.streamingAssetsPath, this.VolumeFileName);
    VTKUnityNativePlugin.SetVolumeFileName(fullVolumeFileName);

    // Build volume color transfer function
    VTKUnityNativePlugin.ResetVolumeColorTransferFunction();
    VTKUnityNativePlugin.AddVolumeColorTransferFunctionPoint(0, .67, .07, 1);
    VTKUnityNativePlugin.AddVolumeColorTransferFunctionPoint(94, .67, .07, 1);
    VTKUnityNativePlugin.AddVolumeColorTransferFunctionPoint(139, 0, 0, 0);
    VTKUnityNativePlugin.AddVolumeColorTransferFunctionPoint(160, .28, .047, 1);
    VTKUnityNativePlugin.AddVolumeColorTransferFunctionPoint(254, .38, .013, 1);

    // Build volume scalar opacity function
    VTKUnityNativePlugin.ResetVolumeScalarOpacityFunction();
    VTKUnityNativePlugin.AddVolumeScalarOpacityFunctionPoint(30, 0.0);
    VTKUnityNativePlugin.AddVolumeScalarOpacityFunctionPoint(130, 0.0);
    VTKUnityNativePlugin.AddVolumeScalarOpacityFunctionPoint(150, 0.9);
    VTKUnityNativePlugin.AddVolumeScalarOpacityFunctionPoint(250, 1);

    // Build volume gradient opacity function
    VTKUnityNativePlugin.ResetVolumeGradientOpacityFunction();
    VTKUnityNativePlugin.AddVolumeGradientOpacityFunctionPoint(0, .2);
    VTKUnityNativePlugin.AddVolumeGradientOpacityFunctionPoint(10, .2);
    VTKUnityNativePlugin.AddVolumeGradientOpacityFunctionPoint(25, 1);
  }

  void Update()
  {
    // Volume Rendering
    VTKUnityNativePlugin.SetVolumeVisibility(this.VolumeVisibility);
    VTKUnityNativePlugin.SetVolumeSampleDistance(this.SampleDistance);
    VTKUnityNativePlugin.SetVolumeAutoAdjustSampleDistance(this.AutoAdjustSampleDistance);

    // Axial-Sagittal-Coronal slices
    VTKUnityNativePlugin.SetAxialSliceVisibility(this.AxialSliceVisibility);
    if (this.AxialSlice != this.lastAxialSlice)
    {
      this.lastAxialSlice = this.AxialSlice;
      VTKUnityNativePlugin.SetAxialSlice(this.AxialSlice);
    }

    VTKUnityNativePlugin.SetSagittalSliceVisibility(this.SagittalSliceVisibility);
    if (this.SagittalSlice != this.lastSagittalSlice)
    {
      this.lastSagittalSlice = this.SagittalSlice;
      VTKUnityNativePlugin.SetSagittalSlice(this.SagittalSlice);
    }

    VTKUnityNativePlugin.SetCoronalSliceVisibility(this.CoronalSliceVisibility);
    if (this.CoronalSlice != this.lastCoronalSlice)
    {
      this.lastCoronalSlice = this.CoronalSlice;
      VTKUnityNativePlugin.SetCoronalSlice(this.CoronalSlice);
    }

    // Isosurface
    VTKUnityNativePlugin.SetIsoSurfaceVisibility(this.IsosurfaceVisibility);
    if (this.IsosurfaceValue != this.lastIsosurfaceValue)
    {
      this.lastIsosurfaceValue = this.IsosurfaceValue;
      VTKUnityNativePlugin.SetIsoSurfaceValue(0, this.IsosurfaceValue);
    }

    // Histogram
    VTKUnityNativePlugin.SetHistogramVisibility(this.HistogramVisibility);
  }

  void OnDestroy()
  {
  }
}
