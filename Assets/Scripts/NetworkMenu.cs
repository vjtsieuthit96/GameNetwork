
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkMenu : MonoBehaviour
{
    [SerializeField] private Button readybutton;
    private void Update()
    {
        readybutton.interactable = GameManager.instance.playerPrefabs != null;
    }
    public void LoadScene()
    {        
        SceneManager.LoadSceneAsync("Overview");
    }
    public void ChooseCharacter(GameObject character)
    {
        GameManager.instance.playerPrefabs = character;
    }

    public void OnReadyClick()
    {      
        LoadScene();  
    }
}

  