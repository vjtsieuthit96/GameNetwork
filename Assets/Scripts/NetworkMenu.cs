
using Fusion;
using UnityEngine;
using TMPro;

public class NetworkMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private FusionBootstrap fusionBootstrap;

    public void StartGameClick()
    {
        fusionBootstrap.StartSharedClient();

    }

    public void StartAsHost()
    {
        fusionBootstrap.StartHost();
    }

    public void StartAsClient()
    {
        fusionBootstrap.StartClient();
    }
}

  