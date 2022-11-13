using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool canDamage = true;
    float cooldown = 1.3f;
    public float health = 0f;
    [SerializeField] public float maxHealth;

    void Start()
    {
        health = maxHealth;
    }
    void Update()
    {
        //add healthbar logic here eventually
    }

    public void Damage(float damage)
    {
        canDamage = false;
        health -= damage;
        StartCoroutine(ResetPrimaryAttack());
    }

    IEnumerator ResetPrimaryAttack()
    {
        yield return new WaitForSeconds(cooldown);
        canDamage = true;
    }
}
