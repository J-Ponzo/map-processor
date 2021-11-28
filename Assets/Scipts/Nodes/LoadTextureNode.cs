using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[Node(false, "IO/Load")]
public class LoadTextureNode : AMP_Node
{
	public const string ID = "loadTexture2DNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Load"; } }
	public override Vector2 DefaultSize { get { return new Vector2(100, 80); } }

	[ValueConnectionKnob("In", Direction.In, "Exec")]
	public ValueConnectionKnob execInKnob;

	[ValueConnectionKnob("Out", Direction.Out, "Exec")]
	public ValueConnectionKnob execOutKnob;

	[ValueConnectionKnob("Tex2D", Direction.Out, "Texture2D")]
	public ValueConnectionKnob outputTex2DKnob;

	[ValueConnectionKnob("Path", Direction.In, "string")]
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
