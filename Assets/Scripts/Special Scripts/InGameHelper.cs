using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class InGameHelper : MonoBehaviour
{
    // Start is called before the first frame update
    private MainManager mainManager;
    private TextMeshProUGUI playerName;
    public TextMeshProUGUI bestScoreName;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI scoreNow;

    public TextMeshProUGUI playerHP;
    public TextMeshProUGUI playerLVL;

    public TextMeshProUGUI weaponNowEquiped;
    public GameObject perkMenu;
    void Awake()
    {
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
        playerName = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        playerName.text = mainManager.playerName;
        LoadBestScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void IsANewRecordScore()
    {
        if(int.Parse(scoreNow.text) > int.Parse(bestScore.text))
        {
            bestScore.text = scoreNow.text;
            SaveBestScore();
        }
    }

    public void OpenPerkMenu()
    {
        Time.timeScale = 0f;
        perkMenu.SetActive(true);
        perkMenu.GetComponent<PerkUI>().Initialization();
    }

    [Serializable]
    class PlayerData
    {
        public string playerName;
        public int score;

    }

    public void SaveBestScore()
    {
        PlayerData data = new PlayerData();
        data.playerName = mainManager.playerName;

        // Corrected line:
        data.score = int.Parse(GameObject.Find("Number Of Best Score").GetComponent<TextMeshProUGUI>().text);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/bestPlayerData.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/bestPlayerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            bestScoreName.text = data.playerName;
            bestScore.text = (data.score).ToString();
        }
    }
}
