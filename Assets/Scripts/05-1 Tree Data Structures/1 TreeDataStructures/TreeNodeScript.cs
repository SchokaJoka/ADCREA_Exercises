using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class TreeNodeScript : MonoBehaviour
{
    //##  Primary Node Parameters
    public string nodeName = "Undefined";
    public GameObject parentNode = null;
    public List<GameObject> childNodes = new List<GameObject>();

    private TreeNodeVisualizer treeNodeVisualizer;

    public void UpdateNode()
    {        
        if (treeNodeVisualizer == null)
        {
            treeNodeVisualizer = gameObject.GetComponent<TreeNodeVisualizer>();
        }
        treeNodeVisualizer.UpdateNodeVisuals();

        //  Refresh all child nodes
        foreach (GameObject childNode in childNodes)
        {
            childNode.GetComponent<TreeNodeScript>().UpdateNode();
        }
    }   

}
