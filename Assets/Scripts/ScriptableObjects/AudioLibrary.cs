using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Library")]
public class AudioLibrary : ScriptableObject {

    public AudioClip mainBGM;

    public AudioClip striker;
    public AudioClip healer;
    public AudioClip producer;

    public AudioClip[] freed = new AudioClip[2];
    public AudioClip[] death = new AudioClip[2];
    public AudioClip[] attack = new AudioClip[2];
    public AudioClip[] heal = new AudioClip[2];
    public AudioClip[] spawned = new AudioClip[2];

    public AudioClip gameStart;
    public AudioClip gameEnd;

    public AudioClip[] dogCaption = new AudioClip[8];
    public AudioClip[] catCaption = new AudioClip[8];
}
