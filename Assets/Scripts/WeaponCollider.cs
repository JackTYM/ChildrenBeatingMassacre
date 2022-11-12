using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        if (transform.parent.GetComponent<WeaponHandler>().swinging) {
            if (collider.gameObject.tag == "Enemy") {
                Destroy(collider.gameObject);
            }
        }
    }
}
