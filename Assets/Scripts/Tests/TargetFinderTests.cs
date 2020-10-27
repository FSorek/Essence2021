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
            var closestTarget = targetFinder.GetTarget(availableTargets);
            
            Assert.AreSame(enemy, closestTarget);
        }
    }
}