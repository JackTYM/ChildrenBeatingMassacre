using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHandler : MonoBehaviour
{
    public bool canPrimaryAttack = true;
    public float primaryCooldown = 1.0f;
    Animation primaryAnim;
    public string primaryAnimation;
    public float damage;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canPrimaryAttack)
            {
                PrimaryAttack();
            }
        }
    }

    public void PrimaryAttack()
    {

        canPrimaryAttack = false;
        primaryAnim = GetComponent<Animation>();
        primaryAnim.Play(primaryAnimation);
        StartCoroutine(ResetPrimaryAttack());

    }

    IEnumerator ResetPrimaryAttack()
    {
        yield return new WaitForSeconds(primaryCooldown);
        canPrimaryAttack = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (primaryAnim.isPlaying)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth.canDamage)
                {
                    enemyHealth.Damage(damage);
                }
            }
        }

    }
}