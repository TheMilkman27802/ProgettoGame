using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HolyWaterShooter : MonoBehaviour
{
    public float rayRange;
    public GameObject ghost;
    private GhostBehaviour ghostBehaviour;
    public Transform mainCamera;
    public GameObject particleSystem;
    void Start()
    {
        particleSystem.SetActive(false);
        rayRange = 50f;
    }
    void Update()
    {
        Debug.DrawRay(mainCamera.position, mainCamera.forward * rayRange, Color.blue);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            particleSystem.SetActive(true);
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit hitInfo, rayRange)
            && hitInfo.transform.gameObject.GetComponent<GhostBehaviour>())
            {
                ghostBehaviour = hitInfo.transform.gameObject.GetComponent<GhostBehaviour>();
                if (ghostBehaviour.ghostType == GhostBehaviour.GhostType.Demon)
                {
                    print("Demone colpito");
                    Destroy(ghost);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                }
            }
        }
        else
        {
            particleSystem.SetActive(false);
        }

    }
}
