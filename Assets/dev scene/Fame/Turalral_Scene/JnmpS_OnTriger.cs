using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JnmpS_OnTriger : MonoBehaviour
{
    public GameObject JumpS;
    private void Start()
    {
        JumpS.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            JumpS.SetActive(true);
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
