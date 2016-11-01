using UnityEngine;
using System.Collections;

public class ExitGate_Mgr : MonoBehaviour
{
    private bool Finish;

    void Start()
    {
        Finish = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Witch")
        {
            if (!Finish)
            {
                GameInfo_Mgr.GetInstance.Success_Processing = true;
                Finish = true;
            }
        }
    }
}
