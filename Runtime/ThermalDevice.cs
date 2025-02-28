# if !UNITY_ANDROID
using System;
using UnityEngine;
using Unity.Mathematics;
using Object = System.Object;

public class ThermalDevice : MonoBehaviour
{
    public int DMXAddress;

    public ArduinoDmxController _arduinoDmxController;

    [SerializeField] private float intensity;

    public float Intensity
    {
        get => intensity;
        set
        {
            intensity = value;
            if (Math.Abs(intensity - previousIntensity) > 0.01f)
            {
                SendIntensity();
            }
        }
    }
    
    private float previousIntensity;
    
    public float nextIntensity;

    private float elapsed;


    private void Start()
    {
        previousIntensity = intensity;

        _arduinoDmxController = FindFirstObjectByType<ArduinoDmxController>();

        _arduinoDmxController.OnDmxOpen += SendIntensity;
    }
    
    void SendIntensity()
    {
        if (_arduinoDmxController == null)
        {
            return;
        }


        var valueDmx = (int)math.remap(0, 1, 0, 255, Mathf.Clamp(intensity, 0f, 1f));
        _arduinoDmxController.SendData(valueDmx.ToString(), DMXAddress.ToString());
        previousIntensity = intensity;
    }
}
#endif