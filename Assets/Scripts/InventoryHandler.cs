using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{

    public int inventorySlots = 2;
    public GameObject[] currentInventory;
    public int selectedSlot = 0;
    public GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        currentInventory = new GameObject[inventorySlots];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < 9; i++) {
            int keyCode = 48+i;
            if (i <= inventorySlots) {
                if (Input.GetKeyDown((KeyCode)keyCode)) {
                    selectedSlot = i-1;
                    selectedObject = currentInventory[selectedSlot];
                }
            } else {
                break;
            }
        }
    }

    public void addItem(GameObject item, Vector3 positionOffset, Vector3 rotationOffset) {

        GameObject newWeapon = GameObject.Instantiate(item);

        newWeapon.name = item.name;
        newWeapon.GetComponent<MeshRenderer>().enabled = true;
        newWeapon.transform.SetParent(transform);
        newWeapon.transform.localPosition = positionOffset;
        newWeapon.transform.eulerAngles = transform.eulerAngles + rotationOffset;

        if (getAmountOfObjects() <= inventorySlots) {
            //Not full
            currentInventory[getAmountOfObjects()] = newWeapon;

            selectedSlot = getAmountOfObjects()-1;
            selectedObject = currentInventory[selectedSlot];
        } else {
            //Full
            GameObject.Destroy((GameObject)currentInventory[selectedSlot]);

            currentInventory[selectedSlot] = newWeapon;
            selectedObject = currentInventory[selectedSlot];
        }
    }

    public int getAmountOfObjects() {
        int objectAmount = 0;

        for (int i = 0; i < inventorySlots; i++) {
            if (currentInventory[i] != null) {
                objectAmount++;
            }
        }

        return objectAmount;
    }
}
