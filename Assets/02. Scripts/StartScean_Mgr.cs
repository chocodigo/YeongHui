using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class StartScean_Mgr : MonoBehaviour {    
    public Image QuitPanel;
    public Text Btn_Text;
    public AudioSource bgm;

    private Transform[] ChildQuit;
    private DateTime Now;
    private bool LoginFirst;
    private bool CheckLogin;

    void Start()
    {
        ChildQuit = QuitPanel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildQuit)
            cts.gameObject.SetActive(false);

        Now = DateTime.Now;
        LoginFirst = true;
        CheckLogin = false;

        bgm.loop = true;
        bgm.volume = Cloud_Mgr.GetInstance.UserData.BGMvol;
        bgm.mute = Cloud_Mgr.GetInstance.UserData.BGMMute;
        bgm.Play();
    }

    void Update()
    {
        TimeSpan ts = DateTime.Now.Subtract(Now);

        if (ts.CompareTo(new TimeSpan(500000)) > 0 && LoginFirst)
        {
            GPGS_Mgr.GetInstance.LoginGPGS();
            LoginFirst = false;
        }

        if (GPGS_Mgr.GetInstance.bLogin)
            Btn_Text.text = "게임 시작";
        else
            Btn_Text.text = "구글 계정으로 로그인";

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (Scene_Mgr.GetInstance.bSetting)
                {
                    Scene_Mgr.GetInstance.UnLoadSetting();
                }
                else if (QuitPanel.IsActive())
                {
                    ClickCancle();
                }
                else
                {
                    foreach (RectTransform cts in ChildQuit)
                        cts.gameObject.SetActive(true);
                }
            }
        }

        if (GPGS_Mgr.GetInstance.bLogin && CheckLogin)
        {            
            StartCoroutine(Scene_Mgr.GetInstance.LoadLoading());

            Cloud_Mgr.GetInstance.LoadFromCloud();

            CheckLogin = false;
        }

        if(Cloud_Mgr.GetInstance.UserData.ID != null)
        {
            if (Cloud_Mgr.GetInstance.UserData.ID == "TEMP USER DATA")
            {
                StartCoroutine(Scene_Mgr.GetInstance.LoadInputID());
            }
            else
            {
                CharacterAbility.GetInstance.FinishLoad = true;
                StartCoroutine(Scene_Mgr.GetInstance.LoadMain());
                StartCoroutine(Scene_Mgr.GetInstance.LoadLobby());
            }

            Scene_Mgr.GetInstance.UnLoadStart();
            Scene_Mgr.GetInstance.UnLoadLoading();
        }
    }

    public void ClickQuit()
    {
        Application.Quit();
    }

    public void ClickCancle()
    {
        foreach (RectTransform cts in ChildQuit)
            cts.gameObject.SetActive(false);
    }

    public void ClickEvent()
    {
        if (GPGS_Mgr.GetInstance.bLogin)
        {
            CheckLogin = true;
        }
        else
        {            
            GPGS_Mgr.GetInstance.LoginGPGS();
            CheckLogin = true;
        }        
    }
}
