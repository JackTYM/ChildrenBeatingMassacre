using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyHandler : MonoBehaviour
{
    public float currentCurrency = 0f;

    public bool editCurrency(float amount) {
        if (currentCurrency + amount < 0) {
            return false;
        }

        currentCurrency += amount;

        return true;
    }
}
