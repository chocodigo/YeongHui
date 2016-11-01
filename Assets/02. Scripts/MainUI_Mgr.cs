using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI_Mgr : MonoBehaviour
{   
    public Text Text_Gem;
    public Text Text_Coin;
    public Text Text_Heart;
    public Text Text_Time;
    public AudioSource bgm;

    void Start()
    {
        bgm.loop = true;
        bgm.volume = Cloud_Mgr.GetInstance.UserData.BGMvol;
        bgm.mute = Cloud_Mgr.GetInstance.UserData.BGMMute;
        bgm.Play();
    }

    void Update()
    {
        bgm.volume = Cloud_Mgr.GetInstance.UserData.BGMvol;
        bgm.mute = Cloud_Mgr.GetInstance.UserData.BGMMute;

        Text_Gem.text = Cloud_Mgr.GetInstance.UserData.Gem.ToString("#,##0");
        Text_Coin.text = Cloud_Mgr.GetInstance.UserData.Coin.ToString("#,##0");

        if (Cloud_Mgr.GetInstance.UserData.Heart < 3 + (Cloud_Mgr.GetInstance.UserData.LV * 2)) //재충전을 위해 남은시간
        {
            TimeSpan default_ts = new TimeSpan(0, 10, 0);
            TimeSpan ts = default_ts - DateTime.Now.Subtract(Convert.ToDateTime(Cloud_Mgr.GetInstance.UserData.LastTime));

            Text_Time.text = ts.Minutes.ToString() + ":" + ts.Seconds.ToString();

            if (ts.CompareTo(new TimeSpan(0, 0, 0)) < 0) //재충전 시간이 되었을 경우 하트보충
            {
                int plus_Heart = ((int)ts.TotalSeconds * -1) / (int)default_ts.TotalSeconds + 1;

                if (plus_Heart + Cloud_Mgr.GetInstance.UserData.Heart > 3 + (Cloud_Mgr.GetInstance.UserData.LV * 2))
                {
                    Cloud_Mgr.GetInstance.UserData.Heart = 3 + (Cloud_Mgr.GetInstance.UserData.LV * 2);
                    Cloud_Mgr.GetInstance.UserData.LastTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else
                {
                    Cloud_Mgr.GetInstance.UserData.Heart += plus_Heart;
                    TimeSpan rest = new TimeSpan(0, 0, ((int)ts.TotalSeconds * -1) % (int)default_ts.TotalSeconds);
                    Cloud_Mgr.GetInstance.UserData.LastTime = (DateTime.Now - rest).ToString("yyyy/MM/dd HH:mm:ss");
                }
                Cloud_Mgr.GetInstance.SaveToCloud();
            }
        }
        else
            Text_Time.text = "MAX";

        Text_Heart.text = Cloud_Mgr.GetInstance.UserData.Heart.ToString() + "/" + Convert.ToString(3 + (Cloud_Mgr.GetInstance.UserData.LV * 2));
    }

    public void ClickSetting()
    {
        StartCoroutine(Scene_Mgr.GetInstance.LoadSetting());
    }

    public void ClickHeartPlus()
    {        
        Scene_Mgr.GetInstance.bStoreHeart = true;
        if (!Scene_Mgr.GetInstance.bStore)
            StartCoroutine(Scene_Mgr.GetInstance.LoadStore());

        if (Scene_Mgr.GetInstance.bLobby)
            Scene_Mgr.GetInstance.UnLoadLobby();
        if (Scene_Mgr.GetInstance.bInventory)
            Scene_Mgr.GetInstance.UnLoadInventory();
        if (Scene_Mgr.GetInstance.bGameRoom)
            Scene_Mgr.GetInstance.UnLoadGameRoom();
        if (Scene_Mgr.GetInstance.bCompose)
            Scene_Mgr.GetInstance.UnLoadCompose();
    }

    public void ClickGemPlus()
    {
        Scene_Mgr.GetInstance.bStoreGem = true;
        if (!Scene_Mgr.GetInstance.bStore)
            StartCoroutine(Scene_Mgr.GetInstance.LoadStore());

        if (Scene_Mgr.GetInstance.bLobby)
            Scene_Mgr.GetInstance.UnLoadLobby();
        if (Scene_Mgr.GetInstance.bInventory)
            Scene_Mgr.GetInstance.UnLoadInventory();
        if (Scene_Mgr.GetInstance.bGameRoom)
            Scene_Mgr.GetInstance.UnLoadGameRoom();
        if (Scene_Mgr.GetInstance.bCompose)
            Scene_Mgr.GetInstance.UnLoadCompose();
    }

    public void ClickCoinPlus()
    {
        Scene_Mgr.GetInstance.bStoreCoin = true;
        if(!Scene_Mgr.GetInstance.bStore)
            StartCoroutine(Scene_Mgr.GetInstance.LoadStore());

        if (Scene_Mgr.GetInstance.bLobby)
            Scene_Mgr.GetInstance.UnLoadLobby();
        if (Scene_Mgr.GetInstance.bInventory)
            Scene_Mgr.GetInstance.UnLoadInventory();
        if (Scene_Mgr.GetInstance.bGameRoom)
            Scene_Mgr.GetInstance.UnLoadGameRoom();
        if (Scene_Mgr.GetInstance.bCompose)
            Scene_Mgr.GetInstance.UnLoadCompose();        
    }    
}
