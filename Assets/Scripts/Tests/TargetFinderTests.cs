using NSubstitute;
using NUnit.Framework;

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
            var enemy = Substitute.For<IWorldPosition>();
            origin.GlobalPosition.Returns(5);
            enemy.GlobalPosition.Returns(9);
            var availableTargets = new IWorldPosition[]
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
            var enemy = Substitute.For<IWorldPosition>();
            origin.GlobalPosition.Returns(5);
            enemy.GlobalPosition.Returns(11);
            var availableTargets = new IWorldPosition[]
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
            var enemy = Substitute.For<IWorldPosition>();
            origin.GlobalPosition.Returns(96);
            enemy.GlobalPosition.Returns(1);
            var availableTargets = new IWorldPosition[]
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
            var enemy = Substitute.For<IWorldPosition>();
            origin.GlobalPosition.Returns(96);
            enemy.GlobalPosition.Returns(2);
            var availableTargets = new IWorldPosition[]
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