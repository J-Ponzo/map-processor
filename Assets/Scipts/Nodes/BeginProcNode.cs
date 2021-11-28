using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[Node(false, "Exec/BeginProc")]
public class BeginProcNode : AMP_Node
{
	public const string ID = "beginProcNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Begin"; } }
	public override Vector2 DefaultSize { get { return new Vector2(200, 50); } }

	[ValueConnectionKnob("Out", Direction.Out, "Exec")]
	public ValueConnectionKnob outputExecKnob;

	public string procName;

    protected override void OnCreate()
    {
        base.OnCreate();

		int nbBeginProcNodes = 0;
		foreach (Node node in canvas.nodes)
			if (node is BeginProcNode)
				nbBeginProcNodes++;

		procName = "Procedure " + nbBeginProcNodes;
    }

    public override void NodeGUI()
	{
		base.NodeGUI();

		procName = RTEditorGUI.TextField(new GUIContent("Proc. Name", "The name of the procedure"), procName);
		outputExecKnob.SetPosition();
		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}

	public override bool Calculate()
	{
		
		return true;
	}
}
