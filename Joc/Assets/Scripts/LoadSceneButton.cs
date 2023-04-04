using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public Button button;
    public string sceneName;

    void Start()
    {
        // Add listener to the button
        button.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        // Load the scene with the given name
        SceneManager.LoadScene(sceneName);
    }
}
