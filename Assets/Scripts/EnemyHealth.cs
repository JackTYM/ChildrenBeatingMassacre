using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public bool canDamage = true;
    public float cooldown = 1.3f;
    public float health = 0f;
    public float maxHealth;
    public float killWorth = 1f;
    public float regenPercent;

    Slider slider;
    Stopwatch sw = Stopwatch.StartNew();
    GameObject player;
    void Start()
    {
        health = maxHealth;
        slider = GetComponentInChildren<Slider>();
        player = GameObject.Find("Player");
    }
    void Update()
    {
        slider.value = health / maxHealth * 100;
        if (sw.ElapsedMilliseconds > 1000 && health + maxHealth * regenPercent / 100 < maxHealth)
        {
            health += maxHealth * regenPercent / 100;
            sw = Stopwatch.StartNew();
        }

        if (health <= 0)
        {
            player.GetComponent<PlayerStatHandler>().editCurrency(killWorth);
            Destroy(gameObject);
        }
    }

    public void Damage(float damage)
    {
        canDamage = false;
        health -= damage;
        StartCoroutine(ResetIFrames());
    }

    IEnumerator ResetIFrames()
    {
        yield return new WaitForSeconds(cooldown);
        canDamage = true;
    }
}
