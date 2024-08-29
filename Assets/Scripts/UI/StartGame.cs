using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject settingPanel; // 需要在Inspector中赋值
    public GameObject myBag;
   
    public Boolean music=true;
    public Boolean backgroundMusic=true;
    
    public void Update()
    {
        OpenMyBag();
        OpenSetting();
    }

    public void OnStartGameButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    // 关闭设置面板
    public void CloseSettingPanel()
    {
        settingPanel.SetActive(false);
    }

    public void OpenMyBag()
    {
        bool bagIsopen = myBag.activeSelf;
        if (Input.GetKeyDown(KeyCode.B))
        {
            bagIsopen = !bagIsopen;
            myBag.SetActive(bagIsopen);
        }
    }

    public void OpenSetting()
    {
        bool settingIsopen = settingPanel.activeSelf;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingIsopen = !settingIsopen;
            settingPanel.SetActive(settingIsopen);
        }
    }

    // public void ChangeMusic()
    // {
    //     music = !music;
    //     GameObject bgm = GameObject.Find("MusicSetting");
    //     if (music)
    //     {
    //         Sprite sp = Resources.Load<Sprite>("UI/bgmopen");
    //         bgm.GetComponent<Image>().sprite = sp;
    //     }
    //     else
    //     {
    //         Sprite sp = Resources.Load<Sprite>("UI/bgmclose");
    //         bgm.GetComponent<Image>().sprite = sp;
    //     }
    // }
    //
    // public void ChangeBackgroudMusic()
    // {
    //     backgroundMusic = !backgroundMusic;
    //     GameObject bgm = GameObject.Find("BackgroundMusicSetting");
    //     if (backgroundMusic)
    //     {
    //         Sprite sp = Resources.Load<Sprite>("UI/bgmopen");
    //         bgm.GetComponent<Image>().sprite = sp;
    //     }
    //     else
    //     {
    //         Sprite sp = Resources.Load<Sprite>("UI/bgmclose");
    //         bgm.GetComponent<Image>().sprite = sp;
    //     }
    // }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        // 在编辑器中退出播放模式
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // 在构建的游戏中退出应用程序
        Application.Quit();
        #endif
    }
    
}