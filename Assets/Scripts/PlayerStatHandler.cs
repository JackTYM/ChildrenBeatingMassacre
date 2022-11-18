using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class PlayerStatHandler : MonoBehaviour
{

    //Health
    public float maxHealth = 20f;
    public float healPercent = 0.0f;
    public float health = 20f;
    
    Stopwatch healTimer = Stopwatch.StartNew();

    //Rage
    public float maxRage = 100f;
    public float currentRage;

    //Currency
    public float currentCurrency = 0f;
    
    //Power-Ups
    public float currentStrengthMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check for death
        if (health == 0) {
            Destroy(this.gameObject);
            GameObject.Find("Enemy").GetComponent<AIPath>().enabled = false;
            UnityEngine.Debug.Log("Dead. Killing AI");
        }

        //Heal x per second
        if (healTimer.ElapsedMilliseconds > 1000 && health + maxHealth*healPercent/100 < maxHealth) {
            health += maxHealth*healPercent/100;
            healTimer = Stopwatch.StartNew();
        }


        //Round health to tenths place
        health = Mathf.Round(health*10)/10;


    }

    public void takeDamage(float damage) {
        if (health-damage > 0) {
            health -= damage;
        } else {
            health = 0;
        }
    }

    public bool editCurrency(float amount) {
        if (currentCurrency + amount < 0) {
            return false;
        }

        currentCurrency += amount;

        return true;
    }
    
    public void addRage(float rageAmount)
    {
        currentRage += rageAmount;

        if (currentRage > maxRage)
        {
            currentRage = maxRage;
        }

    }
}
