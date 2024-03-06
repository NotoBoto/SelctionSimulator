using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float EntitiesStartCount = 10f;
    public float FoodCount = 5f;
    public int Day = 0;

    public float StartSpeed;
    public float StartSize;
    public float StartMaxHP;
    public float StartDamage;
    public Color StartColor;

    public float DayTime;

    public bool IsGameStarted;
    public bool IsGamePaused;
    public bool IsDayRunning;

    private GameObject _entityPrefab;
    private GameObject _foodPrefab;
    private Transform _entityParent;
    private Transform _foodParent;

    private GameObject[] _food;

    private void Awake()
    {
        _entityPrefab = Resources.Load<GameObject>("Entity");
        _foodPrefab = Resources.Load<GameObject>("Food");
        _entityParent = transform.Find("EntitiesParent");
        _foodParent = transform.Find("FoodParent");
    }

    public void StartGame()
    {
        for (int i = 0; i < EntitiesStartCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-24f, 24f), 0f, Random.Range(-24f, 24f));
            GameObject newEntity = Instantiate(_entityPrefab, position, Quaternion.identity);
            newEntity.transform.parent = _entityParent;
            SetÑharacteristics(newEntity.GetComponent<EntityController>(), StartSpeed, StartSize, StartMaxHP, StartDamage, StartColor);
        }
        for (int i = 0; i < FoodCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-24f, 24f), 0f, Random.Range(-24f, 24f));
            GameObject newFood = Instantiate(_foodPrefab, position, Quaternion.identity);
            newFood.transform.parent = _foodParent;
        }
        IsGameStarted = true;
        Invoke("StartNewDay", 0.01f);
    }

    public void NextDay()
    {
        if(FindObjectsOfType<EntityController>().Length == 0)
        {
            NewDay();
        }
        else
        {
            Invoke("NewDay", DayTime);
        }
    }

    public void NewDay()
    {
        if (IsDayRunning)
        {
            CancelInvoke("NewDay");
            EntityController[] entities = Resources.FindObjectsOfTypeAll<EntityController>();
            Day++;
            IsDayRunning = false;
            for (int i = 0; i < entities.Length - 1; i++)
            {
                if (entities[i] != null && entities[i].EntityModel.FoodCount >= 2 && entities[i].EntityModel.IsGotHome)
                {
                    entities[i].gameObject.SetActive(false);
                    Evolve(entities[i]);
                }
                else if (entities[i] != null && entities[i].EntityModel.FoodCount == 1 && entities[i].EntityModel.IsGotHome)
                {
                    entities[i].gameObject.SetActive(false);
                }
                else
                {
                    Destroy(entities[i].gameObject);
                }
            }
            StartNewDay();
        }
    }

    private void StartNewDay()
    {
        EntityController[] entities = Resources.FindObjectsOfTypeAll<EntityController>();
        for (int i = 0; i < entities.Length - 1; i++)
        {
            entities[i].gameObject.SetActive(true);
        }

        _food = GameObject.FindGameObjectsWithTag("Food");
        for (int i =0; i < _food.Length; i++)
        {
            Destroy(_food[i]);
        }
        for (int i = 0; i < FoodCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-24f, 24f), 0f, Random.Range(-24f, 24f));
            GameObject newFood = Instantiate(_foodPrefab, position, Quaternion.identity);
            newFood.transform.parent = _foodParent;
        }

        IsDayRunning = true;
    }

    private void SetÑharacteristics(EntityController entityController, float speed, float size, float maxHP, float damage, Color color)
    {
        entityController.EntityModel.Speed = speed;
        entityController.EntityModel.Size = size;
        entityController.EntityModel.MaxHP = maxHP;
        entityController.EntityModel.Damage = damage;
        entityController.EntityModel.Color = color;
    }

    private void Evolve(EntityController entity)
    {
        Vector3 position = new Vector3(Random.Range(-24f, 24f), 0f, Random.Range(-24f, 24f));
        GameObject newEntity = Instantiate(_entityPrefab, position, Quaternion.identity);
        newEntity.transform.parent = _entityParent;
        SetÑharacteristics(newEntity.GetComponent<EntityController>(),
            entity.EntityModel.Speed + Random.Range(-1f, 1f),
            entity.EntityModel.Size + Random.Range(-0.5f, 0.5f),
            entity.EntityModel.MaxHP + Random.Range(-2f, 2f),
            entity.EntityModel.Damage + Random.Range(-1f, 1f),
            new Color(entity.EntityModel.Damage / 50, entity.EntityModel.MaxHP / 100, (entity.EntityModel.Speed - 8) / 100));
    }
}
