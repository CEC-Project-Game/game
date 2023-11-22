using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class cameraSpeed : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        slider.value = 1.25f;
    }

    public void Update()
    {
        vFirstPersonCamera.cameraConfig = slider.value;
        //Debug.Log(slider.value);
    }
}
