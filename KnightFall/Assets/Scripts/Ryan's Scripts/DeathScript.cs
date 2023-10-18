using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    GameObject player;
    public Vector3 spawnPosition;
    float minimumHeight;
    public Playerhealth playerhealth;
    GameObject deathScreen;
    
    public void Start()
    {
        player.transform.position = spawnPosition;
        var pos = transform.position;
        pos.y = minimumHeight;
        deathScreen.SetActive(false);
    }
    public void Update()
    {
        if(player.transform.position.y <= minimumHeight)
        {
            player.transform.position = spawnPosition;
        }
        
        if(playerhealth.hp <= 0)
        {
            deathScreen.SetActive(true);
        }

    
    
    }


}
