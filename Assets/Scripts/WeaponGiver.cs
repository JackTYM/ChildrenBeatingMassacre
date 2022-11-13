using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGiver : MonoBehaviour
{

    public GameObject weapon;
    public Vector3 positionOffset = new Vector3(0,0,0);
    public Vector3 rotationOffset = new Vector3(0,0,80);

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(player.transform.position, player.transform.forward, out var hit, 5f)) {
            var hitObj = hit.collider.gameObject;

            if (hitObj.tag.Equals("Weapon Giver")) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    weapon.GetComponent<MeshRenderer>().enabled = true;
                    weapon.transform.SetParent(player.transform);
                    weapon.transform.localPosition = positionOffset;
                    weapon.transform.eulerAngles = player.transform.eulerAngles + rotationOffset;
                }
            }
        }
    }
}
