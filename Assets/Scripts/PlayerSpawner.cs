using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    public void PlayerJoined(PlayerRef player)
    {
        print($"Player {player.PlayerId} joined the game. Spawning player object...");
        if(player == Runner.LocalPlayer)
        {
            int locationIndex = player.PlayerId - 1 % _spawnPoints.Length;
            var spawnPoint = _spawnPoints[locationIndex];
            Runner.Spawn(_playerPrefab, spawnPoint.position, spawnPoint.rotation, player);
        }       
    }
}
