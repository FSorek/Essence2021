using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class a_player
    {
        [UnityTest]
        public IEnumerator starts_in_idle_state()
        {
            yield return TestHelpers.LoadEmptyTestScene();
            var playerInput = Substitute.For<IPlayerInput>();
            PlayerInput.Instance = playerInput;
            var player = GetPlayer();
            yield return null;
            Assert.AreEqual(typeof(Idle),player.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator pressing_obelisk_button_switches_state_to_placing_obelisk()
        {
            var playerInput = Substitute.For<IPlayerInput>();
            PlayerInput.Instance = playerInput;
            
            var player = GetPlayer();
            yield return null;
            Assert.AreEqual(typeof(Idle),player.CurrentStateType);
            playerInput.ObeliskKeyDown.Returns(true);
            yield return null;
            
            Assert.AreEqual(typeof(PlacingObelisk),player.CurrentStateType);
        }
        [UnityTest]
        public IEnumerator pressing_element_button_switches_state_to_invoke_element()
        {
            var playerInput = Substitute.For<IPlayerInput>();
            PlayerInput.Instance = playerInput;
            
            var player = GetPlayer();
            yield return null;
            Assert.AreEqual(typeof(Idle),player.CurrentStateType);
            playerInput.InvokeFireDown.Returns(true);
            yield return null;
            
            Assert.AreEqual(typeof(InvokeElement),player.CurrentStateType);
        }
        private PlayerStateMachine GetPlayer()
        {
            var player = AssetDatabase.LoadAssetAtPath<PlayerStateMachine>("Assets/Prefabs/Player/Player.prefab");
            return Object.Instantiate(player);
        }
    }
}