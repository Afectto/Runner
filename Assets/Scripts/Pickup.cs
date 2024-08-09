using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]private float value = 5;
    public Action<float> OnPickupItem;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickupItem?.Invoke(value);
            Destroy(gameObject);
        }
    }
}
