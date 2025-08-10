using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject _emptyPlayerObject;
    [SerializeField] private GameObject _selectionPanel;

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log($"Player {player.PlayerId} joined the game.");

        var obj = Runner.Spawn(_emptyPlayerObject, Vector3.zero, Quaternion.identity, player);
        Runner.SetPlayerObject(player, obj);

        var selectionUI = FindAnyObjectByType<CharacterSelectionUI>();
        if (selectionUI != null)
        {
            selectionUI.ShowSelectionPanel(obj.GetComponent<PlayerSelection>());
        }

        _selectionPanel.SetActive(true); // ✅ Bỏ điều kiện để test
    }
}