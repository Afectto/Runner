using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]private float value = 5;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerController>().AddValue(value);
            Destroy(gameObject);
        }
    }
}
