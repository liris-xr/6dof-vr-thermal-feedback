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
    [HideInInspector] public float distanceObsatacleAfterPlayer;

    [Tooltip ("Maximum distance for obstacle awareness")][SerializeField] private float maxDist = 5.0f;

    private ThermalListener _thermalListener;

    private GameObject player;
    [HideInInspector]public bool obsatacleAfterPlayer;

    [Tooltip("Maximum distance between the device and the obstacles before it turns off")]
    public float turnOffDistance = 1.0f;

    void Start()
    {
        renderers = GetComponent<Renderer>();
        _thermalListener = FindFirstObjectByType<ThermalListener>();
        player = _thermalListener.gameObject;

    }

    public void CheckForColliders()
    {
        hitPlayer = false;
        obsatacleAfterPlayer= false;

        playerRay = new Ray(transform.position, (player.transform.position - transform.position).normalized);

        float dist = Vector3.Distance(transform.position, player.transform.position);

        RaycastHit[] hits= Physics.RaycastAll(playerRay, dist + maxDist, layers);
        Debug.DrawRay(playerRay.origin, playerRay.direction * (dist + maxDist), Color.blue);

        Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (var hit in  hits) 
        {
            //Debug.Log(hit.collider.gameObject.name + " was hit");

            if (hit.collider.gameObject == player)
            {

                renderers.material = greenMat;
                hitPlayer = true;
                
            }
            else
            {
                if (!hitPlayer)
                {
                    hitPlayer = false;
                    renderers.material = redMat;
                    break;
                }

                else if(hit.distance - dist <= turnOffDistance)
                {
                    obsatacleAfterPlayer = true;
                    distanceObsatacleAfterPlayer= hit.distance;
                }                
            }
        }
        

    }

    void Update()
    {
        if (mode== ThermalComputeMode.Spatial)
        {
            CheckForColliders();
        }
        Debug.Log("hit player = " + hitPlayer);
        Debug.Log("Obstacle after player = " + obsatacleAfterPlayer);


    }
}
