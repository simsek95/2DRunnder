using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Transform player;
    float yOffset;
    void Start()
    {
        FindPlayer();
        yOffset =transform.position.y- player.position.y;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos =new Vector3(player.position.x,player.position.y+yOffset,transform.position.z);
        transform.position = pos;
    }
    
    void FindPlayer()
    {
        player= FindObjectOfType<PlayerMove>().transform;
    }
}
