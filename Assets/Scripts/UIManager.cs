using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Image BackgroundImage;
    public Button StartButton;
    public Text StartTitleText;

    public Text EndTitleText;
    public Button EndRestartButton;
    public Text ScoreText;
    public Text HealthText;
    public InputField nameField;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void InitUI() {
        BackgroundImage.gameObject.SetActive(true);
        StartButton.gameObject.SetActive(true);
        StartTitleText.gameObject.SetActive(true);
        ScoreText.gameObject.SetActive(false);
        HealthText.gameObject.SetActive(false);
        EndRestartButton.gameObject.SetActive(false);
        EndTitleText.gameObject.SetActive(false);
        nameField.gameObject.SetActive(false);
    }

    public void DisableStartInterface() {
        BackgroundImage.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);
        StartTitleText.gameObject.SetActive(false);
        
        ScoreText.gameObject.SetActive(true);
        HealthText.gameObject.SetActive(true);
    }

    public void EnableEndInterface() {
        BackgroundImage.gameObject.SetActive(true);
        EndRestartButton.gameObject.SetActive(true);
        EndTitleText.gameObject.SetActive(true);
        nameField.gameObject.SetActive(true);
    }

    public void ModifyHealthValueText(int _num) {
        HealthText.text = "Health : " + _num.ToString();
    }

    public void ModifyScoreValueText(int _num) {
        ScoreText.text = "Score : " + _num.ToString();
    }

}
