using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    WsClient wsClient;

    private string id; 
    public string Id { get; set; }

    private float axisH;
    private float axisV;

    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        wsClient = GameObject.Find("WsClient").GetComponent<WsClient>();
    }

    // Update is called once per frame
    void Update()
    {
        //send input to server, only if this player object is controlled by the local player
        if(Id == wsClient.PlayerId)
        {
            axisH = Input.GetAxis("Horizontal");
            axisV = Input.GetAxis("Vertical");
            if(axisH != 0 || axisV != 0)
            {
                wsClient.Send("input", JsonUtility.ToJson(new Vector2(axisH, axisV)));
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
}
