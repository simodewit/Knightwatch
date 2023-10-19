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
        }
    }

    #region buttons

    public void OnCLickKanon()
    {
        Conditions(0);
    }

    public void OnCLickCrossbow()
    {
        Conditions(1);
    }

    public void OnClickKatapult()
    {
        Conditions(2);
    }

    public void OnClickTrap()
    {
        Conditions(3);
    }

    public void OnClickMuur()
    {
        Conditions(4);
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
