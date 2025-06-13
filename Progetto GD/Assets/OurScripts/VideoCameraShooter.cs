using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoCameraShooter : MonoBehaviour
{
    public float rayRange;
    public GameObject ghost;
    private GhostBehaviour ghostBehaviour;
    public Transform mainCamera;
    void Start()
    {
        rayRange = 50f;
    }

    void Update()
    {
        Debug.DrawRay(mainCamera.position, mainCamera.forward*rayRange, Color.blue);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit hitInfo, rayRange)
            && hitInfo.transform.gameObject.GetComponent<GhostBehaviour>())
            {
                ghostBehaviour = hitInfo.transform.gameObject.GetComponent<GhostBehaviour>();
                if (ghostBehaviour.ghostType == GhostBehaviour.GhostType.Thaye)
                {
                    print("Thaye colpito");
                    Destroy(ghost);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                }
            }
        }
    }
}
