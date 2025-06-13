using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class JumpscareScript : MonoBehaviour
{
    public GameObject ghost;
    public string scenename;

    // Update is called once per frame
    void Update()
    {
        OnTriggerEnter(ghost.GetComponent<BoxCollider>());
    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            SceneManager.LoadScene(scenename);
        }
    }
}
