using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public const string GameScene = nameof(GameScene);
    public const string LoadingScene = nameof(LoadingScene);
    public const string MainMenuScene = nameof(MainMenuScene);

    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
