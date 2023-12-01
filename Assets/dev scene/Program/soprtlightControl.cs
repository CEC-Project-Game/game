using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soprtlightControl : MonoBehaviour
{
    public Light mylight;
    private bool setlight;
    public AudioClip audiolight;
    public AudioSource audioSource;


    void Start()
    {
        mylight.enabled = false;
        setlight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (setlight == true)  // Fix: use '==' for comparison
        {
            StartCoroutine(WaitLight());
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player")
        {
            setlight = true;
        }
    }

    IEnumerator WaitLight()
    {
        yield return new WaitForSeconds(2);
        mylight.enabled = true;
        audioSource.PlayOneShot(audiolight);
        yield return new WaitForSeconds(5);
        audioSource.Stop();
        mylight.enabled = false;
    }
}
