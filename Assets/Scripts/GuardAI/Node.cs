﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Node : MonoBehaviour, IComparable<Node>
{

    private List<Node> _successors;
    private GraphOfMap _graph;

    private GameObject _gameMap;

    bool _justSpawned = true;

	void Start () {	    
	    _gameMap = GameObject.FindGameObjectWithTag("Map");
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    /**
     * Add the passed node as this node's successor
     */
    public void AddSuccessor(Node n, ref GraphOfMap graph)
    {
        Node succesor = graph.nodeWith(n);        
        _successors.Add(succesor);
    }

    /**
     * returns the list of this node's successors
     */
    public List<Node> GetSuccessors()
    {
        return _successors;
    }

    /**
     * Adds the node to the passed graph
     */
    public void SetUpNode(ref GraphOfMap graph)
    {       
        _successors = new List<Node>();
        Node newNode = graph.nodeWith(this);        
    }    

    /**
     * Adds this node horizontal neighbour as it successors
     */
    public void AddNeighbour(float direction, ref GraphOfMap graph)
    {
        for(int i = 0; i < graph.ReturnGraph().Count; i++)
        {
            Node node = graph.ReturnGraph().ElementAt(i);
            
            if (node.GetX() == GetX() - direction && node.GetY() == GetY())
            {
                AddSuccessor(node, ref graph);                
            }
        }
    }


    public int CompareTo(Node comparedNode)
    {
        if (this.GetX() == comparedNode.GetX() &&
            this.GetY() == comparedNode.GetY())
            return 0;
        else
        {
            return -1;
        }
    }

    /**
     * If this node is the position of a rising platform, add the node above it as its horizontal sucecssor's successor
     */
    void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.gameObject.layer == 10 && col.gameObject.tag == "RisingPlatform" && _justSpawned)
        {            
            Node platformNode = null;
            GraphOfMap graph = _gameMap.GetComponent<GenerateNodes>().ReturnGeneratedGraph();

            for (int i = 0; i < graph.ReturnGraph().Count; i++)
            {
                Node node = graph.ReturnGraph().ElementAt(i);

                if (node.GetX() == this.GetX() &&
                    node.GetY() == this.GetY() + 2)
                {
                    platformNode = node;
                }
            }

           for (int j = 0; j < graph.ReturnGraph().Count; j++)
           {
               Node node = graph.ReturnGraph().ElementAt(j);

                if ((node.GetX() == this.GetX() - 2 || node.GetX() == this.GetX() + 2) && node.GetY() == this.GetY())
                {                    
                    node.AddSuccessor(platformNode, ref graph);                   
                }
            }

            AddSuccessor(platformNode,ref graph);
            
            _justSpawned = false;
        }
    }

    public float GetX()
    {
        return gameObject.transform.position.x;
    }

    public float GetY()
    {
        return gameObject.transform.position.y;
    }
}