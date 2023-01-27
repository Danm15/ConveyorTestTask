using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Food : MonoBehaviour
{
    
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private Transform _defaultArmTarget;
    [SerializeField] private Transform _defaultHeadTarget;
    [SerializeField] private Plate _plate;
    [SerializeField] private GameObject _foodUI;
    [SerializeField] private GameManager _gameManager;

    private Rigidbody _foodgRB;
    private Collider _foodColider;
    private bool _isCliked = false;
    private bool _isPickedUp = false;
    private Transform _freeSlot;

    public static event Action OnFoodCollected;


    private void Start()
    {
        _foodgRB = GetComponent<Rigidbody>();
        _foodColider = GetComponent<Collider>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _isCliked)
        {
            _isPickedUp = true;
            _foodgRB.useGravity = false;
            _foodgRB.isKinematic = true;
            _foodColider.enabled = false;
            _freeSlot = _plate.FreeSlot();
            StartCoroutine(DelayOfDisable());
        }
    }
    private void FixedUpdate()
    {
        if (!GameManager.gameOver)
        {
            MoveToPlate(_isPickedUp);
        }

    }

    private void OnMouseDown()
    {
        if (!_animationController.ispickingUp)
        {
            _animationController.GetTarget(transform, transform);
            _animationController.ispickingUp = true;
            _isCliked = true;
        }
       
    }

    private void MoveToPlate(bool isPikedUp)
    {
        if(isPikedUp)
            transform.position = Vector3.Lerp(transform.position, _freeSlot.position, Time.fixedDeltaTime * 2);
    }

    private IEnumerator DelayOfDisable()
    {
        yield return new WaitForSecondsRealtime(0.7f);
        _animationController.SetDefaultTarget();
        _animationController.ispickingUp = false;
        OnFoodCollected?.Invoke();
        yield return new WaitForSecondsRealtime(1f);
        _gameManager.ChekingFood(gameObject);
        yield return new WaitForSecondsRealtime(1f);
        Instantiate(_foodUI, gameObject.transform.position, gameObject.transform.rotation);
        _isPickedUp = false;
        _foodgRB.useGravity = true;
        _foodgRB.isKinematic = false;
        _foodColider.enabled = true;
        
        gameObject.SetActive(false);


    }

    
}
