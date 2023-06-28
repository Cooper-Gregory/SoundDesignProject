using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectScene : MonoBehaviour
{
    public string sceneName;
    public GameObject menuManager;

    public void OnSceneSelected()
    {
        //You could put a switch case in here if you want, checking the scene name and playing different audio based on the map you select.
        menuManager.GetComponent<loadScene>().sceneToLoad = sceneName;
        menuManager.GetComponent<loadScene>().sceneSelected = true;
        menuManager.GetComponent<uiManagement>().playButtonUpdate();
    }
}
