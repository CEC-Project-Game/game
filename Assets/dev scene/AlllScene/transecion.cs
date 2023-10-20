using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transecion : MonoBehaviour
{
    public int Scene_Warp;
    public Animator transitionAim;

    public transecion Scene;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transitionAim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(Scene.Scene_Warp);
        transitionAim.SetTrigger("Start");
    }
}
