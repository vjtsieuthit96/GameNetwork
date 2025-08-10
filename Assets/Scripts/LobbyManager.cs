using Fusion;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    public void SpawnPlayer()
    {
        var runner = FindAnyObjectByType<NetworkRunner>();
        if (runner == null) return;

        int index = 0;
        foreach (var player in runner.ActivePlayers)
        {
            var oldObj = runner.GetPlayerObject(player);
            if (oldObj != null)
            {
                runner.Despawn(oldObj); // ✅ Gỡ object tạm
            }

            var spawnPoint = spawnPoints[index % spawnPoints.Length];
            var character = runner.Spawn(playerPrefab, spawnPoint.position, spawnPoint.rotation, player);
            runner.SetPlayerObject(player, character); // ✅ Gán lại PlayerObject

            Debug.Log($"✅ Spawned Player {player.PlayerId} at {spawnPoint.name}");
            index++;
        }
    }
}