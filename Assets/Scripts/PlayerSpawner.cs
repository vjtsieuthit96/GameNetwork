using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject _selectionPanel;

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log($"Player {player.PlayerId} joined the game.");

        if (player == Runner.LocalPlayer)
        {         

            if (_selectionPanel != null)
                _selectionPanel.SetActive(true);
        }
    }
}