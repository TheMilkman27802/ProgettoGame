using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucifixShooter : MonoBehaviour
{
    public float rayRange;
    private GhostBehaviour ghostBehaviour;
    public Transform laserOrigin;
    public Camera mainCamera;
    private LineRenderer laserLine;
    public float fireRate = 0.2f;
    private float fireTimer;
    public float laserDuration = 0.5f;
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        rayRange = 50f;
    }
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && fireTimer > fireRate)
        {
            fireTimer = 0;
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hitInfo, rayRange)
            && hitInfo.transform.gameObject.GetComponent<GhostBehaviour>())
            {
                laserLine.SetPosition(1, hitInfo.point);
                ghostBehaviour = hitInfo.transform.gameObject.GetComponent<GhostBehaviour>();
                if (ghostBehaviour.ghostType == GhostBehaviour.GhostType.Revenant)
                {
                    print("Revenant colpito");
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (mainCamera.transform.forward * rayRange));
            }
            StartCoroutine(ShootLaser());
        }
    }
    private IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
