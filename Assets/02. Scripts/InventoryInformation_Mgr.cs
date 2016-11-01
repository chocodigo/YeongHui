using UnityEngine;
using System.Collections;

public class InventoryInformation_Mgr : Singleton<InventoryInformation_Mgr>
{
    public int nowCharacter;
    public int nowInventory;    
    public int NowPage;
    public bool ChangeCharacterInfo;
    public bool ChangeInventoryInfo;
    public int[] IVinfo;

    void Start()
    {
        nowCharacter = 1;
        nowInventory = 1;
        NowPage = 1;
        ChangeCharacterInfo = false;
        ChangeInventoryInfo = false;

        IVinfo = new int[90];
    }

    public void InitIVinfo()
    {
        for (int i = 0; i < 90; i++)
            IVinfo[i] = -1;
    }

    public int FindEmptyHold()
    {
        for (int i = 0; i < 90; i++)
        {
            if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] == '$')
                return i;
        }
        return 90;
    }
}
