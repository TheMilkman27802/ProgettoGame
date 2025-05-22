using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum MouseRotation {
        HorizontalRotation,
        VerticalRotation,
        BothRotation
    }

    public float horSensitivity = 9.0f;
    public float vertSensitivity = 9.0f;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;
    private float verticalRot = 0;
    private float horizontalRot = 0;
    public MouseRotation mouseRotation = MouseRotation.BothRotation;
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null){
            body.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(mouseRotation) {
            case MouseRotation.HorizontalRotation:
            transform.Rotate(0, Input.GetAxis("Mouse X")* horSensitivity, 0);
            break;
            case MouseRotation.VerticalRotation:
            verticalRot -= Input.GetAxis ("Mouse Y")* vertSensitivity;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);
            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
            break;
            case MouseRotation.BothRotation:
            verticalRot -= Input.GetAxis ("Mouse Y") * vertSensitivity;
            horizontalRot += Input.GetAxis ("Mouse X")* horSensitivity;
            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
            break;


        }
    }
}
