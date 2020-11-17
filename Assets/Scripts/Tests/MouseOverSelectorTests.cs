using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace mouse_over_selector
{
    [TestFixture]
    public class with_targetcap_of_one
    {
        [Test]
        public void given_one_in_target_pool_returns_one_target()
        {
            Collider[] targetPool = new Collider[1];
            ISelectionStrategy selectionStrategy = Substitute.For<ISelectionStrategy>();
            selectionStrategy.GetTargets().Returns(targetPool);
            MouseOverSelector selector = new MouseOverSelector(selectionStrategy, 1);

            var selectedTargets = selector.GetAllTargets();

            Assert.AreEqual(1, selectedTargets.Length);
        }
        [Test]
        public void given_five_available_targets_returns_one_target()
        {
            Collider[] availableTargets = new Collider[5];
            ISelectionStrategy selectionStrategy = Substitute.For<ISelectionStrategy>();
            selectionStrategy.GetTargets().Returns(availableTargets);
            MouseOverSelector selector = new MouseOverSelector(selectionStrategy, 1);

            var selectedTargets = selector.GetAllTargets();

            Assert.AreEqual(1, selectedTargets.Length);
        }
        [Test]
        public void given_no_available_targets_returns_zero_targets()
        {
            Collider[] availableTargets = new Collider[0];
            ISelectionStrategy selectionStrategy = Substitute.For<ISelectionStrategy>();
            selectionStrategy.GetTargets().Returns(availableTargets);
            MouseOverSelector selector = new MouseOverSelector(selectionStrategy, 1);

            var selectedTargets = selector.GetAllTargets();

            Assert.AreEqual(0, selectedTargets.Length);
        }
    }
}