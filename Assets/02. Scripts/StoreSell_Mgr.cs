using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class StoreSell_Mgr : MonoBehaviour {
    public GameObject SellPanel; // Sell
    public Image BuyItemPanel; //ItemBox
    public Image BuyPanel; //BuyBox    
    public Image WarningPanel; //Warining

    private Transform[] ChildSellPanel;
    private Transform[] ChildBuyItem;
    private Transform[] ChildBuy;
    private Transform[] ChildWarining;
    private Button All_Btn;
    private Button Weapon_Btn;
    private Button Defence_Btn;
    private Button Item_Btn;
    private Image Inventory;    
    private Text Page;
    private Sprite Menu_Btn1;
    private Sprite Menu_Btn2;
    private int MaxPage;
    private Image[] IV;
    private Text[] IV_Text;
    private int NowState; //All 0, Weapon 1, Defence 2, Item 3
    private Image itemInfo;
    private Text Name;
    private Image ItemImage;
    private Text ItemValue;
    private Text info;
    private GameObject ability;
    private Text Attribute;
    private Text Health;
    private Text Attack;
    private Text Defence;
    private Transform[] ChildAbility;
    private Image[] IV_Btn;
    private int SelectIV;
    private int NowPage;
    private int[] IVinfo;
    private int NowValue;
    private int NowMaxValue;

    // Use this for initialization
    void Start () {
        ChildSellPanel = SellPanel.GetComponentsInChildren<RectTransform>();
        ChildBuyItem = BuyItemPanel.GetComponentsInChildren<RectTransform>();
        ChildBuy = BuyPanel.GetComponentsInChildren<RectTransform>();
        ChildWarining = WarningPanel.GetComponentsInChildren<RectTransform>();

        foreach (RectTransform cts in ChildSellPanel)
            cts.gameObject.SetActive(false);

        Inventory = SellPanel.transform.FindChild("Inventory").GetComponent<Image>();
        All_Btn = Inventory.transform.FindChild("All").GetComponent<Button>();
        Weapon_Btn = Inventory.transform.FindChild("Weapon").GetComponent<Button>();
        Defence_Btn = Inventory.transform.FindChild("Defence").GetComponent<Button>();
        Item_Btn = Inventory.transform.FindChild("Item").GetComponent<Button>();
        Page = Inventory.transform.FindChild("Page").FindChild("Text").GetComponent<Text>();

        Menu_Btn1 = Resources.Load<Sprite>("Menu_Btn1");
        Menu_Btn2 = Resources.Load<Sprite>("Menu_Btn2");

        IV_Btn = new Image[15];
        IV = new Image[15];
        IV_Text = new Text[15];
        
        IV_Btn[0] = Inventory.transform.FindChild("IV1").GetComponent<Image>();
        IV_Btn[1] = Inventory.transform.FindChild("IV2").GetComponent<Image>();
        IV_Btn[2] = Inventory.transform.FindChild("IV3").GetComponent<Image>();
        IV_Btn[3] = Inventory.transform.FindChild("IV4").GetComponent<Image>();
        IV_Btn[4] = Inventory.transform.FindChild("IV5").GetComponent<Image>();
        IV_Btn[5] = Inventory.transform.FindChild("IV6").GetComponent<Image>();
        IV_Btn[6] = Inventory.transform.FindChild("IV7").GetComponent<Image>();
        IV_Btn[7] = Inventory.transform.FindChild("IV8").GetComponent<Image>();
        IV_Btn[8] = Inventory.transform.FindChild("IV9").GetComponent<Image>();
        IV_Btn[9] = Inventory.transform.FindChild("IV10").GetComponent<Image>();
        IV_Btn[10] = Inventory.transform.FindChild("IV11").GetComponent<Image>();
        IV_Btn[11] = Inventory.transform.FindChild("IV12").GetComponent<Image>();
        IV_Btn[12] = Inventory.transform.FindChild("IV13").GetComponent<Image>();
        IV_Btn[13] = Inventory.transform.FindChild("IV14").GetComponent<Image>();
        IV_Btn[14] = Inventory.transform.FindChild("IV15").GetComponent<Image>();

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

        IV_Text[0] = Inventory.transform.FindChild("IV1").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[1] = Inventory.transform.FindChild("IV2").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[2] = Inventory.transform.FindChild("IV3").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[3] = Inventory.transform.FindChild("IV4").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[4] = Inventory.transform.FindChild("IV5").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[5] = Inventory.transform.FindChild("IV6").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[6] = Inventory.transform.FindChild("IV7").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[7] = Inventory.transform.FindChild("IV8").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[8] = Inventory.transform.FindChild("IV9").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[9] = Inventory.transform.FindChild("IV10").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[10] = Inventory.transform.FindChild("IV11").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[11] = Inventory.transform.FindChild("IV12").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[12] = Inventory.transform.FindChild("IV13").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[13] = Inventory.transform.FindChild("IV14").FindChild("Item").FindChild("Text").GetComponent<Text>();
        IV_Text[14] = Inventory.transform.FindChild("IV15").FindChild("Item").FindChild("Text").GetComponent<Text>();

        itemInfo = SellPanel.transform.FindChild("ItemInfo").GetComponent<Image>();
        Name = itemInfo.transform.FindChild("Name").GetComponent<Text>();
        ItemImage = itemInfo.transform.FindChild("ItemImage").FindChild("Image").GetComponent<Image>();
        ItemValue = itemInfo.transform.FindChild("ItemImage").FindChild("Image").FindChild("Text").GetComponent<Text>();
        info = itemInfo.transform.FindChild("Info").GetComponent<Text>();
        ability = itemInfo.transform.FindChild("WeaponDefence").gameObject;
        Attribute = ability.transform.FindChild("Attribute").GetComponent<Text>();
        Health = ability.transform.FindChild("Health").GetComponent<Text>();
        Attack = ability.transform.FindChild("Attack").GetComponent<Text>();
        Defence = ability.transform.FindChild("Defence").GetComponent<Text>();
        ChildAbility = ability.GetComponentsInChildren<RectTransform>();
        
        foreach (RectTransform cts in ChildAbility)
            cts.gameObject.SetActive(false);

        MaxPage = 1;
        SelectIV = -1;
        IVinfo = new int[90];

        clickAll();
    }
	
    private void AlginIVinfo(int state) //All : 0, Weapon 1, Defence 2, Item 3
    {
        int now = 0;
        if (state == 0)
        {
            for (int i = 0; i < 90; i++)
                if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] != '$')
                    IVinfo[now++] = i;
        }
        else if (state == 1)
        {
            for (int i = 0; i < 90; i++)
                if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] == 'W')
                    IVinfo[now++] = i;
        }
        else if (state == 2)
        {
            for (int i = 0; i < 90; i++)
                if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] == 'D')
                    IVinfo[now++] = i;
        }
        else if (state == 3)
        {
            for (int i = 0; i < 90; i++)
                if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] == 'I')
                    IVinfo[now++] = i;
        }
        MaxPage = (now - 1) / 15 + 1;
    }

    private void ShowInvntory()
    {
        for (int i = 0; i < 15; i++)
        {
            if (IVinfo[i + (NowPage - 1) * 15] == -1)
            {
                IV[i].overrideSprite = ItemInfo.GetInstance.GetItemInfo("$").ItemImage;
                IV_Text[i].text = "";
            }
            else
            {
                string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[IVinfo[i + (NowPage - 1) * 15]].Split(new char[] { ':' });
                IV[i].overrideSprite = ItemInfo.GetInstance.GetItemInfo(p[0]).ItemImage;
                if (p[0][0] == 'I')
                    IV_Text[i].text = p[1];
                else
                    IV_Text[i].text = "";
            }
        }
    }

    private void InitIVinfo()
    {
        for (int i = 0; i < 90; i++)
            IVinfo[i] = -1;
    }

    public void clickAll()
    {
        ClickCancle();
        NowState = 0;

        All_Btn.image.overrideSprite = Menu_Btn1;
        Weapon_Btn.image.overrideSprite = Menu_Btn2;
        Defence_Btn.image.overrideSprite = Menu_Btn2;
        Item_Btn.image.overrideSprite = Menu_Btn2;

        NowPage = 1;
        InitIVinfo();
        AlginIVinfo(NowState);
        Page.text = NowPage.ToString() + " / " + MaxPage.ToString();

        ShowInvntory();
    }

    public void clickWeapon()
    {
        ClickCancle();
        NowState = 1;

        All_Btn.image.overrideSprite = Menu_Btn2;
        Weapon_Btn.image.overrideSprite = Menu_Btn1;
        Defence_Btn.image.overrideSprite = Menu_Btn2;
        Item_Btn.image.overrideSprite = Menu_Btn2;

        NowPage = 1;
        InitIVinfo();
        AlginIVinfo(NowState);
        Page.text = NowPage.ToString() + " / " + MaxPage.ToString();

        ShowInvntory();
    }

    public void clickDefence()
    {
        ClickCancle();
        NowState = 2;

        All_Btn.image.overrideSprite = Menu_Btn2;
        Weapon_Btn.image.overrideSprite = Menu_Btn2;
        Defence_Btn.image.overrideSprite = Menu_Btn1;
        Item_Btn.image.overrideSprite = Menu_Btn2;

        NowPage = 1;
        InitIVinfo();
        AlginIVinfo(NowState);
        Page.text = NowPage.ToString() + " / " + MaxPage.ToString();

        ShowInvntory();
    }

    public void clickItem()
    {
        ClickCancle();

        NowState = 3;

        All_Btn.image.overrideSprite = Menu_Btn2;
        Weapon_Btn.image.overrideSprite = Menu_Btn2;
        Defence_Btn.image.overrideSprite = Menu_Btn2;
        Item_Btn.image.overrideSprite = Menu_Btn1;

        NowPage = 1;
        InitIVinfo();
        AlginIVinfo(NowState);
        Page.text = NowPage.ToString() + " / " + MaxPage.ToString();

        ShowInvntory();
    }

    public void ClickAtiveIV(int index)
    {
        if (IVinfo[index + (NowPage - 1) * 15] == -1)
            return;
        if (SelectIV == index)
        {
            ClickCancle();
            return;
        }
        if(SelectIV != -1)
            IV_Btn[SelectIV].overrideSprite = Resources.Load<Sprite>("Item_Panel");
        SelectIV = index;
        IV_Btn[SelectIV].overrideSprite = Resources.Load<Sprite>("ItemSelect_Panel");
                
        string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[IVinfo[SelectIV + (NowPage - 1) * 15]].Split(new char[] { ':' });

        Name.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Name;
        ItemImage.overrideSprite = ItemInfo.GetInstance.GetItemInfo(p[0]).ItemImage;
        info.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Information;
        ItemValue.text = "";
        

        if (p[0][0] == 'I')
        {
            foreach (RectTransform cts in ChildAbility)
                cts.gameObject.SetActive(false);

            ItemImage.GetComponentInChildren<Text>().text = p[1];            
        }
        else
        {
            foreach (RectTransform cts in ChildAbility)
                cts.gameObject.SetActive(true);

            ItemImage.GetComponentInChildren<Text>().text = "";

            if (ItemInfo.GetInstance.GetItemInfo(p[0]).Attribute == 1)
                Attribute.text = "불";
            else if (ItemInfo.GetInstance.GetItemInfo(p[0]).Attribute == 2)
                Attribute.text = "바람";
            else if (ItemInfo.GetInstance.GetItemInfo(p[0]).Attribute == 3)
                Attribute.text = "땅";
            else if (ItemInfo.GetInstance.GetItemInfo(p[0]).Attribute == 4)
                Attribute.text = "물";
            else if (ItemInfo.GetInstance.GetItemInfo(p[0]).Attribute == 5)
                Attribute.text = "빛";

            Health.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Health.ToString();
            Attack.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Attack.ToString();
            Defence.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Defence.ToString();
        }
    }

    public void SellItem()
    {
        if (SelectIV != -1)
        {
            string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[IVinfo[SelectIV + (NowPage - 1) * 15]].Split(new char[] { ':' });

            ItemInfo.ItemInformation itemInfo = ItemInfo.GetInstance.GetItemInfo(p[0]);
            NowMaxValue = Convert.ToInt32(p[1]);

            if (p[0][0] == 'I')
            {
                foreach (RectTransform cts in ChildBuyItem)
                    cts.gameObject.SetActive(true);

                Transform Box = BuyItemPanel.transform.FindChild("Box");
                NowValue = 1;
                              
                Box.FindChild("Name").GetComponent<Text>().text = itemInfo.Name;
                Box.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Coin");
                Box.FindChild("Cost_Text").GetComponent<Text>().text = itemInfo.sell.ToString("#,##0");

                Box.FindChild("Value").FindChild("Text").GetComponent<Text>().text = NowValue.ToString();

                Box.FindChild("Ok").FindChild("Text").GetComponent<Text>().text = "판 매";

                Box.FindChild("Value").FindChild("Foward").GetComponent<Button>().onClick.RemoveAllListeners();
                Box.FindChild("Value").FindChild("Foward2").GetComponent<Button>().onClick.RemoveAllListeners();
                Box.FindChild("Value").FindChild("Back").GetComponent<Button>().onClick.RemoveAllListeners();
                Box.FindChild("Value").FindChild("Back2").GetComponent<Button>().onClick.RemoveAllListeners();
                Box.FindChild("Ok").GetComponent<Button>().onClick.RemoveAllListeners();
                Box.FindChild("Cancle").GetComponent<Button>().onClick.RemoveAllListeners();

                Box.FindChild("Value").FindChild("Foward").GetComponent<Button>().onClick.AddListener(() => { ChanageValue(itemInfo, 1); });
                Box.FindChild("Value").FindChild("Foward2").GetComponent<Button>().onClick.AddListener(() => { ChanageValue(itemInfo, 10); });
                Box.FindChild("Value").FindChild("Back").GetComponent<Button>().onClick.AddListener(() => { ChanageValue(itemInfo, -1); });
                Box.FindChild("Value").FindChild("Back2").GetComponent<Button>().onClick.AddListener(() => { ChanageValue(itemInfo, -10); });
                Box.FindChild("Ok").GetComponent<Button>().onClick.AddListener(() => { SellItem_Btn(itemInfo); });
                Box.FindChild("Cancle").GetComponent<Button>().onClick.AddListener(() => { Cancle_Btn(); });
            }
            else 
            {
                foreach (RectTransform cts in ChildBuy)
                    cts.gameObject.SetActive(true);

                Transform Box = BuyPanel.transform.FindChild("Box");
                NowValue = 1;

                Box.FindChild("Name").GetComponent<Text>().text = itemInfo.Name;
                Box.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Coin");
                Box.FindChild("Cost_Text").GetComponent<Text>().text = itemInfo.sell.ToString("#,##0");

                Box.FindChild("Ok").FindChild("Text").GetComponent<Text>().text = "판 매";

                Box.FindChild("Ok").GetComponent<Button>().onClick.RemoveAllListeners();
                Box.FindChild("Cancle").GetComponent<Button>().onClick.RemoveAllListeners();

                Box.FindChild("Ok").GetComponent<Button>().onClick.AddListener(() => { SellItem_Btn(itemInfo); });
                Box.FindChild("Cancle").GetComponent<Button>().onClick.AddListener(() => { Cancle_Btn(); });
            }
        }
        else
        {
            foreach (RectTransform cts in ChildWarining)
                cts.gameObject.SetActive(true);

            WarningPanel.transform.FindChild("Box").FindChild("Text").GetComponent<Text>().text = "아이템이 선택되지 않았습니다.";
        }
    }
        
    private void SellItem_Btn(ItemInfo.ItemInformation itemInfo)
    {
        if (itemInfo.Code[0] == 'I')
        {
            if (NowMaxValue == NowValue)
                Cloud_Mgr.GetInstance.UserData.HoldItem[IVinfo[SelectIV + (NowPage - 1) * 15]] = "$:0";
            else
                Cloud_Mgr.GetInstance.UserData.HoldItem[IVinfo[SelectIV + (NowPage - 1) * 15]] = itemInfo.Code + ":" + (NowMaxValue - NowValue).ToString();
        }
        else
        {
            Cloud_Mgr.GetInstance.UserData.HoldItem[IVinfo[SelectIV + (NowPage - 1) * 15]] = "$:0";
        }
        Cloud_Mgr.GetInstance.UserData.Coin += NowValue * itemInfo.sell;
        ClickCancle();
        Cancle_Btn();        
        InitIVinfo();
        AlginIVinfo(NowState);
        ShowInvntory();

        Cloud_Mgr.GetInstance.SaveToCloud();
    }

    private void ChanageValue(ItemInfo.ItemInformation itemInfo, int num)
    {
        NowValue += num;

        if (NowValue > NowMaxValue)
            NowValue = 1;
        if (NowValue < 1)
            NowValue = NowMaxValue;
        
        Transform Box = BuyItemPanel.transform.FindChild("Box");
        Box.FindChild("Value").FindChild("Text").GetComponent<Text>().text = NowValue.ToString();
        Box.FindChild("Cost_Text").GetComponent<Text>().text = (itemInfo.sell * NowValue).ToString("#,##0");
    }

    private void ClickCancle()
    {
        Name.text = "";
        ItemImage.overrideSprite = ItemInfo.GetInstance.GetItemInfo("$").ItemImage;
        info.text = "";
        ItemValue.text = "";

        foreach (RectTransform cts in ChildAbility)
            cts.gameObject.SetActive(false);

        if (SelectIV != -1)
            IV_Btn[SelectIV].overrideSprite = Resources.Load<Sprite>("Item_Panel");
        SelectIV = -1;
    }

    private void Cancle_Btn()
    {
        foreach (RectTransform cts in ChildBuyItem)
            cts.gameObject.SetActive(false);
        foreach (RectTransform cts in ChildBuy)
            cts.gameObject.SetActive(false);
    }

    public void ClickBackPage()
    {
        if (NowPage == 1)
            return;
        
        NowPage--;
        Page.text = ToString() + " / " + MaxPage.ToString();

        Cancle_Btn();
        ClickCancle();
        ShowInvntory();
    }

    public void ClickFowardPage()
    {
        if (NowPage == MaxPage)
            return;
        
        NowPage++;
        Page.text = NowPage.ToString() + " / " + MaxPage.ToString();

        Cancle_Btn();
        ClickCancle();
        ShowInvntory();
    }

    public void ClickSellPanel()
    {
        foreach (RectTransform cts in ChildSellPanel)
            cts.gameObject.SetActive(true);
        clickAll();
    }

    public void ClickBuyPanel()
    {
        foreach (RectTransform cts in ChildSellPanel)
            cts.gameObject.SetActive(false);
    }
}
