using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages player objects in the game
public class PlayerManager : MonoBehaviour
{
    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>(); // Dictionary to hold player objects by their ID
    private Dictionary<string, Vector2> serverPositions = new Dictionary<string, Vector2>(); // Dictionary to hold player positions from the server

    public GameObject playerPrefab; // Prefab for player objects

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //compare player positions and update them if necessary. if a player is not in the dictionary, create a new player object
        foreach (var kv in serverPositions)
        {
            Debug.Log("Updating player: " + kv.Key + " with position: " + kv.Value);
            string playerId = kv.Key;
            Vector3 position = new Vector3(kv.Value.x, kv.Value.y, 0);
            if (!players.ContainsKey(playerId))
            {
                // Create a new player object if it doesn't exist
                Debug.Log("Creating new player: " + playerId + " at position: " + position);
                GameObject newPlayer = Instantiate(playerPrefab, position, Quaternion.identity);
                PlayerController playerController = newPlayer.GetComponent<PlayerController>();
                playerController.Id = playerId; // Set the player ID
                players.Add(playerId, newPlayer);
            }
            else
            {
                //update existing player position
                players[playerId].transform.position = Vector2.Lerp(
                    players[playerId].transform.position,
                    position,
                    Time.deltaTime
                );
            }
        }
    }

    //public void createSelf(string PlayerId)
    //{
    //    Debug.Log("Creating self");
    //    GameObject myPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    //    myPlayer.GetComponent<PlayerController>().Id = PlayerId; // Set the player ID
    //    players.Add(PlayerId, myPlayer); // Add the player to the dictionary
    //}

    public void updatePlayers(string JSONData)
    {
        Debug.Log("Json Data: " + JSONData);
        serverPositions = JsonUtility.FromJson<Dictionary<string, Vector2>>(JSONData);
       
    }
}
