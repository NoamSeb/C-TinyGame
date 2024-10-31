using UnityEngine;

public class HitZone : MonoBehaviour
{
   [SerializeField] health _health;

   public bool Damage(int damage)
   {
      _health.TakeDamage(damage);
      return _health.TakeDamage(damage);
   }

   public void Heal(int heal)
   {
      _health.Heal(heal);
   }
}
