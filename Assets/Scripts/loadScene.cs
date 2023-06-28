using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public bool sceneSelected;
    public string sceneToLoad;
    public void OnLoadSceneCalled()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
