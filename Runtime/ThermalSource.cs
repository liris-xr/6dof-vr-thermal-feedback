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

    public float Intensity { get => _intensity; set => _intensity = Mathf.Clamp(value, 0.0f, 2.0f); }

    private Ray playerRay;
    private Renderer renderers;
    public Material redMat;
    public Material greenMat;

    [HideInInspector] public bool hitPlayer;

    public LayerMask layers;

    [HideInInspector] public float distanceObjectHit;

    //private ThermalListener _thermalListener;

    [SerializeField] GameObject player;

    void Start()
    {
        renderers = GetComponent<Renderer>();
       // _thermalListener = FindFirstObjectByType<ThermalListener>();

    }

    public void CheckForColliders()
    {
        hitPlayer = true;

        playerRay = new Ray(transform.position, (player.transform.position - transform.position).normalized);

        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (Physics.Raycast(playerRay, out RaycastHit hit, dist, layers))
        {
            Debug.Log(hit.collider.gameObject.name + " was hit");

            if (hit.collider.gameObject == player)
            {
                Debug.DrawRay(playerRay.origin, playerRay.direction * hit.distance, Color.green);

                renderers.material = greenMat;
                hitPlayer = true;
                
            }
            else
            {
                Debug.DrawRay(playerRay.origin, playerRay.direction * dist, Color.red);

                hitPlayer = false;
                renderers.material = redMat;
                distanceObjectHit = hit.distance;
            }
        }
        else
        {
            Debug.DrawRay(playerRay.origin, playerRay.direction * dist, Color.blue);

            hitPlayer = false;
        }
        

    }

    void Update()
    {
        if (mode== ThermalComputeMode.Spatial)
        {
            CheckForColliders();
        }
        Debug.Log(hitPlayer);
        
    }
}
