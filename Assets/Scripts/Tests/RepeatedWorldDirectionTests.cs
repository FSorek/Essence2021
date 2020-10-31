using System.Collections;
using Game;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RepeatedWorldDirectionTests
    {
        [Test]
        public void given_target_at_start_of_map_and_origin_at_end_of_map_returns_Vector3_Right()
        {
            IWorldGenerator generator = Substitute.For<IWorldGenerator>();
            generator.MapLength.Returns(100);
            var worldDirection = new RepeatedWorldDirection(generator);
            var origin = Substitute.For<IWorldPosition>();
            var target = Substitute.For<IWorldPosition>();
            origin.TruePosition.Returns(new Vector3(98,0,0));
            target.TruePosition.Returns(new Vector3(2,0,0));

            var direction = worldDirection.GetDirection(origin, target).normalized;
            Assert.AreEqual(Vector3.right, direction);
        }
        
        [Test]
        public void given_origin_at_start_of_map_and_target_at_end_of_map_returns_Vector3_Left()
        {
            IWorldGenerator generator = Substitute.For<IWorldGenerator>();
            generator.MapLength.Returns(100);
            var worldDirection = new RepeatedWorldDirection(generator);
            var origin = Substitute.For<IWorldPosition>();
            var target = Substitute.For<IWorldPosition>();
            origin.TruePosition.Returns(new Vector3(2,0,0));
            target.TruePosition.Returns(new Vector3(98,0,0));

            var direction = worldDirection.GetDirection(origin, target).normalized;
            Assert.AreEqual(Vector3.left, direction);
        }
        
        [Test]
        public void given_origin_at_start_of_map_and_target_at_start_of_map_returns_Vector3_Right()
        {
            IWorldGenerator generator = Substitute.For<IWorldGenerator>();
            generator.MapLength.Returns(100);
            var worldDirection = new RepeatedWorldDirection(generator);
            var origin = Substitute.For<IWorldPosition>();
            var target = Substitute.For<IWorldPosition>();
            origin.TruePosition.Returns(new Vector3(5,0,0));
            target.TruePosition.Returns(new Vector3(25,0,0));

            var direction = worldDirection.GetDirection(origin, target).normalized;
            Assert.AreEqual(Vector3.right, direction);
        }
    }
}