using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    private PlayerInputManager manager;
    [SerializeField] private Transform[] spawnsArray;
    [SerializeField] private Color[] colorArray;

    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        manager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        manager.onPlayerJoined -= AddPlayer;
    }

    private void AddPlayer(PlayerInput player)
    {
        player.gameObject.GetComponent<CharacterController>().enabled = false;
        player.transform.position = spawnsArray[player.playerIndex].position;
        player.transform.GetChild(0).GetComponent<Renderer>().material.color = colorArray[player.playerIndex];
        player.gameObject.GetComponent<CharacterController>().enabled = true;
    }
}
