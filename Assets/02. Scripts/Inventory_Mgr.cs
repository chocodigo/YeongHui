using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Inventory_Mgr : MonoBehaviour {    
    public Button All_Btn;
    public Button Weapon_Btn;
    public Button Defence_Btn;
    public Button Item_Btn;
    public Image Inventory;
    public Text Page;

    private Sprite Menu_Btn1;
    private Sprite Menu_Btn2;   
    private int MaxPage;
    private Image[] IV;
    private Text[] IV_Text;
    private int NowState; //All 0, Weapon 1, Defence 2, Item 3

    // Use this for initialization
    void Start()
    {
        Menu_Btn1 = Resources.Load<Sprite>("Menu_Btn1");
        Menu_Btn2 = Resources.Load<Sprite>("Menu_Btn2");

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

        IV_Text[0] = Inventory.transform.FindChild("IV1").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[1] = Inventory.transform.FindChild("IV2").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[2] = Inventory.transform.FindChild("IV3").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[3] = Inventory.transform.FindChild("IV4").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[4] = Inventory.transform.FindChild("IV5").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[5] = Inventory.transform.FindChild("IV6").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[6] = Inventory.transform.FindChild("IV7").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[7] = Inventory.transform.FindChild("IV8").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[8] = Inventory.transform.FindChild("IV9").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[9] = Inventory.transform.FindChild("IV10").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[10] = Inventory.transform.FindChild("IV11").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[11] = Inventory.transform.FindChild("IV12").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[12] = Inventory.transform.FindChild("IV13").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[13] = Inventory.transform.FindChild("IV14").FindChild("Item").GetComponentInChildren<Text>();
        IV_Text[14] = Inventory.transform.FindChild("IV15").FindChild("Item").GetComponentInChildren<Text>();
        
        MaxPage = 1;        
               
        clickAll();
    }

    void Update()
    {
        if (InventoryInformation_Mgr.GetInstance.ChangeInventoryInfo)
        {
            InventoryInformation_Mgr.GetInstance.InitIVinfo();
            AlginIVinfo(NowState);
            if (MaxPage < InventoryInformation_Mgr.GetInstance.NowPage)
                InventoryInformation_Mgr.GetInstance.NowPage = MaxPage;
            Page.text = InventoryInformation_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();
            ShowInvntory();
            InventoryInformation_Mgr.GetInstance.ChangeInventoryInfo = false;
        }
    }

    private void AlginIVinfo(int state) //All : 0, Weapon 1, Defence 2, Item 3
    {
        int now = 0;
        if (state == 0)
        {            
            for (int i = 0; i < 90; i++)
                if(Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] != '$')
                    InventoryInformation_Mgr.GetInstance.IVinfo[now++] = i;
        }
        else if(state == 1)
        {
            for (int i = 0; i < 90; i++)
                if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] == 'W')
                    InventoryInformation_Mgr.GetInstance.IVinfo[now++] = i;
        }
        else if(state == 2)
        {
            for (int i = 0; i < 90; i++)
                if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] == 'D')
                    InventoryInformation_Mgr.GetInstance.IVinfo[now++] = i;
        }
        else if(state == 3)
        {
            for (int i = 0; i < 90; i++)
                if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] == 'I')
                    InventoryInformation_Mgr.GetInstance.IVinfo[now++] = i;
        }
        MaxPage = (now-1)/15 + 1;
    }

    private void ShowInvntory()
    {
        for(int i = 0; i<15;i++)
        {
            if(InventoryInformation_Mgr.GetInstance.IVinfo[i + (InventoryInformation_Mgr.GetInstance.NowPage - 1) * 15] == -1)
            {
                IV[i].overrideSprite = ItemInfo.GetInstance.GetItemInfo("$").ItemImage;
                IV_Text[i].text = "";
            }
            else
            {
                string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[InventoryInformation_Mgr.GetInstance.IVinfo[i + (InventoryInformation_Mgr.GetInstance.NowPage - 1) * 15]].Split(new char[] { ':' });
                IV[i].overrideSprite = ItemInfo.GetInstance.GetItemInfo(p[0]).ItemImage;
                if (p[0][0] == 'I')
                    IV_Text[i].text = p[1];
                else
                    IV_Text[i].text = "";
            }            
        }        
    }

    public void clickAll()
    {
        NowState = 0;

        All_Btn.image.overrideSprite = Menu_Btn1;
        Weapon_Btn.image.overrideSprite = Menu_Btn2;
        Defence_Btn.image.overrideSprite = Menu_Btn2;
        Item_Btn.image.overrideSprite = Menu_Btn2;

        InventoryInformation_Mgr.GetInstance.NowPage = 1;
        InventoryInformation_Mgr.GetInstance.InitIVinfo();
        AlginIVinfo(NowState);
        Page.text = InventoryInformation_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();

        ShowInvntory();
    }

    public void clickWeapon()
    {
        NowState = 1;

        All_Btn.image.overrideSprite = Menu_Btn2;
        Weapon_Btn.image.overrideSprite = Menu_Btn1;
        Defence_Btn.image.overrideSprite = Menu_Btn2;
        Item_Btn.image.overrideSprite = Menu_Btn2;

        InventoryInformation_Mgr.GetInstance.NowPage = 1;
        InventoryInformation_Mgr.GetInstance.InitIVinfo();
        AlginIVinfo(NowState);
        Page.text = InventoryInformation_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();

        ShowInvntory();
    }

    public void clickDefence()
    {
        NowState = 2;

        All_Btn.image.overrideSprite = Menu_Btn2;
        Weapon_Btn.image.overrideSprite = Menu_Btn2;
        Defence_Btn.image.overrideSprite = Menu_Btn1;
        Item_Btn.image.overrideSprite = Menu_Btn2;

        InventoryInformation_Mgr.GetInstance.NowPage = 1;
        InventoryInformation_Mgr.GetInstance.InitIVinfo();
        AlginIVinfo(NowState);
        Page.text = InventoryInformation_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();

        ShowInvntory();
    }

    public void clickItem()
    {
        NowState = 3;

        All_Btn.image.overrideSprite = Menu_Btn2;
        Weapon_Btn.image.overrideSprite = Menu_Btn2;
        Defence_Btn.image.overrideSprite = Menu_Btn2;
        Item_Btn.image.overrideSprite = Menu_Btn1;

        InventoryInformation_Mgr.GetInstance.NowPage = 1;
        InventoryInformation_Mgr.GetInstance.InitIVinfo();
        AlginIVinfo(NowState);
        Page.text = InventoryInformation_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();

        ShowInvntory();
    }

    public void ClickBackPage()
    {
        if (InventoryInformation_Mgr.GetInstance.NowPage == 1)
            return;

        InventoryInformation_Mgr.GetInstance.NowPage--;
        Page.text = InventoryInformation_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();

        ShowInvntory();
    }

    public void ClickFowardPage()
    {
        if (InventoryInformation_Mgr.GetInstance.NowPage == MaxPage)
            return;

        InventoryInformation_Mgr.GetInstance.NowPage++;
        Page.text = InventoryInformation_Mgr.GetInstance.NowPage.ToString() + " / " + MaxPage.ToString();

        ShowInvntory();
    }
}
