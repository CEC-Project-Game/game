using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public float interactionDistance;
    //public GameObject intText;
    public string doorOpenAnimName, doorCloseAnimName;
    //public AudioClip doorOpen, doorClose;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject.tag == "door")
            {
                GameObject doorParent = hit.collider.transform.root.gameObject;
                Animator doorAnim = doorParent.GetComponent<Animator>();
                //AudioSource doorSound = hit.collider.gameObject.GetComponent<AudioSource>();
                //intText.SetActive(true);
                Debug.Log("hit");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimName))
                    {
                        //doorSound.clip = doorClose;
                        //doorSound.Play();
                        doorAnim.ResetTrigger("open");
                        doorAnim.SetTrigger("close");
                    }
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimName))
                    {
                        //doorSound.clip = doorOpen;
                        //doorSound.Play();
                        doorAnim.ResetTrigger("close");
                        doorAnim.SetTrigger("open");
                    }
                }
            }
            else
            {
                //intText.SetActive(false);
            }
        }
        else
        {
           //intText.SetActive(false);
        }
    }
}

