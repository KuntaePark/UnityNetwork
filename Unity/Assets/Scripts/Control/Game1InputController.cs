using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Game1InputController : MonoBehaviour
{
    //���� �Է��� �޾� ������ ����
    public GameClient1 gameClient;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float axisH = Input.GetAxisRaw("Horizontal");
        if (Input.GetButton("Fire2"))
        {
            //���� ����
            Debug.Log("Charging mana...");    
            gameClient.Send("input",JsonConvert.SerializeObject(new { type = "chargeMana" }));
        }
        if(axisH == -1.0f)
        {

        }
        if(axisH == 1.0f)
        {
         
        }
    }
}
