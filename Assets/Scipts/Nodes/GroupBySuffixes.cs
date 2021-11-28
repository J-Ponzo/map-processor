using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[Node(false, "Utils/Group By Suffixes")]
public class GroupBySuffixes : AMP_Node
{
	public const string ID = "GroupBySuffixes";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "Group By Suffixes"; } }
	public override Vector2 DefaultSize { get { return new Vector2(150, 100); } }

	[ValueConnectionKnob("In", Direction.In, "Exec")]
	public ValueConnectionKnob execInKnob;

	[ValueConnectionKnob("Out", Direction.Out, "Exec")]
	public ValueConnectionKnob execOutKnob;

	[ValueConnectionKnob("Strings", Direction.In, "List<string>")]
	public ValueConnectionKnob inputStringsKnob;

	[ValueConnectionKnob("Suffixes", Direction.In, "string", ConnectionCount.Multi)]
	public ValueConnectionKnob inputSuffixesKnob;

	[ValueConnectionKnob("Radix", Direction.Out, "List<string>")]
	public ValueConnectionKnob outputGroupsKnob;

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

		inputStringsKnob.DisplayLayout();

		outputGroupsKnob.DisplayLayout();

		GUILayout.EndHorizontal();

		inputSuffixesKnob.DisplayLayout();

		GUILayout.EndVertical();

		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}

	public override bool Calculate()
	{
		return true;
	}
}
