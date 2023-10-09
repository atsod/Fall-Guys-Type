using System.Collections;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected int Damage { get; set; }
    protected Renderer TrapRenderer { get; set; }
    protected Color NormalTrapColor { get; set; }
    protected IEnumerator ActivateTrapCoroutine { get; set; }

    protected void StartTrapCoroutine()
    {
        ActivateTrapCoroutine = ActivateTrap();
        StartCoroutine(ActivateTrapCoroutine);
    }

    protected void StopTrapCoroutine()
    {
        StopCoroutine(ActivateTrapCoroutine);
    }

    protected abstract IEnumerator ActivateTrap();
}
