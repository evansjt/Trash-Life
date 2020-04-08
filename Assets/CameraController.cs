using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform pivot;

    [SerializeField] float rotationSpeed = 3f;

    float prevXMousePosition;

    float currentYRotation;

    bool previouslyPressed = false;

    private void Awake()
    {
        pivot = transform.parent;
        currentYRotation = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if(!previouslyPressed)
            {
                prevXMousePosition = Input.mousePosition.x;
                previouslyPressed = true;
                
            }
            float mousePos = Input.mousePosition.x;
            float delta = mousePos - prevXMousePosition;
            pivot.localRotation = Quaternion.Euler(0f, (pivot.localRotation.y + (delta*rotationSpeed)), 0f);

        } else
        {
            previouslyPressed = false;
        }

        
    }
}
