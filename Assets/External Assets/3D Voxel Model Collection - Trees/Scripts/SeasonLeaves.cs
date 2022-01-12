using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonLeaves : MonoBehaviour
{
   
    //Array of textures, for changing foliage
    public Texture[] textures;

    private string[] Mounts;

    //Changing the selected material
    public Material Leaves;

    public Text MountsText;

    //Month counter
    private int MountsCoin;

    //add month button
    public void RightMounts()
    {   
        MountsCoin++; 
        if (MountsCoin > 11)
        {          
            MountsCoin = 0;
        }
         WorkMounts();     
    }
    // take away a month button
    public void LeftMounts()
    { 
        MountsCoin--; 
        if (MountsCoin < 0)
        {
            MountsCoin = 11;
        }
        WorkMounts();
    }
    private void WorkMounts()
    {
        MountsText.text = Mounts[MountsCoin] + "";
        //Changing the texture of the material
        Leaves.mainTexture = textures[MountsCoin];
    }
    void Start()
    {
        //Creating an array of months of the year
        Mounts = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    }
}
