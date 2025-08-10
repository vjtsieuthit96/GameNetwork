using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private TextMeshProUGUI statusLabel;

    private List<PlayerRef> waitingPlayers = new List<PlayerRef>();

    void Awake()
    {
        _playerPrefab = GameManager.instance.playerPrefabs;
    }

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log($"Player {player.PlayerId} joined the game.");
        waitingPlayers.Add(player);

        if (Runner.ActivePlayers.Count() >= 2)
        {
            statusLabel.text = "Ready to Start!";
            foreach (var p in waitingPlayers)
            {
                if (p == Runner.LocalPlayer)
                {
                    StartCoroutine(Spawn(p));
                }
            }
            waitingPlayers.Clear();
        }
        else
        {
            statusLabel.text = "Waiting for other players to join...";
        }
    }

    private IEnumerator Spawn(PlayerRef player)
    {
        for (int i = 3; i > 0; i--)
        {
            statusLabel.text = $"Spawning in {i}...";
            yield return new WaitForSecondsRealtime(1f);
        }

        int spawnIndex = player.PlayerId % spawnPoint.Length;
        Runner.Spawn(_playerPrefab, spawnPoint[spawnIndex].position, spawnPoint[spawnIndex].rotation, player);
        statusLabel.text = "Player Spawned!";
    }
}