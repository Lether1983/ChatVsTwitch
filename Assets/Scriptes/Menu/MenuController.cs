using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject OptionPanel;
    [SerializeField]
    private GameObject MenuPanel;
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private GameObject QuitPanel;
    [SerializeField]
    private ValueHolderScript valueholder;

    private void OnEnable()
    {
        this.gameObject.GetComponent<SettingManager>().LoadSettings();
    }

    public void ActivateOptionPanel()
    {
        OptionPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void ActiveMenuPanel()
    {
        MenuPanel.SetActive(true);
        OptionPanel.SetActive(false);
    }

    public void StartLevel()
    {
        if (inputField.text != "")
        {
            valueholder.TwitchName = inputField.text;
            SceneManager.LoadScene("GameScene");
        }
    }

    public void BackToMenu()
    {
        QuitPanel.SetActive(false);
    }
    public void ReallyQuitGame()
    {
        QuitPanel.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
