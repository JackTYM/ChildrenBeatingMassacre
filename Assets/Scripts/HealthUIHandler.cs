using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIHandler : MonoBehaviour
{

    Text textBox;
    HealthController hc;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<Text>();
        hc = GameObject.Find("Player").GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (hc.health == 0) {
            textBox.text = "You died.";
        } else {
            textBox.text = "Health: " + hc.health + "/" + hc.maxHealth;
        }
    }
}
