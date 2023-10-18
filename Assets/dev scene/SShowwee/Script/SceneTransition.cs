using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int Scene_Warp;
    [SerializeField] Animator transitionAim;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
    }


    IEnumerator LoadScene()
    {
        transitionAim.SetTrigger("End");
        yield return new

        
        transitionAim.SetTrigger("Start");
    }
}
