using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public CharacterController charController;
    public float crouchSpeed, normalHeight, crouchHeight;
    public Vector3 offset;
    public Transform player;
    bool crouching;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = !crouching;
        }
        if(crouching == true)
        {
            charController.height = charController.height - crouchSpeed * Time.deltaTime;
            if(charController.height <= crouchHeight)
            {
                charController.height = crouchHeight;
            }
        }
        if (crouching == false)
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