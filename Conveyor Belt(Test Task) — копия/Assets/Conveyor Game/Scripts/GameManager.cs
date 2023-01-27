using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TaskText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
   
    [SerializeField] private SlotsUIManager _slotsUIManager;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private Animator _animator;
    [SerializeField] private ChainIKConstraint _playerPlateArm;
    


    private string[] _foodNames = new string[] { "Donut", "Croissant" , "Cupcake"}; 
    private int _randomNumber;
    private int _randomProductIndex;
    private int _countCorrectFood = 0;

    public static bool gameOver = false;

    private void Start()
    {
        GenerateTaskRandom();
    }
    private void Update()
    {

        if(!gameOver)
            GameOverCheck();
    }

    private void GenerateTaskRandom()
    {
        _randomNumber = Random.Range(1, 6);
        _randomProductIndex = Random.Range(0, _foodNames.Length);
        _TaskText.text = $"Collect : {_randomNumber} {_foodNames[_randomProductIndex]}";
        _TaskText.gameObject.SetActive(true);

    }

    public void ChekingFood(GameObject food)
    {
        if(food.name == $"{_foodNames[_randomProductIndex]}(Clone)")
        {
            _countCorrectFood++;
        }
    }

    private void GameOverCheck()
    {
        if(_countCorrectFood == _randomNumber)
        {
            _gameOverText.text = "Level Passed";
            _TaskText.gameObject.SetActive(false);
            _gameOverText.gameObject.SetActive(true);
          
            StartCoroutine(StopGame());
        }
        else if (_slotsUIManager.slotsCount == 5)
        {
            _gameOverText.text = "Game Over";
            _TaskText.gameObject.SetActive(false);
            _gameOverText.gameObject.SetActive(true);
            
            StartCoroutine(StopGame());

        }

    }

    private IEnumerator StopGame()
    {
        gameOver = true;
        
        yield return new WaitForSecondsRealtime(1);

        foreach(GameObject food in GameObject.FindGameObjectsWithTag("Food"))
        {
            Destroy(food);
        }
        Destroy(GameObject.FindGameObjectWithTag("Conveyor"));
        _animator.SetBool("gameOver", true);
        _animationController.enabled = false;
        _playerPlateArm.weight = 0;
        StopAllCoroutines();


    }


    

}
