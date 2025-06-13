using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public CharacterController charController;
    public float crouchSpeed, normalHeight, crouchHeight;
    public Vector3 offset;
    public Transform player;
    private bool crouching;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = !crouching;
        }
        if(crouching)
        {
            charController.height = charController.height - crouchSpeed * Time.deltaTime;
            if(charController.height <= crouchHeight)
            {
                charController.height = crouchHeight;
            }
        }
        if (!crouching)
        {
            var castOrigin = charController.transform.position + new Vector3(0, normalHeight - crouchHeight, 0);
            if (Physics.Raycast(castOrigin, Vector3.up, out RaycastHit hit, 0.2f))
            {
                if (hit.point.y > normalHeight + castOrigin.y)
                {
                    charController.height = charController.height + crouchSpeed * Time.deltaTime;
                    if (charController.height < normalHeight)
                    {
                        player.position = player.position + offset * Time.deltaTime;
                    }
                    if (charController.height >= normalHeight)
                    {
                        charController.height = normalHeight;
                    }
                }
                else
                {
                    crouching = !crouching;
                }
            }
            else
            {
                charController.height = charController.height + crouchSpeed * Time.deltaTime;
                    if (charController.height < normalHeight)
                    {
                        player.position = player.position + offset * Time.deltaTime;
                    }
                    if (charController.height >= normalHeight)
                    {
                        charController.height = normalHeight;
                    }
            }
        }
    }
}