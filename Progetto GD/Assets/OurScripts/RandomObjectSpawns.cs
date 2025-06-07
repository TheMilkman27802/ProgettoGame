using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawns : MonoBehaviour
{
    public Transform obj;
    public Transform[] spawnPoints;
    void Start()
    {
        int indexNumber = Random.Range(0, spawnPoints.Length);
        obj.position = spawnPoints[indexNumber].position;
    }

    
    void Update()
    {
        
    }
}
