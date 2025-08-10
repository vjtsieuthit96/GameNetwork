using Fusion;
using System.Linq;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    [Networked] public NetworkDictionary<PlayerRef, bool> ReadyPlayers { get; } = new NetworkDictionary<PlayerRef, bool>();

    [SerializeField] private GameObject[] playerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcSetPlayerReady(PlayerRef player)
    {
        ReadyPlayers.Set(player, true);
        Debug.Log($"[RPC] Player {player.PlayerId} is ready");

        CheckAllReady();
    }

    public void SetPlayerReady(PlayerRef player)
    {
        if (Object.HasStateAuthority)
        {
            ReadyPlayers.Set(player, true);
            Debug.Log($" Player {player.PlayerId} is ready");
            CheckAllReady();
        }
        else
        {
            RpcSetPlayerReady(player);
        }
    }

    public bool IsPlayerReady(PlayerRef player)
    {
        return ReadyPlayers.TryGet(player, out bool isReady) && isReady;
    }

    private void CheckAllReady()
    {
        var runner = FindAnyObjectByType<NetworkRunner>();
        if (runner == null) return;

       
        if (runner.ActivePlayers.Count() != 2) return;

        
        int readyCount = 0;
        foreach (var player in runner.ActivePlayers)
        {
            if (IsPlayerReady(player))
            {
                readyCount++;
            }
        }

        // Chỉ spawn nếu cả 2 người đã ready
        if (readyCount == 2)
        {            
            Debug.Log(readyCount + " players are ready. Spawning characters...");
            SpawnPlayers();
        }
    }

    public void SpawnPlayers()
    {
        var runner = FindAnyObjectByType<NetworkRunner>();
        int index = 0;

        foreach (var player in runner.ActivePlayers)
        {
            var spawnPoint = spawnPoints[index % spawnPoints.Length];
            var character = runner.Spawn(playerPrefab[Random.Range(0,playerPrefab.Length)], spawnPoint.position, spawnPoint.rotation, player);
            runner.SetPlayerObject(player, character);
            index++;
        }
    }
}