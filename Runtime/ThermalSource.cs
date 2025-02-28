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
    public float intensity;
    public ThermalComputeMode mode = ThermalComputeMode.Spatial;
}
