using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip completedSFX;
    public AudioClip failedSFX;
    public AudioClip tapAndHitSFX;
    public AudioClip powerUpSFX;
    public AudioClip abyssSFX;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void CompletedEffect()
    {
        audioSource.PlayOneShot(completedSFX);
    }
    public void FailedEffect()
    {
        audioSource.PlayOneShot(failedSFX);
    }
    public void HitEffect()
    {
        audioSource.PlayOneShot(tapAndHitSFX);
    }
    public void powerUpEffect()
    {
        audioSource.PlayOneShot(powerUpSFX);
    }
    public void abyssEffect()
    {
        audioSource.PlayOneShot(abyssSFX);
    }
}
