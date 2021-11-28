using NodeEditorFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecConnectionType : ValueConnectionType
{
	public override string Identifier { get { return "Exec"; } }
	public override Color Color { get { return Color.white; } }
	public override Type Type { get { return typeof(ExecType); } }
}

public class FloatConnectionType : ValueConnectionType
{
	public override string Identifier { get { return "float"; } }
	public override Color Color { get { return Color.green; } }
	public override Type Type { get { return typeof(float); } }
}

public class StringConnectionType : ValueConnectionType
{
	public override string Identifier { get { return "string"; } }
	public override Color Color { get { return Color.magenta; } }
	public override Type Type { get { return typeof(string); } }
}

public class ListStringConnectionType : ValueConnectionType
{
	public override string Identifier { get { return "List<string>"; } }
	public override Color Color { get { return Color.magenta; } }
	public override Type Type { get { return typeof(List<string>); } }
}

//public class DictionnaryStringStringConnectionType : ValueConnectionType
//{
//	public override string Identifier { get { return "Dictionary<string,string>"; } }
//	public override Color Color { get { return Color.magenta; } }
//	public override Type Type { get { return typeof(Dictionary<string,string>); } }
//}

//public class ListDictionnaryStringStringConnectionType : ValueConnectionType
//{
//	public override string Identifier { get { return "List<Dictionary<string,string>>"; } }
//	public override Color Color { get { return Color.magenta; } }
//	public override Type Type { get { return typeof(List<Dictionary<string, string>>); } }
//}

public class Texture2DConnectionType : ValueConnectionType
{
	public override string Identifier { get { return "Texture2D"; } }
	public override Color Color { get { return Color.yellow; } }
	public override Type Type { get { return typeof(Texture2D); } }
}

public class RedChannelConnectionType : ValueConnectionType
{
	public override string Identifier { get { return "RedChannel"; } }
	public override Color Color { get { return Color.red; } }
	public override Type Type { get { return typeof(Texture2D); } }
}

public class GreenChannelConnectionType : ValueConnectionType
{
	public override string Identifier { get { return "GreenChannel"; } }
	public override Color Color { get { return Color.green; } }
	public override Type Type { get { return typeof(Texture2D); } }
}

public class BlueChannelConnectionType : ValueConnectionType
{
	public override string Identifier { get { return "BlueChannel"; } }
	public override Color Color { get { return Color.blue; } }
	public override Type Type { get { return typeof(Texture2D); } }
}

public class AlphaChannelConnectionType : ValueConnectionType
{
	public override string Identifier { get { return "AlphaChannel"; } }
	public override Color Color { get { return Color.gray; } }
	public override Type Type { get { return typeof(Texture2D); } }
}


