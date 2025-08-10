using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerPrefabs; 
    public string PlayerNickname = "Player";
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
}
