using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class NodeReader : MonoBehaviour
{
    //This script is attached to a game object with the specific conversation graph

    //It is specifically a dialogue node reader here really 
    //We may want to make more readers for different functionality to streamline code

    // Should change this things name early
    public TestNodeGraph graph;
    
    Coroutine _parser;

    //Who speaking
    public Text speaker;
    //What they say
    public Text dialogue;
    //What they look like
    public Image speakerImage;
    //Buttons for options
    public Button option1;

    public Button option2;

    public Text reply1;

    public Text reply2;

    public GameObject dialogueCanvas;

    public int optionSelected;

    private void Start()
    {
        dialogueCanvas.SetActive(false);        
    }

    //Tucking our conversation within a trigger
    private void OnTriggerEnter(Collider other)
    {

        dialogueCanvas.SetActive(true);

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

    private void OnTriggerExit(Collider other)
    {

        dialogueCanvas.SetActive(false);

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
        if (dataParts[0] == "DialogueOptionsNode")
        {
            //Set our first String to speaker text
            speaker.text = dataParts[1];
            //etc..
            dialogue.text = dataParts[2];
            //Set the sprite from our current code line
            speakerImage.sprite = b.GetSprite();

            reply1.text = dataParts[3];

            reply2.text = dataParts[4];

            // Bad way of holding canvas between states, needs switching out
            //option1.onClick.AddListener(OptionButtonClick1);
            //option2.onClick.AddListener(OptionButtonClick2);
            //Call the next node
            NextNode("exit");
        }
    }

    public void OptionButtonClick1()
    {
        optionSelected = 1;
    }

    public void OptionButtonClick2()
    {
        optionSelected = 2;
    }

    public void NextNode(string fieldName)
    {   
        //Check if our parser is null, if not
        if (_parser != null)
        {
            //Do the thing
            StopCoroutine(_parser);
            _parser = null;
        }
        //Checking each port in our node of the current section of our graph (Ports are our Input/output to the nodes)
        foreach (NodePort p in graph.current.Ports)
        {
            if (p.fieldName == fieldName)
            {
                //set our chosen next as .current
                graph.current = p.Connection.node as BaseNode;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }
}
