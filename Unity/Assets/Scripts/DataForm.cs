using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Collection of data format classes. 
 */

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

    [System.Serializable]
    //item data received from the server
    public class ItemData : ScriptableObject
    {
        private long itemId;
        public long ItemId { get; set; }

        private string itemName;
        public string ItemName { get; set; }

        private string itemDescription;
        public string ItemDescription { get; set; }

        private string itemImgPath;
        public string ItemImgPath { get; set; }

    }

}
