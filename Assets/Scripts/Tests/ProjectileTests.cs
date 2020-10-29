using System.Collections;
using Game;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ProjectileTests
    {
        [UnityTest]
        public IEnumerator with_no_target_set_disables_itself()
        {
            var projectile = new GameObject("PROJECTILE").AddComponent<Projectile>();
            yield return null;
            Assert.IsFalse(projectile.gameObject.activeSelf);
        }
        [UnityTest]
        public IEnumerator moves_towards_target()
        {
            var projectile = new GameObject("PROJECTILE").AddComponent<Projectile>();
            var target = Substitute.For<IWorldPosition>();
            target.TruePosition.Returns(Vector3.right);
            projectile.SetTarget(target);
            var startingDistance = Vector2.Distance(projectile.transform.position, target.TruePosition);
            yield return null;
            var newDistance = Vector2.Distance(projectile.transform.position, target.TruePosition);
            Assert.Less(newDistance, startingDistance);
        }
        [UnityTest]
        public IEnumerator hitting_damageable_target_performs_taking_damage()
        {
            var projectile = new GameObject("PROJECTILE").AddComponent<Projectile>();
            var target = Substitute.For<IWorldPosition>();
            target.TruePosition.Returns(Vector3.right);
            projectile.SetTarget(target);
            var startingDistance = Vector2.Distance(projectile.transform.position, target.TruePosition);
            yield return null;
            var newDistance = Vector2.Distance(projectile.transform.position, target.TruePosition);
            Assert.Less(newDistance, startingDistance);
        }
    }
}