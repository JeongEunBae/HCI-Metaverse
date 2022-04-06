using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ProgramManager : MonoBehaviourPunCallbacks
{

    /* Player Info Panel */
    public GameObject playerInfoPanel;

    /* Player Prefab */
    public GameObject playerPrefab;

    private void Start()
    {
        float randomPosX = Random.Range(-3.0f, 3.0f);
        float randomPosY = -5.246f;
        float randomPosZ = Random.Range(-3.0f, 3.0f);

        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomPosX, randomPosY, randomPosZ), Quaternion.identity);
    }
    private void Update()
    {
        StartCoroutine(ViewPlayerList());

        if(Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    IEnumerator ViewPlayerList()
    {
        Player[] players = PhotonNetwork.PlayerList;

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(player);
        }

        int scrollView_y = 0;
        for (int i = 0; i < players.Length; i++)
        {
            GameObject playerInstance = Instantiate(playerInfoPanel, new Vector3(0, scrollView_y, 0), Quaternion.identity);
            playerInstance.transform.SetParent(GameObject.Find("Viewport").transform.Find("Content").transform);

            scrollView_y -= 55;

            playerInstance.transform.Find("Player Text").GetComponent<Text>().text = players[i].NickName;

        }

        yield return new WaitForSeconds(1.0f);
    }
}
