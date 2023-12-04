using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcontro : MonoBehaviour
{
    public GameObject light;
    void Start()
    {
        light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            light.SetActive(!light.activeSelf);
        }
    }
}
