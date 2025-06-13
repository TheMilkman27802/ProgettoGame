using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LomoCameraShooter : MonoBehaviour
{
    public float rayRange;
    private GhostBehaviour ghostBehaviour;
    public Transform mainCamera;
    public GameObject flash;
    void Start()
    {
        flash.SetActive(false);
        rayRange = 50f;
    }

    void Update()
    {
        Debug.DrawRay(mainCamera.position, mainCamera.forward * rayRange, Color.blue);
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(MantainLight());
            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit hitInfo, rayRange)
            && hitInfo.transform.gameObject.GetComponent<GhostBehaviour>())
            {
                ghostBehaviour = hitInfo.transform.gameObject.GetComponent<GhostBehaviour>();
                if (ghostBehaviour.ghostType == GhostBehaviour.GhostType.Phantom)
                {
                    print("Phantom colpito");
                }
            }
        }
    }
    private IEnumerator MantainLight()
    {
        flash.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        flash.SetActive(false);
    }
}
