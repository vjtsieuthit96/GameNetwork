using Fusion;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
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

    public void ShowSelectionPanel()
    {
        Debug.Log(" ShowSelectionPanel called");

        gameObject.SetActive(true);

        if (readyButton != null)
            readyButton.interactable = true;

        if (statusLabel != null)
            statusLabel.text = " Waiting for players...";
    }

    public void OnReadyClicked()
    {
        var runner = FindAnyObjectByType<NetworkRunner>();
        var lobby = FindAnyObjectByType<LobbyManager>();

        if (runner != null && lobby != null)
        {
            lobby.SetPlayerReady(runner.LocalPlayer);
        }

        if (readyButton != null)
            readyButton.interactable = false;

        if (statusLabel != null)
            statusLabel.text = " You are ready!";
    }

    private void UpdateStatusLabel()
    {
        var runner = FindAnyObjectByType<NetworkRunner>();
        var lobby = FindAnyObjectByType<LobbyManager>();
        if (runner == null || lobby == null || statusLabel == null) return;

        int readyCount = 0;
        string statusText = "";

        foreach (var player in runner.ActivePlayers)
        {
            bool isReady = lobby.IsPlayerReady(player);
            string state = isReady ? "✅" : "❌";
            statusText += $"Player {player.PlayerId}: {state}   ";

            if (isReady) readyCount++;
        }

        if (runner.ActivePlayers.Count() == 2 && readyCount == 2)
        {
            statusLabel.text = " All players are ready! Starting the match...";
            //lobby.SpawnPlayers();
            gameObject.SetActive(false);
        }
        else
        {
            statusLabel.text = statusText + "\n Waiting for other players to be ready...";
        }
    }
}