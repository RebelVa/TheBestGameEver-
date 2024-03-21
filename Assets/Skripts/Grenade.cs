using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    public void OnCollisionEnter(Collision collision)
    {
        Invoke("Explosion", delay);
    }

    public void Explosion()
    {
        Destroy(gameObject);
        var eexplosion = Instantiate(explosionPrefab);
        eexplosion.transform.position = transform.position;
    }
}
