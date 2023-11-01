using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class TowerPlacement : MonoBehaviour
{
    [Header("conditions")]
    public GameObject backButton;
    public GameObject[] towerPrefabs;
    public LayerMask layer;
    public CounterForMaterials counterForMaterials;
    public PlayerAttack attackScript;
    public IngameUI uiScript;

    [Header("do not touch")]
    public bool inBuildingPhase;
    public PlacingFromTowerScript towerScript;
    public Info info;
    public InputMaster input;
    public InputAction move;

    private GameObject currentTower;
    private Vector2 mousePosition;

    #region input

    private void Awake()
    {
        input = new InputMaster();
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

    #endregion

    public void Update()
    {
        mousePosition = move.ReadValue<Vector2>();

        if (inBuildingPhase == true)
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                currentTower.transform.position = hit.point;
            }
        }
    }

    public void MouseButtonAction()
    {
        if (currentTower == null)
            return;

        towerScript = currentTower.GetComponent<PlacingFromTowerScript>();

        if (towerScript.collides == false && inBuildingPhase == true)
        {
            if (counterForMaterials.woodAmount < towerScript.woodNeeded)
                return;

            if (counterForMaterials.stoneAmount < towerScript.stoneNeeded)
                return;

            if (counterForMaterials.metalAmount < towerScript.metalNeeded)
                return;

            if (counterForMaterials.coinsAmount < towerScript.coinsNeeded)
                return;

            counterForMaterials.woodAmount -= towerScript.woodNeeded;
            counterForMaterials.stoneAmount -= towerScript.stoneNeeded;
            counterForMaterials.metalAmount -= towerScript.metalNeeded;
            counterForMaterials.coinsAmount -= towerScript.coinsNeeded;

            towerScript.gameObject.layer = default;
            inBuildingPhase = false;
            towerScript.IsPlaced();
            towerScript.enabled = false;
            attackScript.enabled = true;
            currentTower = null;
        }
    }

    #region buttons

    public void OnCLickKanon()
    {
        BackButton();
        Conditions(0);
        uiScript.PanelIn();
    }

    public void OnClickKatapult()
    {
        BackButton();
        Conditions(1);
        uiScript.PanelIn();
    }

    public void OnCLickCrossbow()
    {
        BackButton();
        Conditions(2);
        uiScript.PanelIn();
    }

    public void OnClickTrap()
    {
        BackButton();
        Conditions(3);
        uiScript.PanelIn();
    }

    public void OnClickMuur()
    {
        BackButton();
        Conditions(4);
        uiScript.PanelIn();
    }

    public void BackButton()
    {
        inBuildingPhase = false;
        Destroy(currentTower);
        attackScript.enabled = true;
        uiScript.PanelIn();
    }

    public void Conditions(int index)
    {
        inBuildingPhase = true;
        currentTower = Instantiate(towerPrefabs[index]);
        attackScript.enabled = false;
        uiScript.PanelIn();
    }

    #endregion
}
