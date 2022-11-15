using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{

    public enum PowerUpType
        {
            InstantHealth, 
            Strength
        };
    public PowerUpType type;

    GameObject player;
    bool strengthOn = false;

    Stopwatch sw;
    InventoryHandler ih;

    float damagePercent = 150f;
    float healPercent = 100f;

    void Start() {
        player = GameObject.Find("Player");
        ih = player.GetComponent<InventoryHandler>();
    }

    void Update() {
        if (strengthOn) {
            if (sw.ElapsedMilliseconds > 15000) {
                sw.Stop();
                strengthOn = false;
                player.GetComponent<PlayerStatHandler>().currentStrengthMultiplier = 1f;
            }
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.Equals(player)) {
            switch (type) {
                case PowerUpType.InstantHealth:
                    player.GetComponent<PlayerStatHandler>().health = player.GetComponent<PlayerStatHandler>().maxHealth*healPercent/100f;
                    break;
                case PowerUpType.Strength:
                    sw = Stopwatch.StartNew();
                    strengthOn = true;
                    player.GetComponent<PlayerStatHandler>().currentStrengthMultiplier = 1.5f;
                    break;
            }
            GameObject.Destroy(transform.gameObject);
        }
    }
}
