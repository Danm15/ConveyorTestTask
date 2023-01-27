using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyourSurface : MonoBehaviour
{
    [SerializeField] private float _conveyorSpeed;

    private Rigidbody _conveyorRigidbody;
    private Material _conveyorMaterial;
    private float _conveyorTextureSpeed;
    private float _tilingScaleCoeff;

    // The ratio of the speed of the texture to the speed of movement of the object on the conveyor is calculated
    private float _conveyorTextureSpeedCoeff = -0.02f;
    
    
    private void Start()
    {
        _conveyorMaterial = GetComponent<MeshRenderer>().material;
        _conveyorRigidbody = GetComponent<Rigidbody>();

        _tilingScaleCoeff = _conveyorMaterial.mainTextureScale.y / transform.localScale.z; 
    }

    private void FixedUpdate()
    {
        ConveyourSurfaceMove();
    }

    private void ConveyourSurfaceMove()
    {
        _conveyorMaterial.mainTextureOffset = new Vector2(0f, Time.time * CalculateTextureSpeed() * Time.fixedDeltaTime);
        Vector3 rigibbodyPosition = _conveyorRigidbody.position;
        _conveyorRigidbody.position += transform.forward * _conveyorSpeed * Time.fixedDeltaTime;
        _conveyorRigidbody.MovePosition(rigibbodyPosition);
    }

    private float CalculateTextureSpeed()
    {
        _conveyorTextureSpeed = (_conveyorSpeed / _conveyorTextureSpeedCoeff) * _tilingScaleCoeff;

        return _conveyorTextureSpeed;
    }
}
