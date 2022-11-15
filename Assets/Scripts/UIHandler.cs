using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    Slider sd;
    Text healthTextBox;
    Text currencyTextBox;
    PlayerStatHandler psh;

    // Start is called before the first frame update
    void Start()
    {
        healthTextBox = transform.Find("Health Container").Find("Health Number").GetComponent<Text>();
        currencyTextBox = transform.Find("Currency Container").Find("Currency Amount").GetComponent<Text>();
        psh = GameObject.Find("Player").GetComponent<PlayerStatHandler>();
        sd = transform.Find("Health Container").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (psh != null) {
            sd.value = psh.health / psh.maxHealth * 100;
            healthTextBox.text = psh.health.ToString();
            currencyTextBox.text = psh.currentCurrency.ToString();
        }
    }
}
