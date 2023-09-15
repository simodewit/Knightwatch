using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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
    public PlacingFromTowerScript script2;

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
            if (Physics.Raycast(ray, out hit, 100000,layer))
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
        if (script2.collides == true)
        {
            if(inBuildingPhase == true)
            {
                backButton.SetActive(false);
                panel.SetActive(true);
                inBuildingPhase = false;
                //enable tower
            }
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
}
