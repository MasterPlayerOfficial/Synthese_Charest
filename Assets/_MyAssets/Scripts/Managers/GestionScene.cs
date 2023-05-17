using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionScene : MonoBehaviour
{
    public void ShowInstructions()
    {
        SceneManager.LoadScene(1);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToStart()
    {
        SceneManager.LoadScene(0);
    }
}
