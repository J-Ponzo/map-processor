using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[Node(false, "Utils/Merge")]
public class MergeNode : AMP_Node
{
	public const string ID = "mergeTexture2DNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Merge"; } }
	public override Vector2 DefaultSize { get { return new Vector2(100, 120); } }

	[ValueConnectionKnob("Tex2D", Direction.Out, "Texture2D")]
	public ValueConnectionKnob outputTex2DKnob;

	[ValueConnectionKnob("R", Direction.In, "RedChannel")]
	public ValueConnectionKnob inputRedChanKnob;
	[ValueConnectionKnob("G", Direction.In, "GreenChannel")]
	public ValueConnectionKnob inputGreenChanKnob;
	[ValueConnectionKnob("B", Direction.In, "BlueChannel")]
	public ValueConnectionKnob inputBlueChanKnob;
	[ValueConnectionKnob("A", Direction.In, "AlphaChannel")]
	public ValueConnectionKnob inputAlphaChanKnob;

	public override void NodeGUI()
	{
		base.NodeGUI();

		GUILayout.BeginHorizontal();

		GUILayout.BeginVertical();

		inputRedChanKnob.DisplayLayout();
		inputGreenChanKnob.DisplayLayout();
		inputBlueChanKnob.DisplayLayout();
		inputAlphaChanKnob.DisplayLayout();

		GUILayout.EndVertical();

		outputTex2DKnob.DisplayLayout();

		GUILayout.EndHorizontal();
	}

	public override bool Calculate()
	{
		return true;
	}
}
