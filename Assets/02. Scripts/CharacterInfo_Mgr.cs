using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterInfo_Mgr : MonoBehaviour {
    public Image YeongHui;
    public Image Dragon;
    public Image Weapon;
    public Image Defencer;
    public Text Health;
    public Text Attack;
    public Text Defence;
    public Button YeongHui_Btn;
    public Button Dragon_Btn;

    private Sprite Menu_Btn1;
    private Sprite Menu_Btn2;

    // Use this for initialization
    void Start ()
    {
        Menu_Btn1 = Resources.Load<Sprite>("Menu_Btn1");
        Menu_Btn2 = Resources.Load<Sprite>("Menu_Btn2");

        clickYeongHui();
    }

    void Update()
    {        
        if (InventoryInformation_Mgr.GetInstance.ChangeCharacterInfo)
        {
            if(InventoryInformation_Mgr.GetInstance.nowCharacter == 1)
                clickYeongHui();
            else if (InventoryInformation_Mgr.GetInstance.nowCharacter == 2)
                clickDragon();
            InventoryInformation_Mgr.GetInstance.ChangeCharacterInfo = false;
        }
    }

    public void clickYeongHui()
    {
        InventoryInformation_Mgr.GetInstance.nowCharacter = 1;

        YeongHui_Btn.image.overrideSprite = Menu_Btn1;
        Dragon_Btn.image.overrideSprite = Menu_Btn2;
        YeongHui.enabled = true;
        Dragon.enabled = false;

        Health.text = CharacterAbility.GetInstance.yeonghui.Health.ToString();
        Attack.text = CharacterAbility.GetInstance.yeonghui.Attack.ToString();
        Defence.text = CharacterAbility.GetInstance.yeonghui.Defence.ToString();

        if (Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon == "$")
            Weapon.overrideSprite = Resources.Load<Sprite>("Notting");
        else
            Weapon.overrideSprite = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).ItemImage;
        if(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence == "$")
            Defencer.overrideSprite = Resources.Load<Sprite>("Notting");
        else
            Defencer.overrideSprite = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).ItemImage;
    }

    public void clickDragon()
    {
        InventoryInformation_Mgr.GetInstance.nowCharacter = 2;

        YeongHui_Btn.image.overrideSprite = Menu_Btn2;
        Dragon_Btn.image.overrideSprite = Menu_Btn1;
        YeongHui.enabled = false;
        Dragon.enabled = true;

        Health.text = CharacterAbility.GetInstance.dragon.Health.ToString();
        Attack.text = CharacterAbility.GetInstance.dragon.Attack.ToString();
        Defence.text = CharacterAbility.GetInstance.dragon.Defence.ToString();

        if (Cloud_Mgr.GetInstance.UserData.DragonWeapon == "$")
            Weapon.overrideSprite = Resources.Load<Sprite>("Notting");
        else
            Weapon.overrideSprite = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).ItemImage;
        if (Cloud_Mgr.GetInstance.UserData.DragonDefence == "$")
            Defencer.overrideSprite = Resources.Load<Sprite>("Notting");
        else
            Defencer.overrideSprite = ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).ItemImage;
    }
}
