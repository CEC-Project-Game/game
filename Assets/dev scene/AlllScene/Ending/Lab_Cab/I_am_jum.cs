using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class I_am_jum : MonoBehaviour
{
    public GameObject Sound;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            Sound.SetActive(true);
        }
    }
}
