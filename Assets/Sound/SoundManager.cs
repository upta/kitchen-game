using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClipRefs clips;

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

        Player.OnPickUp += (_, player) =>
        {
            PlaySound(clips.objectPickup, player.transform.position);
        };

        Player.OnDrop += (_, player) =>
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
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1.0f)
    {
        AudioSource.PlayClipAtPoint(
            audioClips[UnityEngine.Random.Range(0, audioClips.Length)],
            position,
            volume
        );
    }
}
