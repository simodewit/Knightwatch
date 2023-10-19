using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DeathScript : MonoBehaviour
{
    public GameObject player;
    public GameObject currentPlayer;
    public Vector3 spawnPosition;
    public float minimumHeight;
    public Playerhealth playerhealth;
    public CameraFollow cameraScript;

    public GameObject deathScreen;
    
    public GameObject deathCam;
    public GameObject mainCamera;
    public int respawnTime;

    private bool spawns;
    
    public void Start()
    {
        
        currentPlayer = GameObject.Instantiate(player);
        MainCamera();
        Vector3 pos = transform.position;
        pos.y = minimumHeight;
       
    }
    public void Update()
    {        
        
        if(currentPlayer != null)
        {
            if (currentPlayer.GetComponent<Playerhealth>().hp <= 0)
            {
                if (spawns == false)
                {
                    spawns = true;
                    //deathScreen.SetActive(true);
                    StartCoroutine(WaitForRespawn());
                    DeathCamera();
                }
            }
        }
    }
    public IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        currentPlayer = GameObject.Instantiate(player);
        currentPlayer.transform.position = spawnPosition;
        mainCamera.GetComponent<CameraFollow>().player = currentPlayer;
        MainCamera();
        //deathScreen.SetActive(false);
        spawns = false;
    }

    public void MainCamera()
    {
        mainCamera.SetActive(true);
        deathCam.SetActive(false);
    }
    public void DeathCamera()
    {
        mainCamera.SetActive(false);
        deathCam.SetActive(true);
    }


}
