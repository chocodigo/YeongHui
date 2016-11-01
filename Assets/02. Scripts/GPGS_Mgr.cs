using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GPGS_Mgr : Singleton<GPGS_Mgr>
{
    public bool bLogin
    {
        get;
        set;
    }

    /// GPGS를 초기화 합니다.
    public void InitializeGPGS()
    {
        bLogin = false;

        PlayGamesClientConfiguration _config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(_config);
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = false;
    }

    /// GPGS를 로그인 합니다.
    public void LoginGPGS()
    {
        if (!Social.localUser.authenticated)
            Social.localUser.Authenticate(LoginCallBackGPGS);
    }

    public void LoginCallBackGPGS(bool result)
    {
        bLogin = result;
    }
    
    /// GPGS를 로그아웃 합니다.
    public void LogoutGPGS()
    {
        // 로그인이 되어 있으면
        if (Social.localUser.authenticated)
        {
            ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
            Cloud_Mgr.GetInstance.UserData.ID = null;
            bLogin = false;
        }
    }
}
