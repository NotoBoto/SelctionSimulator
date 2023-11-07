using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EntityController : MonoBehaviour
{
    [HideInInspector]
    public EntityModel EntityModel;
    private EntityView _entityView;
    
    private GameController _gameController;

    [HideInInspector]
    public Renderer _entityRenderer;
    [HideInInspector]
    public NavMeshAgent _entityAgent;

    private void Awake()
    {
        EntityModel = new EntityModel();
        _entityView = GetComponent<EntityView>();

        _gameController = FindAnyObjectByType<GameController>();

        _entityRenderer = GetComponent<Renderer>();
        _entityAgent = GetComponent<NavMeshAgent>();

        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke("Change—haracteristics", 0.01f);


        EntityModel.FoodCount = 0;
        EntityModel.HomePosition = transform.position;
        EntityModel.IsGoingHome = false;
        EntityModel.IsGotHome = false;
    }

    private void Update()
    {
        if (_gameController.IsGameStarted && !_gameController.IsGamePaused && _gameController.IsDayRunning)
        {
            _entityAgent.isStopped = false;
            if (EntityModel.FoodCount < 2 && !EntityModel.IsGoingHome)
            {
                FindNearestTarget();
            }
            else
            {
                _entityAgent.SetDestination(EntityModel.HomePosition);
                if (_entityAgent.remainingDistance <= 0.25f)
                {
                    EntityModel.IsGotHome = true;
                    gameObject.SetActive(false);
                    _gameController.NextDay();
                }
            }
        }
        else
        {
            _entityAgent.isStopped = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            EntityModel.FoodCount++;
        }
        else if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<EntityController>().EntityModel.HP -= EntityModel.Damage;
        }

        if(EntityModel.HP <= 0) 
        {
            Destroy(this.gameObject);
        }
    }

    private void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Food");

        if (targets.Length > 0 )
        {
            Transform nearestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestTarget = target.transform;
                }
            }

            if (nearestTarget != null)
            {
                _entityAgent.SetDestination(nearestTarget.position);
            }
        }
        else
        {
            EntityModel.IsGoingHome = true;
        }
    }

    public void Change—haracteristics()
    {
        _entityAgent.speed = EntityModel.Speed;
        _entityAgent.angularSpeed = EntityModel.Speed * (120 / _gameController.StartSpeed);
        transform.localScale = new Vector3(EntityModel.Size, 1f, EntityModel.Size);
        EntityModel.HP = EntityModel.MaxHP;
        _entityRenderer.material.color = EntityModel.Color;
    }
}
