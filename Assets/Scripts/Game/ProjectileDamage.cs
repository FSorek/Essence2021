using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Projectile))]
    public class ProjectileDamage : MonoBehaviour
    {
        [SerializeField] private float damage;
        private Projectile projectile;
        private void Awake()
        {
            projectile = GetComponent<Projectile>();
            projectile.OnTargetHit += ProjectileOnTargetHit;
        }

        private void ProjectileOnTargetHit(IEntity entity)
        {
            var damageable = entity as ITakeDamage;
            damageable?.Health.TakeDamage(damage);
        }
    }
}