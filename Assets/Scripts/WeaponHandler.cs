using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class WeaponHandler : MonoBehaviour
{
    public bool canPrimaryAttack = true;
    public bool canSecondaryAttack = true;
    public float primaryCooldown = 1.0f;
    public float secondaryCooldown = 5.0f;
    public float rageCost = 20f;
    Animation primaryAnim;
    public string primaryAnimation;
    public float damage;
    //hammer

    public GameObject hammer;
    public string hammerVariant;
    public bool secondaryActive;
    //other stuff
    PlayerStatHandler psh;
    InventoryHandler ih;
    GameObject player;

    //for secondary weapons
    public string weaponGroup;

    void Start()
    {
        primaryAnim = GetComponent<Animation>();
        ih = GameObject.Find("Player").GetComponent<InventoryHandler>();
        psh = GameObject.Find("Player").GetComponent<PlayerStatHandler>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (ih.selectedObject == transform.gameObject)
        {
            if(weaponGroup == "Hammer" && secondaryActive)
            {
                    transform.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                transform.GetComponent<MeshRenderer>().enabled = true;
            }
            if (transform.parent.name == "Player")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (canPrimaryAttack && !primaryAnim.isPlaying && !secondaryActive)
                    {
                        PrimaryAttack();
                    }
                }

                //secondary attack
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (canSecondaryAttack && psh.currentRage >= rageCost)
                    {
                        switch(weaponGroup)
                        {
                            case "Hammer":
                                psh.currentRage -= rageCost;
                                primaryAnim.Play("HammerThrow");
                                StartCoroutine(HammerWait());
                                return;
                        }
                    }
                }

            }
        }
        else
        {
            transform.GetComponent<MeshRenderer>().enabled = false;
        }
    }


    public void PrimaryAttack()
    {
        canPrimaryAttack = false;
        primaryAnim.Play(primaryAnimation);
        StartCoroutine(ResetPrimaryAttack());
    }

    //cooldown stuff
    IEnumerator ResetPrimaryAttack()
    {
        yield return new WaitForSeconds(primaryCooldown + primaryAnim.GetClip(primaryAnimation).length);
        canPrimaryAttack = true;
    }
    IEnumerator ResetSecondaryAttack(float duration)
    {
        yield return new WaitForSeconds(secondaryCooldown + duration);
        canSecondaryAttack = true;
    }

    IEnumerator HammerWait()
    {
        canSecondaryAttack = false;
        yield return new WaitForSeconds(primaryAnim.GetClip("HammerThrow").length);
        GameObject activeHammer = Instantiate(hammer, player.transform.position, player.transform.rotation);
        float addseconds = activeHammer.GetComponent<HammerThrow>().duration;
        StartCoroutine(ResetSecondaryAttack(addseconds));

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
                    enemyHealth.Damage(damage * psh.currentStrengthMultiplier);
                }
            }
        }
    }
}