using System;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public static event Action OnEventTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            OnEventTriggered?.Invoke();
        }
    }
}
