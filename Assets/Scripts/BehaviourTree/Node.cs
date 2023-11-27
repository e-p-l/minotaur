using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        public NodeState state;
        public bool isRoot;
        public Node parent;
        public List<Node> children = new List<Node>();

        public UnityEngine.GameObject target;
        public UnityEngine.GameObject treasure;
        public UnityEngine.Transform transform;
        public UnityEngine.Vector3[] destinations;

        public Node()
        {
            this.parent = null;
            this.isRoot = false;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                AddChildren(child);

            this.isRoot = false;
        }

        public void AddChildren(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, GameObject value)
        {
            if (this.isRoot)
            {
                this.target = value;
            }
            else
            {
                this.parent.SetData(key, value);
            }
        }

        public GameObject GetData(string key)
        {
            if (this.isRoot)
            {
                return this.target;
            }
            else
            {
                return this.parent.GetData(key);
            }
        }

        public void ClearData(string key)
        {
            if (this.isRoot)
            {
                this.target = null;
            }
            else
            {
                this.parent.ClearData(key);
            }
        }
    }

}

