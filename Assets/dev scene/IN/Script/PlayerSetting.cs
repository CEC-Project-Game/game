using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCamera;

public class PlayerSetting : MonoBehaviour
{
    public GameObject[] Controllers;
    public GameObject[] players;
    public GameObject camera1;
    private int currentPlayerIndex = 0;

    void Start()
    {

        GameObject controller1 = GameObject.FindWithTag("Controller1");
        GameObject controller2 = GameObject.FindWithTag("Controller2");
        GameObject controller3 = GameObject.FindWithTag("Controller3");
        GameObject controller4 = GameObject.FindWithTag("Controller4");
        Controllers = new GameObject[4] {controller1,controller2,controller3,controller4};
        
        controller1.SetActive(true);
        controller2.SetActive(false);
        controller3.SetActive(false);
        controller4.SetActive(false);

        camera1 = GameObject.Find("vThirdPersonCamera1");

        GameObject player1 = GameObject.FindWithTag("Player1");
        GameObject player2 = GameObject.FindWithTag("Player2");
        GameObject player3 = GameObject.FindWithTag("Player3");
        GameObject player4 = GameObject.FindWithTag("Player4");
        players = new GameObject[4] {player1, player2, player3, player4};

        player1.SetActive(true);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            SwitchToNextPlayer();
        }
    }

    void SwitchToNextPlayer()
    {

        
        currentPlayerIndex += 1;
        Debug.Log(currentPlayerIndex);
        
        if (currentPlayerIndex >= Controllers.Length)
        {
            currentPlayerIndex = 0;
        }
        Debug.Log(currentPlayerIndex);

        camera1.GetComponent<vThirdPersonCamera>().SetTarget(players[currentPlayerIndex].transform); // Call the SetTarget method from your custom camera script.
       
    }

}
