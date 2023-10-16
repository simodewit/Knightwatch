using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public float damage;
    public float cooldown;
    public float cooldownTime;

    void Start()
    {
        cooldown = cooldownTime;
    }

    void Update()
    {
        cooldown -= Time.deltaTime; 
    }

    public void OnTriggerStay(UnityEngine.Collider other)
    {   
        
            if (other.gameObject.transform.tag == "Enemy")
            {
                print("enemy ");
                if(cooldown <= 0f) 
                {
                    cooldown = cooldownTime;
                    print("Do Damage");
                    other.GetComponent<Collider>().gameObject.GetComponent<NPCScript>().DoDamage(damage);
                    return; 
                }
            }
    }

}
