using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public PlayerController player;
    public float viewAngle;
    public float damage = 30;
    public float attack = 1;
    public Animator animator;
    
    private NavMeshAgent _nevMeshAgent;
    private bool _isPlayerNoticed;
    private PlayerHealth _playerHealth;

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
        AttackUpdate();
        PickUpdate();
    }

    private void AttackUpdate()
    {
        if (_isPlayerNoticed)
        {
            if (_nevMeshAgent.remainingDistance <= _nevMeshAgent.stoppingDistance)
            {
                animator.SetTrigger("ata");
            }
        }
    }

    public void At()
    {
        if (!_isPlayerNoticed) return;
        if (_nevMeshAgent.remainingDistance > (_nevMeshAgent.stoppingDistance + attack)) return;
        
        _playerHealth.DealDamage(damage);
    }

    private void NoticePlayerUpdate()
    {
        _isPlayerNoticed = false;
        if (!_playerHealth.IsAlive()) return;

        var direction = player.transform.position - transform.position;
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
            if (_nevMeshAgent.remainingDistance <= _nevMeshAgent.stoppingDistance)
            {
                PickNewPatrolPoint();
            }
        }
    }

    private void InitComponentLinks()
    {
        _nevMeshAgent = GetComponent<NavMeshAgent>();
        _playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void PickNewPatrolPoint()
    {
        _nevMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }
}
