using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class cameraSpeed : MonoBehaviour
{
    public Slider slider;

    public void Update()
    {
        slider.value = 5f;
        vFirstPersonCamera.cameraConfig = slider.value;
    }
}
