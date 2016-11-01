using UnityEngine;
using System.Collections;

public class GameCamera_Mgr : MonoBehaviour
{
    private Transform NowPos;
    private Vector3 Right;
    private Vector3 Left;
    private bool characterChange;
    private bool dirChange;
    private float CloudSpeed = 0.03f;
    private float Cloud_X = 0.0f;
    private float Cloud_FlowX = 0.0f;
    private float BG_X = 0.0f;

    public GameObject BG;
    public GameObject Cloud;
    public GameObject Background;
    public GameObject YeongHui;
    public GameObject Dragon;
   

    void Start()
    {
        NowPos = GetComponent<Transform>();

        Right = new Vector3(6, 5, -10);
        Left = new Vector3(-6, 5, -10);
    }
    
    void Update()
    {        
        Vector3 YeongHuiPos = YeongHui.GetComponent<Transform>().position;
        Vector3 DragonPos = Dragon.GetComponent<Transform>().position;

        if (GameInfo_Mgr.GetInstance.CharacterState)
        {
            if (CharacterAbility.GetInstance.dragon.NowHealth <= 0)
            {
                NowPos.position = new Vector3(DragonPos.x, DragonPos.y + 5, -10);
            }
            else
            {
                if (GameInfo_Mgr.GetInstance.DragonDir)
                {
                    if (NowPos.position != DragonPos + Left)
                    {
                        if (GameInfo_Mgr.GetInstance.DirChange)
                        {
                            if (DragonPos.x < 12)
                                NowPos.position = Vector3.Lerp(NowPos.position, new Vector3(12, DragonPos.y, DragonPos.z) + Left, Time.deltaTime * 2.0f);
                            else
                                NowPos.position = Vector3.Lerp(NowPos.position, DragonPos + Left, Time.deltaTime * 2.0f);

                            if (DragonPos.x < 12)
                            {
                                if (Vector3.Distance(NowPos.position, new Vector3(12, DragonPos.y, DragonPos.z) + Left) < 0.2f)
                                    GameInfo_Mgr.GetInstance.DirChange = false;
                            }
                            else
                            {
                                if (Vector3.Distance(NowPos.position, DragonPos + Left) < 0.2f)
                                    GameInfo_Mgr.GetInstance.DirChange = false;
                            }
                        }
                        else
                        {
                            if (DragonPos.x < 12)
                                NowPos.position = new Vector3(12, DragonPos.y, DragonPos.z) + Left;
                            else
                                NowPos.position = DragonPos + Left;
                        }
                    }
                }
                else
                {
                    if (NowPos.position != DragonPos + Right)
                    {
                        if (GameInfo_Mgr.GetInstance.DirChange)
                        {
                            if (DragonPos.x > GameInfo_Mgr.GetInstance.MapSize - 12)
                                NowPos.position = Vector3.Lerp(NowPos.position, new Vector3(GameInfo_Mgr.GetInstance.MapSize - 12, DragonPos.y, DragonPos.z) + Right, Time.deltaTime * 2.0f);
                            else
                                NowPos.position = Vector3.Lerp(NowPos.position, DragonPos + Right, Time.deltaTime * 2.0f);

                            if (DragonPos.x > GameInfo_Mgr.GetInstance.MapSize - 12)
                            {
                                if (Vector3.Distance(NowPos.position, new Vector3(GameInfo_Mgr.GetInstance.MapSize - 12, DragonPos.y, DragonPos.z) + Right) < 0.2f)
                                    GameInfo_Mgr.GetInstance.DirChange = false;
                            }
                            else
                            {
                                if (Vector3.Distance(NowPos.position, DragonPos + Right) < 0.2f)
                                    GameInfo_Mgr.GetInstance.DirChange = false;
                            }
                        }
                        else
                        {
                            if (DragonPos.x > GameInfo_Mgr.GetInstance.MapSize - 12)
                                NowPos.position = new Vector3(GameInfo_Mgr.GetInstance.MapSize - 12, DragonPos.y, DragonPos.z) + Right;
                            else
                                NowPos.position = DragonPos + Right;
                        }
                    }
                }
            }                    
        }
        else
        {
            if (GameInfo_Mgr.GetInstance.YeongHuiDir)
            {
                if (GameInfo_Mgr.GetInstance.DirChange)
                {
                    if (NowPos.position != YeongHuiPos + Left)
                    {
                        if (YeongHuiPos.x < 12)
                            NowPos.position = Vector3.Lerp(NowPos.position, new Vector3(12, YeongHuiPos.y, YeongHuiPos.z) + Left, Time.deltaTime * 2.0f);
                        else
                            NowPos.position = Vector3.Lerp(NowPos.position, YeongHuiPos + Left, Time.deltaTime * 2.0f);

                        if (YeongHuiPos.x < 12)
                        {
                            if (Vector3.Distance(NowPos.position, new Vector3(12, YeongHuiPos.y, YeongHuiPos.z) + Left) < 0.2f)
                                GameInfo_Mgr.GetInstance.DirChange = false;
                        }
                        else
                        {
                            if (Vector3.Distance(NowPos.position, YeongHuiPos + Left) < 0.2f)
                                GameInfo_Mgr.GetInstance.DirChange = false;
                        }
                    }
                }
                else
                {
                    if (NowPos.position != YeongHuiPos + Left)
                    {
                        if (YeongHuiPos.x < 12)
                            NowPos.position = new Vector3(12, YeongHuiPos.y, YeongHuiPos.z) + Left;
                        else
                            NowPos.position = YeongHuiPos + Left;
                    }
                }                    
            }
            else
            {
                if (GameInfo_Mgr.GetInstance.DirChange)
                {
                    if (NowPos.position != YeongHuiPos + Right)
                    {
                        if (YeongHuiPos.x > GameInfo_Mgr.GetInstance.MapSize - 12)
                            NowPos.position = Vector3.Lerp(NowPos.position, new Vector3(GameInfo_Mgr.GetInstance.MapSize - 12, YeongHuiPos.y, YeongHuiPos.z) + Right, Time.deltaTime * 2.0f);
                        else
                            NowPos.position = Vector3.Lerp(NowPos.position, YeongHuiPos + Right, Time.deltaTime * 2.0f);

                        if (YeongHuiPos.x > GameInfo_Mgr.GetInstance.MapSize - 12)
                        {
                            if (Vector3.Distance(NowPos.position, new Vector3(GameInfo_Mgr.GetInstance.MapSize - 12, YeongHuiPos.y, YeongHuiPos.z) + Right) < 0.2f)
                                GameInfo_Mgr.GetInstance.DirChange = false;
                        }
                        else
                        {
                            if (Vector3.Distance(NowPos.position, YeongHuiPos + Right) < 0.2f)
                                GameInfo_Mgr.GetInstance.DirChange = false;
                        }
                    }
                }
                else
                {
                    if (NowPos.position != YeongHuiPos + Right)
                    {
                        if (YeongHuiPos.x > GameInfo_Mgr.GetInstance.MapSize - 12)
                            NowPos.position = new Vector3(GameInfo_Mgr.GetInstance.MapSize - 12, YeongHuiPos.y, YeongHuiPos.z) + Right;
                        else
                            NowPos.position = YeongHuiPos + Right;
                    }
                }                    
            }
        }

        if (NowPos.position.y > 42)
            NowPos.position = new Vector3(NowPos.position.x, 42f, -10);

        Cloud_X = GetComponent<Transform>().position.x / 72f;

        if(!GameInfo_Mgr.GetInstance.Pause)
        {
            Cloud_FlowX += Time.deltaTime * CloudSpeed;
            Cloud_FlowX = Cloud_FlowX % 1;
        }

        Cloud.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Cloud_X + Cloud_FlowX, 0);

        BG_X = GetComponent<Transform>().position.x / 36f;
        BG_X = BG_X % 1;
        Background.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(BG_X, 0);
        
        float y = GetComponent<Transform>().position.y - 5;

        if (y < 0)
            y = 0;
        else if (y > 42)
            y = -42;
        else
            y = y * -1;

        BG.GetComponent<Transform>().localPosition = new Vector3(0, y, 100);
    }
}
