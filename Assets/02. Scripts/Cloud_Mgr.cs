using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using UnityEngine.SocialPlatforms;


public class Cloud_Mgr : Singleton<Cloud_Mgr>
{
    // 게임 데이터 클래스
    public class GameData
    {        
        static readonly string HEADER = "GDv6";

        string id;
        int lv;
        int exp;
        int stage;
        string stage_star;
        int coin;
        int gem;
        int heart;
        string lasttime; //yyyy/MM/dd HH:mm:ss
        string AddTime0;
        string AddTime1;
        string AddTime2;
        int Add0;
        int Add1;
        int Add2;
        float BackgroundVol;
        float ButtonSoundVol;
        float EffectSoundVol;
        bool BackgroundMute;
        bool ButtonSoundMute;
        bool EffectMute;
        string YEONGHUIWEAPON; //Code
        string YEONGHUIDEFENCE;
        string DRAGONWEAPON;
        string DRAGONDEFENCE;        
        public int[] ItemSlot = new int[4]; //Pos
        public string[] HoldItem = new string[90]; //Code:value

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                GetInstance.SaveToCloud();
            }
        }
        public int LV
        {
            get
            {
                return lv;
            }
            set
            {
                lv = value;
                GetInstance.SaveToCloud();
            }
        }
        public int EXP
        {
            get
            {
                return exp;
            }
            set
            {
                exp = value;
                GetInstance.SaveToCloud();
            }
        }
        public int Stage
        {
            get
            {
                return stage;
            }
            set
            {
                stage = value;
                GetInstance.SaveToCloud();
            }
        }
        public string StageStar
        {
            get
            {
                return stage_star;
            }
            set
            {
                stage_star = value;
                GetInstance.SaveToCloud();
            }
        }
        public int Coin
        {
            get
            {
                return coin;
            }
            set
            {
                coin = value;
                GetInstance.SaveToCloud();
            }
        }
        public int Gem
        {
            get
            {
                return gem;
            }
            set
            {
                gem = value;
                GetInstance.SaveToCloud();
            }
        }
        public int Heart
        {
            get
            {
                return heart;
            }
            set
            {
                heart = value;
                GetInstance.SaveToCloud();
            }
        }
        public string LastTime
        {
            get
            {
                return lasttime;
            }
            set
            {
                lasttime = value;
                GetInstance.SaveToCloud();
            }
        }
        public string ADDTime0
        {
            get
            {
                return AddTime0;
            }
            set
            {
                AddTime0 = value;
                GetInstance.SaveToCloud();
            }
        }
        public string ADDTime1
        {
            get
            {
                return AddTime1;
            }
            set
            {
                AddTime1 = value;
                GetInstance.SaveToCloud();
            }
        }
        public string ADDTime2
        {
            get
            {
                return AddTime2;
            }
            set
            {
                AddTime2 = value;
                GetInstance.SaveToCloud();
            }
        }
        public int ADD0
        {
            get
            {
                return Add0;
            }
            set
            {
                Add0 = value;
                GetInstance.SaveToCloud();
            }
        }
        public int ADD1
        {
            get
            {
                return Add1;
            }
            set
            {
                Add1 = value;
                GetInstance.SaveToCloud();
            }
        }
        public int ADD2
        {
            get
            {
                return Add2;
            }
            set
            {
                Add2 = value;
                GetInstance.SaveToCloud();
            }
        }
        public float BGMvol
        {
            get
            {
                return BackgroundVol;
            }
            set
            {
                BackgroundVol = value;
                GetInstance.SaveToCloud();
            }
        }
        public float BtnVol
        {
            get
            {
                return ButtonSoundVol;
            }
            set
            {
                ButtonSoundVol = value;
                GetInstance.SaveToCloud();
            }
        }
        public float EffVol
        {
            get
            {
                return EffectSoundVol;
            }
            set
            {
                EffectSoundVol = value;
                GetInstance.SaveToCloud();
            }
        }
        public bool BGMMute
        {
            get
            {
                return BackgroundMute;
            }
            set
            {
                BackgroundMute = value;
                GetInstance.SaveToCloud();
            }
        }
        public bool BtnMute
        {
            get
            {
                return ButtonSoundMute;
            }
            set
            {
                ButtonSoundMute = value;
                GetInstance.SaveToCloud();
            }
        }
        public bool EffMute
        {
            get
            {
                return EffectMute;
            }
            set
            {
                EffectMute = value;
                GetInstance.SaveToCloud();
            }
        }
        public string YeonghuiWeapon
        {
            get
            {
                return YEONGHUIWEAPON;
            }
            set
            {
                YEONGHUIWEAPON = value;
                GetInstance.SaveToCloud();
            }
        }
        public string YeonghuiDefence
        {
            get
            {
                return YEONGHUIDEFENCE;
            }
            set
            {
                YEONGHUIDEFENCE = value;
                GetInstance.SaveToCloud();
            }
        }
        public string DragonWeapon
        {
            get
            {
                return DRAGONWEAPON;
            }
            set
            {
                DRAGONWEAPON = value;
                GetInstance.SaveToCloud();
            }
        }
        public string DragonDefence
        {
            get
            {
                return DRAGONDEFENCE;
            }
            set
            {
                DRAGONDEFENCE = value;
                GetInstance.SaveToCloud();
            }
        }

        public GameData(string _initData)
        {
            id = _initData; //1
            lv = 1; //2
            exp = 0; //3
            stage = 1; //4
            stage_star = "0"; //5
            coin = 1000; //6
            gem = 0; //7
            heart = 5; //8
            lasttime = "2016/01/01 00:00:00"; //9
            AddTime0 = "2016/01/01 00:00:00"; //10
            AddTime1 = "2016/01/01 00:00:00"; //11
            AddTime2 = "2016/01/01 00:00:00"; //12
            Add0 = 5; //13
            Add1 = 5; //14
            Add2 = 3; //15
            BackgroundVol = 1f; //16
            ButtonSoundVol = 1f; //17
            EffectSoundVol = 1f; //18
            BackgroundMute = false; //19
            ButtonSoundMute = false; //20
            EffectMute = false; //21
            YEONGHUIWEAPON = "$"; //22  "$"는 비어있음을 의미
            YEONGHUIDEFENCE = "$"; //23 
            DRAGONWEAPON = "$"; //24
            DRAGONDEFENCE = "$"; //25        
            for (int i = 0; i < 4; i++)
                ItemSlot[i] = -1;
            for (int i = 0; i < 90; i++)
                HoldItem[i] = "$:0";

            GetInstance.SaveToCloud();            
        }

        public override string ToString()
        {
            string s = HEADER; //0
            s += "@" + id; //1
            s += "@" + lv.ToString(); //2
            s += "@" + exp.ToString(); //3
            s += "@" + stage.ToString(); //4
            s += "@" + stage_star; //5
            s += "@" + coin.ToString(); //6
            s += "@" + gem.ToString(); //7
            s += "@" + heart.ToString(); //8
            s += "@" + lasttime; //9
            s += "@" + AddTime0; //10
            s += "@" + AddTime1; //11
            s += "@" + AddTime2; //12
            s += "@" + Add0; //13
            s += "@" + Add1; //14
            s += "@" + Add2; //15
            s += "@" + BackgroundVol.ToString(); //16
            s += "@" + ButtonSoundVol.ToString(); //17
            s += "@" + EffectSoundVol.ToString(); //18
            s += "@" + BackgroundMute.ToString(); //19
            s += "@" + ButtonSoundMute.ToString(); //20
            s += "@" + EffectMute.ToString(); //21
            s += "@" + YEONGHUIWEAPON; //22
            s += "@" + YEONGHUIDEFENCE; //23
            s += "@" + DRAGONWEAPON; //24
            s += "@" + DRAGONDEFENCE; //25
            for (int i = 0; i < 4; i++) //26~29
                s += "@" + ItemSlot[i].ToString();
            for (int i = 0; i < 90; i++)
                s += "@" + HoldItem[i];

            return s;
        }

        public byte[] ToBytes()
        {
            return System.Text.ASCIIEncoding.Default.GetBytes(ToString());
        }

        public static GameData FromBytes(byte[] bytes)
        {
            return FromString(System.Text.ASCIIEncoding.Default.GetString(bytes));
        }

        public static GameData FromString(string _s)
        {
            GameData _gd = new GameData("TEMP USER DATA");
            string[] p = _s.Split(new char[] { '@' });

            if (!p[0].StartsWith(HEADER))
            {
                return _gd;
            }
            _gd.id = p[1];
            _gd.lv = Convert.ToInt32(p[2]);
            _gd.exp = Convert.ToInt32(p[3]);
            _gd.stage = Convert.ToInt32(p[4]);
            _gd.stage_star = p[5];
            _gd.coin = Convert.ToInt32(p[6]);
            _gd.gem = Convert.ToInt32(p[7]);
            _gd.heart = Convert.ToInt32(p[8]);
            _gd.lasttime = p[9];
            _gd.AddTime0 = p[10];
            _gd.AddTime1 = p[11];
            _gd.AddTime2 = p[12];
            _gd.Add0 = Convert.ToInt32(p[13]);
            _gd.Add1 = Convert.ToInt32(p[14]); 
            _gd.Add2 = Convert.ToInt32(p[15]);
            _gd.BackgroundVol = (float)Convert.ToDouble(p[16]);
            _gd.ButtonSoundVol = (float)Convert.ToDouble(p[17]);
            _gd.EffectSoundVol = (float)Convert.ToDouble(p[18]);
            _gd.BackgroundMute = Convert.ToBoolean(p[19]);
            _gd.ButtonSoundMute = Convert.ToBoolean(p[20]);
            _gd.EffectMute = Convert.ToBoolean(p[21]);
            _gd.YEONGHUIWEAPON = p[22];
            _gd.YEONGHUIDEFENCE = p[23];
            _gd.DRAGONWEAPON = p[24];
            _gd.DRAGONDEFENCE = p[25];
            for (int i = 0; i < 4; i++)
                _gd.ItemSlot[i] = Convert.ToInt32(p[26 + i]);
            for (int i = 0; i < 90; i++)
                _gd.HoldItem[i] = p[30+i];
            
            return _gd;
        }
    }

    public GameData UserData;

    private void OpenSavedGame(string filename, bool bSave)
    {
        if (bSave)     //저장루틴진행
            PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToSave);
        else             //로딩루틴 진행
        {
            PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToRead);
        }
    }

    //-----------------------------게임 저장---------------------------------------

    public void SaveToCloud()
    {
        if (!Social.localUser.authenticated)
            return;
        OpenSavedGame("CloudData", true);
    }

    public void OnSavedGameOpenedToSave(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            SaveGame(game, UserData.ToBytes(), DateTime.Now.TimeOfDay);            
        }
        else
        {
            Debug.Log("error : File open fail");
        }
    }

    public void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder.WithUpdatedPlayedTime(totalPlaytime).WithUpdatedDescription("Saved game at " + DateTime.Now);
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        PlayGamesPlatform.Instance.SavedGame.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
            Debug.Log("Data written success");
        else
            Debug.Log("Data written fail");
    }

    //----------------------------------------파일읽기------------------------------------------------------------------------
    public void LoadFromCloud()
    {
        
        if (!Social.localUser.authenticated)
            return;
        OpenSavedGame("CloudData", false);
    }

    public void OnSavedGameOpenedToRead(SavedGameRequestStatus status, ISavedGameMetadata game)
    {        
        if (status == SavedGameRequestStatus.Success)
        {            
            LoadGameData(game);
        }
        else
        {
            Debug.Log("error : File open fail");
        }
    }

    //데이터 읽기를 시도합니다.
    public void LoadGameData(ISavedGameMetadata game)
    {
        PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            //data 배열을 복구해서 적절하게 사용하시면됩니다.
            UserData = GameData.FromBytes(data);
        }
        else
        {
            Debug.Log("Data read fail");
        }
    }
}