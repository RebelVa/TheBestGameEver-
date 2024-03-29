using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySP1 : MonoBehaviour
{
    private List<Transform> _spawnerPoints;

    private void Start()
    {
        _spawnerPoints = new List<Transform>(transform.GetComponentsInChildren<Transform>());
    }
}
