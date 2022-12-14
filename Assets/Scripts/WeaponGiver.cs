using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGiver : MonoBehaviour
{

    public GameObject weapon;
    public Vector3 positionOffset = new Vector3(0,0,0);
    public Vector3 rotationOffset = new Vector3(0,0,80);
    public float cost = 1f;

    GameObject player;
    GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        canvas = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(player.transform.position, player.transform.forward, out var hit, 3f)) {
            var hitObj = hit.collider.gameObject;

            if (hitObj.Equals(transform.gameObject)) {
                canvas.GetComponent<Canvas>().enabled = true;
                if (!playerOwnsWeapon()) {
                    canvas.GetComponentInChildren<Text>().text = weapon.name + "\n" + cost + " Dead Child";

                    if (cost != 1) {
                        canvas.GetComponentInChildren<Text>().text = canvas.GetComponentInChildren<Text>().text + "ren";
                    }

                    if (Input.GetKeyDown(KeyCode.E)) {
                        if (player.GetComponent<PlayerStatHandler>().editCurrency(cost*-1)) {
                            player.GetComponent<InventoryHandler>().addItem(weapon, positionOffset, rotationOffset);
                        }
                    }
                } else {
                    canvas.GetComponentInChildren<Text>().text = "Weapon Already Owned!";
                }
                return;
            }
        }
        canvas.GetComponent<Canvas>().enabled = false;
    }

    bool playerOwnsWeapon() {
        return player.transform.Find(weapon.name) != null;
    }
}
