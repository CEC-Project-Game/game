using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    public GameObject[] players;
    public Transform[] Holdercameras;
    private int currentPlayerIndex = 0;
    public Transform camera;

    void Start()
    {
        GameObject player1 = GameObject.FindWithTag("Player1");
        GameObject player2 = GameObject.FindWithTag("Player2");
        GameObject player3 = GameObject.FindWithTag("Player3");
        GameObject player4 = GameObject.FindWithTag("Player4");

        players = new GameObject[4];
        players[0] = player1;
        players[1] = player2;
        players[2] = player3;
        players[3] = player4;
        
        player1.GetComponent<Player>().enabled = true;
        player2.GetComponent<Player>().enabled = false;
        player3.GetComponent<Player>().enabled = false;
        player4.GetComponent<Player>().enabled = false;

        Transform Holdercamera1 = GameObject.Find("CameraHolder1").transform;
        Transform Holdercamera2 = GameObject.Find("CameraHolder2").transform;
        Transform Holdercamera3 = GameObject.Find("CameraHolder3").transform;
        Transform Holdercamera4 = GameObject.Find("CameraHolder4").transform;

        Holdercameras =  new Transform[4];
        Holdercameras[0] = Holdercamera1;
        Holdercameras[1] = Holdercamera2;
        Holdercameras[2] = Holdercamera3;
        Holdercameras[3] = Holdercamera4;

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
        Player playerComponent1 = players[currentPlayerIndex].GetComponent<Player>();
        if (playerComponent1 != null)
        {
            playerComponent1.enabled = false;
        }
        currentPlayerIndex += 1;
        
        if (currentPlayerIndex >= Holdercameras.Length)
        {
            currentPlayerIndex = 0;
        }
        Player playerComponent2 = players[currentPlayerIndex].GetComponent<Player>();
        if (playerComponent2 != null)
        {
            playerComponent2.enabled = true;
        }
        Debug.Log(currentPlayerIndex);
        camera.transform.SetParent(Holdercameras[currentPlayerIndex]);
        camera.transform.localPosition = Vector3.zero;
    }

}
