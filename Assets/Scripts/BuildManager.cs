using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake () {
        if(instance != null) {
            Debug.Log("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject speedTurretPrefab;

    private TurretBluePrint turretToBuild;
    
    public bool CanBuild {
        get {   // If => return true  ; else return false
            return turretToBuild != null;
        }
    }

    public void BuildTurretOn (Node node) {
        if(PlayerStats.Money < turretToBuild.cost) {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        Debug.Log("Money: " + PlayerStats.Money);
        
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret build!");
    }

    public void SelectTurretToBuild(TurretBluePrint turret) {
        turretToBuild = turret;
    }
     
}
