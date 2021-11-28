using NodeEditorFramework;
using NodeEditorFramework.Standard;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_NodeCanvasTraversal : NodeCanvasTraversal
{
	public string procedureName;
	public List<Node> workList;
	public MP_NodeCanvasTraversal(NodeCanvas canvas) : base(canvas) { }

	public override void TraverseAll()
	{
	}

    public void StartProcedure(string procedureName)
    {
		Debug.Log("Exec " + procedureName);

		workList = new List<Node>();
		for (int i = 0; i < nodeCanvas.nodes.Count; i++)
		{
			Node node = nodeCanvas.nodes[i];
			if (node.isInput())
			{ // Add all Inputs
				node.ClearCalculation();
				workList.Add(node);
			}
		}
		StartCalculation();
	}

    /// <summary>
    /// Recalculate from the specified node
    /// </summary>
    public override void OnChange(Node node)
	{
		//node.ClearCalculation();
		//workList = new List<Node> { node };
		//StartCalculation();
	}

	/// <summary>
	/// Iteratively calculates all nodes from the worklist, including child nodes, until no further calculation is possible
	/// </summary>
	private void StartCalculation()
	{
		if (workList == null || workList.Count == 0)
			return;

		bool limitReached = false;
		while (!limitReached)
		{ // Runs until the whole workList is calculated thoroughly or no further calculation is possible
			limitReached = true;
			for (int workCnt = 0; workCnt < workList.Count; workCnt++)
			{ // Iteratively check workList
				if (ContinueCalculation(workList[workCnt]))
					limitReached = false;
			}
		}
		if (workList.Count > 0)
		{
			Debug.LogError("Did not complete calculation! " + workList.Count + " nodes block calculation from advancing!");
			foreach (Node node in workList)
				Debug.LogError("" + node.name + " blocks calculation!");
		}
	}

	/// <summary>
	/// Recursively calculates this node and it's children
	/// All nodes that could not be calculated in the current state are added to the workList for later calculation
	/// Returns whether calculation could advance at all
	/// </summary>
	private bool ContinueCalculation(Node node)
	{
		if (node.calculated)
		{ // Already calculated
			workList.Remove(node);
			return true;
		}
		if (node.ancestorsCalculated() && node.Calculate())
		{ // Calculation was successful
			node.calculated = true;
			workList.Remove(node);
			if (node.ContinueCalculation)
			{ // Continue with children
				for (int i = 0; i < node.outputPorts.Count; i++)
				{
					ConnectionPort outPort = node.outputPorts[i];
					for (int t = 0; t < outPort.connections.Count; t++)
						ContinueCalculation(outPort.connections[t].body);
				}
			}
			return true;
		}
		else if (!workList.Contains(node))
		{ // Calculation failed, record to calculate later on
			workList.Add(node);
		}
		return false;
	}
}
