using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    CharacterController controller;
    public int gravityTicks = 150;
    public float movementPower = 5f;
    public float jumpPower = 2f;


    bool jumpHeld = false;
    float currentYVelo = 0f;
    int jumpCount = 0;
    int lastFrame = 0;
    int lastJumpFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Debug.Log($"{GetType().Name} Initialized");
    }

    // Update is called once per frame
    void Update()
    {
        currentYVelo -= (jumpPower / gravityTicks) * movementPower;

        if (controller.isGrounded && lastFrame < lastJumpFrame + 150) {
            currentYVelo = 0;
        }

        if (Input.GetKey(KeyCode.Space) && controller.isGrounded && lastFrame > lastJumpFrame + 150 && !jumpHeld) {
            currentYVelo = jumpPower*movementPower;
            jumpCount++;
            lastJumpFrame = lastFrame++;
        }

        float speedBuff = 1f;

        if (Input.GetKey(KeyCode.LeftShift)) {
            speedBuff = 3f;
        }

        jumpHeld = Input.GetKey(KeyCode.Space);
        controller.Move((Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward)*Time.deltaTime*movementPower*speedBuff);
        controller.Move(new Vector3(0, currentYVelo*Time.deltaTime, 0));

        lastFrame++;
    }
}
