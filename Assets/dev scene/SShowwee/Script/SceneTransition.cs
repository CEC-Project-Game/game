using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int Scene_Warp;
    [SerializeField] Animator transitionAim;

    public SceneTransition Scene;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
    }


    IEnumerator LoadScene()
    {
        transitionAim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(Scene.Scene_Warp);
        transitionAim.SetTrigger("Start");
    }
}
