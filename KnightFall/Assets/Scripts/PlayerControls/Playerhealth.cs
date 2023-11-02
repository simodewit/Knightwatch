using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerhealth : MonoBehaviour
{
    [Header("conditions")]
    public float maxhp;
    public Slider slider;

    [Header("do not touch")]
    public float hp;

    public void Start()
    {
        hp = maxhp;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
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
