using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public PlayerController player;
    public float viewAngle;
    
    private NavMeshAgent _nevMeshAgent;
    private bool _isPlayerNoticed;

    // Start is called before the first frame update
    void Start()
    {
        InitComponentLinks();
        PickNewPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {
        NoticePlayerUpdate();
        ChaseUpdate();
        PickUpdate();
    }

    private void NoticePlayerUpdate()
    {
        var direction = player.transform.position - transform.position;
        _isPlayerNoticed = false;
        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
            }
        }
    }

    private void ChaseUpdate()
    {
        if (_isPlayerNoticed == true)
        {
            _nevMeshAgent.destination = player.transform.position;
        }
    }

    private void PickUpdate()
    {
        if (_isPlayerNoticed == false)
        {
            if (_nevMeshAgent.remainingDistance == 0)
            {
                PickNewPatrolPoint();
            }
        }
    }

    private void InitComponentLinks()
    {
        _nevMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void PickNewPatrolPoint()
    {
        _nevMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }
}
