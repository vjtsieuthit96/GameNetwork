using Fusion;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    [SerializeField] private PlayerSelection _playerSelection;

    [Header("UI Elements")]
    [SerializeField] private Button readyButton;
    [SerializeField] private TextMeshProUGUI statusLabel;

   
        private void Awake()
    {
        if (readyButton == null)
            readyButton = GameObject.Find("ReadyButton")?.GetComponent<Button>();

        if (statusLabel == null)
            statusLabel = GameObject.Find("StatusLabel")?.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateStatusLabel();
    }

    public void ShowSelectionPanel(PlayerSelection selection)
    {
        Debug.Log("🔔 ShowSelectionPanel called");

        _playerSelection = selection;
        selection.gameObject.SetActive(true);

        if (readyButton != null)
            readyButton.interactable = true;

        if (statusLabel != null)
            statusLabel.text = "⏳ Waiting for players...";
    }
    public void OnReadyClicked()
    {
        _playerSelection.SetReady();

        if (readyButton != null)
            readyButton.interactable = false;

        if (statusLabel != null)
            statusLabel.text = "✅ You are ready!";
    }

    private void UpdateStatusLabel()
    {
        var runner = FindAnyObjectByType<NetworkRunner>();
        if (runner == null || statusLabel == null) return;

        int readyCount = 0;
        string statusText = "";

        foreach (var player in runner.ActivePlayers)
        {
            var obj = runner.GetPlayerObject(player);
            var sel = obj?.GetComponent<PlayerSelection>();
            if (sel == null) continue;

            string state = sel.IsReady ? "✅" : "❌";
            statusText += $"Player {player.PlayerId}: {state}   ";

            if (sel.IsReady) readyCount++;
        }

        if (readyCount >= runner.ActivePlayers.Count())
        {
            statusLabel.text = "🎮 All players are ready! Starting the match...";
            var spawner = FindAnyObjectByType<LobbyManager>();
            if (spawner != null)
            {
                spawner.SpawnPlayer(); // ✅ Spawn nhân vật thật
            }

            gameObject.SetActive(false);
        }
        else
        {
            statusLabel.text = statusText + "\n⏳ Waiting for other players to be ready...";
        }
    }
}