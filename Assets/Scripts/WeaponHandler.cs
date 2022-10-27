using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

    float xOffset = 0.5f;
    float yOffset = 0.2f;
    float zOffset = -0.8f;

    float xRotOffset = 0f;
    float yRotOffset = -80f;
    float zRotOffset = 0f;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(player.transform.position + new Vector3(xOffset, yOffset, zOffset), player.transform.rotation*Quaternion.Euler(xRotOffset, yRotOffset, zRotOffset));
    }
}
