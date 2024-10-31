using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private List<GameObject> EnnemiesInZone;


    private void Awake()
    {
        EnnemiesInZone = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ennemy") || other.CompareTag("Barrel"))
        {
            EnnemiesInZone.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnnemiesInZone.Remove(other.gameObject);
    }

    public void Attack(int damage)
    {
        foreach (var ennemy in EnnemiesInZone)
        {
            var hz = ennemy.GetComponent<HitZone>();
                hz.Damage(damage);

                // hz.Damage() return true if the enemy is dead or if the Barrel is destroyed
                if (hz.Damage(damage))
                {
                    EnnemiesInZone.Remove(ennemy);
                }
        }
    }
}