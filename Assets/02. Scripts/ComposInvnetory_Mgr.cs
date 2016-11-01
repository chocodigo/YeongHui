using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComposInvnetory_Mgr : MonoBehaviour {
    public Image Inventory;
    
    private Transform[] ChildTs;
    private Button Menu1_Btn;
    private Button Menu2_Btn;
    private Text Menu1Btn_Text;
    private Text Menu2Btn_Text;
    private Sprite Image_Btn1;
    private Sprite Image_Btn2;
    private Text Page;
    private int MaxPage;
    private Image[] IV;
    private Text[] IV_Text;
    private int nowState;

    void Start () {
        ChildTs = Inventory.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildTs)
            cts.gameObject.SetActive(false);
        
        Menu1_Btn = Inventory.transform.FindChild("Menu1").GetComponent<Button>();
        Menu2_Btn = Inventory.transform.FindChild("Menu2").GetComponent<Button>();
        Menu1Btn_Text = Menu1_Btn.transform.FindChild("Text").GetComponent<Text>();
        Menu2Btn_Text = Menu2_Btn.transform.FindChild("Text").GetComponent<Text>();

        Image_Btn1 = Resources.Load<Sprite>("Menu_Btn1");
        Image_Btn2 = Resources.Load<Sprite>("Menu_Btn2");

        Page = Inventory.transform.FindChild("Page").FindChild("Text").GetComponent<Text>();

        IV = new Image[15];
        IV_Text = new Text[15];

        IV[0] = Inventory.transform.FindChild("IV1").FindChild("Item").GetComponent<Image>();
        IV[1] = Inventory.transform.FindChild("IV2").FindChild("Item").GetComponent<Image>();
        IV[2] = Inventory.transform.FindChild("IV3").FindChild("Item").GetComponent<Image>();
        IV[3] = Inventory.transform.FindChild("IV4").FindChild("Item").GetComponent<Image>();
        IV[4] = Inventory.transform.FindChild("IV5").FindChild("Item").GetComponent<Image>();
        IV[5] = Inventory.transform.FindChild("IV6").FindChild("Item").GetComponent<Image>();
        IV[6] = Inventory.transform.FindChild("IV7").FindChild("Item").GetComponent<Image>();
        IV[7] = Inventory.transform.FindChild("IV8").FindChild("Item").GetComponent<Image>();
        IV[8] = Inventory.transform.FindChild("IV9").FindChild("Item").GetComponent<Image>();
        IV[9] = Inventory.transform.FindChild("IV10").FindChild("Item").GetComponent<Image>();
        IV[10] = Inventory.transform.FindChild("IV11").FindChild("Item").GetComponent<Image>();
        IV[11] = Inventory.transform.FindChild("IV12").FindChild("Item").GetComponent<Image>();
        IV[12] = Inventory.transform.FindChild("IV13").FindChild("Item").GetComponent<Image>();
        IV[13] = Inventory.transform.FindChild("IV14").FindChild("Item").GetComponent<Image>();
        IV[14] = Inventory.transform.FindChild("IV15").FindChild("Item").GetComponent<Image>();

        for (int i=0;i<15;i++)
            IV_Text[i] = IV[i].transform.FindChild("Text").GetComponent<Text>();       

        nowState = 0;
    }

    void Update()
    {
        if(ComposForm_Mgr.GetInstance.ChangeIV)
        {
            ComposForm_Mgr.GetInstance.InitIVinfo();
            AlginIVinfo(nowState);
            if (MaxPage < ComposForm_Mgr.GetInstance.NowPage)
                ComposForm_Mgr.GetInstance.NowPage = MaxPage;
            Page.text = ComposForm_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();
            ShowInvntory(nowState);
            ComposForm_Mgr.GetInstance.ChangeIV = false;
        }
    }

    private void AlginIVinfo(int state) //Weapon 0, Defence 1, Item 2
    {
        int now = 0;

        if (state == 0)
        {
            for (int i = 0; i < ItemInfo.GetInstance.composeValue; i++)
                if (ItemInfo.GetInstance.GetComposeInfo(i).result[0] == 'W')
                    ComposForm_Mgr.GetInstance.IVinfo[now++] = i;
        }
        else if (state == 1)
        {
            for (int i = 0; i < ItemInfo.GetInstance.composeValue; i++)
                if (ItemInfo.GetInstance.GetComposeInfo(i).result[0] == 'D')
                    ComposForm_Mgr.GetInstance.IVinfo[now++] = i;
        }
        else if (state == 2)
        {
            for (int i = 0; i < 90; i++)
                if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith("IT16") ||
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith("IT17") ||
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith("IT18"))
                    ComposForm_Mgr.GetInstance.IVinfo[now++] = i;
        }
        MaxPage = (now - 1) / 15 + 1;
    }

    private void ShowInvntory(int state) //Weapon 0, Defence 1, Item 2
    {
        for (int i = 0; i < 15; i++)
        {
            if (ComposForm_Mgr.GetInstance.IVinfo[i + (ComposForm_Mgr.GetInstance.NowPage - 1) * 15] == -1)
            {
                IV[i].overrideSprite = ItemInfo.GetInstance.GetItemInfo("$").ItemImage;
                IV_Text[i].text = "";
            }
            else
            {
                if(state == 2)
                {
                    string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[ComposForm_Mgr.GetInstance.IVinfo[i + (ComposForm_Mgr.GetInstance.NowPage - 1) * 15]].Split(new char[] { ':' });
                    IV[i].overrideSprite = ItemInfo.GetInstance.GetItemInfo(p[0]).ItemImage;
                    IV_Text[i].text = p[1];
                }
                else
                {
                    IV[i].overrideSprite = ItemInfo.GetInstance.GetItemInfo(ItemInfo.GetInstance.GetComposeInfo(ComposForm_Mgr.GetInstance.IVinfo[i + (ComposForm_Mgr.GetInstance.NowPage - 1) * 15]).result).ItemImage;
                    IV_Text[i].text = ItemInfo.GetInstance.GetComposeInfo(ComposForm_Mgr.GetInstance.IVinfo[i + (ComposForm_Mgr.GetInstance.NowPage - 1) * 15]).order.ToString();
                }
            }
        }
    }

    public void ClickComposeReinforce()
    {
        foreach (RectTransform cts in ChildTs)
            cts.gameObject.SetActive(true);

        if(ComposForm_Mgr.GetInstance.isCompose)
        {
            Menu1_Btn.enabled = true;

            Menu1Btn_Text.text = "무기";
            Menu2Btn_Text.text = "갑옷";
            
            if (ComposForm_Mgr.GetInstance.isWeapon)
                ClickComposWD_W();
            else
                ClickComposWD_D();
        }
        else
        {
            nowState = 2;

            Menu1_Btn.enabled = false;

            Menu1_Btn.image.overrideSprite = Image_Btn1;
            Menu2_Btn.transform.gameObject.SetActive(false);

            Menu1Btn_Text.text = "아이템";

            ComposForm_Mgr.GetInstance.NowPage = 1;            
            ComposForm_Mgr.GetInstance.InitIVinfo();
            AlginIVinfo(nowState);
            Page.text = ComposForm_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();
            ShowInvntory(nowState);
        }        
    }

    public void ClickComposWD_W()
    {
        nowState = 0;

        Menu1_Btn.image.overrideSprite = Image_Btn1;
        Menu2_Btn.image.overrideSprite = Image_Btn2;

        ComposForm_Mgr.GetInstance.NowPage = 1;
        ComposForm_Mgr.GetInstance.InitIVinfo();
        AlginIVinfo(nowState);
        Page.text = ComposForm_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();
        ShowInvntory(nowState);
    }

    public void ClickComposWD_D()
    {
        nowState = 1;

        Menu1_Btn.image.overrideSprite = Image_Btn2;
        Menu2_Btn.image.overrideSprite = Image_Btn1;

        ComposForm_Mgr.GetInstance.NowPage = 1;        
        ComposForm_Mgr.GetInstance.InitIVinfo();
        AlginIVinfo(nowState);
        Page.text = ComposForm_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();
        ShowInvntory(nowState);
    }

    public void ClickBackPage()
    {
        if (ComposForm_Mgr.GetInstance.NowPage == 1)
            return;

        ComposForm_Mgr.GetInstance.NowPage--;
        Page.text = ComposForm_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();
        ComposForm_Mgr.GetInstance.ChangeIV = true;
    }

    public void ClickFowardPage()
    {
        if (ComposForm_Mgr.GetInstance.NowPage == MaxPage)
            return;

        ComposForm_Mgr.GetInstance.NowPage++;
        Page.text = ComposForm_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();
        ComposForm_Mgr.GetInstance.ChangeIV = true;
    }
}
