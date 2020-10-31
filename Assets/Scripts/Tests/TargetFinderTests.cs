using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class TargetFinderTests
    {
        [Test]
        public void finds_target_in_range()
        {
            float range = 5f;
            IWorldGenerator generator = Substitute.For<IWorldGenerator>();
            var origin = Substitute.For<IWorldPosition>();
            var enemy = Substitute.For<IEntity>();
            var enemyPosition = Substitute.For<IWorldPosition>();
            origin.GlobalPosition.Returns(5);
            enemyPosition.GlobalPosition.Returns(9);
            enemy.Position.Returns(enemyPosition);
            var availableTargets = new IEntity[]
            {
                enemy
            };
            
            generator.MapLength.Returns(100);
            var targetFinder = new TargetFinder(origin, range, generator);
            var closestTarget = targetFinder.GetClosestTarget(availableTargets);
            
            Assert.AreSame(enemy, closestTarget);
        }
        [Test]
        public void doesnt_find_target_when_outside_of_range()
        {
            float range = 5f;
            IWorldGenerator generator = Substitute.For<IWorldGenerator>();
            var origin = Substitute.For<IWorldPosition>();
            var enemy = Substitute.For<IEntity>();
            var enemyPosition = Substitute.For<IWorldPosition>();
            origin.GlobalPosition.Returns(5);
            enemyPosition.GlobalPosition.Returns(11);
            enemy.Position.Returns(enemyPosition);
            var availableTargets = new IEntity[]
            {
                enemy
            };
            
            generator.MapLength.Returns(100);
            var targetFinder = new TargetFinder(origin, range, generator);
            var closestTarget = targetFinder.GetClosestTarget(availableTargets);
            
            Assert.IsNull(closestTarget);
        }
        [Test]
        public void finds_target_in_range_close_to_map_repeat_point()
        {
            float range = 5f;
            IWorldGenerator generator = Substitute.For<IWorldGenerator>();
            var origin = Substitute.For<IWorldPosition>();
            var enemy = Substitute.For<IEntity>();
            var enemyPosition = Substitute.For<IWorldPosition>();
            origin.GlobalPosition.Returns(96);
            enemyPosition.GlobalPosition.Returns(1);
            enemy.Position.Returns(enemyPosition);
            var availableTargets = new IEntity[]
            {
                enemy
            };
            
            generator.MapLength.Returns(100);
            var targetFinder = new TargetFinder(origin, range, generator);
            var closestTarget = targetFinder.GetClosestTarget(availableTargets);
            
            Assert.AreSame(enemy, closestTarget);
        }
        [Test]
        public void doesnt_find_target_out_of_range_close_to_map_repeat_point()
        {
            float range = 5f;
            IWorldGenerator generator = Substitute.For<IWorldGenerator>();
            var origin = Substitute.For<IWorldPosition>();
            var enemy = Substitute.For<IEntity>();
            var enemyPosition = Substitute.For<IWorldPosition>();
            origin.GlobalPosition.Returns(96);
            enemyPosition.GlobalPosition.Returns(9);
            enemy.Position.Returns(enemyPosition);
            var availableTargets = new IEntity[]
            {
                enemy
            };
            
            generator.MapLength.Returns(100);
            var targetFinder = new TargetFinder(origin, range, generator);
            var closestTarget = targetFinder.GetClosestTarget(availableTargets);
            
            Assert.IsNull(closestTarget);
        }
    }
}