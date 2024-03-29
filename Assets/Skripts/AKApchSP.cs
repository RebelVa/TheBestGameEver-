using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKApchSP : MonoBehaviour
{
    public Apch apchPrefab;
    public float delayMin = 3;
    public float delayMax = 9;
    
    private List<Transform> _spawnerPoints;
    
    private Apch _apch;

    private void Start()
    {
        _spawnerPoints = new List<Transform>(transform.GetComponentsInChildren<Transform>());
    }

    private void Update()
    {
        if (_apch != null) return;
        if (IsInvoking()) return;

        Invoke("CreateApch", Random.Range(delayMin, delayMax));
    }

    private void CreateApch()
    {
        _apch = Instantiate(apchPrefab);
        _apch.transform.position = _spawnerPoints[Random.Range(0, _spawnerPoints.Count)].position;
    }
}
