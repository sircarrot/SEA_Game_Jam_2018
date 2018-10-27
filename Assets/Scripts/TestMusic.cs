using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusic : MonoBehaviour {

    public AudioClip click;
    public AudioClip bgm;

    public float baseTimer = 1f;
    public float timer = 0;

    public UIManager UIManager;
    private AudioManager audioManager = null;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        if (audioManager == null)
        {
            audioManager = Toolbox.Instance.GetManager<AudioManager>();

            audioManager.BGMPlayer(bgm);
        }

        if(timer >= baseTimer)
        {
            audioManager.PlaySoundEffect(click);
            timer -= baseTimer;
        }

        timer += Time.deltaTime;

    }
}
