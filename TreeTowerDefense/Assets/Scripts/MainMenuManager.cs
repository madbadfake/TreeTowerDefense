using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button startButton;
    public Button optionButton;
    public Button exitButton;

    public GameObject optionCanvas;
    public GameObject menuCanvas;
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        optionButton.onClick.AddListener(Options);
        exitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("NewTerrain");
    }

    public void Options()
    {
        menuCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }

    public void Back()
    {
        menuCanvas.SetActive(true);
        optionCanvas.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
}
