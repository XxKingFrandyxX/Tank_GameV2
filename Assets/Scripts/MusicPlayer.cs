using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource music;
    public AudioClip[] musicClips;
    int currentClip = 0;

    void update() {
        if(Input.GetKeyDown(KeyCode.Q) || !music.isPlaying)
        {
            currentClip++;
            if (currentClip >= musicClips.Length)
                currentClip = 0;

            music.clip = musicClips[currentClip];
            music.Play();
        }
    }

}
