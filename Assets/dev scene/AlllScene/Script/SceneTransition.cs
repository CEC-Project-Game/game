using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int Scene_Warp;
    public Animator transitionAim;

    public SceneTransition Scene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
    }


    IEnumerator LoadScene()
    {
        transitionAim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(Scene.Scene_Warp);
        transitionAim.SetTrigger("Start");
    }
}
