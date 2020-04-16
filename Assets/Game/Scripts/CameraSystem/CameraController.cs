using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float mouseSens = 7f;

    [SerializeField] float minYRotation = -20f;
    [SerializeField] float maxYRotation = 70f;

    [SerializeField] float cameraDistance = 5f;

    Transform pivot;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Awake()
    {
        pivot = transform.parent;
        
    }


    private void LateUpdate()
    {
        //if (!Input.GetMouseButton(1)) return;         If this is uncommented, then the player must press the mouse button in order to move the camera

        xRotation += Input.GetAxis("Mouse X") * mouseSens;
        yRotation -= Input.GetAxis("Mouse Y") * mouseSens;

        yRotation = Mathf.Clamp(yRotation, minYRotation, maxYRotation);

        pivot.rotation = Quaternion.Euler(yRotation, xRotation, 0f);

        transform.position = (pivot.position - (transform.forward * cameraDistance));
    }
}
