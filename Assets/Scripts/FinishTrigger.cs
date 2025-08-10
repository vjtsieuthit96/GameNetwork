using UnityEngine;
using Fusion;
using TMPro; // Nếu dùng TextMeshPro

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text winText;

    private bool hasWinner = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasWinner) return;
        if (!other.CompareTag("Player")) return;

        NetworkObject networkObject = other.GetComponent<NetworkObject>();
        if (networkObject == null) return;

        var player = networkObject.InputAuthority;
        if (player == null) return;

        hasWinner = true;

        // Hiển thị panel thắng cuộc
        winPanel.SetActive(true);
        winText.text = $"Player {player.PlayerId} wins!";
    }
}