using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoostComponent : MonoBehaviour
{
    [Header("[CarBoostData]")]
    public int boostAmount = 50;
    public float boostMultiplier = 2;
    public int boostConsumptionRate = 5;
    public float currentBoostAmount;
    public void Start()
    {
        currentBoostAmount = boostAmount;
    }

    
    public float GetCurrentBoostAmount()
    {
        return currentBoostAmount;
    }
    public bool IsBoostEmpty()
    {
        return currentBoostAmount > 0f;
    }

    public float Boost(float currentVelocity)
    {
        if (IsBoostEmpty())
        {
            currentBoostAmount -= boostConsumptionRate;
            Debug.Log($"BoostLeft: {currentBoostAmount}");
            return currentVelocity * boostMultiplier;
        }
        else
        {
            Debug.Log("No boost");
            return currentVelocity;
        }
    }
    
}
