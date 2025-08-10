using Fusion;
using UnityEngine;

public class PlayerNickname : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(NicknnameChanged))]

    [SerializeField] private string NetworkedNickname { get; set; } = "Player";
    public string Nickname
    {
        get => NetworkedNickname;
        set
        {
            if (NetworkedNickname != value)
            {
                NetworkedNickname = value;
                NicknnameChanged();
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NetworkedNickname = GameManager.instance.PlayerNickname;
    }

    void NicknnameChanged()
    {
        Debug.Log($"Nickname changed to: {NetworkedNickname}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
