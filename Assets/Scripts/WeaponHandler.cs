using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    GameObject player;
    Animation swingAnim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        swingAnim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (swingAnim.isPlaying) return;
        if (Input.GetMouseButton(0)) {
            Debug.Log("Swing!");
            swingAnim.Play("Swing");
        }
    }
}
