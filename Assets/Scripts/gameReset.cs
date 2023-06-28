using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class gameReset : MonoBehaviour
{
    public GameObject gameManager;
    public string sceneName;

    public void GameReset()
    {
        SceneManager.LoadScene(sceneName);
    }
}
