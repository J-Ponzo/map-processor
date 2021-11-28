using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[Node(false, "Exec/Foreach")]
public class ForeachString : AMP_Node
{
	public const string ID = "foreachString";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Foreach"; } }
	public override Vector2 DefaultSize { get { return new Vector2(150, 120); } }

	[ValueConnectionKnob("In", Direction.In, "Exec")]
	public ValueConnectionKnob execInKnob;

	[ValueConnectionKnob("Loop Body", Direction.Out, "Exec")]
	public ValueConnectionKnob execLoopKnob;

	[ValueConnectionKnob("Out", Direction.Out, "Exec")]
	public ValueConnectionKnob execOutKnob;

	[ValueConnectionKnob("Array", Direction.In, "List<string>")]
	public ValueConnectionKnob inputArrayKnob;

	[ValueConnectionKnob("Elt", Direction.Out, "string")]
	public ValueConnectionKnob outputEltKnob;

	public override void NodeGUI()
	{
		base.NodeGUI();

		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();

		execInKnob.DisplayLayout();

		execLoopKnob.DisplayLayout();

		GUILayout.EndHorizontal();

		GUILayout.Space(10);

		GUILayout.BeginHorizontal();

		inputArrayKnob.DisplayLayout();

		outputEltKnob.DisplayLayout();

		GUILayout.EndHorizontal();

		GUILayout.Space(10);

		execOutKnob.DisplayLayout();

		GUILayout.EndVertical();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}

	public override bool Calculate()
	{
		return true;
	}
}
