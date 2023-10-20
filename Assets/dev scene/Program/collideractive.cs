using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideractive : MonoBehaviour
{
    public GameObject endscene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            {
                endscene.SetActive(true);
            }
    }
}
