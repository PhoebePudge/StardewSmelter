using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//make placment of objects on click
//apply placment to a grid
//allow only one obnject to be placed per grid

public class BuildingManager : MonoBehaviour {
    public static int MAPWIDTH = 100;
    public static int MAPHEIGHT = 100;
    public TextMeshProUGUI outputLog;
    [SerializeField] GameObject Hover;

    public static Tools CurrentTool = Tools.dig;

    [System.Serializable]
    public enum Tools {dig, build, place, inspect};

    public static int[,] DATAMAP = new int[MAPWIDTH, MAPHEIGHT];

    public void ChangeTool(int newtool) {
        CurrentTool = (Tools)newtool;
    }

    // Update is called once per frame
    void Update() {
        //Hover Selection Marker
        //Raycast Position 
        RaycastHit hoverHit;
        Ray hoverRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        int mapPosition = 0;
        if (Physics.Raycast(hoverRay, out hoverHit, 100.0f, 1 << LayerMask.NameToLayer("Ground"))) {

            //Set Gameobject to looked at position
            //--Add in space checking and grid aligntment 
            Vector3 newPosition = new Vector3();
            newPosition.x = (int)hoverHit.point.x;
            newPosition.z = (int)hoverHit.point.z;
            newPosition.y = 0.01f;

            mapPosition = GetComponent<MapGenerator>().map[(MAPWIDTH / 2) + (int)hoverHit.point.x, (MAPHEIGHT / 2) + (int)hoverHit.point.z];
            if (mapPosition == 1) {
                newPosition.y = 4.51f;
            }
             
            Hover.transform.position = new Vector3(
                Mathf.Lerp(Hover.transform.position.x, newPosition.x, Time.deltaTime * 10),
                newPosition.y,
                Mathf.Lerp(Hover.transform.position.z, newPosition.z, Time.deltaTime * 10)
                );
            //Hover.transform.position = newPosition;
        }

        switch (CurrentTool) {
            case (Tools.dig):
                if (Input.GetMouseButton(0)) {
                    if (mapPosition == 1) {
                        GetComponent<MapGenerator>().map[(MAPWIDTH / 2) + (int)hoverHit.point.x, (MAPHEIGHT / 2) + (int)hoverHit.point.z] = 0;
                        GetComponent<MapGenerator>().GenerateMesh(); 
                    }
                }
                break;

            case (Tools.place):
                //mouse click
                if (Input.GetMouseButtonDown(0)) {

                    //find a raycast from the mouse position  
                    Vector3 newPosition = new Vector3();
                    newPosition.x = (int)Hover.transform.position.x;
                    newPosition.z = (int)Hover.transform.position.z;
                    newPosition.y = 0;

                    if (DATAMAP[(MAPWIDTH / 2) + (int)newPosition.x, (MAPHEIGHT / 2) + (int)newPosition.z] == 0) {
                        DATAMAP[(MAPWIDTH / 2) + (int)newPosition.x, (MAPHEIGHT / 2) + (int)newPosition.z] = 1;
                        GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        newObject.transform.position = newPosition;
                    }
                }
                break;
            case (Tools.build):
                if (Input.GetMouseButton(0)) {
                    if (mapPosition == 0) {
                        GetComponent<MapGenerator>().map[(MAPWIDTH / 2) + (int)hoverHit.point.x, (MAPHEIGHT / 2) + (int)hoverHit.point.z] = 1;
                        GetComponent<MapGenerator>().GenerateMesh(); 
                    }
                }
                break;
            case (Tools.inspect):
                if (Input.GetMouseButtonDown(0)) { 
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100.0f)) {
                        Debug.Log(hit.transform.gameObject.name);
                        //if (hit.transform.gameObject.GetComponent<MonsterBehaviour>()) {
                            //outputLog.text = hit.transform.gameObject.GetComponent<MonsterBehaviour>().thisMonster.ToString();
                        //}
                    }
                }
             break;
        }
    }

    private void OnDrawGizmos() {
        //Draw boundries of placement area 
        Gizmos.DrawLine(new Vector3(-(MAPWIDTH / 2), 0, (MAPHEIGHT/2)), new Vector3((MAPWIDTH / 2), 0, (MAPHEIGHT / 2)));
        Gizmos.DrawLine(new Vector3(-(MAPWIDTH / 2), 0, -(MAPHEIGHT/2)), new Vector3((MAPWIDTH / 2), 0, -(MAPHEIGHT / 2)));
        Gizmos.DrawLine(new Vector3(-(MAPWIDTH / 2), 0, -(MAPHEIGHT/2)), new Vector3(-(MAPWIDTH / 2), 0, (MAPHEIGHT / 2)));
        Gizmos.DrawLine(new Vector3((MAPWIDTH / 2), 0, -(MAPHEIGHT/2)), new Vector3((MAPWIDTH / 2), 0, (MAPHEIGHT / 2)));
    }
}
