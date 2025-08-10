using Fusion;
using UnityEngine;

public class NetworkActivator : NetworkBehaviour
{
    [SerializeField] private GameObject[] _playerObjects;

    public override void Spawned ()
    {
        base.Spawned();
        bool isPlayer = HasInputAuthority;
        foreach (var obj in _playerObjects)
        {
            if (obj != null)
            {
                obj.SetActive(isPlayer);
            }
        }
    }
}
