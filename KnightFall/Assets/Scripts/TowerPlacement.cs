using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class TowerPlacement : MonoBehaviour
{
    public GameObject backButton;
    public GameObject panel;
    public GameObject[] towerInfo;
    public InputMaster input;
    public InputAction move;
    public bool inBuildingPhase;
    public LayerMask layer;
    private GameObject currentTower;
    private Vector2 mousePosition;
    public PlacingFromTowerScript towerScript;
    public Info info;

    private void Awake()
    {
        input = new InputMaster();
    }

    public void Update()
    {
        mousePosition = move.ReadValue<Vector2>();

        if(inBuildingPhase == true)
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(mousePosition);
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,layer))
            {
                currentTower.transform.position = hit.point;
            }
        }
    }

    private void OnEnable()
    {
        move = input.Movement3rdperson.mouse;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    public void MouseButtonAction()
    {
        if (currentTower == null)
            return;

        towerScript = currentTower.GetComponent<PlacingFromTowerScript>();

        if (towerScript.collides == false && inBuildingPhase == true)
        {
            if (counterInfo.coins < towerScript.coinsNeeded)
                return;

            if (counterInfo.wood < towerScript.woodNeeded)
                return;

            if (counterInfo.stone < towerScript.stoneNeeded)
                return;

            if (counterInfo.metal < towerScript.metalNeeded)
                return;

            counterInfo.coins -= towerScript.coinsNeeded;
            counterInfo.wood -= towerScript.woodNeeded;
            counterInfo.stone -= towerScript.stoneNeeded;
            counterInfo.metal -= towerScript.metalNeeded;

            towerScript.gameObject.layer = default;
            backButton.SetActive(false);
            panel.SetActive(true);
            inBuildingPhase = false;
            towerScript.IsPlaced();
            towerScript.enabled = false;
        }
    }

    #region buttons

    public void OnCLickKanon()
    {
        Conditions(0);
    }

    public void OnClickTrap()
    {

    }

    public void OnClickKatapult()
    {

    }

    public void OnClickMuur()
    {

    }

    #endregion

    public void BackButton()
    {
        backButton.SetActive(false);
        panel.SetActive(true);
        inBuildingPhase = false;
        Destroy(currentTower);
    }

    public void Conditions(int index)
    {
        backButton.SetActive(true);
        panel.SetActive(false);
        inBuildingPhase = true;
        currentTower = Instantiate(towerInfo[index]);
    }

    public InfoForCounters counterInfo;
}
