using TMPro;
using UnityEngine;

public class InventoryUIHandler : MonoBehaviour
{
    public GameObject[] weapons;
    InventoryHandler inventory;
    GameObject[] pInventory;
    TextMeshProUGUI weaponName;
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<InventoryHandler>();
        weaponName = transform.Find("Weapon Name").GetComponent<TextMeshProUGUI>();
    }

    void genInvWeapon(string name, int slot, int prefabIndex, float xScale, float yScale, float zScale)
    {
        if (transform.Find("Slot " + slot).childCount != 0)
        {
            if (!transform.Find("Slot " + slot).GetChild(0).name.Equals(name + "(Clone)"))
            {
                for (int i = 0; i < transform.Find("Slot " + slot).childCount; i++)
                {
                    Destroy(transform.Find("Slot " + slot).GetChild(i).gameObject);
                }

                GameObject weapon = Instantiate(weapons[prefabIndex]);
                weapon.transform.parent = transform.Find("Slot " + slot).transform;
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localScale = new Vector3(xScale, yScale, zScale);
                weapon.GetComponent<MeshRenderer>().enabled = true;

            }
        }
        else
        {
            GameObject weapon = Instantiate(weapons[prefabIndex]);
            weapon.transform.parent = transform.Find("Slot " + slot).transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localScale = new Vector3(xScale, yScale, zScale);
            weapon.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    
    void Update()
    {
        pInventory = inventory.currentInventory;
        weaponName.SetText("Weapon: " + inventory.selectedObject.name);

        //Slot 1
        switch (pInventory[0].name)
        {
            case "Toy Hammer":
                genInvWeapon("Toy Hammer", 1, 0, 5000, 5000, 5000);
                break;
            case "Toy Bat":
                genInvWeapon("Toy Bat", 1, 1, 5, 5, 37.5f);
                break;

        }

        //Slot
        switch (pInventory[1].name)
        {
            case "Toy Hammer":
                genInvWeapon("Toy Hammer", 2, 0, 5000, 5000, 5000);
                break;
            case "Toy Bat":
                genInvWeapon("Toy Bat", 2, 1, 5, 5, 37.5f);
                break;

        }
    }
}
