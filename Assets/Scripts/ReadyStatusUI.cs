using Fusion;
using TMPro;
using UnityEngine;

public class ReadyStatusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player0Status;
    [SerializeField] private TextMeshProUGUI player1Status;

    private void Update()
    {
        var runner = FindAnyObjectByType<NetworkRunner>();
        if (runner == null) return;

        foreach (var player in runner.ActivePlayers)
        {
            var obj = runner.GetPlayerObject(player);
            var sel = obj?.GetComponent<PlayerSelection>();
            if (sel == null) continue;

            string status = sel.IsReady ? "✅ Ready" : "❌ Not Ready";
            // hiển thị lên UI...
        }
    }
}