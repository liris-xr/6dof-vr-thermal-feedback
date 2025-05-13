using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThermalComputeMode
{
    Environment,
    Falloff,
    Spatial
}

public class ThermalSource : MonoBehaviour
{
    public float _intensity;
    public ThermalComputeMode mode = ThermalComputeMode.Spatial;
    
    public float Intensity { get => _intensity; set  => _intensity = Mathf.Clamp(value, 0.0f, 2.0f); }

}
