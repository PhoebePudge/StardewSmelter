using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class NodeReader : MonoBehaviour
{
    // Should change this things name early
    //It is specifically a dialogue node reader here really 
    //We may want to make more readers for different functionality to streamline code
    public TestNodeGraph graph;
    
    Coroutine _parser;

    public Text speaker;
    public Text dialogue;
    public Image speakerImage;

    private void Start()
    {
        //There may be a better way to find start than a foreach but that's what was in the tutorial
        foreach (BaseNode b in graph.nodes)
        {
            //If our string is >
            if (b.GetString() == "Start")
            {
                //Make it our current node at Start
                graph.current = b;
                break;
            }
        }
        //Run our checker
        _parser = StartCoroutine(ParseNode());
    }
    //Node checker 
    IEnumerator ParseNode()
    {
        //Reference current node
        BaseNode b = graph.current;
        //Load our GetString data from b
        string data = b.GetString();
        // Create an array of strings split from the elements of our GetString in b
        string[] dataParts = data.Split('/');
        //Some ifs to identify our node type, could use alternative but just following the tutorial for now
        if (dataParts[0] == "Start")
        {
            NextNode("exit");
        }
        if (dataParts[0] == "DialogueNode")
        {
            //Set our first String to speaker text
            speaker.text = dataParts[1];
            //etc..
            dialogue.text = dataParts[2];
            //Set the sprite from our current code line
            speakerImage.sprite = b.GetSprite();

            // Bad way of holding canvas between states, needs switching out
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            //Call the next node
            NextNode("exit");
        }
    }
    
    public void NextNode(string fieldName)
    {   
        //Check if our parser is null
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }
        // checking each node in our port now called p within the ports of the current area of our graph  (Ports are our Input/output to the nodes)
        foreach (NodePort p in graph.current.Ports)
        {
            if (p.fieldName == fieldName)
            {
                graph.current = p.Connection.node as BaseNode;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }
}
