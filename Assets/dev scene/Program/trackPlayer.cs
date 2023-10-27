using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackPlayer : MonoBehaviour
{
    public GameObject target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(target.transform);
    }
}
