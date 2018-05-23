using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static public GameManager instance;
    public int playerScore;
    public int plateformLayer;
    public int ballLayer;
    public int healthValue;
    public GameObject ballObject;
    public PillerController pillerController;
    public NetworkCommunication networkServer;
    public UIManager interfaceManager;
    private void Awake() {
        instance = this;
        playerScore = 0;
        plateformLayer = 0;
        ballLayer = 0;
        healthValue = 2;
        networkServer = new NetworkCommunication("127.0.0.1", 8829);
        interfaceManager.InitUI();
    }
    // Use this for initialization
    void Start() {
        for (int i = 0; i < 10; ++i) {
            pillerController.GenPlateform();
        }
        PauseGame();
    }

    // Update is called once per frame
    void Update() {
    }

    public void AddPlateformLayer() {
        plateformLayer++;
    }

    public void AddBallLayer() {
        ballLayer++;
    }

    public void AddScore(int _s = 1) {
        playerScore += _s;
        interfaceManager.ModifyScoreValueText(playerScore);
    }

    public void AddHealth(int _h = 1) {
        healthValue += _h;
        interfaceManager.ModifyHealthValueText(healthValue);
    }
    public void PauseGame() {
        Time.timeScale = 0;
    }
    public void StartGame() {
        Time.timeScale = 1;
        interfaceManager.DisableStartInterface();
        AddHealth(0);
        AddScore(0);
    }
    public void EndGame() {
        Time.timeScale = 0;
        interfaceManager.EnableEndInterface();
        String[] results = networkServer.QueryResults();
        for(int i =0;i < results.Length-1;++i) {
            interfaceManager.EndTitleText.text += ((i+1).ToString() + " : " + results[i] + '\n');
        }

    }

    public void RestartGame() {
        UploadNewResult();
        SceneManager.LoadScene(0);
    }
    public void UploadNewResult() {
        string playerName = interfaceManager.nameField.text;
        if(playerName != "") {
            networkServer.UploadResult(playerName, playerScore);
        }
        
    }
}