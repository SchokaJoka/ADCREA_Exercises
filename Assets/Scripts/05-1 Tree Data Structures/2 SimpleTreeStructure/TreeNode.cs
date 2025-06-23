using UnityEngine;
using System.Collections.Generic;

public class TreeNode : MonoBehaviour
{
    public int value;
    public TreeNode parent;
    public List<TreeNode> children = new List<TreeNode>();
}
