using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[Node(false, "IO/Save")]
public class SaveTextureNode : AMP_Node
{
	public const string ID = "saveTexture2DNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Save"; } }
	public override Vector2 DefaultSize { get { return new Vector2(100, 100); } }

	[ValueConnectionKnob("In", Direction.In, "Exec")]
	public ValueConnectionKnob execInKnob;

	[ValueConnectionKnob("Out", Direction.Out, "Exec")]
	public ValueConnectionKnob execOutKnob;

	[ValueConnectionKnob("Path", Direction.In, "string")]
	public ValueConnectionKnob inputPathKnob;

	[ValueConnectionKnob("Tex2D", Direction.In, "Texture2D")]
	public ValueConnectionKnob inputTex2DKnob;

	public override void NodeGUI()
	{
		base.NodeGUI();

		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();

		execInKnob.DisplayLayout();

		execOutKnob.DisplayLayout();

		GUILayout.EndHorizontal();

		GUILayout.Space(10);

		inputPathKnob.DisplayLayout();

		inputTex2DKnob.DisplayLayout();

		GUILayout.EndVertical();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}

	public override bool Execute(out AMP_Node nextNode)
	{
		base.Execute(out nextNode);

		if (execOutKnob.connections.Count > 0)
			nextNode = (AMP_Node)execOutKnob.connections[0].body;

		executed = true;
		return true;
	}
	public override bool Calculate()
	{
		return true;
	}
}
