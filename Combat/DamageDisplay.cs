using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] Canvas bloodEffectCanvas;
    [SerializeField] float bloodEffectTimer = 0.5f;

    private void Start()
    {
        bloodEffectCanvas.enabled = false;
    }

    public void bloodEffectActivator()
    {
        StartCoroutine(DamageDisplayer());
    }
    public IEnumerator DamageDisplayer()
    {
        bloodEffectCanvas.enabled = true;
        yield return new WaitForSeconds(bloodEffectTimer);
        bloodEffectCanvas.enabled = false;
    }
}
