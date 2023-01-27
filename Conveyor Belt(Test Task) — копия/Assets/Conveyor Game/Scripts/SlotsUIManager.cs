using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotsUIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _pickUpPointsText;
    [SerializeField] private TextMeshProUGUI _slotsCountText;
    [SerializeField] private ParticleSystem _particleSystem;
    

    private bool _isMoving = false;
    private Vector3 _defaulPosition;
    public int slotsCount = 0;

    private void OnEnable()
    {
        Food.OnFoodCollected += IncrementFoodPoints;
        _defaulPosition = _pickUpPointsText.transform.position;
       
    }

    private void OnDisable()
    {
        Food.OnFoodCollected -= IncrementFoodPoints;

    }
    private void FixedUpdate()
    {
        if (!GameManager.gameOver)
        {
            MoveToUI(_isMoving);
        }
    }
    private void IncrementFoodPoints()
    {
        StartCoroutine(TextAnimationDuration());
    }

    private IEnumerator TextAnimationDuration()
    {
        _pickUpPointsText.transform.position = _defaulPosition;
        _pickUpPointsText.fontSize = 0.3f;
        _pickUpPointsText.gameObject.SetActive(true);
        _isMoving = true;
        yield return new WaitForSecondsRealtime(1);
        
        _isMoving = false;
        _particleSystem.transform.position = _pickUpPointsText.transform.position;
        _particleSystem.Play();
        _pickUpPointsText.gameObject.SetActive(false);
        slotsCount++;
        _slotsCountText.text = $"{slotsCount} / 5";
        

    }

    private void MoveToUI(bool isMoving)
    {
        if (isMoving)
        {
            _pickUpPointsText.transform.position = Vector3.Lerp(_pickUpPointsText.transform.position, new Vector3(-3.79f, 5, 4.45f), Time.fixedDeltaTime);
            _pickUpPointsText.fontSize = Mathf.Lerp(_pickUpPointsText.fontSize, 0.1f, Time.fixedDeltaTime);

        }

    }
}
