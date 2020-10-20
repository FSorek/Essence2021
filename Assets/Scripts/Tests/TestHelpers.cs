using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class TestHelpers
{
    public static IEnumerator LoadEmptyTestScene()
    {
        var operation = SceneManager.LoadSceneAsync("EmptyTestScene");
        while (operation.isDone == false)
            yield return null;
                
        var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.localScale = new Vector3(50, 0.1f, 50);
        floor.transform.position = Vector3.zero;
    }
}