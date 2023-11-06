using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float EntitiesStartCount = 10f;
    public float FoodMultiplier = 0.1f;
    public int Day = 0;

    private int FoodCount;

    private GameObject _entityPrefab;
    private GameObject _foodPrefab;
    private Transform _entityParent;
    private Transform _foodParent;

    private GameObject[] _entities;

    private void Awake()
    {

        FoodCount = Mathf.FloorToInt(EntitiesStartCount * FoodMultiplier);
        Debug.Log(EntitiesStartCount * FoodMultiplier);

        _entityPrefab = Resources.Load<GameObject>("Entity");
        _foodPrefab = Resources.Load<GameObject>("Food");
        _entityParent = transform.Find("EntitiesParent");
        _foodParent = transform.Find("FoodParent");
    }

    private void Start()
    {
        for (int i = 0; i < EntitiesStartCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-24f, 24f), 0f, Random.Range(-24f, 24f));
            GameObject newEntity = Instantiate(_entityPrefab, position, Quaternion.identity);
            newEntity.transform.parent = _entityParent;
        }
        for(int i = 0; i < FoodCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-24f, 24f), 0f, Random.Range(-24f, 24f));
            GameObject newFood = Instantiate(_foodPrefab, position, Quaternion.identity);
            newFood.transform.parent = _foodParent;
        }
    }

    public void NewDay()
    {
        Day++;
    }
}
