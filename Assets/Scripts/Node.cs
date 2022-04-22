using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffSet;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start () {
        rend = GetComponent<Renderer>();  
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition() {
        return transform.position + positionOffSet;
    }

    void OnMouseDown () {   //Clic
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        if (turret != null) {   //We can't build a turret on this node
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild) { return; } //Start of the game => none turret selected

        //We can build a turret on this node
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret (TurretBluePrint blueprint) {
        if(PlayerStats.Money < blueprint.cost) {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        Debug.Log("Money: " + PlayerStats.Money);
        
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = blueprint;

        GameObject buildEffect = (GameObject) Instantiate(buildManager.BuildTurretEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5f);

        Debug.Log("Turret build!");
    }

    public void UpgradeTurret () {
        if(PlayerStats.Money < turretBluePrint.upgradeCost) {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= turretBluePrint.upgradeCost;
        Debug.Log("Money: " + PlayerStats.Money);
        
        //Get rid of the old turret
        Destroy(turret); 

        //Build a new one
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject buildEffect = (GameObject) Instantiate(buildManager.BuildTurretEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5f);

        isUpgraded = true;
        Debug.Log("Turret upgraded!");

    }
    
    void OnMouseEnter () {  //Hover
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        if (!buildManager.CanBuild) { return; } //Start of the game => none turret selected

        if(buildManager.HasMoney) {
            rend.material.color = hoverColor;
        } else {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit () {
        rend.material.color = startColor;   //Change the color of the node to default color
    }
}
