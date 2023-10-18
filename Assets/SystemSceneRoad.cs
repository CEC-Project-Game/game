using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSceneRoad : MonoBehaviour
{
    public GameObject TimelineStart;
    public GameObject TimelineEnd;
    void Start()
    {
        TimelineStart.SetActive(true);
        TimelineEnd.SetActive(false);
    }

}
