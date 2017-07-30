using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject OptionPanel;
    [SerializeField]
    private GameObject MenuPanel;

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
        SceneManager.LoadScene("GameScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
