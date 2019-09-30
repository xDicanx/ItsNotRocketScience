using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip clipEngine = null;
    [Range(0, 1)]
    [SerializeField] private float clipEngineVolume = 0.0f;


    //
    private AudioSource audioEngine;

    private void Awake()
    {
        audioEngine = AddAudio(clipEngine, true, false, clipEngineVolume, 1.6f);
    }

    private void Update()
    {
        TestAudioVolume();    
    }

    private void TestAudioVolume()
    {
        audioEngine.volume = clipEngineVolume;
    }

    

    private AudioSource AddAudio(AudioClip clip,bool loop, bool playOnAwake, float volume, float pitch)
    {
        var newAudio = transform.Find("Body").gameObject.AddComponent<AudioSource>();   //Adds an audio source to the child called body
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playOnAwake;
        newAudio.volume = volume;
        newAudio.pitch = pitch;
        return newAudio;
    }

    public AudioSource Getaudio(string audioName)
    {
        switch (audioName)
        {
            case "EngineAudio":
                return audioEngine;
        }
        Debug.LogWarning("There's not any audio called: " + audioName);
        return null;
    }
}
