using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterForMaterials : MonoBehaviour
{
    public GameObject coinsButton;
    public GameObject woodButton;
    public GameObject stoneButton;
    public GameObject metalButton;

    public void Update()
    {
        coinsButton.GetComponent<TextMeshProUGUI>().text = counterinfo.coins.ToString();
        woodButton.GetComponent<TextMeshProUGUI>().text = counterinfo.wood.ToString();
        stoneButton.GetComponent<TextMeshProUGUI>().text = counterinfo.stone.ToString();
        metalButton.GetComponent<TextMeshProUGUI>().text = counterinfo.metal.ToString();
    }

    public InfoForCounters counterinfo;
}

[System.Serializable]
public class InfoForCounters
{
    public float coins;
    public float wood;
    public float stone;
    public float metal;
}
