using UnityEngine;
using System.Collections;

public class CharacterAbility : Singleton<CharacterAbility>
{
    public class Character
    {
        public int Health;
        public int Attack;
        public int Defence;
        public int NowHealth;
        public bool poison;
        public bool burn;
        public bool paralysis;
        public bool bleeding;
        public bool curse;
        public float poison_time;
        public float burn_time;
        public float paralysis_time;
        public float bleeding_time;
        public float curse_time;

        public Character()
        {
            Health = 0;
            Attack = 0;
            Defence = 0;
        }

        public void InitState()
        {
            NowHealth = Health;
            poison = false;
            burn = false;
            paralysis = false;
            bleeding = false;
            curse = false;
            poison_time = 0;
            burn_time = 0;
            paralysis_time = 0;
            bleeding_time = 0;
            curse_time = 0;
        }
    }   

    public bool FinishLoad = false; //장비 장착해제시 true, 레벨업시 true
    public Character dragon = new Character();
    public Character yeonghui = new Character();
    
    // Update is called once per frame
    void Update ()
    {
        if(FinishLoad)
        {
            //레벨업 부분 추가해야함


            //데미지 수치 계산법 : 내공격 * 상대 방어구에 대한 속성치(약 : 80% 같음 : 100% 강 :120% / 무속성의 경우 같은 무속 100% 다른속성 120%) * (상대 방어력/3000+상대 방어력) * 상태이상
            yeonghui.Health = Cloud_Mgr.GetInstance.UserData.LV * 150 
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Health
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Health; 
            yeonghui.Attack = Cloud_Mgr.GetInstance.UserData.LV * 50 
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Attack
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Attack;
            yeonghui.Defence = Cloud_Mgr.GetInstance.UserData.LV * 30 
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon).Defence
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.YeonghuiDefence).Defence; 

            dragon.Health = Cloud_Mgr.GetInstance.UserData.LV * 200 
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Health
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Health;
            dragon.Attack = Cloud_Mgr.GetInstance.UserData.LV * 30 
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Attack
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Attack; 
            dragon.Defence = Cloud_Mgr.GetInstance.UserData.LV * 50 
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonWeapon).Defence
                + ItemInfo.GetInstance.GetItemInfo(Cloud_Mgr.GetInstance.UserData.DragonDefence).Defence;       

            FinishLoad = false;
        }
       
        if (yeonghui.poison_time > 0 && yeonghui.poison)
        {
            float ExTime = yeonghui.poison_time;
            yeonghui.poison_time -= Time.deltaTime;
            if((int)ExTime!= (int)yeonghui.poison_time)
                yeonghui.NowHealth -= (int)((float)yeonghui.Health / 100 * 3);
        }
        else
            yeonghui.poison = false;
        if (yeonghui.burn_time > 0 && yeonghui.burn)
            yeonghui.burn_time -= Time.deltaTime;
        else
            yeonghui.burn = false;
        if (yeonghui.paralysis_time > 0 && yeonghui.paralysis)
            yeonghui.paralysis_time -= Time.deltaTime;
        else
            yeonghui.paralysis = false;
        if (yeonghui.bleeding_time > 0 && yeonghui.bleeding)
        {
            float ExTime = yeonghui.bleeding_time;
            yeonghui.bleeding_time -= Time.deltaTime;
            if ((int)ExTime != (int)yeonghui.bleeding_time)
                yeonghui.NowHealth -= (int)((float)yeonghui.Health / 100 * 3);
        }
        else
            yeonghui.bleeding = false;
        if (yeonghui.curse_time > 0 && yeonghui.curse)
            yeonghui.curse_time -= Time.deltaTime;
        else
            yeonghui.curse = false;

        if (dragon.poison_time > 0 && dragon.poison)
        {
            float ExTime = dragon.poison_time;
            dragon.poison_time -= Time.deltaTime;
            if ((int)ExTime != (int)dragon.poison_time)
                dragon.NowHealth -= (int)((float)dragon.Health / 100 * 3);
        }
        else
            dragon.poison = false;
        if (dragon.burn_time > 0 && dragon.burn)
            dragon.burn_time -= Time.deltaTime;
        else
            dragon.burn = false;
        if (dragon.paralysis_time > 0 && dragon.paralysis)
            dragon.paralysis_time -= Time.deltaTime;
        else
            dragon.paralysis = false;
        if (dragon.bleeding_time > 0 && dragon.bleeding)
        {
            float ExTime = dragon.bleeding_time;
            dragon.bleeding_time -= Time.deltaTime;
            if ((int)ExTime != (int)dragon.bleeding_time)
                dragon.NowHealth -= (int)((float)dragon.Health / 100 * 3);
        }
        else
            dragon.bleeding = false;
        if (dragon.curse_time > 0 && dragon.curse)
            dragon.curse_time -= Time.deltaTime;
        else
            dragon.curse = false;
    }
}
