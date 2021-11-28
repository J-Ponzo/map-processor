using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[Node(false, "Utils/Split")]
public class SplitNode : AMP_Node
{
	public const string ID = "splitTexture2DNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Split"; } }
	public override Vector2 DefaultSize { get { return new Vector2(100, 120); } }

	[ValueConnectionKnob("Tex2D", Direction.In, "Texture2D")]
	public ValueConnectionKnob inputTex2DKnob;

	[ValueConnectionKnob("R", Direction.Out, "RedChannel")]
	public ValueConnectionKnob outputRedChanKnob;
	[ValueConnectionKnob("G", Direction.Out, "GreenChannel")]
	public ValueConnectionKnob outputGreenChanKnob;
	[ValueConnectionKnob("B", Direction.Out, "BlueChannel")]
	public ValueConnectionKnob outputBlueChanKnob;
	[ValueConnectionKnob("A", Direction.Out, "AlphaChannel")]
	public ValueConnectionKnob outputAlphaChanKnob;

	public override void NodeGUI()
	{
		base.NodeGUI();

		GUILayout.BeginHorizontal();

		inputTex2DKnob.DisplayLayout();

		GUILayout.BeginVertical();

		outputRedChanKnob.DisplayLayout();
		outputGreenChanKnob.DisplayLayout();
		outputBlueChanKnob.DisplayLayout();
		outputAlphaChanKnob.DisplayLayout();

		GUILayout.EndVertical();

		GUILayout.EndHorizontal();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}

	public override bool Calculate()
	{
		return true;
	}
}
