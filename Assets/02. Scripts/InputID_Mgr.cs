using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputID_Mgr : MonoBehaviour
{
    public GameObject InputID;
    public GameObject Warnning;
    public GameObject CheckID;
    public InputField InputUserId;
    public Text warnning_Text;
    public Text Check_userID_Text;
    public Image QuitPanel;

    private Transform[] ChildQuit;

    void Start()
    {
        InputID.SetActive(true);
        Warnning.SetActive(false);
        CheckID.SetActive(false);

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
                if (QuitPanel.IsActive())
                {
                    ClickCancle();
                }
                else
                {
                    InputID.SetActive(false);
                    Warnning.SetActive(false);
                    CheckID.SetActive(false);

                    foreach (RectTransform cts in ChildQuit)
                        cts.gameObject.SetActive(true);
                }
            }
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
        InputID.SetActive(true);
    }

    public void ClickEventID()
    {
        InputID.SetActive(false);

        bool idChecker = System.Text.RegularExpressions.Regex.IsMatch(InputUserId.text, @"^[a-zA-Z0-9가-힣]*$");
        if (idChecker == false)
        {
            warnning_Text.text = "특수문자 또는 초성을 입력하였습니다.";
            Warnning.SetActive(true);
        }
        else if (string.IsNullOrEmpty(InputUserId.text))
        {
            warnning_Text.text = "입력이 없습니다.";
            Warnning.SetActive(true);
        }
        else if (InputUserId.text.Length > 8)
        {
            warnning_Text.text = "1~8자를 입력하세요.";
            Warnning.SetActive(true);
        }
        else
        {
            Check_userID_Text.text = InputUserId.text;
            CheckID.SetActive(true);
        }
    }

    public void ClickEventWarnning()
    {
        InputUserId.text = "";
        InputID.SetActive(true);
        Warnning.SetActive(false);
    }

    public void ClickEventCheckCancel()
    {
        InputID.SetActive(true);
        CheckID.SetActive(false);
    }

    public void ClickEventCheckOK()
    {
        Cloud_Mgr.GetInstance.UserData = new Cloud_Mgr.GameData(InputUserId.text);

        CharacterAbility.GetInstance.FinishLoad = true;
        StartCoroutine(Scene_Mgr.GetInstance.LoadMain());
        StartCoroutine(Scene_Mgr.GetInstance.LoadLobby());

        Scene_Mgr.GetInstance.UnLoadInputID();
    }
}