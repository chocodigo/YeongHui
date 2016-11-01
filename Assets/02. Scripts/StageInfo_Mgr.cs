using UnityEngine;
using System.Collections;

public class StageInfo_Mgr : MonoBehaviour {
    public float MapSize;
    public int coin;
    public int exp;
    public float ClearTime;
    public string[] item_Code;
    public int[] item_MaxValue;
    
    void Start()
    {
        SelectItem();
        GameInfo_Mgr.GetInstance.InitGame(MapSize, coin, exp);
    }

    void Update()
    {        
        if (GameInfo_Mgr.GetInstance.Success_Processing) 
        {
            int star = 3;

            if (CharacterAbility.GetInstance.dragon.NowHealth == 0)
                star--;
            if(ClearTime <= 0)
                star--;

            GameInfo_Mgr.GetInstance.Star = star;

            GameInfo_Mgr.GetInstance.Success_Processing = false;
            GameInfo_Mgr.GetInstance.Success = true;
        }
        else if(!GameInfo_Mgr.GetInstance.Success)
        {
            ClearTime -= Time.deltaTime;
        }
    }

    void SelectItem()
    {
        int len = item_Code.Length;
        int[] index = new int[3];
        
        for (int i = 0; i < 3; i++)   
        {
            index[i] = (int)Random.Range(0, len+1);

            for (int j = 0; j < i; j++) 
            {    
                if (index[i] == index[j])
                {
                    i--;
                }
            }
        }

        int notting = 0;
        int item_index = 0;

        for(int i=0;i<3- notting;i++)
        {
            if(index[item_index] == len)
            {
                GameInfo_Mgr.GetInstance.item[2 - notting] = "$:0";
                notting++;
                i--;
            }
            else
            {
                int value = (int)Random.Range(0, item_MaxValue[index[item_index]]+1);
                if (value == 0)
                {
                    GameInfo_Mgr.GetInstance.item[2 - notting] = "$:0";
                    notting++;
                }
                else
                    GameInfo_Mgr.GetInstance.item[i] = item_Code[index[item_index]] + ":" + value.ToString();
            }
            item_index++;
        }        
    }
}
