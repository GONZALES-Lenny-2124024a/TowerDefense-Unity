using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffSet;

    public GameObject turret;

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

        if (!buildManager.CanBuild) { return; } //Start of the game => none turret selected

        if (turret != null) {   //We can't build a turret on this node
            Debug.Log("Can't build there");
            return;
        }
        //We can build a turret on this node
        buildManager.BuildTurretOn(this);
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
