using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    Transform playerCameraTarget;


    private void Start()
    {
        playerCameraTarget = GameObject.FindGameObjectWithTag("Player").transform.Find("CameraTarget");
    }

    private void LateUpdate()
    {
        transform.position = playerCameraTarget.position;
    }
}
