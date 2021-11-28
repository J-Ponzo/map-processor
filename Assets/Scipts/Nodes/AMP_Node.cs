using NodeEditorFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMP_Node : Node
{
    public bool executed;

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

    public virtual bool Execute(out AMP_Node nextNode)
    {
        if (IsPure)
            throw new System.Exception("Pure nodes cannot be executed");

        Debug.Log(GetID);

        nextNode = null;
        return false;
    }

    public void ClearExecution()
    {
        executed = false;
    }
}
