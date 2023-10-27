using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapPlayer : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    private string playerTag = "Player";
    private GameObject playerObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerObject = GameObject.FindGameObjectWithTag("PlayerObject");
            DestroyAndInstantiatePlayer(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerObject = GameObject.FindGameObjectWithTag("PlayerObject");
            DestroyAndInstantiatePlayer(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerObject = GameObject.FindGameObjectWithTag("PlayerObject");
            DestroyAndInstantiatePlayer(2);
        }
    }

    private void DestroyAndInstantiatePlayer(int prefabIndex)
    {
        GameObject playerToDestroy = GameObject.FindGameObjectWithTag(playerTag);

        if (playerToDestroy != null)
        {
            Vector3 spawnpos = playerToDestroy.transform.position;
            Destroy(playerObject);

            StartCoroutine(InstantiatePlayerWithDelay(prefabIndex, spawnpos));
        }
    }

    private IEnumerator InstantiatePlayerWithDelay(int prefabIndex, Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(1);

        if (prefabIndex >= 0 && prefabIndex < playerPrefabs.Count)
        {
            Instantiate(playerPrefabs[prefabIndex], spawnPosition, Quaternion.identity);
        }
    }
}
