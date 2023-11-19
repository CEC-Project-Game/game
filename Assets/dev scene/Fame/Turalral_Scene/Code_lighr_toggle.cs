using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code_lighr_toggle : MonoBehaviour
{
    public GameObject Light;

    private void Start()
    {
        StartCoroutine(LightToggle());
    }

    private IEnumerator LightToggle()
    {
        yield return new WaitForSeconds(2);
        Light.SetActive(false);
        yield return new WaitForSeconds(2);
        Light.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Light.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Light.SetActive(true);

        yield return new WaitForSeconds(3);
        StartCoroutine(LightToggle());
    }
}
