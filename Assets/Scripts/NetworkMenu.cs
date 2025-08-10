using Fusion;
using UnityEngine;

public class NetworkMenu : MonoBehaviour
{
    [SerializeField] private FusionBootstrap _boostrap;

    public void OnQuickStartButtonClicked()
    {
        _boostrap.StartSharedClient();    
    }
}
