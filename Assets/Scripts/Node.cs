using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffSet;

    private GameObject turret;

    private Renderer rend;
    private Color startColor;

    void Start () {
        rend = GetComponent<Renderer>();  
        startColor = rend.material.color;
    }

    void OnMouseDown () {   //Clic
        if (turret != null) {   //We can't build a turret on this node
            Debug.Log("Can't build there");
            return;
        }
        //We can build a turret on this node
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject) Instantiate(turretToBuild, transform.position + positionOffSet, transform.rotation);
    }

    void OnMouseEnter () {  //Hover
        rend.material.color = hoverColor;   //Change the color of the node
    }

    void OnMouseExit () {
        rend.material.color = startColor;   //Change the color of the node to default color
    }
}
