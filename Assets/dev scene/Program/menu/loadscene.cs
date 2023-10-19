using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscene : MonoBehaviour
{
    public int what_load;

    public void LoadGame()
    {
        SceneManager.LoadScene(what_load);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
