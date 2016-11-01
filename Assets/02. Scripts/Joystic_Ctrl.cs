using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Joystic_Ctrl : MonoBehaviour {
    public Image HP_Panel;
    public Image PausePanel;
    public Image WarnningPanel;
    public Image Fail_Panel;
    public Image Success_Panel;
    public Image Stick;
    public Image Joy_Panel;
    public Image Weapon;
    public GameObject Slot;
    
    private Image Character;
    private Text HP_Text;
    private Image HP_Bar;
    private Transform[] ChildPause;
    private Transform[] ChildWarnning;
    private Transform[] ChildFail;
    private Transform[] ChildSuccess;
    private float radius;
    private Vector3 defaultCenter;
    private DateTime ClickTime;
    private float AttackMax = 0.5f;
    private float AttackTime;
    private Image[] Slot_Image;
    private Text[] Slot_Text;
    private Image[] Star_Image;
    private Image[] Item_Image;
    private Text[] Item_Value;
    private bool CheckFail;
    private bool CheckSuccess;
    private Text earn_Coin;

    void Start ()
    {      
        ClickTime = DateTime.Now;

        HP_Text = HP_Panel.transform.FindChild("Text").GetComponent<Text>();
        HP_Bar = HP_Panel.transform.FindChild("HP_Bar").GetComponent<Image>();
        Character = HP_Panel.transform.FindChild("Character").GetComponent<Image>();

        ChildPause = PausePanel.GetComponentsInChildren<RectTransform>();
        Pause(false);
        ChildWarnning = WarnningPanel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildWarnning)
            cts.gameObject.SetActive(false);
        ChildFail = Fail_Panel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildFail)
            cts.gameObject.SetActive(false);
        CheckFail = false;
        ChildSuccess = Success_Panel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildSuccess)
            cts.gameObject.SetActive(false);
        CheckSuccess = false;

        defaultCenter = Stick.rectTransform.position;
        radius = 120f;

        Character.overrideSprite = Resources.Load<Sprite>("profile_YeongHui");
        Weapon.overrideSprite = Resources.Load<Sprite>("rod");
        
        Slot_Image = new Image[4];
        Slot_Text = new Text[4];

        Slot_Image[0] = Slot.transform.FindChild("Slot1").FindChild("Image").GetComponent<Image>();
        Slot_Image[1] = Slot.transform.FindChild("Slot2").FindChild("Image").GetComponent<Image>();
        Slot_Image[2] = Slot.transform.FindChild("Slot3").FindChild("Image").GetComponent<Image>();
        Slot_Image[3] = Slot.transform.FindChild("Slot4").FindChild("Image").GetComponent<Image>();

        Slot_Text[0] = Slot.transform.FindChild("Slot1").FindChild("Image").FindChild("Text").GetComponent<Text>();
        Slot_Text[1] = Slot.transform.FindChild("Slot2").FindChild("Image").FindChild("Text").GetComponent<Text>();
        Slot_Text[2] = Slot.transform.FindChild("Slot3").FindChild("Image").FindChild("Text").GetComponent<Text>();
        Slot_Text[3] = Slot.transform.FindChild("Slot4").FindChild("Image").FindChild("Text").GetComponent<Text>();

        for (int i = 0; i < 4; i++)
        {
            int HoldNum = Cloud_Mgr.GetInstance.UserData.ItemSlot[i];
            if (HoldNum != -1)
            {
                string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[HoldNum].Split(new char[] { ':' });
                Slot_Image[i].overrideSprite = ItemInfo.GetInstance.GetItemInfo(p[0]).ItemImage;
                Slot_Text[i].text = p[1];
            }
            else
            {
                Slot_Image[i].overrideSprite = Resources.Load<Sprite>("NoImage");
                Slot_Text[i].text = "";
            }
        }

        Star_Image = new Image[3];
        Item_Image = new Image[3];
        Item_Value = new Text[3];

        Star_Image[0] = Success_Panel.transform.FindChild("Panel").FindChild("Star1").GetComponent<Image>();
        Star_Image[1] = Success_Panel.transform.FindChild("Panel").FindChild("Star2").GetComponent<Image>();
        Star_Image[2] = Success_Panel.transform.FindChild("Panel").FindChild("Star3").GetComponent<Image>();

        Item_Image[0] = Success_Panel.transform.FindChild("Panel").FindChild("I1").FindChild("Item").GetComponent<Image>();
        Item_Image[1] = Success_Panel.transform.FindChild("Panel").FindChild("I2").FindChild("Item").GetComponent<Image>();
        Item_Image[2] = Success_Panel.transform.FindChild("Panel").FindChild("I3").FindChild("Item").GetComponent<Image>();

        Item_Value[0] = Success_Panel.transform.FindChild("Panel").FindChild("I1").FindChild("Item").FindChild("Text").GetComponent<Text>();
        Item_Value[1] = Success_Panel.transform.FindChild("Panel").FindChild("I2").FindChild("Item").FindChild("Text").GetComponent<Text>();
        Item_Value[2] = Success_Panel.transform.FindChild("Panel").FindChild("I3").FindChild("Item").FindChild("Text").GetComponent<Text>();

        earn_Coin = Success_Panel.transform.FindChild("Panel").FindChild("Coin_Value").GetComponent<Text>();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (CheckFail)
                    Exit();
                else if (CheckSuccess)
                    Exit();
                else if (GameInfo_Mgr.GetInstance.Pause)
                    Pause(false);
                else
                    Pause(true);
            }
        }

        if (GameInfo_Mgr.GetInstance.CharacterState)
        {
            HP_Text.text = CharacterAbility.GetInstance.dragon.NowHealth.ToString() + '/' + CharacterAbility.GetInstance.dragon.Health.ToString();
            HP_Bar.fillAmount = (float)CharacterAbility.GetInstance.dragon.NowHealth / (float)CharacterAbility.GetInstance.dragon.Health;
        }
        else
        {
            HP_Text.text = CharacterAbility.GetInstance.yeonghui.NowHealth.ToString() + '/' + CharacterAbility.GetInstance.yeonghui.Health.ToString();
            HP_Bar.fillAmount = (float)CharacterAbility.GetInstance.yeonghui.NowHealth / (float)CharacterAbility.GetInstance.yeonghui.Health;
        }

        if (GameInfo_Mgr.GetInstance.contact_Obstacle)
        {
            Joy_Panel.overrideSprite = Resources.Load<Sprite>("JoyPanel2");
        }
        else
        {
            Joy_Panel.overrideSprite = Resources.Load<Sprite>("JoyPanel");
        }

        if (GameInfo_Mgr.GetInstance.Fail)
        {
            CheckFail = true;
            GameInfo_Mgr.GetInstance.Pause = true;
            Time.timeScale = 0;

            foreach (RectTransform cts in ChildFail)
                cts.gameObject.SetActive(true);

            GameInfo_Mgr.GetInstance.Fail = false;
        }

        if (GameInfo_Mgr.GetInstance.Success)
        {
            CheckSuccess = true;
            GameInfo_Mgr.GetInstance.Pause = true;
            Time.timeScale = 0;

            earn_Coin.text = GameInfo_Mgr.GetInstance.coin.ToString("#,##0");

            foreach (RectTransform cts in ChildSuccess)
                cts.gameObject.SetActive(true);

            for (int i = 0; i < GameInfo_Mgr.GetInstance.Star; i++)
            {
                Star_Image[i].overrideSprite = Resources.Load<Sprite>("star");
            }
            
            char[] tempStart = Cloud_Mgr.GetInstance.UserData.StageStar.ToCharArray();

            if (Convert.ToInt32(tempStart[GameInfo_Mgr.GetInstance.NowStage - 1].ToString()) < GameInfo_Mgr.GetInstance.Star)
            {
                tempStart[GameInfo_Mgr.GetInstance.NowStage - 1] = Convert.ToChar(GameInfo_Mgr.GetInstance.Star.ToString());
                Cloud_Mgr.GetInstance.UserData.StageStar = new string(tempStart);
            }
            if (GameInfo_Mgr.GetInstance.NowStage == Cloud_Mgr.GetInstance.UserData.Stage)
            {
                Cloud_Mgr.GetInstance.UserData.Stage++;
                Cloud_Mgr.GetInstance.UserData.StageStar += "0";
            }

            Cloud_Mgr.GetInstance.UserData.Coin += GameInfo_Mgr.GetInstance.coin;
            Cloud_Mgr.GetInstance.UserData.EXP += GameInfo_Mgr.GetInstance.exp;

            for(int i=0;i<3;i++)
            {
                string[] p = GameInfo_Mgr.GetInstance.item[i].Split(new char[] { ':' });

                if (p[0] == "$")
                {
                    Item_Image[i].overrideSprite = Resources.Load<Sprite>("Notting");
                    Item_Value[i].text = "";
                }
                else
                {                     
                    ItemInfo.ItemInformation itemInfo = ItemInfo.GetInstance.GetItemInfo(p[0]);
                    Item_Image[i].overrideSprite = itemInfo.ItemImage;
                    Item_Value[i].text = p[1];
                    GetItem_Btn(itemInfo, Convert.ToInt32(p[1]));
                }
            }

            Cloud_Mgr.GetInstance.SaveToCloud();
            GameInfo_Mgr.GetInstance.Success = false;
        }

        AttackTime -= Time.deltaTime;
    }

    public void ChangeCharacter()
    {
        if (CharacterAbility.GetInstance.dragon.NowHealth <= 0)
            GameInfo_Mgr.GetInstance.CharacterState = false;
        else
            GameInfo_Mgr.GetInstance.CharacterState = !GameInfo_Mgr.GetInstance.CharacterState;
        GameInfo_Mgr.GetInstance.DirChange = false;

        if (GameInfo_Mgr.GetInstance.CharacterState)
        {
            Character.overrideSprite = Resources.Load<Sprite>("profile_Dragon");
            Weapon.overrideSprite = Resources.Load<Sprite>("greatsword");
        }
        else
        {
            Character.overrideSprite = Resources.Load<Sprite>("profile_YeongHui");
            Weapon.overrideSprite = Resources.Load<Sprite>("rod");
        }
    }

    public void doubleClick()
    {
        if (DateTime.Now - ClickTime < new TimeSpan(0, 0, 1))
        {
            GameInfo_Mgr.GetInstance.Obstacle = true;
            ClickTime = DateTime.Now - new TimeSpan(0, 0, 2);
        }
        else
            ClickTime = DateTime.Now;
    }

    public void Move()
    {
        Vector2 touchPos = Input.GetTouch(0).position;
        Vector3 dir = new Vector3(touchPos.x, touchPos.y, defaultCenter.z) - defaultCenter;
        float Distance = Vector3.Distance(new Vector3(touchPos.x, touchPos.y, defaultCenter.z), defaultCenter);

        if (Distance > radius)
            Stick.rectTransform.position = defaultCenter + dir.normalized * radius;
        else
            Stick.rectTransform.position = defaultCenter + dir;
        
        GameInfo_Mgr.GetInstance.dir = dir;
    }

    public void End()
    {
        Stick.rectTransform.position = defaultCenter;
        GameInfo_Mgr.GetInstance.dir = new Vector3(0, 0, 0);
    }

    public void Attack()
    {
        if (!GameInfo_Mgr.GetInstance.CharacterState && AttackTime <= 0)
        {
            AttackTime = AttackMax;
            GameInfo_Mgr.GetInstance.Attack_YeongHui = true;
        }        
    }

    public void AttackPress()
    {
        if (GameInfo_Mgr.GetInstance.CharacterState)
        {
            GameInfo_Mgr.GetInstance.Attack_Dragon = true;
        }
    }

    public void AttackRelease()
    {
        if (GameInfo_Mgr.GetInstance.CharacterState)
        {
            GameInfo_Mgr.GetInstance.Attack_Dragon = false;
        }
    }
    
    public void slot(int index)
    {
        GameInfo_Mgr.GetInstance.usingItem = index;

        int value = Convert.ToInt32(Slot_Text[index].text) - 1;
        if (value == 0)
        {
            Slot_Image[index].overrideSprite = Resources.Load<Sprite>("NoImage");
            Slot_Text[index].text = "";
        }
        else
        {
            Slot_Text[index].text = value.ToString();
        }
    }

    public void Pause(bool state)
    {
        GameInfo_Mgr.GetInstance.Pause = state;
        if (state)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        foreach (RectTransform cts in ChildPause)
            cts.gameObject.SetActive(state);
    }

    public void Retry()
    {
        if (Cloud_Mgr.GetInstance.UserData.Heart == 0)
        {
            foreach (RectTransform cts in ChildWarnning)
                cts.gameObject.SetActive(true);
        }
        else
        {
            if (Cloud_Mgr.GetInstance.UserData.Heart == 3 + (Cloud_Mgr.GetInstance.UserData.LV * 2)) 
                Cloud_Mgr.GetInstance.UserData.LastTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");     
            Cloud_Mgr.GetInstance.UserData.Heart--;
            Cloud_Mgr.GetInstance.SaveToCloud();

            int Stage = GameInfo_Mgr.GetInstance.NowStage;

            Scene_Mgr.GetInstance.UnLoadGame(Stage);
            StartCoroutine(Scene_Mgr.GetInstance.LoadGame(Stage));
            StartCoroutine(Scene_Mgr.GetInstance.LoadGameUI());            
            Scene_Mgr.GetInstance.UnLoadGameUI();
        }
    }

    public void Next()
    {        
        if (Cloud_Mgr.GetInstance.UserData.Heart == 0)
        {
            foreach (RectTransform cts in ChildWarnning)
                cts.gameObject.SetActive(true);
        }
        else
        {
            if (Cloud_Mgr.GetInstance.UserData.Heart == 3 + (Cloud_Mgr.GetInstance.UserData.LV * 2))
                Cloud_Mgr.GetInstance.UserData.LastTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Cloud_Mgr.GetInstance.UserData.Heart--;
            Cloud_Mgr.GetInstance.SaveToCloud();

            int Stage = GameInfo_Mgr.GetInstance.NowStage;

            Scene_Mgr.GetInstance.UnLoadGame(Stage);
            StartCoroutine(Scene_Mgr.GetInstance.LoadGame(Stage+1));
            StartCoroutine(Scene_Mgr.GetInstance.LoadGameUI());
            Scene_Mgr.GetInstance.UnLoadGameUI();
        }
    }

    public void Exit()
    {
        CharacterAbility.GetInstance.FinishLoad = true;
        StartCoroutine(Scene_Mgr.GetInstance.LoadMain());
        StartCoroutine(Scene_Mgr.GetInstance.LoadGameRoom());
        Scene_Mgr.GetInstance.UnLoadGame(GameInfo_Mgr.GetInstance.NowStage);
        Scene_Mgr.GetInstance.UnLoadGameUI();
    }

    public void WarnningOK()
    {
        foreach (RectTransform cts in ChildWarnning)
            cts.gameObject.SetActive(false);
    }

    private void GetItem_Btn(ItemInfo.ItemInformation itemInfo, int NowValue)
    {
        int empty = -1;
        int[] Item = new int[90];
        int ItemPos = 0;

        for (int i = 0; i < 90; i++)
            Item[i] = -1;

        for (int i = 0; i < 90; i++)
        {
            if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(itemInfo.Code) && itemInfo.Code[0] == 'I')
                Item[ItemPos++] = i;
            if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] == '$' && empty == -1)
                empty = i;
        }               
             
        if (itemInfo.Code[0] == 'I')
        {            
            while(NowValue > 0)
            {
                int maxPos = -1;
                int maxValue = 0;

                for (int i = 0; i < ItemPos; i++)
                {
                    if (Item[i] != -1)
                    {
                        string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[Item[i]].Split(new char[] { ':' });
                        int value = Convert.ToInt32(p[1]);
                        if (maxValue < value)
                        {
                            maxPos = i;
                            maxValue = value;
                        }
                    }
                }

                if (maxPos == -1)
                    break;

                if (NowValue + maxValue > 100)
                {
                    Cloud_Mgr.GetInstance.UserData.HoldItem[Item[maxPos]] = itemInfo.Code + ":100";
                    NowValue -= 100 - maxValue;
                }
                else
                {
                    Cloud_Mgr.GetInstance.UserData.HoldItem[Item[maxPos]] = itemInfo.Code + ":" + (NowValue + maxValue).ToString();
                    NowValue = 0;
                }
                Item[maxPos] = -1;
            }                
        }
        if (NowValue > 0)
        {
            Cloud_Mgr.GetInstance.UserData.HoldItem[empty] = itemInfo.Code + ":" + NowValue.ToString();
        }

        Cloud_Mgr.GetInstance.SaveToCloud();
    }
}
