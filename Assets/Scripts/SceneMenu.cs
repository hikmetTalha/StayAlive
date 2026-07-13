using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMenu : MonoBehaviour
{
    public void OpenScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {

        Application.Quit();
        Debug.Log("Oyundan Çýkýldý");
    }
}
