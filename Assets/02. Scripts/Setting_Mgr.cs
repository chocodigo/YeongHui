using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Setting_Mgr : MonoBehaviour {
    public Image SettingPanel;
    
    private Slider BgmVol;
    private Slider EffectVol;
    private Slider ButtonVol;
    private Image BgmMute;
    private Image EffectMute;
    private Image ButtonMute;

    void Start () {
        BgmVol = SettingPanel.transform.FindChild("Panel").FindChild("Bgm").GetComponent<Slider>();
        EffectVol = SettingPanel.transform.FindChild("Panel").FindChild("Effect").GetComponent<Slider>();
        ButtonVol = SettingPanel.transform.FindChild("Panel").FindChild("Button").GetComponent<Slider>();
        BgmMute = SettingPanel.transform.FindChild("Panel").FindChild("BgmMute").GetComponent<Image>();
        EffectMute = SettingPanel.transform.FindChild("Panel").FindChild("EffectMute").GetComponent<Image>();
        ButtonMute = SettingPanel.transform.FindChild("Panel").FindChild("ButtonMute").GetComponent<Image>();
        
        BgmVol.value = Cloud_Mgr.GetInstance.UserData.BGMvol;
        EffectVol.value = Cloud_Mgr.GetInstance.UserData.EffVol;
        ButtonVol.value = Cloud_Mgr.GetInstance.UserData.BtnVol;

        if (Cloud_Mgr.GetInstance.UserData.BGMMute)
            BgmMute.overrideSprite = Resources.Load<Sprite>("mute");
        else
            BgmMute.overrideSprite = Resources.Load<Sprite>("speaker");

        if (Cloud_Mgr.GetInstance.UserData.EffMute)
            EffectMute.overrideSprite = Resources.Load<Sprite>("mute");
        else
            EffectMute.overrideSprite = Resources.Load<Sprite>("speaker");

        if (Cloud_Mgr.GetInstance.UserData.BtnMute)
            ButtonMute.overrideSprite = Resources.Load<Sprite>("mute");
        else
            ButtonMute.overrideSprite = Resources.Load<Sprite>("speaker");
    }

    void Update()
    {
        if(Cloud_Mgr.GetInstance.UserData.BGMvol != BgmVol.value)
        {
            Cloud_Mgr.GetInstance.UserData.BGMvol = BgmVol.value;
            Cloud_Mgr.GetInstance.SaveToCloud();
        }
        if(Cloud_Mgr.GetInstance.UserData.EffVol != EffectVol.value)
        {
            Cloud_Mgr.GetInstance.UserData.EffVol = EffectVol.value;
            Cloud_Mgr.GetInstance.SaveToCloud();
        }
        if(Cloud_Mgr.GetInstance.UserData.BtnVol != ButtonVol.value)
        {
            Cloud_Mgr.GetInstance.UserData.BtnVol = ButtonVol.value;
            Cloud_Mgr.GetInstance.SaveToCloud();
        }        
    }
        
    public void ClickExitSetting()
    {
        Scene_Mgr.GetInstance.UnLoadSetting();
    }

    public void ClickBgmMute()
    {
        Cloud_Mgr.GetInstance.UserData.BGMMute = !Cloud_Mgr.GetInstance.UserData.BGMMute;

        if (Cloud_Mgr.GetInstance.UserData.BGMMute)
            BgmMute.overrideSprite = Resources.Load<Sprite>("mute");
        else
            BgmMute.overrideSprite = Resources.Load<Sprite>("speaker");

        Cloud_Mgr.GetInstance.SaveToCloud();
    }

    public void ClickEffectMute()
    {
        Cloud_Mgr.GetInstance.UserData.EffMute = !Cloud_Mgr.GetInstance.UserData.EffMute;

        if (Cloud_Mgr.GetInstance.UserData.EffMute)
            EffectMute.overrideSprite = Resources.Load<Sprite>("mute");
        else
            EffectMute.overrideSprite = Resources.Load<Sprite>("speaker");

        Cloud_Mgr.GetInstance.SaveToCloud();
    }

    public void ClickBtnMute()
    {
        Cloud_Mgr.GetInstance.UserData.BtnMute = !Cloud_Mgr.GetInstance.UserData.BtnMute;

        if (Cloud_Mgr.GetInstance.UserData.BtnMute)
            ButtonMute.overrideSprite = Resources.Load<Sprite>("mute");
        else
            ButtonMute.overrideSprite = Resources.Load<Sprite>("speaker");

        Cloud_Mgr.GetInstance.SaveToCloud();
    }

    public void ClickLogout()
    {
        GPGS_Mgr.GetInstance.LogoutGPGS();
        StartCoroutine(Scene_Mgr.GetInstance.LoadStart());
        Scene_Mgr.GetInstance.UnLoadMain();
        Scene_Mgr.GetInstance.UnLoadSetting();

        if (Scene_Mgr.GetInstance.bLobby)
            Scene_Mgr.GetInstance.UnLoadLobby();
        if (Scene_Mgr.GetInstance.bInventory)
            Scene_Mgr.GetInstance.UnLoadInventory();
        if (Scene_Mgr.GetInstance.bGameRoom)
            Scene_Mgr.GetInstance.UnLoadGameRoom();
        if (Scene_Mgr.GetInstance.bCompose)
            Scene_Mgr.GetInstance.UnLoadCompose();
        if (Scene_Mgr.GetInstance.bStore)
            Scene_Mgr.GetInstance.UnLoadStore();
    }
}
