using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneInit : MonoBehaviour
{
    private bool isInitialized = false;

    void Update()
    {
        if (isInitialized)
        {
            return;
        }

        isInitialized = true;
        Loader.Load(Loader.GameScene);
    }
}
