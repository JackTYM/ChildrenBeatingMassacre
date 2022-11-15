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

    InventoryHandler ih;

    void Start()
    {
        primaryAnim = GetComponent<Animation>();
        ih = GameObject.Find("Player").GetComponent<InventoryHandler>();
    }

    void Update()
    {
        if (ih.selectedObject == transform.gameObject) {
            transform.GetComponent<MeshRenderer>().enabled = true;
            if (transform.parent.name == "Player") {
                if (Input.GetMouseButtonDown(0))
                {
                    if (canPrimaryAttack && !primaryAnim.isPlaying)
                    {
                        Debug.Log("Prim Attack");
                        PrimaryAttack();
                    }
                }
            }
        } else {
            transform.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void PrimaryAttack()
    {
        canPrimaryAttack = false;
        primaryAnim.Play(primaryAnimation);
        StartCoroutine(ResetPrimaryAttack());
    }

    IEnumerator ResetPrimaryAttack()
    {
        yield return new WaitForSeconds(primaryCooldown + primaryAnim.GetClip(primaryAnimation).length);
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