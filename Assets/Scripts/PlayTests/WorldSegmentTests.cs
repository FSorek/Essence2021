using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class world_segment
    {
        [UnityTest]
        public IEnumerator anchoring_right_of_another_segment_sets_aligns_position_to_its_length()
        {
            yield return TestHelpers.LoadEmptyTestScene();
            WorldSegment firstSegment = GetWorldSegment();
            WorldSegment secondSegment = GetWorldSegment();
            
            secondSegment.transform.localScale = new Vector3(2,1,1);
            secondSegment.AnchorRight(firstSegment);
            yield return null;
            
            Assert.AreEqual(firstSegment.transform.position.x + secondSegment.Length, secondSegment.transform.position.x);
        }
        
        [UnityTest]
        public IEnumerator anchoring_left_of_another_segment_sets_aligns_position_to_another_segment_length()
        {
            yield return TestHelpers.LoadEmptyTestScene();
            WorldSegment firstSegment = GetWorldSegment();
            WorldSegment secondSegment = GetWorldSegment();
            
            secondSegment.transform.localScale = new Vector3(2,1,1);
            secondSegment.AnchorLeft(firstSegment);
            yield return null;
            
            Assert.AreEqual(firstSegment.transform.position.x - firstSegment.Length, secondSegment.transform.position.x);
        }
        
        private WorldSegment GetWorldSegment()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<WorldSegment>("Assets/Prefabs/World/WorldSegment.prefab");
            return Object.Instantiate(prefab);
        }
        
    }
}