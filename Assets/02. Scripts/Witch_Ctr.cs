using UnityEngine;
using System.Collections;

public class Witch_Ctr : MonoBehaviour
{
    public Animator anim;
    public GameObject[] Prefabs;

    private Quaternion Right = Quaternion.identity;
    private Quaternion Left = Quaternion.identity;
    private bool pause;
    private bool Dath;
    private GameObject currentPrefabObject;
    private int currentPrefabIndex;
    private bool Jump_bool;
       
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.8f);
        GameInfo_Mgr.GetInstance.Fail = true;
    }
    
    private void UpdateEffect()
    {
        if (!GameInfo_Mgr.GetInstance.CharacterState && GameInfo_Mgr.GetInstance.Attack_YeongHui)
        {
            anim.SetTrigger("Attack");
            currentPrefabObject = null;
            BeginEffect();
            GameInfo_Mgr.GetInstance.Attack_YeongHui = !GameInfo_Mgr.GetInstance.Attack_YeongHui;
        }
    }

    private void BeginEffect()
    {
        Vector3 pos;
        Vector3 right = transform.right * 2f;
        Vector3 up = transform.up * 1.5f;

        Quaternion rotation = Quaternion.identity;
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(0, 90f, 0);

        currentPrefabObject = GameObject.Instantiate(Prefabs[currentPrefabIndex]);

        rotation = transform.rotation * rot;
        pos = transform.position + right + up;

        currentPrefabObject.transform.position = pos;
        currentPrefabObject.transform.rotation = rotation;
    }

    void Start()
    {
        CharacterAbility.GetInstance.yeonghui.InitState();

        Right.eulerAngles = new Vector3(0, 0, 0);
        Left.eulerAngles = new Vector3(0, 180, 0);

        pause = false;
        Dath = false;
        Jump_bool = false;

        string weapon = Cloud_Mgr.GetInstance.UserData.YeonghuiWeapon;
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

            if (CharacterAbility.GetInstance.yeonghui.NowHealth <= 0)
            {
                anim.SetTrigger("Dead");
                StartCoroutine(Die());                    
                Dath = true;
            }

            if (!GameInfo_Mgr.GetInstance.CharacterState && !Dath)
            {
                if (GameInfo_Mgr.GetInstance.YeongHuiDir)
                    GetComponent<Transform>().rotation = Quaternion.Slerp(GetComponent<Transform>().rotation, Left, Time.deltaTime * 5.0f);
                else
                    GetComponent<Transform>().rotation = Quaternion.Slerp(GetComponent<Transform>().rotation, Right, Time.deltaTime * 5.0f);
                
                GetComponent<Transform>().position += new Vector3(Time.deltaTime * GameInfo_Mgr.GetInstance.Speed_X, 0, 0);

                if (!Jump_bool && GameInfo_Mgr.GetInstance.Speed_Y > 0)
                {
                    anim.SetBool("Move", false);
                    Jump_bool = true;
                    GetComponent<Rigidbody>().AddForce(Vector3.up * 75f, ForceMode.Impulse);
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                
                if (GameInfo_Mgr.GetInstance.Speed_X != 0 && !Jump_bool)
                    anim.SetBool("Move", true);
                else
                    anim.SetBool("Move", false);
            }

            if (GetComponent<Transform>().position.x < 0f)
                GetComponent<Transform>().position = new Vector3(0, GetComponent<Transform>().position.y, 10);
            else if (GetComponent<Transform>().position.x > GameInfo_Mgr.GetInstance.MapSize)
                GetComponent<Transform>().position = new Vector3(GameInfo_Mgr.GetInstance.MapSize, GetComponent<Transform>().position.y, 10);

            if (GetComponent<Transform>().position.y < 0f)
            {
                GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, 0f, 10);
                CharacterAbility.GetInstance.yeonghui.NowHealth = 0;
            }
            else if (GetComponent<Transform>().position.y > 44f)
                GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, 44f, 10);

            UpdateEffect();
        }
        else
        {
            anim.SetBool("Pause", true);
            pause = true;
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Map")
            Jump_bool = false;
    }
}