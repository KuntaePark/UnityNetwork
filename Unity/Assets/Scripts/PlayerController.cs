using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using DataForm;
public class PlayerController : MonoBehaviour
{
    WsClient wsClient;

    private string id; 
    public string Id { get; set; }

    private float axisH;
    private float axisV;

    public float speed = 5.0f; //make sure it is synchronized with the server
    public Boolean isTestDummy = false;

    //testing value
    private float[] Hinputs = new float[2] { -1.0f, 1.0f };
    private float[] Vinputs = new float[2] { -1.0f, 1.0f };
    public float maxMoveTime = 0.5f;
    public float moveTime = 0;
    public float breakTime = 1.0f;
    public float currentTime = 0;

    public string curState = "move";

    // Start is called before the first frame update
    void Start()
    {
        wsClient = GameObject.Find("WsClient").GetComponent<WsClient>();
        moveTime = UnityEngine.Random.Range(0.1f, maxMoveTime);
    }

    // Update is called once per frame
    void Update()
    {
        //send input to server, only if this player object is controlled by the local player
        if(Id == wsClient.PlayerId)
        {
            if(Input.GetKeyDown("space"))
            {
                isTestDummy = !isTestDummy;
            }
            
            if(!isTestDummy)
            {
                axisH = Input.GetAxisRaw("Horizontal");
                axisV = Input.GetAxisRaw("Vertical");
                if(axisH != 0 || axisV != 0)
                {
                    wsClient.Send("input", JsonConvert.SerializeObject(new Vector2Data(axisH, axisV)));
                }
            }
            else
            {
                switch (curState)
                {
                    case "break":
                        if (currentTime < breakTime)
                        {
                            currentTime += Time.deltaTime;
                            axisH = 0;
                            axisV = 0;
                        }
                        else
                        {
                            int hIndex = UnityEngine.Random.Range(0, Hinputs.Length);
                            int vIndex = UnityEngine.Random.Range(0, Vinputs.Length);
                            axisH = Hinputs[hIndex];
                            axisV = Vinputs[vIndex];
                            curState = "move";
                            currentTime = 0;
                            moveTime = UnityEngine.Random.Range(0.1f, maxMoveTime);
                        }
                        break;
                    case "move":
                        if (currentTime < moveTime)
                        {
                            currentTime += Time.deltaTime;
                        }
                        else
                        {
                            currentTime = 0;
                            curState = "break";
                        }
                        break;
                
                }


                if (axisH != 0 || axisV != 0)
                {
                    wsClient.Send("input", JsonConvert.SerializeObject(new Vector2Data(axisH, axisV)));
                }
            }
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(axisH, axisV, 0) * speed * Time.deltaTime);
    }
}
