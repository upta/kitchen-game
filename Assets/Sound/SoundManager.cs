using System;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private const string PREF_KEY = "SoundVolume";

    [SerializeField]
    private AudioClipRefs clips;

    private int volume;
    public int Volume
    {
        get => volume;
        set
        {
            volume = value;

            PlayerPrefs.SetInt(PREF_KEY, volume);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create another instance of {nameof(SoundManager)}");
        }

        Instance = this;

        Volume = PlayerPrefs.GetInt(PREF_KEY, 5);
    }

    private void Start()
    {
        CuttingCounter.OnCut += (_, counter) =>
        {
            PlaySound(clips.chop, counter.transform.position);
        };

        DeliveryCounter.OnDeliverySucceeded += (_, counter) =>
        {
            PlaySound(clips.deliverySucceeded, counter.transform.position);
        };

        DeliveryCounter.OnDeliveryFailed += (_, counter) =>
        {
            PlaySound(clips.deliveryFailed, counter.transform.position);
        };

        Player.Instance.OnPickUp += (_, player) =>
        {
            PlaySound(clips.objectPickup, player.transform.position);
        };

        Player.Instance.OnDrop += (_, player) =>
        {
            PlaySound(clips.objectDrop, player.transform.position);
        };

        PlayerFootsteps.OnWalk += (_, player) =>
        {
            PlaySound(clips.footsteps, player.transform.position);
        };

        TrashCounter.OnTrashed += (_, counter) =>
        {
            PlaySound(clips.trash, counter.transform.position);
        };

        CountdownMenu.Instance.OnCount += (_, _) =>
        {
            PlaySound(clips.warning, Vector3.zero);
        };

        StoveSoundToggle.OnWarning += (_, counter) =>
        {
            PlaySound(clips.warning, counter.transform.position);
        };
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volumeMultiplier = 1.0f)
    {
        var volume = volumeMultiplier * ((Volume * .1f));

        AudioSource.PlayClipAtPoint(
            audioClips[UnityEngine.Random.Range(0, audioClips.Length)],
            position,
            volume
        );
    }
}
