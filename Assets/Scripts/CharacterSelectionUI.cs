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
    private bool isReady = false;

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

        if (runner == null || lobby == null) return;

        if (!isReady)
        {
            lobby.SetPlayerReady(runner.LocalPlayer);
            statusLabel.text = "You are ready!";
            readyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Click to Cancel";
            isReady = true;
        }
        else
        {
            lobby.UnsetPlayerReady(runner.LocalPlayer);
            statusLabel.text = "You are not ready.";
            readyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ready";
            isReady = false;
        }
    }

    private void UpdateStatusLabel()
    {
        var runner = FindAnyObjectByType<NetworkRunner>();
        var lobby = FindAnyObjectByType<LobbyManager>();
        if (runner == null || lobby == null || statusLabel == null) return;

        int readyCount = 0;
        string statusText = "";

        foreach (var kvp in lobby.ReadyPlayers)
        {
            var player = kvp.Key;
            bool isReady = kvp.Value;
            string state = isReady ? "is ready" : "is not ready";
            statusText += $"Player {player.PlayerId} {state}\n";
            if (isReady) readyCount++;
        }

        if (lobby.ReadyPlayers.Count == 2 && readyCount == 2)
        {
            statusLabel.text = " All players are ready! Starting the match...";
            gameObject.SetActive(false);
        }
        else
        {
            statusLabel.text = statusText + "\n Waiting for other players to be ready...";
        }
    }
}
