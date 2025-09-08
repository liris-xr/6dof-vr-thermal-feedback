# if !UNITY_ANDROID
using Codice.Client.Common.GameUI;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ThermalController : MonoBehaviour
{
    [SerializeField] private List<ThermalSource> thermalSources;
    [SerializeField] private List<ThermalDevice> thermalDevices;
    
    private ThermalListener _thermalListener;

    
    private void Start()
    {
        StartCoroutine(UpdateThermalFeedback());
        _thermalListener = FindFirstObjectByType<ThermalListener>();
    }

    private IEnumerator UpdateThermalFeedback()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            
            foreach(var thermalSource in thermalSources)
            {
                if(!thermalSource.enabled || !thermalSource.gameObject.activeSelf)
                    continue;

                if (thermalSource.mode == ThermalComputeMode.Environment)
                {
                    ComputeEnvironment(thermalSource);
                }
                else if (thermalSource.mode == ThermalComputeMode.Falloff)
                {
                    ComputeFalloff(thermalSource);
                }
                else if (thermalSource.mode == ThermalComputeMode.Spatial)
                {
                    ComputeSpatialized(thermalSource);
                }
            }

            foreach (var thermalDevice in thermalDevices)
            {
                thermalDevice.Intensity = thermalDevice.nextIntensity;
                thermalDevice.nextIntensity = 0.0f;
            }
        }
    }

    private void ComputeEnvironment(ThermalSource thermalSource)
    {
        foreach (var device in thermalDevices)
        {
            device.nextIntensity += thermalSource.Intensity;
        }
    }
    
    private void ComputeFalloff(ThermalSource thermalSource)
    {
        foreach (var device in thermalDevices)
        {
            var distance = Vector3.Distance(_thermalListener.transform.position, thermalSource.transform.position);
            var intensity = thermalSource.Intensity / (1 + Mathf.Pow(distance, 2));

            device.nextIntensity += intensity;
        }
    }
    
    private void ComputeSpatialized(ThermalSource thermalSource)
    {
        foreach (var device in thermalDevices)
        {
            var position = device.transform.position;
            var distanceSourceDevice = Vector3.Distance(thermalSource.transform.position, position);
            var distanceDeviceListener = Vector3.Distance(_thermalListener.transform.position, position);
            var distanceListenerSource = Vector3.Distance(_thermalListener.transform.position,thermalSource.transform.position);

            var intensity = thermalSource.Intensity / (1 + Mathf.Pow((distanceSourceDevice * distanceListenerSource) / distanceDeviceListener, 2));

            if (thermalSource.hitPlayer== false)
            {
                intensity = 0.0f;
            }
            else if(thermalSource.obsatacleAfterPlayer == true && thermalSource.distanceObsatacleAfterPlayer - distanceSourceDevice <= thermalSource.turnOffDistance)
            {
                intensity = 0.0f;
            }
            else
            {
                device.nextIntensity += intensity;
            }

            if (intensity == 0.0f)
            {
                device.renderers.material = device.redMat;

            }
            else
            {
                device.renderers.material = device.greenMat;

            }
        }
    }
}
#endif