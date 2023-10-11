using System.Collections;
using UnityEngine;

public class BreakingTrap : Trap
{
    [SerializeField] private Material _firstBreakingMaterial;
    [SerializeField] private Material _secondBreakingMaterial;
    [SerializeField] private Material _lastBreakingMaterial;

    private MeshRenderer _renderer;

    private int _breakingPhase;

    private bool _isCoroutineStarted;

    private void Awake()
    {
        _renderer = GetComponentInParent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            if(!_isCoroutineStarted)
            {
                _isCoroutineStarted = true;
                StartTrapCoroutine();
            }
        }
    }

    protected override IEnumerator ActivateTrap()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            
            if (_breakingPhase == 3)
            {
                Destroy(_renderer.gameObject);
            }

            _renderer.material = GetNextBreakingMaterial();
        }
    }

    private Material GetNextBreakingMaterial()
    {
        switch (_breakingPhase)
        {
            case 0:
                _breakingPhase++;
                return _firstBreakingMaterial;
            case 1:
                _breakingPhase++;
                return _secondBreakingMaterial;
            case 2:
                _breakingPhase++;
                return _lastBreakingMaterial;
            default:
                return _firstBreakingMaterial;
        }
    }
}
