using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi.SavedGame;
using System.Collections;
using UnityEngine.UI;

public class Lobby_Mgr : MonoBehaviour
{
    RaycastHit hit;
    GameObject[] target;

    public Text user_ID;
    public Text user_LV;
    public Text User_EXP;
    public Image User_EXPBar;
    public Image QuitPanel;

    private Transform[] ChildQuit;

    void Start()
    {
        target = new GameObject[4];

        user_ID.text = Cloud_Mgr.GetInstance.UserData.ID;
        user_LV.text = "LV. " + Cloud_Mgr.GetInstance.UserData.LV.ToString();
        User_EXP.text = Cloud_Mgr.GetInstance.UserData.EXP.ToString() + " / " + Convert.ToString(Cloud_Mgr.GetInstance.UserData.LV * 1000);
        User_EXPBar.fillAmount = (float)Cloud_Mgr.GetInstance.UserData.EXP / (float)(Cloud_Mgr.GetInstance.UserData.LV * 1000);

        ChildQuit = QuitPanel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildQuit)
            cts.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if(Scene_Mgr.GetInstance.bSetting)
                    Scene_Mgr.GetInstance.UnLoadSetting();
                else if(QuitPanel.IsActive())
                    ClickCancle();
                else
                    foreach (RectTransform cts in ChildQuit)
                        cts.gameObject.SetActive(true);
            }
        }
        RaycastToCharacter();
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

    public void RaycastToCharacter()
    {        
        int cnt = Input.touchCount; //현재 터치 갯수 반환   
        int MaxTouch = 4;  

        for (int i = 0; i < cnt && i < MaxTouch; ++i)
        {
            Touch touch = Input.GetTouch(i);
            Vector2 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(pos); //레이를 카메라로부터 Pos로 발사
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {                    
                    if (hit.collider.tag == "Dragon" || hit.collider.tag == "Witch") //케릭터 오브젝트가 히트 되었을 경우만 감지
                    {
                        target[i] = hit.collider.gameObject;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (target != null) //움직일 오브젝트 가 있을경우 해당 오브젝트 회전
                {                    
                    Vector2 prePos = touch.deltaPosition;
                    Transform tr = target[i].GetComponentsInParent<Transform>()[2];
                    tr.Rotate(0, -prePos.x, 0);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            { 
                target[i] = null;
                while(i<4)
                {
                    target[i] = target[i + 1];
                    i++;
                }
            }
        }
    }

    public void InventoryBtn()
    {
        StartCoroutine(Scene_Mgr.GetInstance.LoadInventory());
        Scene_Mgr.GetInstance.UnLoadLobby();
    }

    public void CpomposeBtn()
    {
        StartCoroutine(Scene_Mgr.GetInstance.LoadCompose());
        Scene_Mgr.GetInstance.UnLoadLobby();
    }

    public void StoreBtn()
    {
        StartCoroutine(Scene_Mgr.GetInstance.LoadStore());
        Scene_Mgr.GetInstance.UnLoadLobby();
    }

    public void GameSelectBtn()
    {
        StartCoroutine(Scene_Mgr.GetInstance.LoadGameRoom());
        Scene_Mgr.GetInstance.UnLoadLobby();
    }
}
