using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour
{
   private float moveSpeed;
   public float walkSpeed = 4.0f;
   public float sprintSpeed = 8.0f;
   public float gravity = -9.8f;


    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode fowardKey = KeyCode.W;
    public KeyCode behindKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public AudioSource walkingFootStepsSound;
    public AudioSource runningFootStepsSound;

   private CharacterController charController;
    void Start()
    {
        charController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(fowardKey) || Input.GetKey(behindKey) || Input.GetKey(leftKey) || Input.GetKey(rightKey))
        {
            if (Input.GetKey(sprintKey))
            {
                runningFootStepsSound.enabled = true;
            }
            else
            {
                walkingFootStepsSound.enabled = true;
            }
        }
        else
        {
            runningFootStepsSound.enabled = false;
            walkingFootStepsSound.enabled = false;
        }
        moveSpeed = Input.GetKey(sprintKey) ? sprintSpeed:walkSpeed;
        float deltaX = Input.GetAxis("Horizontal")*moveSpeed;
        float deltaZ = Input.GetAxis ("Vertical")*moveSpeed;
        Vector3 movement = new Vector3(deltaX,0, deltaZ);
        movement = Vector3.ClampMagnitude(movement,moveSpeed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }
}
