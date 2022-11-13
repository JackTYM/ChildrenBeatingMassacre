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
    HealthController hc;
    CurrencyHandler ch;

    // Start is called before the first frame update
    void Start()
    {
        healthTextBox = transform.Find("Health Container").Find("Health Number").GetComponent<Text>();
        currencyTextBox = transform.Find("Currency Container").Find("Currency Amount").GetComponent<Text>();
        hc = GameObject.Find("Player").GetComponent<HealthController>();
        ch = GameObject.Find("Player").GetComponent<CurrencyHandler>();
        sd = transform.Find("Health Container").GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hc != null) {
            sd.value = hc.health / hc.maxHealth * 100;
            healthTextBox.text = hc.health.ToString();
        }
        if (ch != null) {
            currencyTextBox.text = ch.currentCurrency.ToString();
        }
    }
}
