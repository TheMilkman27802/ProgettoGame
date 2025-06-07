using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
   public float speed = 3.0f;
   public float obstacleRange = 5.0f;
   private bool isAlive;
    void Start()
    {
        
    }

    
    void Update()
    {
      transform.Translate(0, 0, speed * Time.deltaTime);

    }
}
