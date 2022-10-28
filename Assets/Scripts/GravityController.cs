using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{

    GameObject player;
    CharacterController controller;
    MovementController movementController;

    float currentYVelo = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        controller = GetComponent<CharacterController>();
        movementController = player.GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentYVelo -= (movementController.jumpPower / movementController.gravityTicks) * movementController.movementPower;

        if (controller.isGrounded) {
            currentYVelo = 0;
        }

        controller.Move(new Vector3(0, currentYVelo*Time.deltaTime, 0));
    }
}
