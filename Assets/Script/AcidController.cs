using System;
using System.Collections;
using UnityEngine;

public class AcidController : MonoBehaviour
{
    Coroutine _takeDamageCoroutine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HitZone>(out HitZone hitZone))
        {
            _takeDamageCoroutine = StartCoroutine(TakeDamage());

            IEnumerator TakeDamage()
            {
                while (true)
                {
                    yield return new WaitForSeconds(1.0f);
                    hitZone.Damage(20);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        StopCoroutine(_takeDamageCoroutine);
    }
}