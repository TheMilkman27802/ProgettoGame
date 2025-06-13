using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    private float walkSpeed;
    public float normalWalkSpeed = 6f;
    private float runSpeed;
    public float normalRunSpeed = 12f;
    public float gravity = 150f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    private bool crouching;
    [SerializeField] Transform basePoint;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    private bool canMove = true;
    public AudioSource walkingFootStepsSound;
    public AudioSource runningFootStepsSound;

    void Start()
    {
        runSpeed = normalRunSpeed;
        walkSpeed = normalWalkSpeed;
        crouching = false;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && canMove)
        {
            crouching = !crouching;
            if (crouching)
            {
                characterController.height = crouchHeight;
                walkSpeed = crouchSpeed;
                runSpeed = crouchSpeed;
                basePoint.Translate(0, 0.5505f, 0);    //per mettero ai piedi dell oggetto
            }
            else
            {
                var castOrigin = basePoint.position + new Vector3(0, crouchHeight, 0);
                if (Physics.Raycast(castOrigin, Vector3.up, out RaycastHit hit, Mathf.Infinity))
                {
                    if (hit.point.y > defaultHeight - crouchHeight + castOrigin.y)
                    {
                        characterController.height = defaultHeight;
                        walkSpeed = normalWalkSpeed;
                        runSpeed = normalRunSpeed;
                        basePoint.Translate(0, -0.5505f, 0);
                    }
                    else
                    {
                        crouching = !crouching;
                    }
                }
                else
                {
                    basePoint.Translate(0, -0.5505f, 0);
                    characterController.height = defaultHeight;
                    walkSpeed = normalWalkSpeed;
                    runSpeed = normalRunSpeed;
                }
            }

        }
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift) && characterController.height != crouchHeight)
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
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
    void OnGUI()
    {
        int size = 24;
        float posX = playerCamera.pixelWidth / 2 - size / 4;
        float posY = playerCamera.pixelHeight / 2 - size / 2;
        GUIStyle myStyle = new GUIStyle();
        myStyle.normal.textColor = Color.red;
        myStyle.fontStyle = FontStyle.Bold;
        myStyle.fontSize = size;
        GUI.Label(new Rect(posX, posY, size, size), ".", myStyle);
    }
}
