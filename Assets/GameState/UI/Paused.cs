using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paused : MonoBehaviour
{
    [SerializeField]
    private Button resume;

    [SerializeField]
    private Button options;

    [SerializeField]
    private Button mainMenu;

    private void Start()
    {
        gameObject.SetActive(false);

        PauseManager.Instance.OnPaused += PauseManager_OnPaused;
        PauseManager.Instance.OnUnpaused += PauseManager_OnUnpaused;

        resume.onClick.AddListener(() =>
        {
            PauseManager.Instance.IsPaused = false;
        });

        options.onClick.AddListener(() =>
        {
            OptionsMenu.Instance.IsVisible = true;
        });

        mainMenu.onClick.AddListener(() =>
        {
            Loader.Load(Loader.MainMenuScene);
            PauseManager.Instance.IsPaused = false;
        });
    }

    private void OnDestroy()
    {
        PauseManager.Instance.OnPaused -= PauseManager_OnPaused;
        PauseManager.Instance.OnUnpaused -= PauseManager_OnUnpaused;
    }

    private void PauseManager_OnPaused(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }

    private void PauseManager_OnUnpaused(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);

        OptionsMenu.Instance.IsVisible = false;
    }
}
