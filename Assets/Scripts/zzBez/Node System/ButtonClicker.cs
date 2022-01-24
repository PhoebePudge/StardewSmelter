using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClicker : MonoBehaviour
{
    public NodeReader nR;

    public void Click1()
    {
        nR.OptionButtonClick1();
    }
    public void Click2()
    {
        nR.OptionButtonClick2();
    }
    public void Click3()
    {
        nR.OptionButtonClick3();
    }
}
