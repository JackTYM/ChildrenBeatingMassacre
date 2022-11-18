using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerThrow : MonoBehaviour
{

    GameObject player;
    GameObject weapon;
    Vector3 endPos;
    bool active;
    public float rotSpeed;
    public float speed;
    public float distance;
    public float duration;
    Animation anims;
    public float damage;
    Transform item;
    void Start()
    {
        player = GameObject.Find("Player");
        weapon = player.transform.Find("Toy Hammer").gameObject;
        item = transform.GetChild(0);
        anims = weapon.gameObject.GetComponent<Animation>();
        endPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + player.transform.forward * distance;
        StartCoroutine(SecondaryAttack());
    }

    void Update()
    {
        item.transform.Rotate(new Vector3(0, Time.deltaTime * rotSpeed, 0));
        item.transform.localPosition = Vector3.zero;

        if (active)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
        }

        if (!active)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
        }

        if (!active && Vector3.Distance(player.transform.position, transform.position) <= 1)
        {
            AnimationState returnHammer = anims.PlayQueued("HammerThrow", QueueMode.PlayNow);
            returnHammer.time = returnHammer.length;
            returnHammer.speed = -1f;
            weapon.GetComponent<WeaponHandler>().secondaryActive = false; 
            weapon.GetComponent<MeshRenderer>().enabled = true;
            Destroy(gameObject);
        }
    }

    IEnumerator SecondaryAttack()
    {
        active = true;
        weapon.GetComponent<WeaponHandler>().secondaryActive = true;
        yield return new WaitForSeconds(duration);
        active = false;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == ("Enemy"))
        {
            EnemyHealth eh = collider.gameObject.GetComponent<EnemyHealth>();
            if (eh.canDamage)
            {
                eh.Damage(damage);
            }
        }
    }
}
