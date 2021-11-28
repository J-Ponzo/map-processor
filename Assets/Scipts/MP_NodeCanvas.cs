using NodeEditorFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeCanvasType("Maps Processing")]
public class MP_NodeCanvas : NodeCanvas
{
	public override string canvasName { get { return "Maps Processing"; } }

	protected override void OnCreate()
	{
		Traversal = new MP_NodeCanvasTraversal(this);
	}

	public void OnEnable()
	{
		// Register to other callbacks, f.E.:
		//NodeEditorCallbacks.OnDeleteNode += OnDeleteNode;
	}

	protected override void ValidateSelf()
	{
        if (Traversal == null)
            Traversal = new MP_NodeCanvasTraversal(this);
    }

    public void StartProcedure(string procedureName)
    {
		if (Traversal != null)
			((MP_NodeCanvasTraversal)Traversal).StartProcedure(procedureName);
	}
}
