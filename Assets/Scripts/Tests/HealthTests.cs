using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class HealthTests
    {
        [Test]
        public void given_100_health_current_health_lowers_by_damage_taken()
        {
            var health = new Health(100);
            float damageToTake = 5f;
            
            health.TakeDamage(damageToTake);
            
            Assert.AreEqual(95, health.CurrentHealth);
        }
        [Test]
        public void starts_alive()
        {
            var health = new Health(100);
            
            Assert.IsTrue(health.IsAlive);
        }
        [Test]
        public void when_current_health_reaches_0_alive_is_false()
        {
            var health = new Health(100);
            float damageToTake = health.MaxHealth;
            
            health.TakeDamage(damageToTake);
            
            Assert.AreEqual(0, health.CurrentHealth);
            Assert.IsFalse(health.IsAlive);
        }
        
    }
}