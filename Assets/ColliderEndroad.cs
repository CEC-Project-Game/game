using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEndroad : MonoBehaviour
{
    public GameObject timelineEnd;
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            timelineEnd.SetActive(true);
        }
    }
}
