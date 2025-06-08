using UnityEngine;

public class TreeStructure : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //  Create a root node
        TreeNode root = gameObject.AddComponent<TreeNode>();
        root.value = 0;
        root.parent = null;

        //  Add childe node 1
        TreeNode node_1 = gameObject.AddComponent<TreeNode>();
        node_1.value = 1;
        node_1.parent = root;
        root.children.Add(node_1);

        //  Add childe node 10
        TreeNode node_10 = gameObject.AddComponent<TreeNode>();
        node_10.value = 10;
        node_10.parent = node_1;
        node_1.children.Add(node_10);

        //  Add childe node 12
        TreeNode node_12 = gameObject.AddComponent<TreeNode>();
        node_12.value = 12;
        node_12.parent = node_1;
        node_1.children.Add(node_12);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
