using Fusion;
using UnityEngine;

public class PlayerSelection : NetworkBehaviour
{
    [Networked] public bool IsReady { get; private set; } = false;

    public void SetReady()
    {
        if (Object.HasStateAuthority)
        {
            IsReady = true;
        }
    }
}