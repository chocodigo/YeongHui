using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComposItemInfo_Mgr : MonoBehaviour
{
    public Image Inventory;
    public Image Result;

    private Transform[] ChildTs;
    private int SelectIV;
    private Image[] IV_Btn;
    private Text Name;
    private Image ItemImage;
    private Text Info;
    private GameObject WeaponDefence;
    private Text Attribute;
    private Text Health;
    private Text Attack;
    private Text Defence;
    private Transform Ok;
    private Transform Cancle;
    private bool inventoryState;
    private bool moveCancle;
    private bool CheckReclick;

    void Start()
    {
        ChildTs = Inventory.GetComponentsInChildren<RectTransform>();

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

        WeaponDefence = Result.transform.FindChild("WeaponDefence").gameObject;

        Name = Result.transform.FindChild("Name").GetComponent<Text>();
        ItemImage = Result.transform.FindChild("ItemImage").FindChild("Image").GetComponent<Image>();
        Info = Result.transform.FindChild("Info").GetComponent<Text>();
        Attribute = WeaponDefence.transform.FindChild("Attribute").GetComponent<Text>();
        Health = WeaponDefence.transform.FindChild("Health").GetComponent<Text>();
        Attack = WeaponDefence.transform.FindChild("Attack").GetComponent<Text>();
        Defence = WeaponDefence.transform.FindChild("Defence").GetComponent<Text>();

        Ok = Result.transform.FindChild("OK").GetComponent<RectTransform>();
        Cancle = Result.transform.FindChild("Cancle").GetComponent<RectTransform>();

        SelectIV = -1;
        inventoryState = false;
        moveCancle = false;
        CheckReclick = false;
    }

    public void ClickComposeReinforce()
    {       
        foreach (Image IVs in IV_Btn)
            IVs.overrideSprite = Resources.Load<Sprite>("Item_Panel");

        if (ComposForm_Mgr.GetInstance.isCompose)
        {
            if(ComposForm_Mgr.GetInstance.NowCompose == -1 || CheckReclick)
            {
                SelectIV = -1;

                Name.text = "합성 재료";
                ItemImage.overrideSprite = Resources.Load<Sprite>("Notting");
                Info.text = "";

                WeaponDefence.SetActive(false);
                Ok.gameObject.SetActive(false);
                                
                if (!moveCancle)
                    Cancle.transform.GetComponent<RectTransform>().localPosition -= new Vector3(194, 0, 0);
                moveCancle = true;
                CheckReclick = false;
                inventoryState = false;
            }
            else
            {
                SelectIV = ComposForm_Mgr.GetInstance.NowComposeSelectIV;
                ComposForm_Mgr.GetInstance.NowPage = ComposForm_Mgr.GetInstance.NowComposePage;

                Ok.gameObject.SetActive(true);
                Ok.transform.FindChild("Text").GetComponent<Text>().text = "해제";
                IV_Btn[SelectIV].overrideSprite = Resources.Load<Sprite>("ItemSelect_Panel");

                ItemInfo.ItemInformation ItemInformation = ItemInfo.GetInstance.GetItemInfo(ItemInfo.GetInstance.GetComposeInfo(ComposForm_Mgr.GetInstance.NowCompose).result);
                Name.text = ItemInformation.Name;
                ItemImage.overrideSprite = ItemInformation.ItemImage;
                Info.text = ItemInformation.Information;

                WeaponDefence.SetActive(true);

                if (ItemInformation.Attribute == 1)
                    Attribute.text = "불";
                else if (ItemInformation.Attribute == 2)
                    Attribute.text = "바람";
                else if (ItemInformation.Attribute == 3)
                    Attribute.text = "땅";
                else if (ItemInformation.Attribute == 4)
                    Attribute.text = "물";
                else if (ItemInformation.Attribute == 5)
                    Attribute.text = "빛";

                Health.text = ItemInformation.Health.ToString();
                Attack.text = ItemInformation.Attack.ToString();
                Defence.text = ItemInformation.Defence.ToString();

                if (moveCancle)
                    Cancle.transform.GetComponent<RectTransform>().localPosition += new Vector3(194, 0, 0);
                moveCancle = false;
                inventoryState = true;
            }
        }
        else
        {
            if(ComposForm_Mgr.GetInstance.NowReinforce == -1 || CheckReclick)
            {
                SelectIV = -1;

                Name.text = "강화석";
                ItemImage.overrideSprite = Resources.Load<Sprite>("Notting");
                Info.text = "";

                WeaponDefence.SetActive(false);
                Ok.gameObject.SetActive(false);

                if (!moveCancle)
                    Cancle.transform.GetComponent<RectTransform>().localPosition -= new Vector3(194, 0, 0);
                moveCancle = true;
                CheckReclick = false;
                inventoryState = false;
            }
            else
            {
                SelectIV = ComposForm_Mgr.GetInstance.NowReinforceSelectIV;
                ComposForm_Mgr.GetInstance.NowPage = ComposForm_Mgr.GetInstance.NowReinforcePage;

                Ok.gameObject.SetActive(true);
                Ok.transform.FindChild("Text").GetComponent<Text>().text = "해제";
                IV_Btn[SelectIV].overrideSprite = Resources.Load<Sprite>("ItemSelect_Panel");

                string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[ComposForm_Mgr.GetInstance.NowReinforce].Split(new char[] { ':' });
                Name.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Name;
                ItemImage.overrideSprite = ItemInfo.GetInstance.GetItemInfo(p[0]).ItemImage;
                Info.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Information;

                WeaponDefence.SetActive(false);

                if (moveCancle)
                    Cancle.transform.GetComponent<RectTransform>().localPosition += new Vector3(194, 0, 0);
                moveCancle = false;
                inventoryState = true;
            }
        }
        ComposForm_Mgr.GetInstance.ChangeIV = true;
    }

    public void ClickAtiveIV(int index)
    {      
        if (ComposForm_Mgr.GetInstance.IVinfo[index + (ComposForm_Mgr.GetInstance.NowPage - 1) * 15] == -1)
            return;

        if(ComposForm_Mgr.GetInstance.isCompose)
        {
            if (ComposForm_Mgr.GetInstance.NowComposeSelectIV == index && ComposForm_Mgr.GetInstance.NowComposePage == ComposForm_Mgr.GetInstance.NowPage && SelectIV == index)
            {
                CheckReclick = true;
                ClickComposeReinforce();
                return;
            }
            else if(ComposForm_Mgr.GetInstance.NowComposeSelectIV == index && ComposForm_Mgr.GetInstance.NowComposePage == ComposForm_Mgr.GetInstance.NowPage && SelectIV != index)
            {
                ClickComposeReinforce();
                return;
            }
        }
        else
        {
            if (ComposForm_Mgr.GetInstance.NowReinforceSelectIV == index && ComposForm_Mgr.GetInstance.NowReinforcePage == ComposForm_Mgr.GetInstance.NowPage && SelectIV == index)
            {
                CheckReclick = true;
                ClickComposeReinforce();
                return;
            }
            else if(ComposForm_Mgr.GetInstance.NowReinforceSelectIV == index && ComposForm_Mgr.GetInstance.NowReinforcePage == ComposForm_Mgr.GetInstance.NowPage && SelectIV != index)
            {
                ClickComposeReinforce();
                return;
            }
        }        

        if (SelectIV == index)
        {
            CheckReclick = true;
            ClickComposeReinforce();
            return;
        }

        SelectIV = index;
        Ok.gameObject.SetActive(true);
        Ok.transform.FindChild("Text").GetComponent<Text>().text = "등록";

        for (int i = 0; i < IV_Btn.Length; i++)
        {
            if (i == SelectIV)
                IV_Btn[i].overrideSprite = Resources.Load<Sprite>("ItemSelect_Panel");
            else
                IV_Btn[i].overrideSprite = Resources.Load<Sprite>("Item_Panel");
        }

        if (ComposForm_Mgr.GetInstance.isCompose)
        {
            ItemInfo.ItemInformation info = ItemInfo.GetInstance.GetItemInfo(ItemInfo.GetInstance.GetComposeInfo(ComposForm_Mgr.GetInstance.IVinfo[SelectIV + (ComposForm_Mgr.GetInstance.NowPage - 1) * 15]).result);
            Name.text = info.Name;
            ItemImage.overrideSprite = info.ItemImage;
            Info.text = info.Information;

            WeaponDefence.SetActive(true);
            if (info.Attribute == 1)
                Attribute.text = "불";
            else if (info.Attribute == 2)
                Attribute.text = "바람";
            else if (info.Attribute == 3)
                Attribute.text = "땅";
            else if (info.Attribute == 4)
                Attribute.text = "물";
            else if (info.Attribute == 5)
                Attribute.text = "암흑";
            Health.text = info.Health.ToString();
            Attack.text = info.Attack.ToString();
            Defence.text = info.Defence.ToString();                    
        }
        else
        {
            string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[ComposForm_Mgr.GetInstance.IVinfo[SelectIV + (ComposForm_Mgr.GetInstance.NowPage - 1) * 15]].Split(new char[] { ':' });
            Name.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Name;
            ItemImage.overrideSprite = ItemInfo.GetInstance.GetItemInfo(p[0]).ItemImage;
            Info.text = ItemInfo.GetInstance.GetItemInfo(p[0]).Information;

            WeaponDefence.SetActive(false);
        }

        if (moveCancle)
            Cancle.transform.GetComponent<RectTransform>().localPosition += new Vector3(194, 0, 0);
        moveCancle = false;
        inventoryState = true;
        ComposForm_Mgr.GetInstance.ChangeIV = true;
    }

    public void ClickOK()
    {
        if (ComposForm_Mgr.GetInstance.isCompose)
        {
            if (ComposForm_Mgr.GetInstance.NowComposeSelectIV == SelectIV && ComposForm_Mgr.GetInstance.NowComposePage == ComposForm_Mgr.GetInstance.NowPage)
            {
                ComposForm_Mgr.GetInstance.NowCompose = -1;
                ComposForm_Mgr.GetInstance.NowComposeSelectIV = -1;
                ComposForm_Mgr.GetInstance.NowComposePage = 1;

                ComposForm_Mgr.GetInstance.isWeapon = true;
            }
            else
            {
                ComposForm_Mgr.GetInstance.NowCompose = ComposForm_Mgr.GetInstance.IVinfo[SelectIV + (ComposForm_Mgr.GetInstance.NowPage - 1) * 15];
                ComposForm_Mgr.GetInstance.NowComposePage = ComposForm_Mgr.GetInstance.NowPage;
                ComposForm_Mgr.GetInstance.NowComposeSelectIV = SelectIV;
                                
                if (ItemInfo.GetInstance.GetComposeInfo(ComposForm_Mgr.GetInstance.NowCompose).result[0] == 'W')
                    ComposForm_Mgr.GetInstance.isWeapon = true;
                else
                    ComposForm_Mgr.GetInstance.isWeapon = false;
            }     
        }
        else
        {
            if (ComposForm_Mgr.GetInstance.NowReinforceSelectIV == SelectIV && ComposForm_Mgr.GetInstance.NowReinforcePage == ComposForm_Mgr.GetInstance.NowPage)
            {
                ComposForm_Mgr.GetInstance.NowReinforce = -1;
                ComposForm_Mgr.GetInstance.NowReinforceSelectIV = -1;
                ComposForm_Mgr.GetInstance.NowReinforcePage = 1;
            }   
            else
            {
                ComposForm_Mgr.GetInstance.NowReinforce = ComposForm_Mgr.GetInstance.IVinfo[SelectIV + (ComposForm_Mgr.GetInstance.NowPage - 1) * 15];
                ComposForm_Mgr.GetInstance.NowReinforcePage = ComposForm_Mgr.GetInstance.NowPage;
                ComposForm_Mgr.GetInstance.NowReinforceSelectIV = SelectIV;
            }         
        }
        inventoryState = false;
        ClickCancle();
    }

    public void ClickCancle()
    {
        if (inventoryState)
        {
            CheckReclick = true;
            ClickComposeReinforce();
        }
        else
        {
            foreach (RectTransform cts in ChildTs)
                cts.gameObject.SetActive(false);
            ComposForm_Mgr.GetInstance.ChangeCompose = true;
        }                  
    }
}
