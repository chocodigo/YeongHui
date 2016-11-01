using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scene_Mgr : Singleton<Scene_Mgr>
{
    public bool bLoading;
    public bool bStart;
    public bool bInputID;
    public bool bMain;
    public bool bSetting;
    public bool bLobby;
    public bool bGameRoom;
    public bool bInventory;
    public bool bCompose;
    public bool bStore;
    public bool bStoreHeart;
    public bool bStoreGem;
    public bool bStoreCoin;
    public int bGame;
    public bool bGameUI;

    void Start()
    {
        bLoading = false;
        bStart = false;
        bInputID = false;
        bMain = false;
        bSetting = false;
        bLobby = false;
        bGameRoom = false;
        bInventory = false;
        bCompose = false;
        bStore = false;
        bStoreHeart = false;
        bStoreGem = false;
        bStoreCoin = false;
        bGame = 0;
        bGameUI = false;
    }

    public IEnumerator LoadLoading()
    {
        bLoading = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scLoading", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadLoading()
    {
        bLoading = !SceneManager.UnloadScene("scLoading");
    }

    public IEnumerator LoadStart()
    {
        bStart = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scStart", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadStart()
    {
        bStart = false;
        SceneManager.UnloadScene("scStart");
    }

    public IEnumerator LoadInputID()
    {
        bInputID = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scInputID", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadInputID()
    {
        bInputID = false;
        SceneManager.UnloadScene("scInputID");
    }

    public IEnumerator LoadMain()
    {
        bMain = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scMainUI", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadMain()
    {
        bMain = false;
        SceneManager.UnloadScene("scMainUI");
    }

    public IEnumerator LoadSetting()
    {
        bSetting = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scSetting", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadSetting()
    {
        bSetting = false;
        SceneManager.UnloadScene("scSetting");
    }

    public IEnumerator LoadLobby()
    {
        bLobby = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scLobby", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadLobby()
    {
        bLobby = false;
        SceneManager.UnloadScene("scLobby");
    }

    public IEnumerator LoadGameRoom()
    {
        bGameRoom = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scGameRoom", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadGameRoom()
    {
        bGameRoom = false;
        SceneManager.UnloadScene("scGameRoom");
    }

    public IEnumerator LoadInventory()
    {
        bInventory = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scInventory", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadInventory()
    {
        bInventory = false;
        SceneManager.UnloadScene("scInventory");
    }

    public IEnumerator LoadCompose()
    {
        bCompose = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scCompose", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadCompose()
    {
        bCompose = false;
        SceneManager.UnloadScene("scCompose");
    }

    public IEnumerator LoadStore()
    {
        bStore = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scStore", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadStore()
    {
        bStore = false;
        SceneManager.UnloadScene("scStore");
    }

    public IEnumerator LoadGameUI()
    {
        bGameUI = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync("scGameUI", LoadSceneMode.Additive);
        yield return ao;
    }

    public void UnLoadGameUI()
    {
        bGameUI = false;
        SceneManager.UnloadScene("scGameUI");
    }

    public IEnumerator LoadGame(int stage)
    {
        bGame = stage;
        GameInfo_Mgr.GetInstance.NowStage = bGame;

        LoadLoading();

        switch (bGame)
        {
            case 1:
                AsyncOperation ao = SceneManager.LoadSceneAsync("scStage1", LoadSceneMode.Additive);
                yield return ao;
                break;

            case 2:
                AsyncOperation ao2 = SceneManager.LoadSceneAsync("scStage2", LoadSceneMode.Additive);
                yield return ao2;
                break;
            case 3:
                AsyncOperation ao3 = SceneManager.LoadSceneAsync("scStage3", LoadSceneMode.Additive);
                yield return ao3;
                break;
            case 4:
                AsyncOperation ao4 = SceneManager.LoadSceneAsync("scStage4", LoadSceneMode.Additive);
                yield return ao4;
                break;
            case 5:
                AsyncOperation ao5 = SceneManager.LoadSceneAsync("scStage5", LoadSceneMode.Additive);
                yield return ao5;
                break;
            case 6:
                AsyncOperation ao6 = SceneManager.LoadSceneAsync("scStage6", LoadSceneMode.Additive);
                yield return ao6;
                break;
            case 7:
                AsyncOperation ao7 = SceneManager.LoadSceneAsync("scStage7", LoadSceneMode.Additive);
                yield return ao7;
                break;
            case 8:
                AsyncOperation ao8 = SceneManager.LoadSceneAsync("scStage8", LoadSceneMode.Additive);
                yield return ao8;
                break;
            case 9:
                AsyncOperation ao9 = SceneManager.LoadSceneAsync("scStage9", LoadSceneMode.Additive);
                yield return ao9;
                break;
            case 10:
                AsyncOperation ao10 = SceneManager.LoadSceneAsync("scStage10", LoadSceneMode.Additive);
                yield return ao10;
                break;
            case 11:
                AsyncOperation ao11 = SceneManager.LoadSceneAsync("scStage11", LoadSceneMode.Additive);
                yield return ao11;
                break;
            case 12:
                AsyncOperation ao12 = SceneManager.LoadSceneAsync("scStage12", LoadSceneMode.Additive);
                yield return ao12;
                break;
            case 13:
                AsyncOperation ao13 = SceneManager.LoadSceneAsync("scStage13", LoadSceneMode.Additive);
                yield return ao13;
                break;
        }
    }

    public void UnLoadGame(int stage)
    {
        switch (stage)
        {
            case 1:
                SceneManager.UnloadScene("scStage1");
                break;
            case 2:
                SceneManager.UnloadScene("scStage2");
                break;
            case 3:
                SceneManager.UnloadScene("scStage3");
                break;
            case 4:
                SceneManager.UnloadScene("scStage4");
                break;
            case 5:
                SceneManager.UnloadScene("scStage5");
                break;
            case 6:
                SceneManager.UnloadScene("scStage6");
                break;
            case 7:
                SceneManager.UnloadScene("scStage7");
                break;
            case 8:
                SceneManager.UnloadScene("scStage8");
                break;
            case 9:
                SceneManager.UnloadScene("scStage9");
                break;
            case 10:
                SceneManager.UnloadScene("scStage10");
                break;
            case 11:
                SceneManager.UnloadScene("scStage11");
                break;
            case 12:
                SceneManager.UnloadScene("scStage12");
                break;
            case 13:
                SceneManager.UnloadScene("scStage13");

                break;
        }
        bGame = 0;
        GameInfo_Mgr.GetInstance.NowStage = bGame;
    }

    void Update()
    {
        if (bGame != 0 && bLoading)
        {
            switch (bGame)
            {
                case 1:
                    if (Application.CanStreamedLevelBeLoaded("scStage1"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 2:
                    if (Application.CanStreamedLevelBeLoaded("scStage2"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 3:
                    if (Application.CanStreamedLevelBeLoaded("scStage3"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 4:
                    if (Application.CanStreamedLevelBeLoaded("scStage4"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 5:
                    if (Application.CanStreamedLevelBeLoaded("scStage5"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 6:
                    if (Application.CanStreamedLevelBeLoaded("scStage6"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 7:
                    if (Application.CanStreamedLevelBeLoaded("scStage7"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 8:
                    if (Application.CanStreamedLevelBeLoaded("scStage8"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 9:
                    if (Application.CanStreamedLevelBeLoaded("scStage9"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 10:
                    if (Application.CanStreamedLevelBeLoaded("scStage10"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 11:
                    if (Application.CanStreamedLevelBeLoaded("scStage11"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 12:
                    if (Application.CanStreamedLevelBeLoaded("scStage12"))
                    {
                        UnLoadLoading();
                    }
                    break;
                case 13:
                    if (Application.CanStreamedLevelBeLoaded("scStage13"))
                    {
                        UnLoadLoading();
                    }
                    break;
            }
        }
    }
}