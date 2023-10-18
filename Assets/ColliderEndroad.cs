using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEndroad : MonoBehaviour
{
    
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObject timelineEnd = GameObject.Find("TimelineEnd");
            timelineEnd.SetActive(true);
        }
    }
}
