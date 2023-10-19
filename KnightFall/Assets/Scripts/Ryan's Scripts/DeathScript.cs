using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DeathScript : MonoBehaviour
{
    public GameObject player;
    public Vector3 spawnPosition;
    public float minimumHeight;
    public Playerhealth playerhealth;

    public GameObject deathScreen;
    
    public GameObject deathCam;
    public GameObject mainCamera;
    public int respawnTime;
    
    public void Start()
    {   
        player.transform.position = spawnPosition;
        MainCamera();
        Vector3 pos = transform.position;
        pos.y = minimumHeight;
       
    }
    public void Update()
    {
        if(player.transform.position.y <= minimumHeight)
        {
            player.transform.position = spawnPosition;
        }
        
        if(playerhealth.hp <= 0)
        {
            //deathScreen.SetActive(true);
            StartCoroutine(WaitForRespawn());
            //player.SetActive(false);
            Destroy(player);
            DeathCamera();           
        }
        else
        {
            MainCamera();
        }

   
    }
    public IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        playerhealth.hp = playerhealth.maxhp;
        player.transform.position = spawnPosition;       
        //player.SetActive(true);
        //deathScreen.SetActive(false);
        MainCamera();
        Instantiate(player);
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
