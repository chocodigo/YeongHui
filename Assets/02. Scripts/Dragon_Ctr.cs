using UnityEngine;
using System.Collections;

public class Dragon_Ctr : MonoBehaviour
{
    public Animator anim;
    public GameObject Model;
    public GameObject[] Prefabs;

    private Quaternion Right = Quaternion.identity;
    private Quaternion Left = Quaternion.identity;
    private bool pause;
    private bool Dath;
    private GameObject currentPrefabObject;
    private int currentPrefabIndex;

    private void UpdateEffect()
    {
        if (GameInfo_Mgr.GetInstance.CharacterState && GameInfo_Mgr.GetInstance.Attack_Dragon && currentPrefabObject == null)
        {
            anim.SetTrigger("Attack");
            BeginEffect();
        }
    }

    private void BeginEffect()
    {       
        currentPrefabObject = GameObject.Instantiate(Prefabs[currentPrefabIndex]);        
    }

    void Start()
    {
        CharacterAbility.GetInstance.dragon.InitState();

        Right.eulerAngles = new Vector3(0, 0, 0);
        Left.eulerAngles = new Vector3(0, 180, 0);

        pause = false;
        Dath = false;

        string weapon = Cloud_Mgr.GetInstance.UserData.DragonWeapon;
        if (weapon.StartsWith("WF"))
            currentPrefabIndex = 0;
        else if (weapon.StartsWith("WG"))
            currentPrefabIndex = 1;
        else if (weapon.StartsWith("WA"))
            currentPrefabIndex = 2;
        else if (weapon.StartsWith("WW"))
            currentPrefabIndex = 3;
        else if (weapon.StartsWith("WD"))
            currentPrefabIndex = 4;
        else
            currentPrefabIndex = 5;
    }

    void Update()
    {
        if (!GameInfo_Mgr.GetInstance.Pause)
        {
            anim.SetBool("Pause", false);
            if (CharacterAbility.GetInstance.dragon.NowHealth <= 0)
            {
                anim.SetTrigger("Dead");
                Dath = true;
            }

            if (GameInfo_Mgr.GetInstance.CharacterState && !Dath)
            {
                if (GameInfo_Mgr.GetInstance.DragonDir)
                    GetComponent<Transform>().rotation = Quaternion.Slerp(GetComponent<Transform>().rotation, Left, Time.deltaTime * 5.0f);
                else
                    GetComponent<Transform>().rotation = Quaternion.Slerp(GetComponent<Transform>().rotation, Right, Time.deltaTime * 5.0f);

                if (GameInfo_Mgr.GetInstance.Speed_X != 0 || GameInfo_Mgr.GetInstance.Speed_Y != 0)
                    anim.SetBool("Move", true);
                else
                    anim.SetBool("Move", false);

                GetComponent<Transform>().position += new Vector3(Time.deltaTime * GameInfo_Mgr.GetInstance.Speed_X, Time.deltaTime * GameInfo_Mgr.GetInstance.Speed_Y, 0);
            }

            if (GetComponent<Transform>().position.x < 0f)
                GetComponent<Transform>().position = new Vector3(0, GetComponent<Transform>().position.y, 10);
            else if (GetComponent<Transform>().position.x > GameInfo_Mgr.GetInstance.MapSize)
                GetComponent<Transform>().position = new Vector3(GameInfo_Mgr.GetInstance.MapSize, GetComponent<Transform>().position.y, 10);

            if (GetComponent<Transform>().position.y < 0f)
            {
                GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, 0f, 10);
                CharacterAbility.GetInstance.dragon.NowHealth = 0;
            }
            else if (GetComponent<Transform>().position.y > 44f)
                GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, 44f, 10);

            UpdateEffect();

            if (currentPrefabObject != null)
            {
                Vector3 pos;
                Vector3 right = transform.right * 4.8f;
                Vector3 down = transform.up * -0.7f;

                Quaternion rotation = Quaternion.identity;
                Quaternion rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 90f, 0);

                rotation = transform.rotation * rot;
                pos = Model.transform.position + right + down;

                currentPrefabObject.transform.position = pos;
                currentPrefabObject.transform.rotation = rotation;
            }
        }
        else
        {
            anim.SetBool("Pause", true);
            pause = true;
        }
    }
}

