using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class speedsetting : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        slider.value = vFirstPersonCamera.cameraConfig;
    }

    public void Update()
    {
        vFirstPersonCamera.cameraConfig = slider.value;
        //Debug.Log(slider.value);
    }
}
