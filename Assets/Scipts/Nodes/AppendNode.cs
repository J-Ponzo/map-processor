using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[Node(false, "String/Append")]
public class AppendNode : AMP_Node
{
	public const string ID = "appendNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Append"; } }
	public override Vector2 DefaultSize { get { return new Vector2(150, 65); } }

	[ValueConnectionKnob("Output String", Direction.Out, "string")]
	public ValueConnectionKnob outputStringKnob;

	[ValueConnectionKnob("Input 1", Direction.In, "string")]
	public ValueConnectionKnob inputString1Knob;
	[ValueConnectionKnob("Input 2", Direction.In, "string")]
	public ValueConnectionKnob inputString2Knob;

	public override void NodeGUI()
	{
		base.NodeGUI();

		GUILayout.BeginVertical();

		GUILayout.BeginHorizontal();

		inputString1Knob.DisplayLayout();

		outputStringKnob.DisplayLayout();

		GUILayout.EndHorizontal();

		inputString2Knob.DisplayLayout();

		GUILayout.EndVertical();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}

	public override bool Calculate()
	{
		return true;
	}
}
