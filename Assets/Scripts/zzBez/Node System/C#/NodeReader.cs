using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using TMPro;

public class NodeReader : MonoBehaviour
{
    // This script is attached to a game object we'll call Node Reader
    // We actually write our nodes methods within here and just make our nodes with identifiers attached

    // You make the conversation by right clicking in the project and creating a "Test Node Graph", the Graph will hold that specific convo

    // In that graph (double click it) you'll make your conversation using the nodes we create


    // We may want to make more readers for different functionality to streamline code, like a shop/quest reader as seperate to avoid weeks of debugging 0_0
    
    // Should change this things name early (after demo)
    public TestNodeGraph graph;


    
    Coroutine _parser;

    //Who speaking
    public TextMeshProUGUI speaker;
    //What they say
    public TextMeshProUGUI dialogue;
    //What they look like
    public Image speakerImage;
    //Buttons for options
    public Button option1;

    public Button option2;

    public Button option3;

    public Text reply1;

    public Text reply2;

    public Text reply3;

    public GameObject dialogueCanvas;

    public int optionSelected;

    private void Start()
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

    public void Update()
    {
        if (optionSelected !=0)
        {
          
        }
    }
    // Tucking our conversation within a trigger
    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        dialogueCanvas.SetActive(true);
    //        //There may be a better way to find start than a foreach but that's what was in the tutorial
    //        foreach (BaseNode b in graph.nodes)
    //        {
    //            //If our string is >
    //            if (b.GetString() == "Start")
    //            {
    //                //Make it our current node at Start
    //                graph.current = b;
    //                break;
    //            }
    //        }
    //        //Run our checker
    //        _parser = StartCoroutine(ParseNode());
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{

    //    dialogueCanvas.SetActive(false);

    //}

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
            //PauseGame();
        }
        if (dataParts[0] == "DialogueNode")
        {
            //Set our first String to speaker text
            speaker.text = dataParts[1];
            //etc..
            dialogue.text = dataParts[2];
            //Set the sprite from our current code line
            //speakerImage.sprite = b.GetSprite();

            // Bad way of holding canvas between states, needs switching out
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));
            //Call the next node

            NextNode("exit");
            
        }
        if (dataParts[0] == "DialogueOptionsNode")
        {
            //Set our first String to speaker text
            speaker.text = dataParts[1];
            //etc..
            dialogue.text = dataParts[2];

            reply1.text = dataParts[3];

            reply2.text = dataParts[4];

            reply3.text = dataParts[5];
            //Set the sprite from our current code line
            //speakerImage.sprite = b.GetSprite();

            


            // Bad way of holding canvas between states, needs switching out
            // Call the next node
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));

            NextNode("exit");
            
        }
        if (dataParts[0] == "End")
        {
            dialogueCanvas.SetActive(false);
            Debug.Log("Canvas should be off");
            //ResumeGame();
        }

    }

    // We attach our node reading handler to button components and call from there
    public void OptionButtonClick1()
    {
        optionSelected = 1;
    }

    public void OptionButtonClick2()
    {
        optionSelected = 2;
    }

    public void OptionButtonClick3()
    {
        optionSelected = 3;
    }
    /*
    void PauseGame()
    {
        Time.timeScale = 0.1f;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }
    */

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
            if (p.fieldName == "exit" + optionSelected)
            {
                graph.current = p.Connection.node as BaseNode;
                break;
            }
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
