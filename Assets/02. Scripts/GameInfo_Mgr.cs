using UnityEngine;
using System;

public class GameInfo_Mgr : Singleton<GameInfo_Mgr>
{
    public int exp;
    public int coin;
    public string[] item = new string[3];
    public bool CharacterState;
    public bool YeongHuiDir;
    public bool DragonDir;
    public bool DirChange;
    public int NowStage;
    public bool Pause;
    public bool Attack_YeongHui;
    public bool Attack_Dragon;
    public int usingItem;
    public bool Fail;
    public bool Success;
    public int Star;
    public Vector3 dir;
    public float Speed_X;
    public float Speed_Y;
    public bool Obstacle;
    public bool contact_Obstacle;
    public float MapSize;
    public bool Success_Processing;

    private float MoveSpeed = 7f; 
    
    void Start()
    {
        NowStage = 0;
        exp = 0;
        coin = 0;
        for(int i=0;i<3;i++)
            item[i] = "$:0";
        Attack_YeongHui = false;
        Attack_Dragon = false;
        usingItem = -1;
        Speed_X = 0.0f;
        Speed_Y = 0.0f;
    }

    void Update()
    {        
        if(usingItem != -1)
        {
            if(Cloud_Mgr.GetInstance.UserData.ItemSlot[usingItem] != -1)
            {             
                string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[Cloud_Mgr.GetInstance.UserData.ItemSlot[usingItem]].Split(new char[] { ':' });

                switch(p[0])
                {
                    case "IU01":
                        if (CharacterState)
                        {
                            CharacterAbility.GetInstance.dragon.NowHealth += 100;
                            if (CharacterAbility.GetInstance.dragon.NowHealth > CharacterAbility.GetInstance.dragon.Health)
                                CharacterAbility.GetInstance.dragon.NowHealth = CharacterAbility.GetInstance.dragon.Health;
                        }
                        else
                        {
                            CharacterAbility.GetInstance.yeonghui.NowHealth += 100;
                            if (CharacterAbility.GetInstance.yeonghui.NowHealth > CharacterAbility.GetInstance.yeonghui.Health)
                                CharacterAbility.GetInstance.yeonghui.NowHealth = CharacterAbility.GetInstance.yeonghui.Health;
                        }
                        break;
                    case "IU02":
                        if (CharacterState)
                        {
                            CharacterAbility.GetInstance.dragon.NowHealth += 500;
                            if (CharacterAbility.GetInstance.dragon.NowHealth > CharacterAbility.GetInstance.dragon.Health)
                                CharacterAbility.GetInstance.dragon.NowHealth = CharacterAbility.GetInstance.dragon.Health;
                        }
                        else
                        {
                            CharacterAbility.GetInstance.yeonghui.NowHealth += 500;
                            if (CharacterAbility.GetInstance.yeonghui.NowHealth > CharacterAbility.GetInstance.yeonghui.Health)
                                CharacterAbility.GetInstance.yeonghui.NowHealth = CharacterAbility.GetInstance.yeonghui.Health;
                        }
                        break;
                    case "IU03":
                        if (CharacterState)
                        {
                            CharacterAbility.GetInstance.dragon.NowHealth += 1000;
                            if (CharacterAbility.GetInstance.dragon.NowHealth > CharacterAbility.GetInstance.dragon.Health)
                                CharacterAbility.GetInstance.dragon.NowHealth = CharacterAbility.GetInstance.dragon.Health;
                        }
                        else
                        {
                            CharacterAbility.GetInstance.yeonghui.NowHealth += 1000;
                            if (CharacterAbility.GetInstance.yeonghui.NowHealth > CharacterAbility.GetInstance.yeonghui.Health)
                                CharacterAbility.GetInstance.yeonghui.NowHealth = CharacterAbility.GetInstance.yeonghui.Health;
                        }
                        break;
                    case "IU04":
                        if (CharacterState)
                            CharacterAbility.GetInstance.dragon.poison = false;
                        else
                            CharacterAbility.GetInstance.yeonghui.poison = false;
                        break;
                    case "IU05":
                        if (CharacterState)
                            CharacterAbility.GetInstance.dragon.burn = false;
                        else
                            CharacterAbility.GetInstance.yeonghui.burn = false;
                        break;
                    case "IU06":
                        if (CharacterState)
                            CharacterAbility.GetInstance.dragon.paralysis = false;
                        else
                            CharacterAbility.GetInstance.yeonghui.paralysis = false;
                        break;
                    case "IU07":
                        if (CharacterState)
                            CharacterAbility.GetInstance.dragon.bleeding = false;
                        else
                            CharacterAbility.GetInstance.yeonghui.bleeding = false;
                        break;
                    case "IU08":
                        if (CharacterState)
                            CharacterAbility.GetInstance.dragon.curse = false;
                        else
                            CharacterAbility.GetInstance.yeonghui.curse = false;
                        break;
                }         

                int value = Convert.ToInt32(p[1]) - 1;

                if (value == 0)
                {  
                    Cloud_Mgr.GetInstance.UserData.HoldItem[Cloud_Mgr.GetInstance.UserData.ItemSlot[usingItem]] = "$:0";
                    Cloud_Mgr.GetInstance.UserData.ItemSlot[usingItem] = -1;
                }
                else
                {
                    Cloud_Mgr.GetInstance.UserData.HoldItem[Cloud_Mgr.GetInstance.UserData.ItemSlot[usingItem]] = p[0] + ':' + value.ToString();
                }

                Cloud_Mgr.GetInstance.SaveToCloud();
                usingItem = -1;
            }            
        }

        if(Obstacle)
        {
            if(contact_Obstacle)
            {

            }
            Obstacle = false;
        }

        if (CharacterState && !CharacterAbility.GetInstance.dragon.paralysis ||
            !CharacterState && !CharacterAbility.GetInstance.yeonghui.paralysis)
        {
            if (dir.x < -130)
            {
                if (CharacterState)
                {
                    if (CharacterAbility.GetInstance.dragon.curse)
                    {
                        Speed_X = MoveSpeed;
                        if (DragonDir)
                            DirChange = true;
                        DragonDir = false;
                    }
                    else
                    {
                        Speed_X = MoveSpeed * -1;
                        if (!DragonDir)
                            DirChange = true;
                        DragonDir = true;
                    }
                }
                else
                {
                    if (CharacterAbility.GetInstance.yeonghui.curse)
                    {
                        Speed_X = MoveSpeed;
                        if (YeongHuiDir)
                            DirChange = true;
                        YeongHuiDir = false;
                    }
                    else
                    {
                        Speed_X = MoveSpeed * -1;
                        if (!YeongHuiDir)
                            DirChange = true;
                        YeongHuiDir = true;
                    }
                }
            }
            else if (dir.x > 130)
            {
                if (CharacterState)
                {
                    if (CharacterAbility.GetInstance.dragon.curse)
                    {
                        Speed_X = MoveSpeed * -1;
                        if (!DragonDir)
                            DirChange = true;
                        DragonDir = true;
                    }
                    else
                    {
                        Speed_X = MoveSpeed;
                        if (DragonDir)
                            DirChange = true;
                        DragonDir = false;
                    }

                }
                else
                {
                    if (CharacterAbility.GetInstance.yeonghui.curse)
                    {
                        Speed_X = MoveSpeed * -1;
                        if (!YeongHuiDir)
                            DirChange = true;
                        YeongHuiDir = true;
                    }
                    else
                    {
                        Speed_X = MoveSpeed;
                        if (YeongHuiDir)
                            DirChange = true;
                        YeongHuiDir = false;
                    }
                }
            }
            else
                Speed_X = 0f;

            if (dir.y < -130)
            {
                if (CharacterState)
                {
                    if (CharacterAbility.GetInstance.dragon.curse)
                        Speed_Y = MoveSpeed;
                    else
                        Speed_Y = MoveSpeed * -1;
                }
                else
                {
                    if (CharacterAbility.GetInstance.yeonghui.curse)
                        Speed_Y = MoveSpeed;
                    else
                        Speed_Y = MoveSpeed * -1;
                }                    
            }
            else if (dir.y > 130)
            {
                if (CharacterState)
                {
                    if (CharacterAbility.GetInstance.dragon.curse)
                        Speed_Y = MoveSpeed * -1;
                    else
                        Speed_Y = MoveSpeed;
                }
                else
                {
                    if (CharacterAbility.GetInstance.yeonghui.curse)
                        Speed_Y = MoveSpeed * -1;
                    else
                        Speed_Y = MoveSpeed;
                }
            }               
            else
                Speed_Y = 0f;
        }
        else
        {
            Speed_X = 0f;
            Speed_Y = 0f;
        }
    }

    public void InitGame(float size, int c, int e)
    {
        Fail = false;
        Success = false;
        Success_Processing = false;
        Star = 1;
        CharacterState = false;
        Pause = false;
        Obstacle = false;
        contact_Obstacle = false;
        YeongHuiDir = false;
        DragonDir = false;
        MapSize = size;
        DirChange = false;
        coin = c;
        exp = e;        
    }
}
