using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIHandler : MonoBehaviour
{
    Slider sd;
    Text textBox;
    HealthController hc;

    // Start is called before the first frame update
    void Start()
    {
        textBox = transform.Find("Health Number").GetComponent<Text>();
        hc = GameObject.Find("Player").GetComponent<HealthController>();
        sd = GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        sd.value = hc.health / hc.maxHealth * 100;
        textBox.text = "" + hc.health;
        
    }
}
