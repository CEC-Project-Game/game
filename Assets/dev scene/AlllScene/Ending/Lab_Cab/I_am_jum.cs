using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class I_am_jum : MonoBehaviour
{
    public GameObject Sound;


    private void Start()
    {
        Sound.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Kuy");
            Sound.SetActive(true);
        }
    }

    IEnumerator TumRan()
    {
        yield return new WaitForSeconds(5);
        Destroy(Sound);
    }
}
