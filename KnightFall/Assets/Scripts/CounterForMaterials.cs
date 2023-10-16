using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterForMaterials : MonoBehaviour
{
    [Header("counters")]
    public GameObject woodCounter;
    public GameObject stoneCounter;
    public GameObject metalCounter;
    public GameObject coinsCounter;

    [Header("ints for amounts")]
    public float woodAmount;
    public float stoneAmount;
    public float metalAmount;
    public float coinsAmount;

    public void Update()
    {
        woodCounter.GetComponent<TextMeshProUGUI>().text = woodAmount.ToString();
        stoneCounter.GetComponent<TextMeshProUGUI>().text = stoneAmount.ToString();
        metalCounter.GetComponent<TextMeshProUGUI>().text = metalAmount.ToString();
        coinsCounter.GetComponent<TextMeshProUGUI>().text = coinsAmount.ToString();
    }
}
