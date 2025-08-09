using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject _playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        print($"Player {player.PlayerId} joined the game. Spawning player object...");
        if(player == Runner.LocalPlayer)
        {
            Runner.Spawn(_playerPrefab, new Vector3(0, 1, 0), Quaternion.identity, player);
        }       
    }
}
