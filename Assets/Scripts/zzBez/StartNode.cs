using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class StartNode : BaseNode {

    //This is just a designated starting node, NodeReader will start here (check Start function in script)
    [Output] public int exit;

    public override string GetString()
    {
        return "Start";
    }
}