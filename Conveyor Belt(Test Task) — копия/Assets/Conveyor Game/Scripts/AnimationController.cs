using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationController : MonoBehaviour
{
    public float distance;
    public bool ispickingUp = false;

    [SerializeField] private MultiAimConstraint _playerHead;
    [SerializeField] private ChainIKConstraint _playerSpine;
    [SerializeField] private ChainIKConstraint _playerArm;
    [SerializeField] private Transform _armController;
    [SerializeField] private Transform _headController;
    [SerializeField] private Transform _pickUpPoint;
    [SerializeField] private Transform _defaultArmTarget;
    [SerializeField] private Transform _defaultHeadTarget;
    
    
    private Vector3 _offset = new Vector3(-0.1f, 0.2f, 0);
    private Transform _headTarget;
    private Transform _armTarget;

    
    private void Start()
    {
        SetDefaultTarget();
    }
    private void FixedUpdate()
    {
        if (!GameManager.gameOver)
        {
            ControlConstraintWeight();
            FollowTarget();
        }


    }

    public void GetTarget(Transform headTarget, Transform armTarget)
    {
        _headTarget = headTarget;
        _armTarget = armTarget;
    }

    private void FollowTarget()
    {
        float animationSpeed;
        if (ispickingUp)
        {
            animationSpeed = 8;
        }else
        {
            animationSpeed = 2;
        }
       _headController.position = Vector3.Lerp(_headController.position, _headTarget.position + _offset, Time.fixedDeltaTime * animationSpeed);
       _armController.position = Vector3.Lerp(_armController.position, _armTarget.position + _offset, Time.fixedDeltaTime * animationSpeed);

    }

    private void ControlConstraintWeight()
    {
        var playerHead = _playerHead.data.sourceObjects;
        playerHead.SetWeight(0, Mathf.Lerp(1, 1, Time.fixedDeltaTime));
        playerHead.SetWeight(1, Mathf.Lerp(1, 0, Time.fixedDeltaTime));
        _playerHead.data.sourceObjects = playerHead;

        if (_headTarget.position != _defaultHeadTarget.position)
        {
            distance = _headTarget.position.x - _pickUpPoint.position.x;

            if (distance <= 0.9)
            _playerSpine.weight = Mathf.Lerp(0.6f, 0, distance);
            _playerArm.weight = 2.5f / (distance * distance);

        }
        else
        { 
            _playerHead.weight = 1;
            _playerSpine.weight = 0;
            _playerArm.weight = 0;
        }
    }

    public void SetDefaultTarget()
    {
        GetTarget(_defaultHeadTarget, _defaultArmTarget);
    }

    public void PickUp(GameObject pickedFood)
    {
        
    }

}
