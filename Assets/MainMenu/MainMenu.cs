using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button play;

    [SerializeField]
    private Button quit;

    private void Awake()
    {
        play.onClick.AddListener(() =>
        {
            Loader.Load(Loader.LoadingScene);
        });

        quit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
