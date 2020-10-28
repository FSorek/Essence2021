using System.Collections;
using Game;
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
            var target = new GameObject("TARGET").transform;
            target.position = Vector3.right;
            projectile.SetTarget(target);
            var startingDistance = Vector2.Distance(projectile.transform.position, target.transform.position);
            yield return null;
            var newDistance = Vector2.Distance(projectile.transform.position, target.transform.position);
            Assert.Less(newDistance, startingDistance);
        }
    }
}