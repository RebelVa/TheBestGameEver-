using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float value = 100;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DealDamage(float damage)
    {
        value -= damage;
        if (value <= 0)
        {
            animator.SetTrigger("boom");
            GetComponent<EnemyAI>().enabled = false;
            GetComponent<NavMeshAgent> ().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }
}
