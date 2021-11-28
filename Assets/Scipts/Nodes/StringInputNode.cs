using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[Node(false, "String/Input")]
public class StringInputNode : AMP_Node
{
	public const string ID = "inputStringNode";
	public override string GetID { get { return ID; } }

	public override string Title { get { return "String"; } }
	public override Vector2 DefaultSize { get { return new Vector2(250, 50); } }

	[ValueConnectionKnob("Value", Direction.Out, "string")]
	public ValueConnectionKnob outputKnob;

	public string value = "";

	public override void NodeGUI()
	{
		base.NodeGUI();

		value = RTEditorGUI.TextField(new GUIContent("Value", "The input value of type string"), value);
		outputKnob.SetPosition();
		if (GUI.changed)
			NodeEditor.curNodeCanvas.OnNodeChange(this);
	}

	public override bool Calculate()
	{
		outputKnob.SetValue<string>(value);
		return true;
	}
}
