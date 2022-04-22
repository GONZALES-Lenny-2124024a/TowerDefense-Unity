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
    public Node selectedNode;
    public NodeUI nodeUI;
    public GameObject BuildTurretEffect;
    
    public bool CanBuild {
        get {   // If => return true  ; else return false
            return turretToBuild != null;
        }
    }

    public bool HasMoney {
        get {   // If => return true  ; else return false
            return PlayerStats.Money >= turretToBuild.cost;
        }
    }

    public void SelectNode (Node node) {
        if (selectedNode == node) { //If i click a second time on th same node, the ui disappear
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode() {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBluePrint turret) {
        turretToBuild = turret;
        DeselectNode();
    }
    
    public TurretBluePrint GetTurretToBuild () {
        return turretToBuild;
    }
    
    
}
