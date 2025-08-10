using UnityEngine;
using Fusion;
using TMPro;

public class FinishTrigger : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI winText;

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
        winText.text = $"Player {player.PlayerId} wins!";
    }
}