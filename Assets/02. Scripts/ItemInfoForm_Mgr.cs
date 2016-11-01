using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInfoForm_Mgr : MonoBehaviour {

    public Image ItemInfoForm;
    public Image ItemImage;
    public Image Inventory;
    public GameObject Slot;
    public GameObject WeaponDefence;
    public GameObject Warnning;

    private Text Attribute;
    private Text Health;
    private Text Attack;
    private Text Defence;
    private int SelectIV;
    private Image[] IV_Btn;
    private Image[] Slot_Btn;
    private Image[] Slot_Image;
    private Text[] Slot_Text;
    private int NowSlot;
    private bool CheckIT;
    private Transform[] ChildTs;
    private bool State; //true 인벤토리에서 선택, false 장비에서 선택
    private bool SelectEquip; //true 무기 선택, false 갑옷 선택
    private bool SelectSlot;

    // Use this for initialization
    void Start () {
        State = false;
        SelectEquip = false;
        CheckIT = false;
        SelectSlot = false;
        ItemInfoForm.transform.FindChild("OK").GetComponentInChildren<Text>().text = "등록";
        ItemInfoForm.transform.FindChild("Cancle").FindChild("Text").GetComponent<Text>().text = "취소";
        ItemInfoForm.transform.FindChild("Name").GetComponent<Text>().text = "";
        ChildTs = ItemInfoForm.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildTs)
            cts.gameObject.SetActive(false);

        Attribute = WeaponDefence.transform.FindChild("Attribute").GetComponent<Text>();
        Health = WeaponDefence.transform.FindChild("Health").GetComponent<Text>();
        Attack = WeaponDefence.transform.FindChild("Attack").GetComponent<Text>();
        Defence = WeaponDefence.transform.FindChild("Defence").GetComponent<Text>();

        IV_Btn = new Image[15];
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

        Slot_Btn = new Image[4];
        Slot_Btn[0] = Slot.transform.FindChild("Slot1").GetComponent<Image>();
        Slot_Btn[1] = Slot.transform.FindChild("Slot2").GetComponent<Image>();
        Slot_Btn[2] = Slot.transform.FindChild("Slot3").GetComponent<Image>();
        Slot_Btn[3] = Slot.transform.FindChild("Slot4").GetComponent<Image>();

        Slot_Image = new Image[4];
        Slot_Image[0] = Slot.transform.FindChild("Slot1").FindChild("Item").GetComponent<Image>();
        Slot_Image[1] = Slot.transform.FindChild("Slot2").FindChild("Item").GetComponent<Image>();
        Slot_Image[2] = Slot.transform.FindChild("Slot3").FindChild("Item").GetComponent<Image>();
        Slot_Image[3] = Slot.transform.FindChild("Slot4").FindChild("Item").GetComponent<Image>();

        Slot_Text = new Text[4];
        Slot_Text[0] = Slot.transform.FindChild("Slot1").FindChild("Item").FindChild("Text").GetComponent<Text>();
        Slot_Text[1] = Slot.transform.FindChild("Slot2").FindChild("Item").FindChild("Text").GetComponent<Text>();
        Slot_Text[2] = Slot.transform.FindChild("Slot3").FindChild("Item").FindChild("Text").GetComponent<Text>();
        Slot_Text[3] = Slot.transform.FindChild("Slot4").FindChild("Item").FindChild("Text").GetComponent<Text>();

        SelectIV = -1;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (Scene_Mgr.GetInstance.bSetting)
                    Scene_Mgr.GetInstance.UnLoadSetting();
                else if (Warnning.active)
                    ClickWarnning();
                else
                    BacktoLobbyBtn();
            }           
        }
    }

    public void clickWeapon()
    {
        if (InventoryInformation_Mgr.GetInstance.nowCharacter == 1)
        {
            if (Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon == "$")
                return;
        }
        else if (InventoryInformation_Mgr.GetInstance.nowCharacter == 2)
        {
            if (Cloud_Mgr.GetInstance.UserData.DragonWeapon == "$")
                return;
        }
        State = false;
        SelectEquip = true;
        foreach (RectTransform cts in ChildTs)
            cts.gameObject.SetActive(true);
        Slot.SetActive(false);
        Warnning.SetActive(false);
        ItemInfoForm.transform.FindChild("OK").GetComponentInChildren<Text>().text = "해제";
        ItemInfoForm.transform.FindChild("Cancle").FindChild("Text").GetComponent<Text>().text = "취소";

        if (InventoryInformation_Mgr.GetInstance.nowCharacter == 1)
        {
            ItemInfoForm.transform.FindChild("Name").GetComponent<Text>().text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Name;
            ItemImage.overrideSprite = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).ItemImage;
            ItemInfoForm.transform.FindChild("Info").GetComponent<Text>().text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Information;
            
            if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Attribute == 1)
                Attribute.text = "불";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Attribute == 2)
                Attribute.text = "바람";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Attribute == 3)
                Attribute.text = "땅";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Attribute == 4)
                Attribute.text = "물";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Attribute == 5)
                Attribute.text = "빛";

            Health.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Health.ToString();
            Attack.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Attack.ToString();
            Defence.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Defence.ToString();
        }
        else if(InventoryInformation_Mgr.GetInstance.nowCharacter == 2)
        {
            ItemInfoForm.transform.FindChild("Name").GetComponent<Text>().text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Name;
            ItemImage.overrideSprite = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).ItemImage;
            ItemInfoForm.transform.FindChild("Info").GetComponent<Text>().text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Information;

            if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Attribute == 1)
                Attribute.text = "불";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Attribute == 2)
                Attribute.text = "바람";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Attribute == 3)
                Attribute.text = "땅";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Attribute == 4)
                Attribute.text = "물";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Attribute == 5)
                Attribute.text = "빛";

            Health.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Health.ToString();
            Attack.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Attack.ToString();
            Defence.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Defence.ToString();
        }
    }

    public void clickDefence()
    {
        if (InventoryInformation_Mgr.GetInstance.nowCharacter == 1)
        {
            if (Cloud_Mgr.GetInstance.UserData.YeonghuiDefence == "$")
                return;
        }
        else if (InventoryInformation_Mgr.GetInstance.nowCharacter == 2)
        {
            if (Cloud_Mgr.GetInstance.UserData.DragonDefence == "$")
                return;
        }

        State = false;
        SelectEquip = false;
        foreach (RectTransform cts in ChildTs)
            cts.gameObject.SetActive(true);
        Slot.SetActive(false);
        Warnning.SetActive(false);

        ItemInfoForm.transform.FindChild("OK").GetComponentInChildren<Text>().text = "해제";
        ItemInfoForm.transform.FindChild("Cancle").FindChild("Text").GetComponent<Text>().text = "취소";

        if (InventoryInformation_Mgr.GetInstance.nowCharacter == 1)
        {
            ItemInfoForm.transform.FindChild("Name").GetComponent<Text>().text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Name;
            ItemImage.overrideSprite = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).ItemImage;
            ItemInfoForm.transform.FindChild("Info").GetComponent<Text>().text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Information;

            if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Attribute == 1)
                Attribute.text = "불";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Attribute == 2)
                Attribute.text = "바람";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Attribute == 3)
                Attribute.text = "땅";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Attribute == 4)
                Attribute.text = "물";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Attribute == 5)
                Attribute.text = "빛";

            Health.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Health.ToString();
            Attack.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Attack.ToString();
            Defence.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Defence.ToString();
        }
        else if (InventoryInformation_Mgr.GetInstance.nowCharacter == 2)
        {
            ItemInfoForm.transform.FindChild("Name").GetComponent<Text>().text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Name;
            ItemImage.overrideSprite = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).ItemImage;
            ItemInfoForm.transform.FindChild("Info").GetComponent<Text>().text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Information;

            if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Attribute == 1)
                Attribute.text = "불";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Attribute == 2)
                Attribute.text = "바람";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Attribute == 3)
                Attribute.text = "땅";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Attribute == 4)
                Attribute.text = "물";
            else if (ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Attribute == 5)
                Attribute.text = "빛";

            Health.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Health.ToString();
            Attack.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Attack.ToString();
            Defence.text = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Defence.ToString();
        }
    }

    public void ClickAtiveIV(int index)
    {
        if (InventoryInformation_Mgr.GetInstance.IVinfo[index + (InventoryInformation_Mgr.GetInstance.NowPage - 1) * 15] == -1)
            return;
        if (SelectIV == index)
        {
            SelectSlot = false;
            ClickCancle();
            return;
        }

        SelectIV = index;

        for (int i=0;i<IV_Btn.Length;i++)
        {
            if(i == SelectIV)
                IV_Btn[i].overrideSprite = Resources.Load<Sprite>("ItemSelect_Panel");
            else
                IV_Btn[i].overrideSprite = Resources.Load<Sprite>("Item_Panel");
        }        

        if (CheckIT)
        {
            ItemInfoForm.transform.FindChild("Cancle").GetComponent<RectTransform>().localPosition += new Vector3(194, 0, 0);
            CheckIT = false;
        }       
        
        State = true;
        foreach (RectTransform cts in ChildTs)
            cts.gameObject.SetActive(true);

        Warnning.SetActive(false);        

        string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[InventoryInformation_Mgr.GetInstance.IVinfo[SelectIV + (InventoryInformation_Mgr.GetInstance.NowPage - 1) * 15]].Split(new char[] { ':' });

        ItemInfoForm.transform.FindChild("Name").GetComponent<Text>().text = ItemInfo.GetInstance.GetItemInfo(p[0]).Name;
        ItemImage.overrideSprite = ItemInfo.GetInstance.GetItemInfo(p[0]).ItemImage;
        ItemInfoForm.transform.FindChild("Info").GetComponent<Text>().text = ItemInfo.GetInstance.GetItemInfo(p[0]).Information;
        ItemInfoForm.transform.FindChild("OK").GetComponentInChildren<Text>().text = "등록";
        ItemInfoForm.transform.FindChild("Cancle").FindChild("Text").GetComponent<Text>().text = "취소";

        if (p[0][0] == 'I')
        {            
            WeaponDefence.SetActive(false);
            ItemImage.GetComponentInChildren<Text>().text = p[1];            

            if (p[0][1] == 'T')
            {
                Slot.SetActive(false);
                ItemInfoForm.transform.FindChild("OK").GetComponent<RectTransform>().gameObject.SetActive(false);
                ItemInfoForm.transform.FindChild("Cancle").GetComponent<RectTransform>().localPosition -= new Vector3(194, 0, 0);
                CheckIT = true;
            }
            else if(p[0][1] == 'U')
            {
                Slot.SetActive(true);                
                
                for (int i = 0; i < 4; i++)
                {
                    Slot_Btn[i].overrideSprite = Resources.Load<Sprite>("Item_Panel");
                    if (Cloud_Mgr.GetInstance.UserData.ItemSlot[i] == -1)
                    {
                        Slot_Image[i].overrideSprite = Resources.Load<Sprite>("Plus");
                        Slot_Text[i].text = "";
                    }
                    else
                    {
                        string[] s = Cloud_Mgr.GetInstance.UserData.HoldItem[Cloud_Mgr.GetInstance.UserData.ItemSlot[i]].Split(new char[] { ':' });
                        Slot_Image[i].overrideSprite = ItemInfo.GetInstance.GetItemInfo(s[0]).ItemImage;
                        Slot_Text[i].text = s[1];
                    }                    
                }
            }

            NowSlot = -1;
        }
        else
        {            
            Slot.SetActive(false);
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
                Attribute.text = "암흑";

            Health.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Health.ToString();
            Attack.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Attack.ToString();
            Defence.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Defence.ToString();
        }           
    }

    public void ClickOK()
    {
        if (State)
        {
            string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[InventoryInformation_Mgr.GetInstance.IVinfo[SelectIV + (InventoryInformation_Mgr.GetInstance.NowPage - 1) * 15]].Split(new char[] { ':' });

            if(p[0][0] == 'I')
            {                
                if (NowSlot == -1)
                {
                    Warnning.SetActive(true);
                    Warnning.transform.FindChild("Pannel").FindChild("Text").GetComponent<Text>().text = "슬롯이 선택되지 않았습니다.";
                    return;
                }
                else
                {
                    Cloud_Mgr.GetInstance.UserData.ItemSlot[NowSlot] = InventoryInformation_Mgr.GetInstance.IVinfo[SelectIV + (InventoryInformation_Mgr.GetInstance.NowPage - 1) * 15];
                }
            }
            else if (p[0][0] == 'W')
            {
                string temp = null;
                if (InventoryInformation_Mgr.GetInstance.nowCharacter == 1)
                {
                    temp = Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon;
                    Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon = p[0];                    
                }
                    
                else if (InventoryInformation_Mgr.GetInstance.nowCharacter == 2)
                {
                    temp = Cloud_Mgr.GetInstance.UserData.DragonWeapon;
                    Cloud_Mgr.GetInstance.UserData.DragonWeapon = p[0];
                }
                Cloud_Mgr.GetInstance.UserData.HoldItem[InventoryInformation_Mgr.GetInstance.IVinfo[SelectIV + (InventoryInformation_Mgr.GetInstance.NowPage - 1) * 15]] = temp + ":1";
            }
            else if (p[0][0] == 'D')
            {
                string temp = null;
                if (InventoryInformation_Mgr.GetInstance.nowCharacter == 1)
                {
                    temp = Cloud_Mgr.GetInstance.UserData.YeonghuiDefence;
                    Cloud_Mgr.GetInstance.UserData.YeonghuiDefence = p[0];
                }                    
                else if (InventoryInformation_Mgr.GetInstance.nowCharacter == 2)
                {
                    temp = Cloud_Mgr.GetInstance.UserData.DragonDefence;
                    Cloud_Mgr.GetInstance.UserData.DragonDefence = p[0];
                }
                Cloud_Mgr.GetInstance.UserData.HoldItem[InventoryInformation_Mgr.GetInstance.IVinfo[SelectIV + (InventoryInformation_Mgr.GetInstance.NowPage - 1) * 15]] = temp + ":1";
            }
            foreach (Image IVs in IV_Btn)
                IVs.overrideSprite = Resources.Load<Sprite>("Item_Panel");
        }
        else
        {
            int EmptyPos = InventoryInformation_Mgr.GetInstance.FindEmptyHold();

            if (EmptyPos == 90) 
            {
                Warnning.SetActive(true);
                Warnning.transform.FindChild("Pannel").FindChild("Text").GetComponent<Text>().text = "가방이 가득찼습니다.";
                return;
            }
            else
            {
                if (SelectEquip)
                {
                    if (InventoryInformation_Mgr.GetInstance.nowCharacter == 1)
                    {
                        Cloud_Mgr.GetInstance.UserData.HoldItem[EmptyPos] = Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon + ":1";
                        Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon = "$";
                    }
                    else if (InventoryInformation_Mgr.GetInstance.nowCharacter == 2)
                    {
                        Cloud_Mgr.GetInstance.UserData.HoldItem[EmptyPos] = Cloud_Mgr.GetInstance.UserData.DragonWeapon + ":1";
                        Cloud_Mgr.GetInstance.UserData.DragonWeapon = "$";
                    }
                }
                else
                {
                    if (InventoryInformation_Mgr.GetInstance.nowCharacter == 1)
                    {
                        Cloud_Mgr.GetInstance.UserData.HoldItem[EmptyPos] = Cloud_Mgr.GetInstance.UserData.YeonghuiDefence + ":1";
                        Cloud_Mgr.GetInstance.UserData.YeonghuiDefence = "$";
                    }
                    else if (InventoryInformation_Mgr.GetInstance.nowCharacter == 2)
                    {
                        Cloud_Mgr.GetInstance.UserData.HoldItem[EmptyPos] = Cloud_Mgr.GetInstance.UserData.DragonDefence + ":1";
                        Cloud_Mgr.GetInstance.UserData.DragonDefence = "$";
                    }
                }
            }
        }
        SelectSlot = false;
        CharacterAbility.GetInstance.FinishLoad = true;
        InventoryInformation_Mgr.GetInstance.ChangeCharacterInfo = true;
        InventoryInformation_Mgr.GetInstance.ChangeInventoryInfo = true;

        Cloud_Mgr.GetInstance.SaveToCloud();

        ClickCancle();
    }

    public void ClickCancle()
    {
        if(SelectSlot)
        {            
            if(Cloud_Mgr.GetInstance.UserData.ItemSlot[NowSlot] == -1)
            {
                Warnning.SetActive(true);
                Warnning.transform.FindChild("Pannel").FindChild("Text").GetComponent<Text>().text = "빈 슬롯은 해제할수 없습니다.";
                SelectSlot = false;
                Slot_Btn[NowSlot].overrideSprite = Resources.Load<Sprite>("Item_Panel");
                NowSlot = -1;
                ItemInfoForm.transform.FindChild("Cancle").FindChild("Text").GetComponent<Text>().text = "취소";          

                return;
            }
            else
            {
                Cloud_Mgr.GetInstance.UserData.ItemSlot[NowSlot] = -1;
                Cloud_Mgr.GetInstance.SaveToCloud();
            }      
        }

        foreach (Image IVs in IV_Btn)
            IVs.overrideSprite = Resources.Load<Sprite>("Item_Panel");

        if (CheckIT)
        {
            ItemInfoForm.transform.FindChild("Cancle").GetComponent<RectTransform>().localPosition += new Vector3(194, 0, 0);
            CheckIT = false;
        }
        foreach (RectTransform cts in ChildTs)
            cts.gameObject.SetActive(false);
        SelectIV = -1;
    }

    public void ClickMovePage()
    {
        SelectSlot = false;
        ClickCancle();
    }    

    public void ClickSlot(int index)
    {
        for(int i=0; i < 4; i++)
        {
            Slot_Btn[i].overrideSprite = Resources.Load<Sprite>("Item_Panel");
        }
        if (NowSlot == index)
        {
            SelectSlot = false;
            ItemInfoForm.transform.FindChild("Cancle").FindChild("Text").GetComponent<Text>().text = "취소";
            NowSlot = -1;
        }
        else
        {
            SelectSlot = true;
            ItemInfoForm.transform.FindChild("Cancle").FindChild("Text").GetComponent<Text>().text = "해제";
            NowSlot = index;
            Slot_Btn[index].overrideSprite = Resources.Load<Sprite>("ItemSelect_Panel");
        }
    }    

    public void ClickWarnning()
    {
        Warnning.SetActive(false);        
    }

    public void BacktoLobbyBtn()
    {
        StartCoroutine(Scene_Mgr.GetInstance.LoadLobby());
        Scene_Mgr.GetInstance.UnLoadInventory();
    }
}