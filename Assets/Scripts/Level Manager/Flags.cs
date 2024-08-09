using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flags : MonoBehaviour
{
    [SerializeField]private Transform[] flags;
    private float _rotationSpeed = 500f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var flag in flags)
            {
                StartCoroutine(RaiseFlag(flag));
            }
        }
    }
    
    private IEnumerator RaiseFlag(Transform flag)
    {
        Quaternion targetRotation = Quaternion.Euler(0, flag.eulerAngles.y, 0);
        while (flag.eulerAngles.z > 0)
        {
            flag.rotation = Quaternion.RotateTowards(flag.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
