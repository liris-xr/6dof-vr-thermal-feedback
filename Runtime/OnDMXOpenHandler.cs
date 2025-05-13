using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class OnDMXOpenHandler : MonoBehaviour
{
    private ArduinoDmxController arduinoDmxController;
    public UnityEvent onDmxOpenEvent;

    void Start()
    {
        arduinoDmxController = FindAnyObjectByType<ArduinoDmxController>();
        if (arduinoDmxController != null)
            arduinoDmxController.OnDmxOpen += OnDmxOpen;
        
    }

    private void OnDmxOpen()
    {
        onDmxOpenEvent.Invoke();
    }
}
