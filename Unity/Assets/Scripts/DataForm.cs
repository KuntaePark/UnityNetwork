using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataForm
{
    [System.Serializable]
    public class Packet
    {
        public string type;
        public string payload;
    }


    [System.Serializable]
    public class Vector2Data
    {
        //currently only used for player input, but can be used for other purposes as well
        public float x;
        public float y;

        public Vector2Data(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
