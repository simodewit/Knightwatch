using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CastleScript : MonoBehaviour
{
    [Header("conditions")]
    public float maxhp;
    public Slider slider;
    public float timer;

    [Header("do not touch")]
    public GameObject mainPanel;
    public GameObject lostPanel;
    public float hp;

    bool lostGame;

    public void Start()
    {
        mainPanel = GameObject.Find("MainPanel");
        lostPanel = GameObject.Find("GameOver");
        hp = maxhp;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            lostGame = true;
            mainPanel.SetActive(false);
            lostPanel.SetActive(true);
            Time.timeScale = 0;
        }

        if(lostGame)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        HpUI();
    }

    public void DoDamage(float damage)
    {
        hp -= damage;
    }

    public void HpUI()
    {
        float percentageHP = hp / maxhp;
        slider.value = percentageHP;
    }
}
