using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public static event Action<int> OnPlayerFellOffLevel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>() != null)
        {
            OnPlayerFellOffLevel?.Invoke(100);
        }
    }
}
