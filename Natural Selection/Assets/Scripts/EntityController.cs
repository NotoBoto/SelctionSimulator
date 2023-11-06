using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.AI;

public class EntityController : MonoBehaviour
{
    [HideInInspector]
    public EntityModel EntityModel;
    private EntityView _entityView;
    
    private Renderer _entityRenderer;
    private NavMeshAgent _entityAgent;

    private void Awake()
    {
        EntityModel = new EntityModel();
        _entityView = GetComponent<EntityView>();

        _entityRenderer = GetComponent<Renderer>();
        _entityAgent = GetComponent<NavMeshAgent>();

        EntityModel.Speed = 1.0f;
        EntityModel.Size = 1.0f;
        EntityModel.MaxHP = 5.0f;
        EntityModel.HP = EntityModel.MaxHP;
        EntityModel.Damage = 1.0f;
        EntityModel.Color = Color.white;
        EntityModel.FoodCount = 0;
        EntityModel.HomePosition = transform.position;
    }

    private void Update()
    {
        if (EntityModel.FoodCount == 0)
        {
            FindNearestTarget();
        }
        else
        {
            _entityAgent.SetDestination(EntityModel.HomePosition);
            if (_entityAgent.remainingDistance <= 0.25f)
            {
                gameObject.SetActive(false);
            }
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

            foreach (GameObject _target in targets)
            {
                float distance = Vector3.Distance(transform.position, _target.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestTarget = _target.transform;
                }
            }

            if (nearestTarget != null)
            {
                _entityAgent.SetDestination(nearestTarget.position);
            }
        }
        else
        {
            _entityAgent.isStopped = true;
        }
    }
}
