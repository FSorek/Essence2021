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
            var origin = GetEntity(5);
            var enemy = GetEntity(9);
            var availableTargets = new IEntity[]
            {
                enemy
            };
            var targetFinder = GetTargetFinder(range, origin.Position, availableTargets);

            var closestTarget = targetFinder.GetClosestTarget();
            
            Assert.AreSame(enemy, closestTarget);
        }
        [Test]
        public void doesnt_find_target_when_outside_of_range()
        {
            float range = 5f;
            var origin = GetEntity(5);
            var enemy = GetEntity(11);
            var availableTargets = new IEntity[]
            {
                enemy
            };
            var targetFinder = GetTargetFinder(range, origin.Position, availableTargets);

            var closestTarget = targetFinder.GetClosestTarget();
            
            Assert.IsNull(closestTarget);
        }
        [Test]
        public void finds_target_in_range_close_to_map_repeat_point()
        {
            float range = 5f;
            var origin = GetEntity(96);
            var enemy = GetEntity(1);
            var availableTargets = new IEntity[]
            {
                enemy
            };
            var targetFinder = GetTargetFinder(range, origin.Position, availableTargets);

            var closestTarget = targetFinder.GetClosestTarget();
            
            Assert.AreSame(enemy, closestTarget);
        }
        [Test]
        public void doesnt_find_target_out_of_range_close_to_map_repeat_point()
        {
            float range = 5f;
            var origin = GetEntity(96);
            var enemy = GetEntity(9);
            var availableTargets = new IEntity[]
            {
                enemy
            };
            var targetFinder = GetTargetFinder(range, origin.Position, availableTargets);

            var closestTarget = targetFinder.GetClosestTarget();
            
            Assert.IsNull(closestTarget);
        }

        private TargetFinder<IEntity> GetTargetFinder(float range, IWorldPosition origin, IEntity[] targets)
        {
            IWorldGenerator generator = Substitute.For<IWorldGenerator>();
            var monsterFactory = Substitute.For<IEntityFactory<IEntity>>();
            monsterFactory.GetActiveEntities().Returns(targets);
            generator.MapLength.Returns(100);
            var targetFinder = new TargetFinder<IEntity>(origin, range, generator, monsterFactory);

            return targetFinder;
        }

        private IEntity GetEntity(float globalPosition)
        {
            var entity = Substitute.For<IEntity>();
            var entityPosition = Substitute.For<IWorldPosition>();
            entityPosition.GlobalPosition.Returns(globalPosition);
            entity.Position.Returns(entityPosition);
            
            return entity;
        }
    }
}