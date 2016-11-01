using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Compos_Mgr : MonoBehaviour {
    public Image Result;
    public Image Compose_Panel;
    public Image TextBox;

    private Transform[] ChildTs;
    private Text TextBox_Text;
    private Text Name;
    private Text Info;
    private Image ItemImage;
    private Text Attribute;
    private Text Health;
    private Text Attack;
    private Text Defence;
    private GameObject WeaponDefence;
    private Transform Ok;
    private Transform Cancle;
    private Transform Compose;
    private Sprite Plus;
    private Sprite Notting;    
    private Transform Compose_Panel1;
    private Transform Compose_Panel2;
    private Transform Compose_Panel3;
    private Transform Compose_Panel4;
    private Transform Compose_Panel5;
    private Image Panel1_Mat1;
    private Image Panel1_Mat2;
    private Image Panel1_Mat3;
    private Image Panel1_Mat4;
    private Image Panel2_Mat1;
    private Image Panel2_Mat2;
    private Image Panel2_Mat3;
    private Image Panel2_Mat4;
    private Image Panel3_Mat1;
    private Text Panel1_Mat1_Text;
    private Text Panel1_Mat2_Text;
    private Text Panel1_Mat3_Text;
    private Text Panel1_Mat4_Text;
    private Text Panel2_Mat1_Text;
    private Text Panel2_Mat2_Text;
    private Text Panel2_Mat3_Text;
    private Text Panel2_Mat4_Text;
    private Text Panel4_text;
    private Text Panel5_text;
    private ItemInfo.ComposeInfo NowCompose;
    private int Mat1;
    private int Mat2;
    private int Mat3;
    private int Mat4;
    private int It1_Value;
    private int It2_Value;
    private int It3_Value;
    private int It4_Value;
    private Color Alpha_half;
    private Color Alpha;
    private float Final_Probability;

    void Start () {
        ChildTs = TextBox.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildTs)
            cts.gameObject.SetActive(false);
        TextBox_Text = TextBox.transform.FindChild("Panel").FindChild("Text").GetComponent<Text>();

        WeaponDefence = Result.transform.FindChild("WeaponDefence").gameObject;

        Name = Result.transform.FindChild("Name").GetComponent<Text>();
        ItemImage = Result.transform.FindChild("ItemImage").FindChild("Image").GetComponent<Image>();
        Info = Result.transform.FindChild("Info").GetComponent<Text>();
        Attribute = WeaponDefence.transform.FindChild("Attribute").GetComponent<Text>();
        Health = WeaponDefence.transform.FindChild("Health").GetComponent<Text>();
        Attack = WeaponDefence.transform.FindChild("Attack").GetComponent<Text>();
        Defence = WeaponDefence.transform.FindChild("Defence").GetComponent<Text>();
        
        Ok = Result.transform.FindChild("OK").GetComponent<RectTransform>();
        Cancle = Result.transform.FindChild("Cancle").GetComponent<RectTransform>();
        Compose = Result.transform.FindChild("Compose").GetComponent<RectTransform>();
        
        Compose_Panel1 = Compose_Panel.transform.FindChild("Panel1");
        Compose_Panel2 = Compose_Panel.transform.FindChild("Panel2");
        Compose_Panel3 = Compose_Panel.transform.FindChild("Panel3");
        Compose_Panel4 = Compose_Panel.transform.FindChild("Panel4");
        Compose_Panel5 = Compose_Panel.transform.FindChild("Panel5");

        Plus = Resources.Load<Sprite>("Plus");
        Notting = Resources.Load<Sprite>("Notting");

        Panel1_Mat1 = Compose_Panel1.transform.FindChild("Mat1").FindChild("Item").GetComponent<Image>();
        Panel1_Mat2 = Compose_Panel1.transform.FindChild("Mat2").FindChild("Item").GetComponent<Image>();
        Panel1_Mat3 = Compose_Panel1.transform.FindChild("Mat3").FindChild("Item").GetComponent<Image>();
        Panel1_Mat4 = Compose_Panel1.transform.FindChild("Mat4").FindChild("Item").GetComponent<Image>();
        Panel2_Mat1 = Compose_Panel2.transform.FindChild("Mat1").FindChild("Item").GetComponent<Image>();
        Panel2_Mat2 = Compose_Panel2.transform.FindChild("Mat2").FindChild("Item").GetComponent<Image>();
        Panel2_Mat3 = Compose_Panel2.transform.FindChild("Mat3").FindChild("Item").GetComponent<Image>();
        Panel2_Mat4 = Compose_Panel2.transform.FindChild("Mat4").FindChild("Item").GetComponent<Image>();
        Panel3_Mat1 = Compose_Panel3.transform.FindChild("Mat1").FindChild("Item").GetComponent<Image>();

        Panel1_Mat1_Text = Panel1_Mat1.transform.FindChild("Text").GetComponent<Text>();
        Panel1_Mat2_Text = Panel1_Mat2.transform.FindChild("Text").GetComponent<Text>();
        Panel1_Mat3_Text = Panel1_Mat3.transform.FindChild("Text").GetComponent<Text>();
        Panel1_Mat4_Text = Panel1_Mat4.transform.FindChild("Text").GetComponent<Text>();
        Panel2_Mat1_Text = Panel2_Mat1.transform.FindChild("Text").GetComponent<Text>();
        Panel2_Mat2_Text = Panel2_Mat2.transform.FindChild("Text").GetComponent<Text>();
        Panel2_Mat3_Text = Panel2_Mat3.transform.FindChild("Text").GetComponent<Text>();
        Panel2_Mat4_Text = Panel2_Mat4.transform.FindChild("Text").GetComponent<Text>();
        Panel4_text = Compose_Panel4.transform.FindChild("Probability").GetComponent<Text>();
        Panel5_text = Compose_Panel5.transform.FindChild("Cost").GetComponent<Text>();

        Alpha_half = Color.white;
        Alpha = Color.white;
        Alpha_half.a = 0.3f;
        Alpha.a = 1f;       
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (Scene_Mgr.GetInstance.bSetting)
                    Scene_Mgr.GetInstance.UnLoadSetting();
                else if (TextBox.IsActive())
                    ClickTextBox_Btn();
                else
                    BacktoLobbyBtn();
            }
        }

        if (ComposForm_Mgr.GetInstance.ChangeCompose)
        {
            Compose.gameObject.SetActive(true);
            Ok.gameObject.SetActive(false);
            Cancle.gameObject.SetActive(false);

            Mat1 = -1;
            Mat2 = -1;
            Mat3 = -1;
            Mat4 = -1;
            It1_Value = 0;
            It2_Value = 0;
            It3_Value = 0;
            It4_Value = 0;

            if (ComposForm_Mgr.GetInstance.NowReinforce == -1)
            {
                Panel3_Mat1.overrideSprite = Plus;
                Final_Probability = 0f;
            }
            else
            {
                string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[ComposForm_Mgr.GetInstance.NowReinforce].Split(new char[] { ':' });
                Panel3_Mat1.overrideSprite = ItemInfo.GetInstance.GetItemInfo(p[0]).ItemImage;
                Final_Probability = ItemInfo.GetInstance.GetItemInfo(p[0]).EffectProbability;
            }

            if (ComposForm_Mgr.GetInstance.NowCompose == -1)
            {
                Name.text = "합성결과";
                ItemImage.overrideSprite = Plus;
                Info.text = "";
                WeaponDefence.gameObject.SetActive(false);

                Panel1_Mat1.overrideSprite = Notting;
                Panel1_Mat1.color = Alpha;
                Panel1_Mat1_Text.text = "";
                Panel1_Mat2.overrideSprite = Notting;
                Panel1_Mat2.color = Alpha;
                Panel1_Mat2_Text.text = "";
                Panel1_Mat3.overrideSprite = Notting;
                Panel1_Mat3.color = Alpha;
                Panel1_Mat3_Text.text = "";
                Panel1_Mat4.overrideSprite = Notting;
                Panel1_Mat4.color = Alpha;
                Panel1_Mat4_Text.text = "";
                Panel2_Mat1.overrideSprite = Notting;
                Panel2_Mat1.color = Alpha;
                Panel2_Mat1_Text.text = "";
                Panel2_Mat2.overrideSprite = Notting;
                Panel2_Mat2.color = Alpha;
                Panel2_Mat2_Text.text = "";
                Panel2_Mat3.overrideSprite = Notting;
                Panel2_Mat3.color = Alpha;
                Panel2_Mat3_Text.text = "";
                Panel2_Mat4.overrideSprite = Notting;
                Panel2_Mat4.color = Alpha;
                Panel2_Mat4_Text.text = "";

                Panel4_text.text = "";
                Panel5_text.text = "";
            }
            else
            {
                NowCompose = ItemInfo.GetInstance.GetComposeInfo(ComposForm_Mgr.GetInstance.NowCompose);
                ItemInfo.ItemInformation info = ItemInfo.GetInstance.GetItemInfo(NowCompose.result);
                Name.text = info.Name;
                ItemImage.overrideSprite = info.ItemImage;
                Info.text = info.Information;

                WeaponDefence.SetActive(true);
                if (info.Attribute == 1)
                    Attribute.text = "불";
                else if (info.Attribute == 2)
                    Attribute.text = "바람";
                else if (info.Attribute == 3)
                    Attribute.text = "땅";
                else if (info.Attribute == 4)
                    Attribute.text = "물";
                else if (info.Attribute == 5)
                    Attribute.text = "빛";
                Health.text = info.Health.ToString();
                Attack.text = info.Attack.ToString();
                Defence.text = info.Defence.ToString();

                bool Check_Mat1 = true;
                bool Check_Mat2 = true;
                bool Check_Mat3 = true;
                bool Check_Mat4 = true;

                for (int i = 0; i < Cloud_Mgr.GetInstance.UserData.HoldItem.Length; i++)
                {                    
                    if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.mat1) && NowCompose.mat1 != "$" && Check_Mat1)
                    {
                        Mat1 = i;
                        Check_Mat1 = false;
                        continue;
                    }
                    if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.mat2) && NowCompose.mat2 != "$" && Check_Mat2)
                    {
                        Mat2 = i;
                        Check_Mat2 = false;
                        continue;
                    }
                    if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.mat3) && NowCompose.mat3 != "$" && Check_Mat3)
                    {
                        Mat3 = i;
                        Check_Mat3 = false;
                        continue;
                    }
                    if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.mat4) && NowCompose.mat4 != "$" && Check_Mat4)
                    {
                        Mat4 = i;
                        Check_Mat4 = false;
                        continue;
                    }
                    if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.it1) && NowCompose.it1 != "$")
                    {
                        string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[i].Split(new char[] { ':' });
                        int value = Convert.ToInt32(p[1]);
                        if (NowCompose.it1_value <= It1_Value + value)
                            It1_Value = NowCompose.it1_value;
                        else
                            It1_Value += value;
                        continue;
                    }
                    if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.it2) && NowCompose.it2 != "$")
                    {
                        string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[i].Split(new char[] { ':' });
                        int value = Convert.ToInt32(p[1]);
                        if (NowCompose.it2_value <= It2_Value + value)
                            It2_Value = NowCompose.it2_value;
                        else
                            It2_Value += value;
                        continue;
                    }
                    if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.it3) && NowCompose.it3 != "$")
                    {
                        string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[i].Split(new char[] { ':' });
                        int value = Convert.ToInt32(p[1]);
                        if (NowCompose.it3_value <= It3_Value + value)
                            It3_Value = NowCompose.it3_value;
                        else
                            It3_Value += value;
                        continue;
                    }
                    if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.it4) && NowCompose.it4 != "$")
                    {
                        string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[i].Split(new char[] { ':' });
                        int value = Convert.ToInt32(p[1]);
                        if (NowCompose.it4_value <= It4_Value + value)
                            It4_Value = NowCompose.it4_value;
                        else
                            It4_Value += value;
                        continue;
                    }
                }

                if (NowCompose.mat1 == "$")
                {
                    Panel1_Mat1.overrideSprite = Notting;
                    Panel1_Mat1.color = Alpha;
                    Panel1_Mat1_Text.text = "";
                }
                else
                {
                    Panel1_Mat1.overrideSprite = ItemInfo.GetInstance.GetItemInfo(NowCompose.mat1).ItemImage;
                    if (Mat1 == -1)
                    {
                        Panel1_Mat1_Text.text = "0 / 1";
                        Panel1_Mat1.color = Alpha_half;
                    }
                    else
                    {
                        Panel1_Mat1_Text.text = "1 / 1";
                        Panel1_Mat1.color = Alpha;
                    }
                }
                if (NowCompose.mat2 == "$")
                {
                    Panel1_Mat2.overrideSprite = Notting;
                    Panel1_Mat2.color = Alpha;
                    Panel1_Mat2_Text.text = "";
                }
                else
                {
                    Panel1_Mat2.overrideSprite = ItemInfo.GetInstance.GetItemInfo(NowCompose.mat2).ItemImage;
                    if (Mat2 == -1)
                    {
                        Panel1_Mat2_Text.text = "0 / 1";
                        Panel1_Mat2.color = Alpha_half;
                    }
                    else
                    {
                        Panel1_Mat2_Text.text = "1 / 1";
                        Panel1_Mat2.color = Alpha;
                    }
                }
                if (NowCompose.mat3 == "$")
                {
                    Panel1_Mat3.overrideSprite = Notting;
                    Panel1_Mat3.color = Alpha;
                    Panel1_Mat3_Text.text = "";
                }
                else
                {
                    Panel1_Mat3.overrideSprite = ItemInfo.GetInstance.GetItemInfo(NowCompose.mat3).ItemImage;
                    if (Mat3 == -1)
                    {
                        Panel1_Mat3_Text.text = "0 / 1";
                        Panel1_Mat3.color = Alpha_half;
                    }
                    else
                    {
                        Panel1_Mat3_Text.text = "1 / 1";
                        Panel1_Mat3.color = Alpha;
                    }
                }
                if (NowCompose.mat4 == "$")
                {
                    Panel1_Mat4.overrideSprite = Notting;
                    Panel1_Mat4.color = Alpha;
                    Panel1_Mat4_Text.text = "";
                }
                else
                {
                    Panel1_Mat4.overrideSprite = ItemInfo.GetInstance.GetItemInfo(NowCompose.mat4).ItemImage;
                    if (Mat4 == -1)
                    {
                        Panel1_Mat4_Text.text = "0 / 1";
                        Panel1_Mat4.color = Alpha_half;
                    }
                    else
                    {
                        Panel1_Mat4_Text.text = "1 / 1";
                        Panel1_Mat4.color = Alpha;
                    }
                }
                if (NowCompose.it1 == "$")
                {
                    Panel2_Mat1.overrideSprite = Notting;
                    Panel2_Mat1.color = Alpha;
                    Panel2_Mat1_Text.text = "";
                }
                else
                {
                    Panel2_Mat1.overrideSprite = ItemInfo.GetInstance.GetItemInfo(NowCompose.it1).ItemImage;
                    Panel2_Mat1_Text.text = It1_Value + " / " + NowCompose.it1_value;
                    if (It1_Value != NowCompose.it1_value)
                        Panel2_Mat1.color = Alpha_half;
                    else
                        Panel2_Mat1.color = Alpha;
                }
                if (NowCompose.it2 == "$")
                {
                    Panel2_Mat2.overrideSprite = Notting;
                    Panel2_Mat2.color = Alpha;
                    Panel2_Mat2_Text.text = "";
                }
                else
                {
                    Panel2_Mat2.overrideSprite = ItemInfo.GetInstance.GetItemInfo(NowCompose.it2).ItemImage;
                    Panel2_Mat2_Text.text = It2_Value + " / " + NowCompose.it2_value;
                    if (It2_Value != NowCompose.it2_value)
                        Panel2_Mat2.color = Alpha_half;
                    else
                        Panel2_Mat2.color = Alpha;
                }
                if (NowCompose.it3 == "$")
                {
                    Panel2_Mat3.overrideSprite = Notting;
                    Panel2_Mat3.color = Alpha;
                    Panel2_Mat3_Text.text = "";
                }
                else
                {
                    Panel2_Mat3.overrideSprite = ItemInfo.GetInstance.GetItemInfo(NowCompose.it3).ItemImage;
                    Panel2_Mat3_Text.text = It3_Value + " / " + NowCompose.it3_value;
                    if (It3_Value != NowCompose.it3_value)
                        Panel2_Mat3.color = Alpha_half;
                    else
                        Panel2_Mat3.color = Alpha;
                }
                if (NowCompose.it4 == "$")
                {
                    Panel2_Mat4.overrideSprite = Notting;
                    Panel2_Mat4.color = Alpha;
                    Panel2_Mat4_Text.text = "";
                }
                else
                {
                    Panel2_Mat4.overrideSprite = ItemInfo.GetInstance.GetItemInfo(NowCompose.it4).ItemImage;
                    Panel2_Mat4_Text.text = It4_Value + " / " + NowCompose.it4_value;
                    if (It4_Value != NowCompose.it4_value)
                        Panel2_Mat4.color = Alpha_half;
                    else
                        Panel2_Mat4.color = Alpha;
                }

                Final_Probability += NowCompose.probability;

                if (Final_Probability > 1f)
                    Final_Probability = 1f;

                Panel4_text.text = (Final_Probability * 100).ToString() + "%";
                Panel5_text.text = NowCompose.cost.ToString("#,##0");
            }
            ComposForm_Mgr.GetInstance.ChangeCompose = false;
        }
    }

    public void ClickCompose()
    {
        if (ComposForm_Mgr.GetInstance.NowCompose == -1)
        {
            foreach (RectTransform cts in ChildTs)
                cts.gameObject.SetActive(true);
            TextBox_Text.fontSize = 100;
            TextBox_Text.text = "합성 결과를 선택하시오.";
            return;
        }

        NowCompose = ItemInfo.GetInstance.GetComposeInfo(ComposForm_Mgr.GetInstance.NowCompose);

        if (NowCompose.mat1 != "$" && Mat1 == -1 ||
            NowCompose.mat2 != "$" && Mat2 == -1 ||
            NowCompose.mat3 != "$" && Mat3 == -1 ||
            NowCompose.mat4 != "$" && Mat4 == -1 ||
            NowCompose.it1 != "$" && NowCompose.it1_value > It1_Value ||
            NowCompose.it2 != "$" && NowCompose.it2_value > It2_Value ||
            NowCompose.it3 != "$" && NowCompose.it3_value > It3_Value ||
            NowCompose.it4 != "$" && NowCompose.it4_value > It4_Value)
        {
            foreach (RectTransform cts in ChildTs)
                cts.gameObject.SetActive(true);
            TextBox_Text.fontSize = 100;
            TextBox_Text.text = "합성 재료가 부족합니다.";
            return;
        }

        if (Cloud_Mgr.GetInstance.UserData.Coin < NowCompose.cost)
        {
            foreach (RectTransform cts in ChildTs)
                cts.gameObject.SetActive(true);
            TextBox_Text.fontSize = 100;
            TextBox_Text.text = "금화가 부족합니다.";
            return;
        }

        Cloud_Mgr.GetInstance.UserData.Coin -= NowCompose.cost;

        if(ComposForm_Mgr.GetInstance.NowReinforce != -1)
        {
            string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[ComposForm_Mgr.GetInstance.NowReinforce].Split(new char[] { ':' });
            int value = Convert.ToInt32(p[1]);
            value--;
            Cloud_Mgr.GetInstance.UserData.HoldItem[ComposForm_Mgr.GetInstance.NowReinforce] = p[0] + ":" + value.ToString();
        }

        for (int i = 0; i < Cloud_Mgr.GetInstance.UserData.HoldItem.Length; i++)
        {
            if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.it1) && NowCompose.it1 != "$" && It1_Value > 0)
            {
                string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[i].Split(new char[] { ':' });
                int value = Convert.ToInt32(p[1]);
                if (It1_Value >= value)
                {
                    It1_Value -= value;
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i] = "$:0";
                }
                else
                {
                    value -= It1_Value;
                    It1_Value = 0;
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i] = p[0] + ":" + value.ToString();
                }
                continue;
            }
            if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.it2) && NowCompose.it2 != "$")
            {
                string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[i].Split(new char[] { ':' });
                int value = Convert.ToInt32(p[1]);
                if (It2_Value >= value)
                {
                    It2_Value -= value;
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i] = "$:0";
                }
                else
                {
                    value -= It2_Value;
                    It2_Value = 0;
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i] = p[0] + ":" + value.ToString();
                }
                continue;
            }
            if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.it3) && NowCompose.it3 != "$")
            {
                string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[i].Split(new char[] { ':' });
                int value = Convert.ToInt32(p[1]);
                if (It3_Value >= value)
                {
                    It3_Value -= value;
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i] = "$:0";
                }
                else
                {
                    value -= It3_Value;
                    It3_Value = 0;
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i] = p[0] + ":" + value.ToString();
                }
                continue;
            }
            if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(NowCompose.it4) && NowCompose.it4 != "$")
            {
                string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[i].Split(new char[] { ':' });
                int value = Convert.ToInt32(p[1]);
                if (It4_Value >= value)
                {
                    It4_Value -= value;
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i] = "$:0";
                }
                else
                {
                    value -= It4_Value;
                    It4_Value = 0;
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i] = p[0] + ":" + value.ToString();
                }
                continue;
            }
        }

        if (UnityEngine.Random.Range(0f, 1f) <= Final_Probability)
        {
            if (NowCompose.mat1 != "$")
                Cloud_Mgr.GetInstance.UserData.HoldItem[Mat1] = "$:0";
            if (NowCompose.mat2 != "$")
                Cloud_Mgr.GetInstance.UserData.HoldItem[Mat2] = "$:0";
            if (NowCompose.mat3 != "$")
                Cloud_Mgr.GetInstance.UserData.HoldItem[Mat3] = "$:0";
            if (NowCompose.mat4 != "$")
                Cloud_Mgr.GetInstance.UserData.HoldItem[Mat4] = "$:0";
            for (int i = 0; i < Cloud_Mgr.GetInstance.UserData.HoldItem.Length; i++)
            {
                if (Cloud_Mgr.GetInstance.UserData.HoldItem[i] == "$:0")
                {
                    Cloud_Mgr.GetInstance.UserData.HoldItem[i] = NowCompose.result + ":1";
                    break;
                }
            }

            ComposForm_Mgr.GetInstance.NowCompose = -1;
            ComposForm_Mgr.GetInstance.NowComposeSelectIV = -1;
            ComposForm_Mgr.GetInstance.NowComposePage = 1;
            ComposForm_Mgr.GetInstance.NowReinforce = -1;
            ComposForm_Mgr.GetInstance.NowReinforceSelectIV = -1;
            ComposForm_Mgr.GetInstance.NowReinforcePage = 1;

            foreach (RectTransform cts in ChildTs)
                cts.gameObject.SetActive(true);
            TextBox_Text.fontSize = 200;
            TextBox_Text.text = "합성 성공";
        }
        else
        {
            ComposForm_Mgr.GetInstance.NowReinforce = -1;
            ComposForm_Mgr.GetInstance.NowReinforceSelectIV = -1;
            ComposForm_Mgr.GetInstance.NowReinforcePage = 1;

            foreach (RectTransform cts in ChildTs)
                cts.gameObject.SetActive(true);
            TextBox_Text.fontSize = 200;
            TextBox_Text.text = "합성 실패";
        }        

        ComposForm_Mgr.GetInstance.ChangeCompose = true;

        Cloud_Mgr.GetInstance.SaveToCloud();
    }

    public void ClickTextBox_Btn()
    {
        foreach (RectTransform cts in ChildTs)
            cts.gameObject.SetActive(false);
    }

    public void ClickComposeReinforce(bool state)
    {
        ComposForm_Mgr.GetInstance.isCompose = state;

        Compose.gameObject.SetActive(false);
        Ok.gameObject.SetActive(true);
        Cancle.gameObject.SetActive(true);        
    }    

    public void BacktoLobbyBtn()
    {
        StartCoroutine(Scene_Mgr.GetInstance.LoadLobby());
        Scene_Mgr.GetInstance.UnLoadCompose();
    }
}
