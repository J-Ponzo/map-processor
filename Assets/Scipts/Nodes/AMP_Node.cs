using NodeEditorFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMP_Node : Node
{
    public bool IsPure { get
        {
            foreach (ConnectionKnob connectionKnob in connectionKnobs)
                if (connectionKnob.styleID == "Exec")
                    return false;

            return true;
        }
    }

    public override void NodeGUI()
    {
    }
}
