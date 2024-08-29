using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource bgmAudio;
    public AudioSource backgroundAudio;
    public Slider bgmSlider;
    public Slider backgroundSlider;
    public Boolean isOpenMusic=true;
    public Boolean isOpenBackground=true;

    void Update()
    {
        //bgmAudio.volume = bgmSlider.value;
        backgroundAudio.volume = backgroundSlider.value;
    }
    // public void ChangeMusic()
    // {
    //     isOpenMusic = !isOpenMusic;
    //     GameObject bgm = GameObject.Find("MusicSetting");
    //     if (isOpenMusic)
    //     {
    //         Sprite sp = Resources.Load<Sprite>("UI/bgmopen");
    //         bgm.GetComponent<Image>().sprite = sp;
    //         bgmAudio.mute = false;
    //     }
    //     else
    //     {
    //         Sprite sp = Resources.Load<Sprite>("UI/bgmclose");
    //         bgm.GetComponent<Image>().sprite = sp;
    //         bgmAudio.mute = true;
    //     }
    // }

    public void ChangeBackgroudMusic()
    {
        isOpenBackground = !isOpenBackground;
        GameObject bgm = GameObject.Find("BackgroundMusicSetting");
        if (isOpenBackground)
        {
            Sprite sp = Resources.Load<Sprite>("UI/bgmopen");
            bgm.GetComponent<Image>().sprite = sp;
            backgroundAudio.mute = false;
        }
        else
        {
            Sprite sp = Resources.Load<Sprite>("UI/bgmclose");
            bgm.GetComponent<Image>().sprite = sp;
            backgroundAudio.mute = true;
        }
    }
}
