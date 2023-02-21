using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
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
        OptionsMenu.Instance.OnVisibilityChanged += OptionsMenu_OnVisibilityChanged;

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

        resume.Select();
    }

    private void PauseManager_OnUnpaused(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);

        OptionsMenu.Instance.IsVisible = false;
    }

    private void OptionsMenu_OnVisibilityChanged(object sender, bool isVisible)
    {
        if (!PauseManager.Instance.IsPaused)
        {
            return;
        }

        gameObject.SetActive(!isVisible);

        if (!isVisible)
        {
            resume.Select();
        }
    }
}
