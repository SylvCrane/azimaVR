using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    // Start is called before the first frame update

    public Material room1;
    public Material room2;
    public Material room3;
    public Material room4;
    public GameObject roomSphere;
    public int materialNum;

    public void changeRoom()
    {
        Renderer roomRender = roomSphere.GetComponent<Renderer>();
       
        switch (materialNum)
        {
            case 0:
                roomRender.material = room2;
                materialNum++;
                break;
            case 1:
                roomRender.material = room3;
                materialNum++;
                break;
            case 2:
                roomRender.material = room4;
                materialNum++;
                break;
            case 3:
                roomRender.material = room1;
                materialNum = 0;
                break;
        }
    }
}
