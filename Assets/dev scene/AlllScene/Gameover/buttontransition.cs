using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttontransition : MonoBehaviour
{
    public int Scene_Warp;
    public Animator transitionAim;

    public buttontransition Scene;

    public void changescene()
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

