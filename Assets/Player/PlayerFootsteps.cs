using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public static event EventHandler<Player> OnWalk;

    private const float MAX_SECONDS = 0.1f;

    private Player player;
    private float timer;

    private void Awake()
    {
        OnWalk = null;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            timer = MAX_SECONDS;

            if (player.IsWalking)
            {
                OnWalk?.Invoke(this, player);
            }
        }
    }
}
