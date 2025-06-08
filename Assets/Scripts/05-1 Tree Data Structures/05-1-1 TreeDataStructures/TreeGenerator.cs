using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public int currentMaxDepth = 4;

    //##  Internal variables
    private GameObject rootNode;
    private TreeNodeScript treeRootScript;

    private TextInputModule keyboardInputHandler;   //  Reference to the keyboard input handler
    private GameObject Prefab_Node;

    void OnEnable()
    {
        keyboardInputHandler = FindAnyObjectByType<TextInputModule>();
        keyboardInputHandler.AddNewNodeToTree += GenerateNewNode;
    }

    void OnDisable()
    {
        keyboardInputHandler.AddNewNodeToTree -= GenerateNewNode;
    }

    private void Start()
    {
        Prefab_Node = Resources.Load<GameObject>("Prefabs/Prefab_VisualNode");

        rootNode = InstantiateNewNode("Root", null);
        treeRootScript = rootNode.GetComponent<TreeNodeScript>();
        treeRootScript.UpdateNode();
    }


    //##  Generate New Node
    //######################
    //
    // - Check if there is a node with the first letter of the input under the root node
    //      - If there is, add the new node as a child of that node
    //      - If there isn't, create a new node under the root node with the first letter,
    //        and create a new node with the input name under that node
    private void GenerateNewNode(string nodeName)
    {
        bool nodeNotFound = true;
        char firstLetter = nodeName.ToUpper()[0];
        foreach (GameObject childNode in treeRootScript.childNodes)
        {
            if (childNode.GetComponent<TreeNodeScript>().nodeName[0] == firstLetter)
            {
                GameObject newTreeNode = InstantiateNewNode(nodeName, childNode);
                nodeNotFound = false;
            }
        }

        // If no child node with the first letter is found, create a new node with the first letter
        // and then add the new word node as a child
        if (nodeNotFound)
        {
            GameObject newLetterNode = InstantiateNewNode(firstLetter.ToString(), rootNode);

            // Only create word node, if the input is more than one character long
            if (nodeName.Length > 1)
            {
                GameObject newWordNode = InstantiateNewNode(nodeName, newLetterNode);
            }
        }

        //  Refresh all nodes in the tree
        treeRootScript.UpdateNode();
    }





    //  Instantiate a new tree node with its scripts
    private GameObject InstantiateNewNode(string nodeName, GameObject parentNode)
    {
        GameObject newTreeNode = Instantiate(Prefab_Node, transform);
        newTreeNode.AddComponent<TreeNodeScript>();
        newTreeNode.AddComponent<TreeNodeVisualizer>();

        TreeNodeScript newTreeNodeScript = newTreeNode.GetComponent<TreeNodeScript>();  //  Reference to the new node's script
        newTreeNodeScript.nodeName = nodeName;                                          //  Set node name
        if (parentNode != null)
        {
            newTreeNodeScript.parentNode = parentNode;                                         //  Set parent node
            TreeNodeScript parentTreeNodeScript = parentNode.GetComponent<TreeNodeScript>();   //  Reference to the parent node's script
            parentTreeNodeScript.childNodes.Add(newTreeNode);                                  //  Add new node as child of new letter node
        }
        else
        {
            newTreeNodeScript.parentNode = null;
        }

        return newTreeNode;
    }

}
