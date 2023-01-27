using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 2f;
    private void Start()
    {
        StartCoroutine(SpawnRate());
    }

    private void SpawnFood()
    {
        GameObject food = ObjectPool.Instance.GetGameObject();

        if(food != null)
        {
            food.transform.position = transform.position;
            food.transform.rotation = transform.rotation;
            food.SetActive(true);
        }
        
    }

    private IEnumerator SpawnRate()
    {
        SpawnFood();
        yield return new WaitForSecondsRealtime(_spawnTime);
        StartCoroutine(SpawnRate());
    }
}
