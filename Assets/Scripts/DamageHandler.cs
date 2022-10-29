using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public GameObject victim;
    public float damage = 1f;
    public long millisecondsPerHit = 1000;

    Stopwatch sw = Stopwatch.StartNew();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject == victim && sw.ElapsedMilliseconds > millisecondsPerHit) {
            victim.GetComponent<HealthController>().takeDamage(damage);
            sw = Stopwatch.StartNew();
        }
    }
}
