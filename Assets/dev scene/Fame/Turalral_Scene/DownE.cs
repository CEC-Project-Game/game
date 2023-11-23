using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownE : MonoBehaviour
{
    public GameObject _DownE;
    void Start()
    {
        _DownE.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _DownE.SetActive(true);
            StartCoroutine(Delay());
        }
      
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        Destroy(_DownE);
    }
}
