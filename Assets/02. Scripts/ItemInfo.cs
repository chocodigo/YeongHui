using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInfo : Singleton<ItemInfo>
{
    public class ItemInformation
    {
        public string Code;
        public string Name;
        public string Information;
        public Sprite ItemImage;
        public int Attribute; //불 1, 바람 2, 땅 3, 물 4 빛 5
        public float EffectProbability;
        public int Health;
        public int Attack;
        public int Defence;
        public int cost;
        public int sell;

        public ItemInformation(string CODE, string NAME, string INFO, Sprite IMAGE, int ATTRIBUTE, float PROBABILITY, int HEALTH, int ATTACK, int DEFENCE, int COST, int SELL)
        {
            Code = CODE;
            Name = NAME;
            Information = INFO;
            ItemImage = IMAGE;
            Attribute = ATTRIBUTE;
            EffectProbability = PROBABILITY;
            Health = HEALTH;
            Attack = ATTACK;
            Defence = DEFENCE;
            cost = COST;
            sell = SELL;
        }
    }

    public class ComposeInfo
    {
        public string result;
        public string mat1;
        public string mat2;
        public string mat3;
        public string mat4;
        public string it1;
        public int it1_value;
        public string it2;
        public int it2_value;
        public string it3;
        public int it3_value;
        public string it4;
        public int it4_value;
        public int cost;
        public float probability;
        public int order;

        public ComposeInfo(string Result, string Mat1, string Mat2, string Mat3, string Mat4, string IT1, int IT1_Value, string IT2, int IT2_Value, string IT3, int IT3_Value, string IT4, int IT4_Value, int Cost, float Probability, int Order)
        {
            result = Result;
            mat1 = Mat1;
            mat2 = Mat2;
            mat3 = Mat3;
            mat4 = Mat4;
            it1 = IT1;
            it1_value = IT1_Value;
            it2 = IT2;
            it2_value = IT2_Value;
            it3 = IT3;
            it3_value = IT3_Value;
            it4 = IT4;
            it4_value = IT4_Value;
            cost = Cost;
            probability = Probability;
            order = Order;
        }
    }

    ItemInformation[] Iteminfo;
    ComposeInfo[] composinfo;

    public int itemValue; //아이템 개수
    public int composeValue; //합성 개수

    public ItemInformation GetItemInfo(string code)
    {
        ItemInformation _IIF = null;

        for (int i = 0; i < itemValue; i++)
        {
            if (Iteminfo[i].Code == code)
            {
                _IIF = Iteminfo[i];
                return _IIF;
            }
        }
        return null;
    }

    public ItemInformation[] GetAllItemInfo()
    {
        return Iteminfo;
    }

    public ComposeInfo GetComposeInfo(int index)
    {        
        return composinfo[index];
    }

    void Start()
    {
        SstItemInfo();
        SetComposeInfo();
    }

    private void SstItemInfo()
    {
        itemValue = 77;
        Iteminfo = new ItemInformation[itemValue];
        //아이템 정보
        //Non
        Iteminfo[0] = new ItemInformation(
            "$", "NULL",
            "NULL",
            Resources.Load<Sprite>("NoImage"),
            1, 0.0f, 0, 0, 0, 0, 0);
        //뷸 무기
        Iteminfo[1] = new ItemInformation(
            "WF01", "초급 불 무기",
            "불의 힘을 담은 초급자용 무기, 5%확률로 화상효과",
            Resources.Load<Sprite>("WF01"),
            1, 0.05f, 0, 30, 0, 1000, 200);
        Iteminfo[2] = new ItemInformation(
            "WF02", "살라멘더 무기",
            "하위 불의 정령의 힘을 담은 무기, 10%확률로 화상효과",
            Resources.Load<Sprite>("WF02"),
            1, 0.1f, 100, 100, 0, 3000, 600);
        Iteminfo[3] = new ItemInformation(
            "WF03", "이프리트 무기",
            "중위 불의 정령의 힘을 담은 무기, 20%확률로 화상효과",
            Resources.Load<Sprite>("WF03"),
            1, 0.2f, 200, 300, 0, 6000, 1500);
        Iteminfo[4] = new ItemInformation(
            "WF04", "드레이크 무기",
            "상위 불의 정령의 힘을 담은 무기, 30%확률로 화상효과",
            Resources.Load<Sprite>("WF04"),
            1, 0.3f, 300, 500, 0, 9000, 2000);
        Iteminfo[5] = new ItemInformation(
            "WF05", "피닉스 무기",
            "불의 정령왕의 힘을 담은 무기, 50%확률로 화상효과",
            Resources.Load<Sprite>("WF05"),
            1, 0.5f, 400, 900, 0, 12000, 3000);
        //바람 무기
        Iteminfo[6] = new ItemInformation(
            "WW01", "초급 바람 무기",
            "바람의 힘을 담은 초급자용 무기, 5%확률로 출혈효과",
            Resources.Load<Sprite>("WW01"),
            2, 0.05f, 0, 30, 0, 1000, 200);
        Iteminfo[7] = new ItemInformation(
            "WW02", "실피드 무기",
            "하위 바람의 정령의 힘을 담은 무기, 10%확률로 출혈효과",
            Resources.Load<Sprite>("WW02"),
            2, 0.1f, 100, 100, 0, 3000, 600);
        Iteminfo[8] = new ItemInformation(
            "WW03", "실프 무기",
            "중위 바람의 정령의 힘을 담은 무기, 20%확률로 출혈효과",
            Resources.Load<Sprite>("WW03"),
            2, 0.2f, 200, 300, 0, 6000, 1500);
        Iteminfo[9] = new ItemInformation(
            "WW04", "진 무기",
            "상위 바람의 정령의 힘을 담은 무기, 30%확률로 출혈효과",
            Resources.Load<Sprite>("WW04"),
            2, 0.3f, 300, 500, 0, 9000, 2000);
        Iteminfo[10] = new ItemInformation(
            "WW05", "에리얼 무기",
            "바람의 정령왕의 힘을 담은 무기, 50%확률로 출혈효과",
            Resources.Load<Sprite>("WW05"),
            2, 0.5f, 400, 900, 0, 12000, 3000);
        //땅 무기
        Iteminfo[11] = new ItemInformation(
            "WG01", "초급 땅 무기",
            "땅의 힘을 담은 초급자용 무기, 5%확률로 마비효과",
            Resources.Load<Sprite>("WG01"),
            3, 0.05f, 0, 30, 0, 1000, 200);
        Iteminfo[12] = new ItemInformation(
            "WG02", "어스웜 무기",
            "하위 땅의 정령의 힘을 담은 무기, 10%확률로 마비효과",
            Resources.Load<Sprite>("WG02"),
            3, 0.1f, 100, 100, 0, 3000, 600);
        Iteminfo[13] = new ItemInformation(
            "WG03", "노움 무기",
            "중위 땅의 정령의 힘을 담은 무기, 20%확률로 마비효과",
            Resources.Load<Sprite>("WG03"),
            3, 0.2f, 200, 300, 0, 6000, 1500);
        Iteminfo[14] = new ItemInformation(
            "WG04", "클레이 무기",
            "상위 땅의 정령의 힘을 담은 무기, 30%확률로 마비효과",
            Resources.Load<Sprite>("WG04"),
            3, 0.3f, 300, 500, 0, 9000, 2000);
        Iteminfo[15] = new ItemInformation(
            "WG05", "오리에드 무기",
            "땅의 정령왕의 힘을 담은 무기, 50%확률로 마비효과",
            Resources.Load<Sprite>("WG05"),
            3, 0.5f, 400, 900, 0, 12000, 3000);
        //물 무기
        Iteminfo[16] = new ItemInformation(
            "WA01", "초급 물의 무기",
            "물의 힘을 담은 초급자용 무기, 5%확률로 중독효과",
            Resources.Load<Sprite>("WA01"),
            4, 0.05f, 0, 30, 0, 1000, 200);
        Iteminfo[17] = new ItemInformation(
            "WA02", "닉시 무기",
            "하위 물의 정령의 힘을 담은 무기, 10%확률로 중독효과",
            Resources.Load<Sprite>("WA02"),
            4, 0.1f, 100, 100, 0, 3000, 600);
        Iteminfo[18] = new ItemInformation(
            "WA03", "운디네 무기",
            "중위 물의 정령의 힘을 담은 무기, 20%확률로 중독효과",
            Resources.Load<Sprite>("WA03"),
            4, 0.2f, 200, 300, 0, 6000, 1500);
        Iteminfo[19] = new ItemInformation(
            "WA04", "네레이드 무기",
            "상위 물의 정령의 힘을 담은 무기, 30%확률로 중독효과",
            Resources.Load<Sprite>("WA04"),
            4, 0.3f, 300, 500, 0, 9000, 2000);
        Iteminfo[20] = new ItemInformation(
            "WA05", "나이아드 무기",
            "물의 정령왕의 힘을 담은 무기, 50%확률로 중독효과",
            Resources.Load<Sprite>("WA05"),
            4, 0.5f, 400, 900, 0, 12000, 3000);
        //암흑 무기
        Iteminfo[21] = new ItemInformation(
            "WD01", "초급 암흑 무기",
            "암흑의 힘을 담은 초급자용 무기, 10%확률로 랜덤 상태이상효과",
            Resources.Load<Sprite>("WD01"),
            5, 0.1f, 50, 80, 0, 0, 1000);
        Iteminfo[22] = new ItemInformation(
            "WD02", "데스파이어 무기",
            "하위 암흑의 정령의 힘을 담은 무기, 20%확률로 랜덤 상태이상효과",
            Resources.Load<Sprite>("WD02"),
            5, 0.2f, 150, 200, 0, 0, 2000);
        Iteminfo[23] = new ItemInformation(
            "WD03", "셰이드 무기",
            "중위 암흑의 정령의 힘을 담은 무기, 40%확률로 랜덤 상태이상효과",
            Resources.Load<Sprite>("WD03"),
            5, 0.4f, 250, 450, 0, 0, 3000);
        Iteminfo[24] = new ItemInformation(
            "WD04", "스프라이트 무기",
            "상위 암흑의 정령의 힘을 담은 무기, 50%확률로 랜덤 상태이상효과",
            Resources.Load<Sprite>("WD04"),
            5, 0.5f, 350, 700, 0, 0, 4000);
        Iteminfo[25] = new ItemInformation(
            "WD05", "아르카네 무기",
            "암흑의 정령왕의 힘을 담은 무기, 70%확률로 랜덤 상태이상효과",
            Resources.Load<Sprite>("WD05"),
            5, 0.7f, 450, 1200, 0, 0, 5000);
        //뷸 갑옷
        Iteminfo[26] = new ItemInformation(
            "DF01", "초급 불 갑옷",
            "불의 힘을 담은 초급자용 갑옷, 5%확률로 화상효과 지속시간 감소",
            Resources.Load<Sprite>("DF01"),
            1, 0.05f, 300, 0, 30, 1000, 200);
        Iteminfo[27] = new ItemInformation(
            "DF02", "살라멘더 갑옷",
            "하위 불의 정령의 힘을 담은 갑옷, 10%확률로 화상효과 지속시간 감소",
            Resources.Load<Sprite>("DF02"),
            1, 0.1f, 700, 0, 100, 3000, 600);
        Iteminfo[28] = new ItemInformation(
            "DF03", "이프리트 갑옷",
            "중위 불의 정령의 힘을 담은 갑옷, 20%확률로 화상효과 지속시간 감소",
            Resources.Load<Sprite>("DF03"),
            1, 0.2f, 1300, 0, 300, 6000, 1500);
        Iteminfo[29] = new ItemInformation(
            "DF04", "드레이크 갑옷",
            "상위 불의 정령의 힘을 담은 갑옷, 30%확률로 화상효과 지속시간 감소",
            Resources.Load<Sprite>("DF04"),
            1, 0.3f, 2400, 0, 500, 9000, 2000);
        Iteminfo[30] = new ItemInformation(
            "DF05", "피닉스 갑옷",
            "불의 정령왕의 힘을 담은 갑옷, 50%확률로 화상효과 지속시간 감소",
            Resources.Load<Sprite>("DF05"),
            1, 0.5f, 3500, 0, 900, 12000, 3000);
        //바람 갑옷
        Iteminfo[31] = new ItemInformation(
            "DW01", "초급 바람 갑옷",
            "바람의 힘을 담은 초급자용 갑옷, 5%확률로 출혈효과 데미지 감소",
            Resources.Load<Sprite>("DW01"),
            2, 0.05f, 300, 0, 30, 1000, 200);
        Iteminfo[32] = new ItemInformation(
            "DW02", "실피드 갑옷",
            "하위 바람의 정령의 힘을 담은 갑옷, 10%확률로 출혈효과 데미지 감소",
            Resources.Load<Sprite>("DW02"),
            2, 0.1f, 700, 0, 100, 3000, 600);
        Iteminfo[33] = new ItemInformation(
            "DW03", "실프 갑옷",
            "중위 바람의 정령의 힘을 담은 갑옷, 20%확률로 출혈효과 데미지 감소",
            Resources.Load<Sprite>("DW03"),
            2, 0.2f, 1300, 0, 300, 6000, 1500);
        Iteminfo[34] = new ItemInformation(
            "DW04", "진 갑옷",
            "상위 바람의 정령의 힘을 담은 갑옷, 30%확률로 출혈효과 데미지 감소",
            Resources.Load<Sprite>("DW04"),
            2, 0.3f, 2400, 0, 500, 9000, 2000);
        Iteminfo[35] = new ItemInformation(
            "DW05", "에리얼 갑옷",
            "바람의 정령왕의 힘을 담은 갑옷, 50%확률로 출혈효과 데미지 감소",
            Resources.Load<Sprite>("DW05"),
            2, 0.5f, 3500, 0, 900, 12000, 3000);
        //땅 갑옷
        Iteminfo[36] = new ItemInformation(
            "DG01", "초급 땅 갑옷",
            "땅의 힘을 담은 초급자용 갑옷, 5%확률로 마비효과 지속시간 감소",
            Resources.Load<Sprite>("DG01"),
            3, 0.05f, 300, 0, 30, 1000, 200);
        Iteminfo[37] = new ItemInformation(
            "DG02", "어스웜 갑옷",
            "하위 땅의 정령의 힘을 담은 갑옷, 10%확률로 마비효과 지속시간 감소",
            Resources.Load<Sprite>("DG02"),
            3, 0.1f, 700, 0, 100, 3000, 600);
        Iteminfo[38] = new ItemInformation(
            "DG03", "노움 갑옷",
            "중위 땅의 정령의 힘을 담은 갑옷, 20%확률로 마비효과 지속시간 감소",
            Resources.Load<Sprite>("DG03"),
            3, 0.2f, 1300, 0, 300, 6000, 1500);
        Iteminfo[39] = new ItemInformation(
            "DG04", "클레이 갑옷",
            "상위 땅의 정령의 힘을 담은 갑옷, 30%확률로 마비효과 지속시간 감소",
            Resources.Load<Sprite>("DG04"),
            3, 0.3f, 2400, 0, 500, 9000, 2000);
        Iteminfo[40] = new ItemInformation(
            "DG05", "오리에드 갑옷",
            "땅의 정령왕의 힘을 담은 갑옷, 50%확률로 마비효과 지속시간 감소",
            Resources.Load<Sprite>("DG05"),
            3, 0.5f, 3500, 0, 900, 12000, 3000);
        //물 갑옷
        Iteminfo[41] = new ItemInformation(
            "DA01", "초급 물의 갑옷",
            "물의 힘을 담은 초급자용 갑옷, 5%확률로 중독효과 지속시간 감소",
            Resources.Load<Sprite>("DA01"),
            4, 0.05f, 300, 0, 30, 1000, 200);
        Iteminfo[42] = new ItemInformation(
            "DA02", "닉시 갑옷",
            "하위 물의 정령의 힘을 담은 갑옷, 10%확률로 중독효과 지속시간 감소",
            Resources.Load<Sprite>("DA02"),
            4, 0.1f, 700, 0, 100, 3000, 600);
        Iteminfo[43] = new ItemInformation(
            "DA03", "운디네 갑옷",
            "중위 물의 정령의 힘을 담은 갑옷, 20%확률로 중독효과 지속시간 감소",
            Resources.Load<Sprite>("DA03"),
            4, 0.2f, 1300, 0, 300, 6000, 1500);
        Iteminfo[44] = new ItemInformation(
            "DA04", "네레이드 갑옷",
            "상위 물의 정령의 힘을 담은 갑옷, 30%확률로 중독효과 지속시간 감소",
            Resources.Load<Sprite>("DA04"),
            4, 0.3f, 2400, 0, 500, 9000, 2000);
        Iteminfo[45] = new ItemInformation(
            "DA05", "나이아드 갑옷",
            "물의 정령왕의 힘을 담은 갑옷, 50%확률로 중독효과 지속시간 감소",
            Resources.Load<Sprite>("DA05"),
            4, 0.5f, 3500, 0, 900, 12000, 3000);
        //암흑 갑옷
        Iteminfo[46] = new ItemInformation(
            "DD01", "초급 암흑의 갑옷",
            "암흑의 힘을 담은 초급자용 갑옷, 10%확률로 상태이상효과 저하",
            Resources.Load<Sprite>("DD01"),
            5, 0.1f, 500, 0, 80, 0 , 1000);
        Iteminfo[47] = new ItemInformation(
            "DD02", "데스파이어 갑옷",
            "하위 암흑의 정령의 힘을 담은 갑옷, 20%확률로 상태이상효과 저하",
            Resources.Load<Sprite>("DD02"),
            5, 0.2f, 1000, 0, 200, 0, 2000);
        Iteminfo[48] = new ItemInformation(
            "DD03", "셰이드 갑옷",
            "중위 암흑의 정령의 힘을 담은 갑옷, 40%확률로 상태이상효과 저하",
            Resources.Load<Sprite>("DD03"),
            5, 0.4f, 1900, 0, 450, 0, 3000);
        Iteminfo[49] = new ItemInformation(
            "DD04", "스프라이트 갑옷",
            "상위 암흑의 정령의 힘을 담은 갑옷, 50%확률로 상태이상효과 저하",
            Resources.Load<Sprite>("DD04"),
            5, 0.5f, 3000, 0, 700, 0, 4000);
        Iteminfo[50] = new ItemInformation(
            "DD05", "아르카네 갑옷",
            "암흑의 정령왕의 힘을 담은 갑옷, 70%확률로 상태이상효과 저하",
            Resources.Load<Sprite>("DD05"),
            5, 0.7f, 4500, 0, 1200, 0, 5000);
        //기타 아이템
        Iteminfo[51] = new ItemInformation(
            "IU01", "체력회복(소)",
            "체력 100회복",
            Resources.Load<Sprite>("IU01"),
            0, 0.0f, 100, 0, 0, 100, 10);
        Iteminfo[52] = new ItemInformation(
            "IU02", "체력회복(중)",
            "체력 500회복",
            Resources.Load<Sprite>("IU02"),
            0, 0.0f, 500, 0, 0, 300, 50);
        Iteminfo[53] = new ItemInformation(
            "IU03", "체력회복(대)",
            "체력 1000회복",
            Resources.Load<Sprite>("IU03"),
            0, 0.0f, 1000, 0, 0, 600, 100);
        Iteminfo[54] = new ItemInformation(
            "IU04", "해독 물약",
            "중독 효과 해제",
            Resources.Load<Sprite>("IU04"),
            0, 0.0f, 0, 0, 0, 500, 80);
        Iteminfo[55] = new ItemInformation(
            "IU05", "화상 물약",
            "화상 효과 해제",
            Resources.Load<Sprite>("IU05"),
            0, 0.0f, 0, 0, 0, 500, 80);
        Iteminfo[56] = new ItemInformation(
            "IU06", "마비 물약",
            "마비 효과 해제",
            Resources.Load<Sprite>("IU06"),
            0, 0.0f, 0, 0, 0, 500, 80);
        Iteminfo[57] = new ItemInformation(
            "IU07", "지혈 물약",
            "출혈 효과 해제",
            Resources.Load<Sprite>("IU07"),
            0, 0.0f, 0, 0, 0, 500, 80);
        Iteminfo[58] = new ItemInformation(
            "IU08", "저주 물약",
            "저주 효과 해제",
            Resources.Load<Sprite>("IU08"),
            0, 0.0f, 0, 0, 0, 500, 80);
        Iteminfo[59] = new ItemInformation(
            "IT01", "불의 합성석(소)",
            "불 속성 장비 강화재료",
            Resources.Load<Sprite>("IT01"),
            0, 0.0f, 0, 0, 0, 500, 100);
        Iteminfo[60] = new ItemInformation(
            "IT02", "불의 합성석(중)",
            "불 속성 장비 강화재료",
            Resources.Load<Sprite>("IT02"),
            0, 0.0f, 0, 0, 0, 1500, 500);
        Iteminfo[61] = new ItemInformation(
            "IT03", "불의 합성석(대)",
            "불 속성 장비 강화재료",
            Resources.Load<Sprite>("IT03"),
            0, 0.0f, 0, 0, 0, 3000, 1000);
        Iteminfo[62] = new ItemInformation(
            "IT04", "바람의 합성석(소)",
            "바람 속성 장비 강화재료",
            Resources.Load<Sprite>("IT04"),
            0, 0.0f, 0, 0, 0, 500, 100);
        Iteminfo[63] = new ItemInformation(
            "IT05", "바람의 합성석(중)",
            "바람 속성 장비 강화재료",
            Resources.Load<Sprite>("IT05"),
            0, 0.0f, 0, 0, 0, 1500, 500);
        Iteminfo[64] = new ItemInformation(
            "IT06", "바람의 합성석(대)",
            "바람 속성 장비 강화재료",
            Resources.Load<Sprite>("IT06"),
            0, 0.0f, 0, 0, 0, 3000, 1000);
        Iteminfo[65] = new ItemInformation(
            "IT07", "땅의 합성석(소)",
            "땅 속성 장비 강화재료",
            Resources.Load<Sprite>("IT07"),
            0, 0.0f, 0, 0, 0, 500, 100);
        Iteminfo[66] = new ItemInformation(
            "IT08", "땅의 합성석(중)",
            "땅 속성 장비 강화재료",
            Resources.Load<Sprite>("IT08"),
            0, 0.0f, 0, 0, 0, 1500, 500);
        Iteminfo[67] = new ItemInformation(
            "IT09", "땅의 합성석(대)",
            "땅 속성 장비 강화재료",
            Resources.Load<Sprite>("IT09"),
            0, 0.0f, 0, 0, 0, 3000, 1000);
        Iteminfo[68] = new ItemInformation(
            "IT10", "물의 합성석(소)",
            "물 속성 장비 강화재료",
            Resources.Load<Sprite>("IT10"),
            0, 0.0f, 0, 0, 0, 500, 100);
        Iteminfo[69] = new ItemInformation(
            "IT11", "물의 합성석(중)",
            "물 속성 장비 강화재료",
            Resources.Load<Sprite>("IT11"),
            0, 0.0f, 0, 0, 0, 1500, 500);
        Iteminfo[70] = new ItemInformation(
            "IT12", "물의 합성석(대)",
            "물 속성 장비 강화재료",
            Resources.Load<Sprite>("IT12"),
            0, 0.0f, 0, 0, 0, 3000, 1000);
        Iteminfo[71] = new ItemInformation(
            "IT13", "암흑의 합성석(소)",
            "암흑 속성 장비 강화재료",
            Resources.Load<Sprite>("IT13"),
            0, 0.0f, 0, 0, 0, 500, 100);
        Iteminfo[72] = new ItemInformation(
            "IT14", "암흑의 합성석(중)",
            "암흑 속성 장비 강화재료",
            Resources.Load<Sprite>("IT14"),
            0, 0.0f, 0, 0, 0, 1500, 500);
        Iteminfo[73] = new ItemInformation(
            "IT15", "암흑의 합성석(대)",
            "암흑 속성 장비 강화재료",
            Resources.Load<Sprite>("IT15"),
            0, 0.0f, 0, 0, 0, 3000, 1000);
        Iteminfo[74] = new ItemInformation(
            "IT16", "강화석(소)",
            "강화 확률 10% 상승",
            Resources.Load<Sprite>("IT16"),
            1, 0.1f, 0, 0, 0, 100, 1000);
        Iteminfo[75] = new ItemInformation(
            "IT17", "강화석(중)",
            "강화 확률 30% 상승",
            Resources.Load<Sprite>("IT17"),
            1, 0.3f, 0, 0, 0, 200, 2000);
        Iteminfo[76] = new ItemInformation(
            "IT18", "강화석(대)",
            "강화 확률 50% 상승",
            Resources.Load<Sprite>("IT18"),
            1, 0.5f, 0, 0, 0, 300, 3000);
    }
    private void SetComposeInfo()
    {
        composeValue = 50;
        composinfo = new ComposeInfo[composeValue];
                
        composinfo[0] = new ComposeInfo(
            "WF02",
            "WF01", "WF01", "WF01", "$",
            "IT01", 5, "$", 0, "$", 0, "$", 0,
            2000, 1f, 1
            );
        composinfo[1] = new ComposeInfo(
            "WW02",
            "WW01", "WW01", "WW01", "$",
            "IT04", 5, "$", 0, "$", 0, "$", 0,
            2000, 1f, 1
            );
        composinfo[2] = new ComposeInfo(
            "WG02",
            "WG01", "WG01", "WG01", "$",
            "IT07", 5, "$", 0, "$", 0, "$", 0,
            2000, 1f, 1
            );
        composinfo[3] = new ComposeInfo(
            "WA02",
            "WA01", "WA01", "WA01", "$",
            "IT10", 5, "$", 0, "$", 0, "$", 0,
            2000, 1f, 1
            );
        composinfo[4] = new ComposeInfo(
            "DF02",
            "DF01", "DF01", "DF01", "$",
            "IT01", 5, "$", 0, "$", 0, "$", 0,
            2000, 1f, 1
            );
        composinfo[5] = new ComposeInfo(
            "DW02",
            "DW01", "DW01", "DW01", "$",
            "IT04", 5, "$", 0, "$", 0, "$", 0,
            2000, 1f, 1
            );
        composinfo[6] = new ComposeInfo(
            "DG02",
            "DG01", "DG01", "DG01", "$",
            "IT07", 5, "$", 0, "$", 0, "$", 0,
            2000, 1f, 1
            );
        composinfo[7] = new ComposeInfo(
            "DA02",
            "DA01", "DA01", "DA01", "$",
            "IT10", 5, "$", 0, "$", 0, "$", 0,
            2000, 1f, 1
            );
        composinfo[8] = new ComposeInfo(
            "WF03",
            "WF02", "WF02", "WF02", "$",
            "IT01", 10, "$", 0, "$", 0, "$", 0,
            4000, 0.8f, 1
            );
        composinfo[9] = new ComposeInfo(
            "WW03",
            "WW02", "WW02", "WW02", "$",
            "IT04", 10, "$", 0, "$", 0, "$", 0,
            4000, 0.8f, 1
            );
        composinfo[10] = new ComposeInfo(
            "WG03",
            "WG02", "WG02", "WG02", "$",
            "IT07", 10, "$", 0, "$", 0, "$", 0,
            4000, 0.8f, 1
            );
        composinfo[11] = new ComposeInfo(
            "WA03",
            "WA02", "WA02", "WA02", "$",
            "IT10", 10, "$", 0, "$", 0, "$", 0,
            4000, 0.8f, 1
            );
        composinfo[12] = new ComposeInfo(
            "DF03",
            "DF02", "DF02", "DF02", "$",
            "IT01", 10, "$", 0, "$", 0, "$", 0,
            4000, 0.8f, 1
            );
        composinfo[13] = new ComposeInfo(
            "DW03",
            "DW02", "DW02", "DW02", "$",
            "IT04", 10, "$", 0, "$", 0, "$", 0,
            4000, 0.8f, 1
            );
        composinfo[14] = new ComposeInfo(
            "DG03",
            "DG02", "DG02", "DG02", "$",
            "IT07", 10, "$", 0, "$", 0, "$", 0,
            4000, 0.8f, 1
            );
        composinfo[15] = new ComposeInfo(
            "DA03",
            "DA02", "DA02", "DA02", "$",
            "IT10", 10, "$", 0, "$", 0, "$", 0,
            4000, 0.8f, 1
            );
        composinfo[16] = new ComposeInfo(
            "WF04",
            "WF03", "WF03", "WF03", "$",
            "IT01", 10, "IT02", 5, "$", 0, "$", 0,
            6000, 0.6f, 1
            );
        composinfo[17] = new ComposeInfo(
            "WW04",
            "WW03", "WW03", "WW03", "$",
            "IT04", 10, "IT05", 5, "$", 0, "$", 0,
            6000, 0.6f, 1
            );
        composinfo[18] = new ComposeInfo(
            "WG04",
            "WG03", "WG03", "WG03", "$",
            "IT07", 10, "IT08", 5, "$", 0, "$", 0,
            6000, 0.6f, 1
            );
        composinfo[19] = new ComposeInfo(
            "WA04",
            "WA03", "WA03", "WA03", "$",
            "IT10", 10, "IT11", 5, "$", 0, "$", 0,
            6000, 0.6f, 1
            );
        composinfo[20] = new ComposeInfo(
            "DF04",
            "DF03", "DF03", "DF03", "$",
            "IT01", 10, "IT02", 5, "$", 0, "$", 0,
            6000, 0.6f, 1
            );
        composinfo[21] = new ComposeInfo(
            "DW04",
            "DW03", "DW03", "DW03", "$",
            "IT04", 10, "IT05", 5, "$", 0, "$", 0,
            6000, 0.6f, 1
            );
        composinfo[22] = new ComposeInfo(
            "DG04",
            "DG03", "DG03", "DG03", "$",
            "IT07", 10, "IT08", 5, "$", 0, "$", 0,
            6000, 0.6f, 1
            );
        composinfo[23] = new ComposeInfo(
            "DA04",
            "DA03", "DA03", "DA03", "$",
            "IT10", 10, "IT11", 5, "$", 0, "$", 0,
            6000, 0.6f, 1
            );
        composinfo[24] = new ComposeInfo(
            "WF05",
            "WF04", "WF04", "WF04", "$",
            "IT01", 15, "IT02", 10, "IT03", 5, "$", 0,
            8000, 0.4f, 1
            );
        composinfo[25] = new ComposeInfo(
            "WW05",
            "WW04", "WW04", "WW04", "$",
            "IT04", 15, "IT05", 10, "IT06", 5, "$", 0,
            8000, 0.4f, 1
            );
        composinfo[26] = new ComposeInfo(
            "WG05",
            "WG04", "WG04", "WG04", "$",
            "IT07", 15, "IT08", 10, "IT09", 5, "$", 0,
            8000, 0.4f, 1
            );
        composinfo[27] = new ComposeInfo(
            "WA05",
            "WA04", "WA04", "WA04", "$",
            "IT10", 15, "IT11", 10, "IT12", 5, "$", 0,
            8000, 0.4f, 1
            );
        composinfo[28] = new ComposeInfo(
            "DF05",
            "DF04", "DF04", "DF04", "$",
            "IT01", 15, "IT02", 10, "IT03", 5, "$", 0,
            8000, 0.4f, 1
            );
        composinfo[29] = new ComposeInfo(
            "DW05",
            "DW04", "DW04", "DW04", "$",
            "IT04", 15, "IT05", 10, "IT06", 5, "$", 0,
            8000, 0.4f, 1
            );
        composinfo[30] = new ComposeInfo(
            "DG05",
            "DG04", "DG04", "DG04", "$",
            "IT07", 15, "IT08", 10, "IT09", 5, "$", 0,
            8000, 0.4f, 1
            );
        composinfo[31] = new ComposeInfo(
            "DA05",
            "DA04", "DA04", "DA04", "$",
            "IT10", 15, "IT11", 10, "IT12", 5, "$", 0,
            8000, 0.4f, 1
            );        
        composinfo[32] = new ComposeInfo(
           "WD01",
           "WF01", "WW01", "WG01", "WA01",
           "IT01", 5, "IT04", 5, "IT07", 5, "IT10", 5,
           3000, 0.9f, 1
           );
        composinfo[33] = new ComposeInfo(
           "WD02",
           "WF02", "WW02", "WG02", "WA02",
           "IT01", 10, "IT04", 10, "IT07", 10, "IT10", 10,
           5000, 0.7f, 1
           );
        composinfo[34] = new ComposeInfo(
            "WD03",
            "WF03", "WW03", "WG03", "WA03",
            "IT02", 5, "IT05", 5, "IT08", 5, "IT11", 5,
            7000, 0.5f, 1
            );
        composinfo[35] = new ComposeInfo(
            "WD04",
            "WF04", "WW04", "WG04", "WA04",
            "IT02", 10, "IT05", 10, "IT08", 10, "IT11", 10,
            9000, 0.4f, 1
            );
        composinfo[36] = new ComposeInfo(
            "WD05",
            "WF05", "WW05", "WG05", "WA05",
            "IT03", 5, "IT06", 5, "IT09", 5, "IT12", 5,
            11000, 0.3f, 1
            );
        composinfo[37] = new ComposeInfo(
          "DD01",
          "DF01", "DW01", "DG01", "DA01",
          "IT01", 5, "IT04", 5, "IT07", 5, "IT10", 5,
          3000, 0.9f, 1
          );
        composinfo[38] = new ComposeInfo(
           "DD02",
           "DF02", "DW02", "DG02", "DA02",
           "IT01", 10, "IT04", 10, "IT07", 10, "IT10", 10,
           5000, 0.7f, 1
           );
        composinfo[39] = new ComposeInfo(
            "DD03",
            "DF03", "DW03", "DG03", "DA03",
            "IT02", 5, "IT05", 5, "IT08", 5, "IT11", 5,
            7000, 0.5f, 1
            );
        composinfo[40] = new ComposeInfo(
            "DD04",
            "DF04", "DW04", "DG04", "DA04",
            "IT02", 10, "IT05", 10, "IT08", 10, "IT11", 10,
            9000, 0.4f, 1
            );
        composinfo[41] = new ComposeInfo(
            "DD05",
            "DF05", "DW05", "DG05", "DA05",
            "IT03", 5, "IT06", 5, "IT09", 5, "IT12", 5,
            11000, 0.3f, 1
            );
        composinfo[42] = new ComposeInfo(
            "WD02",
            "WD01", "WD01", "WD01", "$",
            "IT13", 5, "$", 0, "$", 0, "$", 0,
            3000, 1f, 2
            );
        composinfo[43] = new ComposeInfo(
            "WD03",
            "WD02", "WD02", "WD02", "$",
            "IT13", 10, "$", 0, "$", 0, "$", 0,
            5000, 0.9f, 2
            );
        composinfo[44] = new ComposeInfo(
            "WD04",
            "WD03", "WD03", "WD03", "$",
            "IT13", 10, "IT14", 5, "$", 0, "$", 0,
            7000, 0.8f, 2
            );
        composinfo[45] = new ComposeInfo(
            "WD05",
            "WD04", "WD04", "WD04", "$",
            "IT13", 15, "IT14", 10, "IT15", 5, "$", 0,
            9000, 0.7f, 2
            );
        composinfo[46] = new ComposeInfo(
            "DD02",
            "DD01", "DD01", "DD01", "$",
            "IT13", 5, "$", 0, "$", 0, "$", 0,
            3000, 1f, 2
            );
        composinfo[47] = new ComposeInfo(
            "DD03",
            "DD02", "DD02", "DD02", "$",
            "IT13", 10, "$", 0, "$", 0, "$", 0,
            5000, 0.9f, 2
            );
        composinfo[48] = new ComposeInfo(
            "DD04",
            "DD03", "DD03", "DD03", "$",
            "IT13", 10, "IT14", 5, "$", 0, "$", 0,
            7000, 0.8f, 2
            );
        composinfo[49] = new ComposeInfo(
            "DD05",
            "DD04", "DD04", "DD04", "$",
            "IT13", 15, "IT14", 10, "IT15", 5, "$", 0,
            9000, 0.7f, 2
            );
    }
}