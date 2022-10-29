using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    float xSensitivity = 3f;
    float ySensitivity = 1f;

    float xOffset = 0f;
    float yOffset = 0.5f;
    float zOffset = 0f;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            transform.position = player.transform.position + new Vector3(xOffset, yOffset, zOffset);

            float mouseXMovement = Input.GetAxis("Mouse X");
            float mouseYMovement = Input.GetAxis("Mouse Y");

            player.transform.eulerAngles = player.transform.eulerAngles - new Vector3(0, -mouseXMovement * xSensitivity, 0);
            transform.eulerAngles = transform.eulerAngles - new Vector3(mouseYMovement * ySensitivity, -mouseXMovement * xSensitivity, 0);
        }
    }
}
