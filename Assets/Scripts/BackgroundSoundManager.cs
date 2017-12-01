using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour {

    // Need to add an empty game object with an audio source and child it to the HoloLens camera
    // Then, attach this script to that empty game object

    [SerializeField]
    private AudioSource backgroundMusicAudioSource;
    [SerializeField]
    private AudioSource dialogueAudioSource;
    [SerializeField]
    private AudioClip backgroundMusic;
    [SerializeField]
    private AudioClip openingDialogueClip;
    [SerializeField]
    private AudioClip closingDialogueClip;
    [SerializeField]
    private float openingDialogueDelayTime;


    void Awake ()
    {
        // We want the background music to start playing from the beginning of the experience,
        // but we want the opening line of dialogue to be played a little after the start

        backgroundMusicAudioSource.clip = backgroundMusic;
        backgroundMusicAudioSource.Play();

        Invoke("PlayOpeningDialogue", openingDialogueDelayTime);
    }


    void PlayOpeningDialogue ()
    {
        dialogueAudioSource.clip = openingDialogueClip;
        dialogueAudioSource.Play();
    }
}
