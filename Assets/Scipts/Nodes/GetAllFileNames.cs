using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[Node(false, "IO/Get All File Names")]
public class GetAllFileNames : AMP_Node
{
	public const string ID = "getAllFileNamesNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Get All File Names"; } }
	public override Vector2 DefaultSize { get { return new Vector2(150, 80); } }

	[ValueConnectionKnob("In", Direction.In, "Exec")]
	public ValueConnectionKnob execInKnob;

	[ValueConnectionKnob("Out", Direction.Out, "Exec")]
	public ValueConnectionKnob execOutKnob;

	[ValueConnectionKnob("Files Paths", Direction.Out, "List<string>")]
	public ValueConnectionKnob outputTex2DKnob;

	[ValueConnectionKnob("Dir. Path", Direction.In, "string")]
	public ValueConnectionKnob inputPathKnob;

	public override void NodeGUI()
	{
		base.NodeGUI();

		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();

		execInKnob.DisplayLayout();

		execOutKnob.DisplayLayout();

		GUILayout.EndHorizontal();

		GUILayout.Space(10);

		GUILayout.BeginHorizontal();

		inputPathKnob.DisplayLayout();

		outputTex2DKnob.DisplayLayout();

		GUILayout.EndHorizontal();
		GUILayout.EndVertical();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}

	public override bool Calculate()
	{
		return true;
	}
}
