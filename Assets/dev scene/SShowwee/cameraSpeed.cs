using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class cameraSpeed : MonoBehaviour
{
    public Slider slider;
    public float speedcamra;

    public void Start()
    {
        slider.value = 5f;
    }

    public void Update()
    {
        vFirstPersonCamera.cameraConfig = slider.value;
    }
}
