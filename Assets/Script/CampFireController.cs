using UnityEngine;
using System.Collections;

public class CampFireController : MonoBehaviour
{
    Coroutine _HealPlayerCoroutine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HitZone>(out HitZone hitZone))
        {
            _HealPlayerCoroutine = StartCoroutine(HealPlayer());

            IEnumerator HealPlayer()
            {
                while (true)
                {
                    yield return new WaitForSeconds(1.0f);
                    hitZone.Heal(5);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        StopCoroutine(_HealPlayerCoroutine);
    }
}
