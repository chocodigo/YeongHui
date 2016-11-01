using UnityEngine;
using UnityEngine.UI;
using System;

public class GameRoom_Mgr : MonoBehaviour
{
    public GameObject Stage;
    public GameObject Page;
    public Image Back_Arrow;
    public Image Foward_Arrow;
    public Image WarnningPanel;

    private int NowPage;
    private int MaxPage;
    private Button[] S = new Button[10];
    private Image[] Page_Dot = new Image[2];
    private Transform[] ChildWarnning;
    
    void Start()
    {
        NowPage = 1;
        MaxPage = 2;

        S[0] = Stage.transform.FindChild("S1").GetComponent<Button>();
        S[1] = Stage.transform.FindChild("S2").GetComponent<Button>();
        S[2] = Stage.transform.FindChild("S3").GetComponent<Button>();
        S[3] = Stage.transform.FindChild("S4").GetComponent<Button>();
        S[4] = Stage.transform.FindChild("S5").GetComponent<Button>();
        S[5] = Stage.transform.FindChild("S6").GetComponent<Button>();
        S[6] = Stage.transform.FindChild("S7").GetComponent<Button>();
        S[7] = Stage.transform.FindChild("S8").GetComponent<Button>();
        S[8] = Stage.transform.FindChild("S9").GetComponent<Button>();
        S[9] = Stage.transform.FindChild("S10").GetComponent<Button>();

        Page_Dot[0] = Page.transform.FindChild("Page1").GetComponent<Image>();
        Page_Dot[1] = Page.transform.FindChild("Page2").GetComponent<Image>();

        ChildWarnning = WarnningPanel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildWarnning)
            cts.gameObject.SetActive(false);

        PageChange();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (Scene_Mgr.GetInstance.bSetting)
                    Scene_Mgr.GetInstance.UnLoadSetting();
                else
                    BacktoLobbyBtn();
            }
        }
    }

    private void PageChange()
    {
        Back_Arrow.overrideSprite = Resources.Load<Sprite>("Back_Page");
        Foward_Arrow.overrideSprite = Resources.Load<Sprite>("Foward_Page");
        if (NowPage == 1)
            Back_Arrow.overrideSprite = Resources.Load<Sprite>("BackLock_Page");
        if(NowPage == MaxPage)
            Foward_Arrow.overrideSprite = Resources.Load<Sprite>("FowardLock_Page");

        for(int i=0;i<MaxPage;i++)
            Page_Dot[i].overrideSprite = Resources.Load<Sprite>("Dot1");
        Page_Dot[NowPage-1].overrideSprite = Resources.Load<Sprite>("Dot2");

        int startStage = 1 + (NowPage-1)*10;
        for (int i=0;i<10;i++)
        {
            int NowStageNum = startStage + i;
            S[i].transform.FindChild("Stage").GetComponent<Text>().text = NowStageNum.ToString();

            S[i].onClick.RemoveAllListeners();

            if (startStage+i<=Cloud_Mgr.GetInstance.UserData.Stage)
            {
                S[i].transform.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Stage_Panel");
                S[i].transform.FindChild("Lock").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("NoImage");

                int stageStar = Convert.ToInt32(Cloud_Mgr.GetInstance.UserData.StageStar[NowStageNum - 1].ToString());

                S[i].transform.FindChild("Star1").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("NoImage");
                S[i].transform.FindChild("Star2").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("NoImage");
                S[i].transform.FindChild("Star3").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("NoImage");

                if (stageStar>0)
                    S[i].transform.FindChild("Star1").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("star");
                if (stageStar > 1)
                    S[i].transform.FindChild("Star2").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("star");
                if (stageStar > 2)
                    S[i].transform.FindChild("Star3").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("star");
                
                S[i].onClick.AddListener(() => { StartStage(NowStageNum); });
            }
            else
            {
                S[i].transform.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StageLock_Panel");
                S[i].transform.FindChild("Lock").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Lock");
                S[i].transform.FindChild("Star1").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("NoImage");
                S[i].transform.FindChild("Star2").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("NoImage");
                S[i].transform.FindChild("Star3").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("NoImage");
            }
        }
    }

    public void Back()
    {
        if (NowPage == 1)
            return;
        NowPage--;
        PageChange();
    }

    public void Foward()
    {
        if (NowPage == MaxPage)
            return;
        NowPage++;
        PageChange();
    }

    public void StartStage(int stage)
    {
        if(CheckInventory())
        {
            foreach (RectTransform cts in ChildWarnning)
                cts.gameObject.SetActive(true);            
            WarnningPanel.transform.FindChild("Panel").FindChild("Text").GetComponent<Text>().text = "가방의 공간이 부족합니다.";
        }
        if (Cloud_Mgr.GetInstance.UserData.Heart == 0)
        {
            foreach (RectTransform cts in ChildWarnning)
                cts.gameObject.SetActive(true);
            WarnningPanel.transform.FindChild("Panel").FindChild("Text").GetComponent<Text>().text = "하트가 부족합니다.";
        }
        else
        {
            if (Cloud_Mgr.GetInstance.UserData.Heart == 3 + (Cloud_Mgr.GetInstance.UserData.LV * 2))
                Cloud_Mgr.GetInstance.UserData.LastTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Cloud_Mgr.GetInstance.UserData.Heart--;
            Cloud_Mgr.GetInstance.SaveToCloud();

            StartCoroutine(Scene_Mgr.GetInstance.LoadGame(stage));
            StartCoroutine(Scene_Mgr.GetInstance.LoadGameUI());
            Scene_Mgr.GetInstance.UnLoadMain();
            Scene_Mgr.GetInstance.UnLoadGameRoom();
        }            
    }

    private bool CheckInventory()
    {
        int emptyNum = 0;

        for (int i = 0; i < 90; i++)
        {
            if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] == '$')
                emptyNum++;
        }

        if (emptyNum > 3)
            return false;
        else
            return true;
    }

    public void WarnningOK()
    {
        foreach (RectTransform cts in ChildWarnning)
            cts.gameObject.SetActive(false);
    }

    public void BacktoLobbyBtn()
    {
        StartCoroutine(Scene_Mgr.GetInstance.LoadLobby());
        Scene_Mgr.GetInstance.UnLoadGameRoom();
    }
}
