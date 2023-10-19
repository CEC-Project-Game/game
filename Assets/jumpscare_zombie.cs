using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class jumpscare_zombie : MonoBehaviour
{
    public int what_load;
    
    
    void OnTriggerEnter(Collider other)
    {
        
        
        if(other.CompareTag ("Player"))
        {
            SceneManager.LoadScene(what_load);
        }
    }
}
    

