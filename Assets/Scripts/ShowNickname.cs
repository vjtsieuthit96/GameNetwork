using TMPro;
using UnityEngine;

public class ShowNickname : MonoBehaviour
{
    [SerializeField] private PlayerNickname pn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "" + pn.Nickname;
    }
}
