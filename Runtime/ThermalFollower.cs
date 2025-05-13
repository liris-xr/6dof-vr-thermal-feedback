using System.Diagnostics.Eventing.Reader;
using UnityEngine;

/// <summary>
/// This script must be used to ensure position of the thermal device reference as you are using any locomotion system in your application, like teleportation or continuous movement.
/// Place this script on a GO that will be the parent of all of your thermal devices
/// </summary>
public class ThermalFollower : MonoBehaviour
{

    [Tooltip("Position of the gameObject that follows the player gameobject, in order to make the thermal sources positions fixed as the player moves in VR but not IRL")]
    [SerializeField] private Transform _positionToFollow;

    [Tooltip("Rotation of gameobject that follows the player rotation, in order to make the thermal sources rotation fixed as the player rotate in VR but not IRL\"\"")]
    [SerializeField] private Transform _rotationToFollow;

    [Tooltip("If true, use the local position of the positionToFollow Transform instead of global position  (usefull in case of following a GO that is a child of an already moved GO)")]
    [SerializeField] private bool _followLocalPosition;

    [Tooltip("If true, use the local euler angles of the rotationToFollow Transform instead of global euler angles  (usefull in case of following a GO that is a child of an already rotated GO )\n<i>Example : Camera child of XR Origin</i>")]
    [SerializeField] private bool _followLocalRotation;

    [Tooltip("Thermal Follower will only be rotated on y Axis \n<i>Example : Following only the camera rotation on Y axis</i>")]
    [SerializeField] private bool _onlyRotateOnY;


    void Update()
    {
        this.transform.position = _followLocalPosition ? _positionToFollow.localPosition : _positionToFollow.position;
        if(_onlyRotateOnY)
            this.transform.localEulerAngles = _followLocalRotation ? new Vector3(0f,_rotationToFollow.localEulerAngles.y,0f) : new Vector3(0f, _rotationToFollow.eulerAngles.y, 0f);
        else
            this.transform.localEulerAngles = _followLocalRotation ? _rotationToFollow.localEulerAngles : _rotationToFollow.eulerAngles;
    }
}
