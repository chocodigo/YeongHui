using UnityEngine;
using System.Collections;

public class ComposForm_Mgr : Singleton<ComposForm_Mgr>
{    
    public int NowPage;
    public int[] IVinfo;
    public bool isCompose;
    public bool isWeapon;
    public int NowCompose;
    public int NowComposePage;
    public int NowComposeSelectIV;
    public int NowReinforce;
    public int NowReinforcePage;
    public int NowReinforceSelectIV;
    public bool ChangeIV;
    public bool ChangeCompose;

    void Start()
    {        
        NowPage = 1;

        IVinfo = new int[90];

        isCompose = false;
        isWeapon = true;

        NowCompose = -1;
        NowComposePage = 1;
        NowComposeSelectIV = -1;
        NowReinforce = -1;
        NowReinforcePage = 1;
        NowReinforceSelectIV = -1;
        
        ChangeIV = false;
        ChangeCompose = true;
    }

    public void InitIVinfo()
    {
        for (int i = 0; i < 90; i++)
            IVinfo[i] = -1;
    }        
}
