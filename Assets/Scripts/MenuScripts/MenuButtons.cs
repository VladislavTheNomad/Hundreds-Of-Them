using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    private TMP_InputField playerNameInputField;
    // Start is called before the first frame update

    void Start()
    {
      
    }
    public void StartGame()
    {
        playerNameInputField = GameObject.Find("Name Input").GetComponent<TMP_InputField>();
        string playerName = playerNameInputField.text;
        if (playerName != null) { MainManager.instance.playerName = playerName; }
        else
        {
            { MainManager.instance.playerName = "Player"; }
        }
        SceneManager.LoadScene("My Game");
    }

    public void ExitGame()
    {
#if (UNITY_EDITOR)
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void HardChosen()
    {
        MainManager.instance.difficulty = "Hard";
    }

    public void NormalChosen()
    {
        MainManager.instance.difficulty = "Normal";
    }
}
