using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageEffects : MonoBehaviour
{
    [SerializeField] Material materialToSwitch;
    [SerializeField] Material startMaterial;
    [SerializeField] GameObject _objectToSwitch;

    [SerializeField] GameObject _objectToTrack;

    Coroutine _currentFlashRoutine = null;
    private Health _health;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _health.BossDamaged += FlashMat;
    }

    private void OnDisable()
    {
        _health.BossDamaged -= FlashMat;
    }
    // Start is called before the first frame update
    void Start()
    {
        _health = _objectToTrack.GetComponent<Health>();
        OnEnable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FlashMat(int damage)
    {
        StartMaterialChange();
    }

    void StartMaterialChange()
    {
        if (_currentFlashRoutine != null)
        {
            StopCoroutine(_currentFlashRoutine);
        }

        _currentFlashRoutine = StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        _objectToSwitch.GetComponent<Renderer>().material = materialToSwitch;
        yield return new WaitForSeconds(.1f);
        _objectToSwitch.GetComponent<Renderer>().material = startMaterial;
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
