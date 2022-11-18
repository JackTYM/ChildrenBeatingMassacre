using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    Slider sd;
    Text healthTextBox;
    Text currencyTextBox;
    PlayerStatHandler psh;


    public float inventoryRotationSpeed = 5f;
    InventoryHandler inventory;
    GameObject[] pInventory;
    TextMeshProUGUI weaponText;
    Stopwatch rageTimer = Stopwatch.StartNew();
    public float ragePercent = 0.0f;
    Slider rageSlider;

    // Start is called before the first frame update
    void Start()
    {
        healthTextBox = transform.Find("Health Container").Find("Health Number").GetComponent<Text>();
        currencyTextBox = transform.Find("Currency Container").Find("Currency Amount").GetComponent<Text>();
        psh = GameObject.Find("Player").GetComponent<PlayerStatHandler>();
        sd = transform.Find("Health Container").GetComponent<Slider>();
        rageSlider = transform.Find("Rage Container").Find("Rage Bar").GetComponent<Slider>();
        inventory = GameObject.Find("Player").GetComponent<InventoryHandler>();
        weaponText = transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (psh != null) {
            sd.value = psh.health / psh.maxHealth * 100;
            healthTextBox.text = psh.health.ToString();
            currencyTextBox.text = psh.currentCurrency.ToString();
            rageSlider.maxValue = psh.maxRage;
            rageSlider.value = psh.currentRage;
        }

        //Lose rage every second
        if (rageTimer.ElapsedMilliseconds > 5)
        {
            if (psh.currentRage - psh.maxRage * ((ragePercent/200)/ 100) >= 0)
            {
                psh.currentRage -= psh.maxRage * (ragePercent/200) / 100;
            }
            else if (psh.currentRage - psh.maxRage * ((ragePercent/200) / 100) < 0)
            {
                psh.currentRage = 0;
            }
            rageTimer = Stopwatch.StartNew();
        }

        //inventory UI management

        if (inventory.selectedObject != null) {
            weaponText.SetText("Current Weapon: " + inventory.selectedObject.name);
        }

        for (int i = 1; i <= inventory.getAmountOfObjects(); i++) {
            Transform currentSlot = transform.GetChild(2).transform.GetChild(i);

            //Render and Destroy Weapons
            if (currentSlot.childCount == 0 || currentSlot.GetChild(0).gameObject.name != inventory.currentInventory[i-1].gameObject.name) {
                GameObject weapon = inventory.currentInventory[i-1];
                GameObject newWeapon = new GameObject(weapon.name);

                for (int x = 0; x <= 2; x++) {
                    UnityEditorInternal.ComponentUtility.CopyComponent(weapon.GetComponents<Component>()[x]);
                    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(newWeapon);
                }

                newWeapon.layer = 5; //UI Layer
                newWeapon.transform.parent = currentSlot.transform;
                newWeapon.name = inventory.currentInventory[i-1].name;
                newWeapon.transform.localPosition = Vector3.zero;
                newWeapon.transform.localRotation = weapon.transform.localRotation;
                newWeapon.transform.localScale = new Vector3(weapon.transform.localScale.x*50f, weapon.transform.localScale.y*50f, weapon.transform.localScale.z*50f);
                newWeapon.GetComponent<MeshRenderer>().enabled = true;
            } else if (currentSlot.GetChild(0).gameObject.name != inventory.currentInventory[i-1].gameObject.name) {
                Destroy(currentSlot.GetChild(0).gameObject);
            }

            //Spin Weapons
            currentSlot.Rotate(0, inventoryRotationSpeed * Time.deltaTime, 0);
        }
    }
}
