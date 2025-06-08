using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TreeNodeVisualizer : MonoBehaviour
{
    //##  Tree Parameters
    public Vector3 treeRootPosition = new Vector3(0, 0, 0);
    public float verticalNodeOffset = 1.0f;
    public float horizontalNodeOffset = 2.0f;

    //##  Node Parameters for Visualization
    public int nodeDepth;
    private Vector3 nodeScale = new Vector3(1f, 0.05f, 0.5f);
    private Quaternion nodeRotation = Quaternion.Euler(90, 0, 0);

    private TreeNodeScript treeNodeScript;  //  Reference to TreeNodeScript component
    private TreeGenerator treeGenerator;    //  Reference to TreeGenerator component
    private TextMeshPro nodeLabel;          //  Reference to TextMeshPro component
    private LineRenderer lineToParent;      //  Reference to LineRenderer component


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        treeNodeScript = gameObject.GetComponent<TreeNodeScript>();  //  Get reference to TreeNode component
        treeGenerator = FindAnyObjectByType<TreeGenerator>();        //  Get reference to TreeGenerator component
        nodeLabel = gameObject.GetComponentInChildren<TextMeshPro>();

        // Define line renderer for line to parent node
        lineToParent = gameObject.AddComponent<LineRenderer>();
        lineToParent.material = new Material(Shader.Find("Sprites/Default"));
        lineToParent.enabled = false;
    }


    public void UpdateNodeVisuals()
    {
        
        treeNodeScript = gameObject.GetComponent<TreeNodeScript>(); //  Get reference to TreeNode component
        gameObject.name = "NodeShape_" + treeNodeScript.nodeName;   //  Set names
        if (nodeLabel != null)
        {
            nodeLabel.text = treeNodeScript.nodeName;
        }

        //  Find current node depth
        bool rootNodeNotFound = true;
        nodeDepth = 0;
        GameObject hierarchicalParent = treeNodeScript.parentNode;
        while (rootNodeNotFound && hierarchicalParent != null)
        {
            nodeDepth++;
            if (hierarchicalParent == null)
            {
                rootNodeNotFound = false;
            }
            else {
                hierarchicalParent = hierarchicalParent.GetComponent<TreeNodeScript>().parentNode;
            }
            if (nodeDepth > treeGenerator.currentMaxDepth)
            {
                treeGenerator.currentMaxDepth = nodeDepth;
//                Debug.Log("New max depth: " + treeGenerator.currentMaxDepth);
            }
        }

        //  Ajust node position based on node level
        gameObject.transform.localPosition = OptimizedNodePosition(treeNodeScript);

        //  Ajust node size and color based on node level
        gameObject.transform.localScale = nodeScale / (nodeDepth*0.5f + 1);
        nodeLabel.fontSize = OptimizeFontSize(treeNodeScript.nodeName);
        Color rainbowColor = Color.HSVToRGB(nodeDepth / 4f, 1f, 1f);
        gameObject.GetComponent<Renderer>().material.color = rainbowColor;


        // Draw line to parent node
        if (treeNodeScript.parentNode != null)
        {
//            Debug.Log("Drawing lineToParent from " + treeNodeScript.nodeName + " to " + treeNodeScript.parentNode.GetComponent<TreeNodeScript>().nodeName);
            lineToParent.enabled = true;
            lineToParent.positionCount = 2;
            Vector3 lineZOffset = new Vector3(0, 0, 0.2f);
            lineToParent.SetPosition(0, treeNodeScript.parentNode.gameObject.transform.localPosition + lineZOffset);
            lineToParent.SetPosition(1, gameObject.transform.localPosition + lineZOffset);
            lineToParent.startWidth = 0.05f / nodeDepth;
            lineToParent.endWidth = 0.05f / nodeDepth;
        }

        // Update child nodes
        foreach (GameObject childNode in treeNodeScript.childNodes)
        {
            childNode.GetComponent<TreeNodeScript>().UpdateNode();
        }
    }

    private float OptimizeFontSize(string nodeName)
    {
        switch (nodeName.Length)
        {
            case <= 4: return (3.0f);
            case <= 8: return (2.0f);
            case <= 12: return (1.4f);
            case <= 16: return (1.0f);
            default: return (0.3f);
        }
    }

    //  Root node X position is always 0.
    //  Childe node X position depends on
    //  - the x position of the parent node,
    //  - the number of child nodes, and
    //  - the index of the child node in the list of child nodes.
    //  ... and is scaled based on the depth of the node
    private Vector3 OptimizedNodePosition(TreeNodeScript treeNodeScript)
    {
        Vector3 nodePosition;
        if (treeNodeScript.parentNode == null)
        {
            nodePosition.x = 0;
        }
        else
        {
            TreeNodeScript parentTreeNodeScript = treeNodeScript.parentNode.GetComponent<TreeNodeScript>();
            float localOffsetX = -0.5f * horizontalNodeOffset * (parentTreeNodeScript.childNodes.Count - 1)
                               + horizontalNodeOffset * parentTreeNodeScript.childNodes.IndexOf(gameObject);
            localOffsetX = localOffsetX / (nodeDepth - 0.5f);
            nodePosition.x = treeNodeScript.parentNode.transform.localPosition.x + localOffsetX;
        }  
        nodePosition.y = verticalNodeOffset * (treeGenerator.currentMaxDepth - nodeDepth);
        nodePosition.z = 0;
        return nodePosition; 
    }

}
