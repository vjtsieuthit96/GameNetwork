using Fusion;
using UnityEngine;

public class StartSharedClient : MonoBehaviour
{
    [SerializeField] private FusionBootstrap fusion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        fusion.StartSharedClient();
    }
}
   
