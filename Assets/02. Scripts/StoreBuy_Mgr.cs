using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Advertisements;
using UnityEngine.Purchasing;
using System.Collections.Generic;

public class StoreBuy_Mgr : MonoBehaviour, IStoreListener
{
    public GameObject _ItemPanel; //ItemPanel
    public GameObject _AddPanel; //AddPanel
    public Transform _ScrollPanel; //scrollView
    public Image StorePanel; //Panel
    public Image BuyItemPanel; //ItemBox
    public Image BuyPanel; //BuyBox    
    public Image WarningPanel; //Warining
    
    private Transform[] ChildStore;
    private Transform[] ChildBuyItem;
    private Transform[] ChildBuy;
    private Transform[] ChildWarining;
    private int NowValue;
    private Sprite Menu_Btn1;
    private Sprite Menu_Btn2;
    private ShowOptions _ShowOpt;
    private bool HeartState;
    private bool CoinState;
    private bool GemState;

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    private static string kProductIDConsumable1 = "com.windmoon.yeonghui.gem1";
    private static string kProductIDConsumable2 = "com.windmoon.yeonghui.gem2";
    private static string kProductIDConsumable3 = "com.windmoon.yeonghui.gem3";
    private static string kProductIDConsumable4 = "com.windmoon.yeonghui.gem4";
    private static string kProductIDConsumable5 = "com.windmoon.yeonghui.gem5";

    void Start ()
    {
        _ShowOpt = new ShowOptions();
        Advertisement.Initialize("128651", false);

        if (m_StoreController == null)
            InitializePurchasing();

        HeartState = false;
        CoinState = false;
        GemState = false;

        Menu_Btn1 = Resources.Load<Sprite>("Menu_Btn1");
        Menu_Btn2 = Resources.Load<Sprite>("Menu_Btn2");

        ChildStore = StorePanel.GetComponentsInChildren<RectTransform>();
        ChildBuyItem = BuyItemPanel.GetComponentsInChildren<RectTransform>();
        ChildBuy = BuyPanel.GetComponentsInChildren<RectTransform>();
        ChildWarining = WarningPanel.GetComponentsInChildren<RectTransform>();

        foreach (RectTransform cts in ChildStore)
            cts.gameObject.SetActive(true);
        Cancle_Btn();
        warningOK_Btn();

        if (Scene_Mgr.GetInstance.bStoreGem)
            ClickGem();
        else if (Scene_Mgr.GetInstance.bStoreCoin)
            ClickCoin();
        else if (Scene_Mgr.GetInstance.bStoreHeart)
            ClickHeart();
        else
            ClickWeapon();

        Scene_Mgr.GetInstance.bStoreHeart = false;
        Scene_Mgr.GetInstance.bStoreGem = false;
        Scene_Mgr.GetInstance.bStoreCoin = false;
    }

    void Update()
    {
        if (Scene_Mgr.GetInstance.bStoreGem)
        {
            Scene_Mgr.GetInstance.bStoreGem = false;
            Cancle_Btn();
            warningOK_Btn();
            ClickGem();
        }
        else if (Scene_Mgr.GetInstance.bStoreCoin)
        {
            Scene_Mgr.GetInstance.bStoreCoin = false;
            Cancle_Btn();
            warningOK_Btn();
            ClickCoin();
        }
        else if (Scene_Mgr.GetInstance.bStoreHeart)
        {
            Scene_Mgr.GetInstance.bStoreHeart = false;
            Cancle_Btn();
            warningOK_Btn();
            ClickHeart();
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (Scene_Mgr.GetInstance.bSetting)
                    Scene_Mgr.GetInstance.UnLoadSetting();
                else if (WarningPanel.IsActive())
                    warningOK_Btn();
                else if (BuyItemPanel.IsActive() || BuyPanel.IsActive())
                    Cancle_Btn();                
                else
                    BacktoLobbyBtn();
            }               
        }

        if (CoinState)
        {
            if (Cloud_Mgr.GetInstance.UserData.ADD1 < 5) //재충전을 위해 남은시간
            {
                TimeSpan default_ts = new TimeSpan(1, 0, 0); //추후 재충전시간 수정
                TimeSpan ts = default_ts - DateTime.Now.Subtract(Convert.ToDateTime(Cloud_Mgr.GetInstance.UserData.ADDTime1));

                GameObject AddPanel = GameObject.FindWithTag("AddPanel");

                if (AddPanel)
                {
                    AddPanel.transform.FindChild("Cost_Text").GetComponent<Text>().text = "Free(" + Cloud_Mgr.GetInstance.UserData.ADD1.ToString() + "/5)";
                    AddPanel.transform.FindChild("Time").GetComponent<Text>().text = ts.Minutes.ToString() + ":" + ts.Seconds.ToString();
                }

                if (ts.CompareTo(new TimeSpan(0, 0, 0)) < 0) //재충전 시간이 되었을 경우
                {
                    int Plus = (int)ts.TotalSeconds * -1 / (int)default_ts.TotalSeconds + 1;

                    if (Plus + Cloud_Mgr.GetInstance.UserData.ADD1 > 5)
                    {
                        Cloud_Mgr.GetInstance.UserData.ADD1 = 5;
                        Cloud_Mgr.GetInstance.UserData.ADDTime1 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    else
                    {
                        Cloud_Mgr.GetInstance.UserData.ADD1 += Plus;
                        TimeSpan rest = new TimeSpan(0, 0, (int)(ts.TotalSeconds % (int)default_ts.TotalSeconds));
                        Cloud_Mgr.GetInstance.UserData.ADDTime1 = (DateTime.Now - rest).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    Cloud_Mgr.GetInstance.SaveToCloud();
                }
            }
            else
            {
                GameObject AddPanel = GameObject.FindWithTag("AddPanel");

                if (AddPanel)
                {
                    AddPanel.transform.FindChild("Cost_Text").GetComponent<Text>().text = "Free(" + Cloud_Mgr.GetInstance.UserData.ADD1.ToString() + "/5)";
                    AddPanel.transform.FindChild("Time").GetComponent<Text>().text = "";
                }
            }
        }

        if (GemState)
        {
            if(DateTime.Now.TimeOfDay.CompareTo(new TimeSpan(6,0,0)) > 0 && Convert.ToDateTime(Cloud_Mgr.GetInstance.UserData.ADDTime2).Date < DateTime.Now.Date)
            {
                Cloud_Mgr.GetInstance.UserData.ADD2 = 3;
                Cloud_Mgr.GetInstance.UserData.ADDTime2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                Cloud_Mgr.GetInstance.SaveToCloud();
            }
            
            GameObject AddPanel = GameObject.FindWithTag("AddPanel");

            if (AddPanel)
            {
                AddPanel.transform.FindChild("Cost_Text").GetComponent<Text>().text = "Free(" + Cloud_Mgr.GetInstance.UserData.ADD2.ToString() + "/3)";
                AddPanel.transform.FindChild("Time").GetComponent<Text>().text = "6시 초기화";
            }
        }

        if (HeartState)
        {
            if (DateTime.Now.TimeOfDay.CompareTo(new TimeSpan(6, 0, 0)) > 0 && Convert.ToDateTime(Cloud_Mgr.GetInstance.UserData.ADDTime0).Date < DateTime.Now.Date)
            {
                Cloud_Mgr.GetInstance.UserData.ADD0 = 5;
                Cloud_Mgr.GetInstance.UserData.ADDTime0 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                Cloud_Mgr.GetInstance.SaveToCloud();
            }

            GameObject AddPanel = GameObject.FindWithTag("AddPanel");

            if (AddPanel)
            {
                AddPanel.transform.FindChild("Cost_Text").GetComponent<Text>().text = "Free(" + Cloud_Mgr.GetInstance.UserData.ADD0.ToString() + "/5)";
                AddPanel.transform.FindChild("Time").GetComponent<Text>().text = "6시 초기화";
            }
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(kProductIDConsumable1, ProductType.Consumable);
        builder.AddProduct(kProductIDConsumable2, ProductType.Consumable);
        builder.AddProduct(kProductIDConsumable3, ProductType.Consumable);
        builder.AddProduct(kProductIDConsumable4, ProductType.Consumable);
        builder.AddProduct(kProductIDConsumable5, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
                m_StoreController.InitiatePurchase(product);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error) { }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) { }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable1, StringComparison.Ordinal))
        {
            Cloud_Mgr.GetInstance.UserData.Gem += 10;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable2, StringComparison.Ordinal))
        {
            Cloud_Mgr.GetInstance.UserData.Gem += 70;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable3, StringComparison.Ordinal))
        {
            Cloud_Mgr.GetInstance.UserData.Gem += 200;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable4, StringComparison.Ordinal))
        {
            Cloud_Mgr.GetInstance.UserData.Gem += 500;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable5, StringComparison.Ordinal))
        {
            Cloud_Mgr.GetInstance.UserData.Gem += 1000;
        }

        Cloud_Mgr.GetInstance.SaveToCloud();
        return PurchaseProcessingResult.Complete;
    }

    public void ClickWeapon()
    {
        HeartState = false;
        CoinState = false;
        GemState = false;

        StorePanel.transform.FindChild("Weapon").GetComponent<Button>().image.overrideSprite = Menu_Btn1;
        StorePanel.transform.FindChild("Defence").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Item").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Heart").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Cost").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Gem").GetComponent<Button>().image.overrideSprite = Menu_Btn2;

        StorePanel.transform.FindChild("ItemList").FindChild("Scrollbar").GetComponent<Scrollbar>().value = 0;
        _ScrollPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1017);

        Transform[] ChildScroll = _ScrollPanel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildScroll)
        {
            if(cts.tag == "ItemPanel" || cts.tag == "AddPanel")
                Destroy(cts.gameObject);
        }

        ItemInfo.ItemInformation[] itemInfo = ItemInfo.GetInstance.GetAllItemInfo();

        for (int i=0;i< ItemInfo.GetInstance.itemValue; i++)
        {
            if(itemInfo[i].Code.StartsWith("WF") || itemInfo[i].Code.StartsWith("WW") || itemInfo[i].Code.StartsWith("WG") || itemInfo[i].Code.StartsWith("WA"))
            {
                _ScrollPanel.GetComponent<RectTransform>().sizeDelta += new Vector2(620, 0);

                var ItemPanel = Instantiate(_ItemPanel, Vector3.zero, Quaternion.identity) as GameObject;
                ItemPanel.transform.parent = _ScrollPanel;
                ItemPanel.transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = itemInfo[i].ItemImage;
                ItemPanel.transform.FindChild("Name").GetComponent<Text>().text = itemInfo[i].Name;
                ItemPanel.transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Coin"); 
                ItemPanel.transform.FindChild("Cost_Text").GetComponent<Text>().text = itemInfo[i].cost.ToString("#,##0");

                int index = i;

                ItemPanel.GetComponent<Button>().onClick.AddListener(() => { Open_BuyPanel(itemInfo[index]); });
            }
        }
    }

    public void ClickDefence()
    {
        HeartState = false;
        CoinState = false;
        GemState = false;

        StorePanel.transform.FindChild("Weapon").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Defence").GetComponent<Button>().image.overrideSprite = Menu_Btn1;
        StorePanel.transform.FindChild("Item").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Heart").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Cost").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Gem").GetComponent<Button>().image.overrideSprite = Menu_Btn2;

        StorePanel.transform.FindChild("ItemList").FindChild("Scrollbar").GetComponent<Scrollbar>().value = 0;
        _ScrollPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1017);

        Transform[] ChildScroll = _ScrollPanel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildScroll)
        {
            if (cts.tag == "ItemPanel" || cts.tag == "AddPanel")
                Destroy(cts.gameObject);
        }

        ItemInfo.ItemInformation[] itemInfo = ItemInfo.GetInstance.GetAllItemInfo();

        for (int i = 0; i < ItemInfo.GetInstance.itemValue; i++)
        {
            if (itemInfo[i].Code.StartsWith("DF") || itemInfo[i].Code.StartsWith("DW") || itemInfo[i].Code.StartsWith("DG") || itemInfo[i].Code.StartsWith("DA"))
            {
                _ScrollPanel.GetComponent<RectTransform>().sizeDelta += new Vector2(620, 0);

                var ItemPanel = Instantiate(_ItemPanel, Vector3.zero, Quaternion.identity) as GameObject;
                ItemPanel.transform.parent = _ScrollPanel;
                ItemPanel.transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = itemInfo[i].ItemImage;
                ItemPanel.transform.FindChild("Name").GetComponent<Text>().text = itemInfo[i].Name;
                ItemPanel.transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Coin");
                ItemPanel.transform.FindChild("Cost_Text").GetComponent<Text>().text = itemInfo[i].cost.ToString("#,##0");

                int index = i;

                ItemPanel.GetComponent<Button>().onClick.AddListener(() => { Open_BuyPanel(itemInfo[index]); });                
            }
        }
    }

    public void ClickItem()
    {
        HeartState = false;
        CoinState = false;
        GemState = false;

        StorePanel.transform.FindChild("Weapon").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Defence").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Item").GetComponent<Button>().image.overrideSprite = Menu_Btn1;
        StorePanel.transform.FindChild("Heart").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Cost").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Gem").GetComponent<Button>().image.overrideSprite = Menu_Btn2;

        StorePanel.transform.FindChild("ItemList").FindChild("Scrollbar").GetComponent<Scrollbar>().value = 0;
        _ScrollPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1017);

        Transform[] ChildScroll = _ScrollPanel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildScroll)
        {
            if (cts.tag == "ItemPanel" || cts.tag == "AddPanel")
                Destroy(cts.gameObject);
        }

        ItemInfo.ItemInformation[] itemInfo = ItemInfo.GetInstance.GetAllItemInfo();

        for (int i = 0; i < ItemInfo.GetInstance.itemValue; i++)
        {
            if (itemInfo[i].Code.StartsWith("I"))
            {
                _ScrollPanel.GetComponent<RectTransform>().sizeDelta += new Vector2(620, 0);

                var ItemPanel = Instantiate(_ItemPanel, Vector3.zero, Quaternion.identity) as GameObject;
                ItemPanel.transform.parent = _ScrollPanel;
                ItemPanel.transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = itemInfo[i].ItemImage;
                ItemPanel.transform.FindChild("Name").GetComponent<Text>().text = itemInfo[i].Name;
                if (itemInfo[i].Attribute == 0)
                    ItemPanel.transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Coin");
                else if (itemInfo[i].Attribute == 1)
                    ItemPanel.transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
                ItemPanel.transform.FindChild("Cost_Text").GetComponent<Text>().text = itemInfo[i].cost.ToString("#,##0");

                int index = i;

                ItemPanel.GetComponent<Button>().onClick.AddListener(() => { Open_BuyItemPanel(itemInfo[index]); });
            }
        }
    }

    public void ClickHeart()
    {
        HeartState = true;
        CoinState = false;
        GemState = false;

        StorePanel.transform.FindChild("Weapon").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Defence").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Item").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Heart").GetComponent<Button>().image.overrideSprite = Menu_Btn1;
        StorePanel.transform.FindChild("Cost").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Gem").GetComponent<Button>().image.overrideSprite = Menu_Btn2;

        StorePanel.transform.FindChild("ItemList").FindChild("Scrollbar").GetComponent<Scrollbar>().value = 0;
        _ScrollPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1017);

        Transform[] ChildScroll = _ScrollPanel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildScroll)
        {
            if (cts.tag == "ItemPanel" || cts.tag == "AddPanel")
                Destroy(cts.gameObject);
        }

        _ScrollPanel.GetComponent<RectTransform>().sizeDelta += new Vector2(3720, 0);

        GameObject AddPanel;
        GameObject[] ItemPanel = new GameObject[5];

        AddPanel = Instantiate(_AddPanel, Vector3.zero, Quaternion.identity) as GameObject;
        AddPanel.transform.parent = _ScrollPanel;
        for (int i = 0; i < 5; i++)
        {
            ItemPanel[i] = Instantiate(_ItemPanel, Vector3.zero, Quaternion.identity) as GameObject;
            ItemPanel[i].transform.parent = _ScrollPanel;
        }

        AddPanel.transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreHeart1");
        ItemPanel[0].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreHeart1");
        ItemPanel[1].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreHeart2");
        ItemPanel[2].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreHeart3");
        ItemPanel[3].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreHeart4");
        ItemPanel[4].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreHeart5");

        AddPanel.transform.FindChild("Name").GetComponent<Text>().text = "1하트";
        ItemPanel[0].transform.FindChild("Name").GetComponent<Text>().text = "1 하트";
        ItemPanel[1].transform.FindChild("Name").GetComponent<Text>().text = "3 하트";
        ItemPanel[2].transform.FindChild("Name").GetComponent<Text>().text = "5 하트";
        ItemPanel[3].transform.FindChild("Name").GetComponent<Text>().text = "10 하트";
        ItemPanel[4].transform.FindChild("Name").GetComponent<Text>().text = "20 하트";

        AddPanel.transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("TV");
        ItemPanel[0].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        ItemPanel[1].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        ItemPanel[2].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        ItemPanel[3].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        ItemPanel[4].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");

        AddPanel.transform.FindChild("Cost_Text").GetComponent<Text>().text = "Free(" + Cloud_Mgr.GetInstance.UserData.ADD0.ToString() + "/5)";
        ItemPanel[0].transform.FindChild("Cost_Text").GetComponent<Text>().text = "5잼";
        ItemPanel[1].transform.FindChild("Cost_Text").GetComponent<Text>().text = "10잼";
        ItemPanel[2].transform.FindChild("Cost_Text").GetComponent<Text>().text = "20잼";
        ItemPanel[3].transform.FindChild("Cost_Text").GetComponent<Text>().text = "40잼";
        ItemPanel[4].transform.FindChild("Cost_Text").GetComponent<Text>().text = "80잼";

        AddPanel.GetComponent<Button>().onClick.AddListener(() => { Addmop_Heart(); });
        ItemPanel[0].GetComponent<Button>().onClick.AddListener(() => { Open_BuyHeart(1, 5); });
        ItemPanel[1].GetComponent<Button>().onClick.AddListener(() => { Open_BuyHeart(3, 10); });
        ItemPanel[2].GetComponent<Button>().onClick.AddListener(() => { Open_BuyHeart(5, 20); });
        ItemPanel[3].GetComponent<Button>().onClick.AddListener(() => { Open_BuyHeart(10, 40); });
        ItemPanel[4].GetComponent<Button>().onClick.AddListener(() => { Open_BuyHeart(20, 80); });
    }

    public void ClickCoin()
    {
        HeartState = false;
        CoinState = true;
        GemState = false;

        StorePanel.transform.FindChild("Weapon").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Defence").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Item").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Heart").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Cost").GetComponent<Button>().image.overrideSprite = Menu_Btn1;
        StorePanel.transform.FindChild("Gem").GetComponent<Button>().image.overrideSprite = Menu_Btn2;

        StorePanel.transform.FindChild("ItemList").FindChild("Scrollbar").GetComponent<Scrollbar>().value = 0;
        _ScrollPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1017);

        Transform[] ChildScroll = _ScrollPanel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildScroll)
        {
            if (cts.tag == "ItemPanel" || cts.tag == "AddPanel")
                Destroy(cts.gameObject);
        }

        _ScrollPanel.GetComponent<RectTransform>().sizeDelta += new Vector2(3720, 0);

        GameObject AddPanel;
        GameObject[] ItemPanel = new GameObject[5];

        AddPanel = Instantiate(_AddPanel, Vector3.zero, Quaternion.identity) as GameObject;
        AddPanel.transform.parent = _ScrollPanel;
        for (int i = 0; i < 5; i++)
        {
            ItemPanel[i] = Instantiate(_ItemPanel, Vector3.zero, Quaternion.identity) as GameObject;
            ItemPanel[i].transform.parent = _ScrollPanel;
        }

        AddPanel.transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreCoin1");
        ItemPanel[0].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreCoin1");
        ItemPanel[1].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreCoin2");
        ItemPanel[2].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreCoin3");
        ItemPanel[3].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreCoin4");
        ItemPanel[4].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreCoin5");

        AddPanel.transform.FindChild("Name").GetComponent<Text>().text = "1,000코인";
        ItemPanel[0].transform.FindChild("Name").GetComponent<Text>().text = "1,000코인";
        ItemPanel[1].transform.FindChild("Name").GetComponent<Text>().text = "5,000코인";
        ItemPanel[2].transform.FindChild("Name").GetComponent<Text>().text = "10,000코인";
        ItemPanel[3].transform.FindChild("Name").GetComponent<Text>().text = "50,000코인";
        ItemPanel[4].transform.FindChild("Name").GetComponent<Text>().text = "100,000코인";

        AddPanel.transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("TV");
        ItemPanel[0].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        ItemPanel[1].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        ItemPanel[2].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        ItemPanel[3].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        ItemPanel[4].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");

        AddPanel.transform.FindChild("Cost_Text").GetComponent<Text>().text = "Free(" + Cloud_Mgr.GetInstance.UserData.ADD1.ToString() + "/5)";
        ItemPanel[0].transform.FindChild("Cost_Text").GetComponent<Text>().text = "50";
        ItemPanel[1].transform.FindChild("Cost_Text").GetComponent<Text>().text = "100";
        ItemPanel[2].transform.FindChild("Cost_Text").GetComponent<Text>().text = "300";
        ItemPanel[3].transform.FindChild("Cost_Text").GetComponent<Text>().text = "800";
        ItemPanel[4].transform.FindChild("Cost_Text").GetComponent<Text>().text = "1,200";

        AddPanel.GetComponent<Button>().onClick.AddListener(() => { Addmop_Coin(); });
        ItemPanel[0].GetComponent<Button>().onClick.AddListener(() => { Open_BuyPanel(1000, 50); });
        ItemPanel[1].GetComponent<Button>().onClick.AddListener(() => { Open_BuyPanel(5000, 100); });
        ItemPanel[2].GetComponent<Button>().onClick.AddListener(() => { Open_BuyPanel(10000, 300); });
        ItemPanel[3].GetComponent<Button>().onClick.AddListener(() => { Open_BuyPanel(50000, 800); });
        ItemPanel[4].GetComponent<Button>().onClick.AddListener(() => { Open_BuyPanel(100000, 1200); });
    }

    public void ClickGem()
    {
        HeartState = false;
        CoinState = false;
        GemState = true;

        StorePanel.transform.FindChild("Weapon").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Defence").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Item").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Heart").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Cost").GetComponent<Button>().image.overrideSprite = Menu_Btn2;
        StorePanel.transform.FindChild("Gem").GetComponent<Button>().image.overrideSprite = Menu_Btn1;

        StorePanel.transform.FindChild("ItemList").FindChild("Scrollbar").GetComponent<Scrollbar>().value = 0;
        _ScrollPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1017);

        Transform[] ChildScroll = _ScrollPanel.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform cts in ChildScroll)
        {
            if (cts.tag == "ItemPanel" || cts.tag == "AddPanel")
                Destroy(cts.gameObject);
        }

        _ScrollPanel.GetComponent<RectTransform>().sizeDelta += new Vector2(3720, 0);

        GameObject AddPanel;
        GameObject[] ItemPanel = new GameObject[5];

        AddPanel = Instantiate(_AddPanel, Vector3.zero, Quaternion.identity) as GameObject;
        AddPanel.transform.parent = _ScrollPanel;
        for (int i = 0; i < 5; i++)
        {
            ItemPanel[i] = Instantiate(_ItemPanel, Vector3.zero, Quaternion.identity) as GameObject;
            ItemPanel[i].transform.parent = _ScrollPanel;
        }

        AddPanel.transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreGem1");
        ItemPanel[0].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreGem1");
        ItemPanel[1].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreGem2");
        ItemPanel[2].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreGem3");
        ItemPanel[3].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreGem4");
        ItemPanel[4].transform.FindChild("Item_Image").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("StoreGem5");

        AddPanel.transform.FindChild("Name").GetComponent<Text>().text = "10잼";
        ItemPanel[0].transform.FindChild("Name").GetComponent<Text>().text = "10잼";
        ItemPanel[1].transform.FindChild("Name").GetComponent<Text>().text = "70잼";
        ItemPanel[2].transform.FindChild("Name").GetComponent<Text>().text = "200잼";
        ItemPanel[3].transform.FindChild("Name").GetComponent<Text>().text = "500잼";
        ItemPanel[4].transform.FindChild("Name").GetComponent<Text>().text = "1,000잼";

        AddPanel.transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("TV");
        ItemPanel[0].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Cash");
        ItemPanel[1].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Cash");
        ItemPanel[2].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Cash");
        ItemPanel[3].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Cash");
        ItemPanel[4].transform.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Cash");

        AddPanel.transform.FindChild("Cost_Text").GetComponent<Text>().text = "Free(" + Cloud_Mgr.GetInstance.UserData.ADD1.ToString() + "/5)";
        ItemPanel[0].transform.FindChild("Cost_Text").GetComponent<Text>().text = "1,000\\";
        ItemPanel[1].transform.FindChild("Cost_Text").GetComponent<Text>().text = "5,000\\";
        ItemPanel[2].transform.FindChild("Cost_Text").GetComponent<Text>().text = "10,000\\";
        ItemPanel[3].transform.FindChild("Cost_Text").GetComponent<Text>().text = "20,000\\";
        ItemPanel[4].transform.FindChild("Cost_Text").GetComponent<Text>().text = "30,000\\";

        AddPanel.GetComponent<Button>().onClick.AddListener(() => { Addmop_Gem(); });
        ItemPanel[0].GetComponent<Button>().onClick.AddListener(() => { BuyProductID(kProductIDConsumable1); });
        ItemPanel[1].GetComponent<Button>().onClick.AddListener(() => { BuyProductID(kProductIDConsumable2); });
        ItemPanel[2].GetComponent<Button>().onClick.AddListener(() => { BuyProductID(kProductIDConsumable3); });
        ItemPanel[3].GetComponent<Button>().onClick.AddListener(() => { BuyProductID(kProductIDConsumable4); });
        ItemPanel[4].GetComponent<Button>().onClick.AddListener(() => { BuyProductID(kProductIDConsumable5); });
    }

    private void Addmop_Heart()
    {
        if (Cloud_Mgr.GetInstance.UserData.ADD0 == 0)
            return;
        _ShowOpt.resultCallback = OnAdsShowResultCallBack0;
        if (Advertisement.IsReady())
            Advertisement.Show(null, _ShowOpt);
    }

    private void Addmop_Coin()
    {
        if (Cloud_Mgr.GetInstance.UserData.ADD1 == 0)
            return;
        _ShowOpt.resultCallback = OnAdsShowResultCallBack1;    
        if(Advertisement.IsReady())
            Advertisement.Show(null, _ShowOpt);    
    }

    private void Addmop_Gem()
    {
        if (Cloud_Mgr.GetInstance.UserData.ADD2 == 0)
            return;
        _ShowOpt.resultCallback = OnAdsShowResultCallBack2;
        if (Advertisement.IsReady())
            Advertisement.Show(null, _ShowOpt);
    }

    void OnAdsShowResultCallBack0(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Cloud_Mgr.GetInstance.UserData.Heart += 1;

            if (Cloud_Mgr.GetInstance.UserData.Heart > 3 + (Cloud_Mgr.GetInstance.UserData.LV * 2))
            {
                Cloud_Mgr.GetInstance.UserData.LastTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }
            else
            {
                TimeSpan default_ts = new TimeSpan(0, 10, 0); 
                TimeSpan ts = default_ts - DateTime.Now.Subtract(Convert.ToDateTime(Cloud_Mgr.GetInstance.UserData.LastTime));

                TimeSpan rest = new TimeSpan(0, 0, ((int)ts.TotalSeconds * -1) % (int)default_ts.TotalSeconds);
                Cloud_Mgr.GetInstance.UserData.LastTime = (DateTime.Now - rest).ToString("yyyy/MM/dd HH:mm:ss");
            }

            if (Cloud_Mgr.GetInstance.UserData.ADD0 == 5)
                Cloud_Mgr.GetInstance.UserData.ADDTime0 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Cloud_Mgr.GetInstance.UserData.ADD0--;

            Cloud_Mgr.GetInstance.SaveToCloud();
        }
    }

    void OnAdsShowResultCallBack1(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Cloud_Mgr.GetInstance.UserData.Coin += 1000;

            if (Cloud_Mgr.GetInstance.UserData.ADD1 == 5)
                Cloud_Mgr.GetInstance.UserData.ADDTime1 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Cloud_Mgr.GetInstance.UserData.ADD1--;

            Cloud_Mgr.GetInstance.SaveToCloud();
        }
    }

    void OnAdsShowResultCallBack2(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Cloud_Mgr.GetInstance.UserData.Gem += 10;

            Cloud_Mgr.GetInstance.UserData.ADDTime2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Cloud_Mgr.GetInstance.UserData.ADD2--;

            Cloud_Mgr.GetInstance.SaveToCloud();
        }
    }

    private void Open_BuyHeart(int heart, int gem)
    {
        foreach (RectTransform cts in ChildBuy)
            cts.gameObject.SetActive(true);

        Transform Box = BuyPanel.transform.FindChild("Box");
        NowValue = 1;

        Box.FindChild("Name").GetComponent<Text>().text = heart.ToString() + "하트";
        Box.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        Box.FindChild("Cost_Text").GetComponent<Text>().text = gem.ToString("#,##0");

        Box.FindChild("Ok").FindChild("Text").GetComponent<Text>().text = "구 매";

        Box.FindChild("Ok").GetComponent<Button>().onClick.RemoveAllListeners();
        Box.FindChild("Cancle").GetComponent<Button>().onClick.RemoveAllListeners();

        Box.FindChild("Ok").GetComponent<Button>().onClick.AddListener(() => { BuyHeart_Btn(heart, gem); });
        Box.FindChild("Cancle").GetComponent<Button>().onClick.AddListener(() => { Cancle_Btn(); });
    }

    private void BuyHeart_Btn(int heart, int gem)
    {
        if (gem > Cloud_Mgr.GetInstance.UserData.Gem)
        {
            foreach (RectTransform cts in ChildWarining)
                cts.gameObject.SetActive(true);

            WarningPanel.transform.FindChild("Box").FindChild("Text").GetComponent<Text>().text = "보석이 부족합니다.";
            return;
        }

        Cloud_Mgr.GetInstance.UserData.Heart += heart;

        if (Cloud_Mgr.GetInstance.UserData.Heart > 3 + (Cloud_Mgr.GetInstance.UserData.LV * 2))
        {
            Cloud_Mgr.GetInstance.UserData.LastTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        else
        {
            TimeSpan default_ts = new TimeSpan(0, 10, 0);
            TimeSpan ts = default_ts - DateTime.Now.Subtract(Convert.ToDateTime(Cloud_Mgr.GetInstance.UserData.LastTime));

            TimeSpan rest = new TimeSpan(0, 0, ((int)ts.TotalSeconds * -1) % (int)default_ts.TotalSeconds);
            Cloud_Mgr.GetInstance.UserData.LastTime = (DateTime.Now - rest).ToString("yyyy/MM/dd HH:mm:ss");
        }

        Cloud_Mgr.GetInstance.UserData.Gem -= gem;
        
        Cloud_Mgr.GetInstance.SaveToCloud();

        Cancle_Btn();
    }

    private void Open_BuyPanel(ItemInfo.ItemInformation itemInfo)
    {
        foreach (RectTransform cts in ChildBuy)
            cts.gameObject.SetActive(true);

        Transform Box = BuyPanel.transform.FindChild("Box");
        NowValue = 1;

        Box.FindChild("Name").GetComponent<Text>().text = itemInfo.Name;
        Box.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Coin");
        Box.FindChild("Cost_Text").GetComponent<Text>().text = itemInfo.cost.ToString("#,##0");

        Box.FindChild("Ok").FindChild("Text").GetComponent<Text>().text = "구 매";

        Box.FindChild("Ok").GetComponent<Button>().onClick.RemoveAllListeners();
        Box.FindChild("Cancle").GetComponent<Button>().onClick.RemoveAllListeners();

        Box.FindChild("Ok").GetComponent<Button>().onClick.AddListener(() => { BuyItem_Btn(itemInfo); });
        Box.FindChild("Cancle").GetComponent<Button>().onClick.AddListener(() => { Cancle_Btn(); });
    }

    private void Open_BuyPanel(int coin, int gem)
    {
        foreach (RectTransform cts in ChildBuy)
            cts.gameObject.SetActive(true);
        Transform Box = BuyPanel.transform.FindChild("Box");

        Box.FindChild("Name").GetComponent<Text>().text = coin.ToString("#,##0") + "코인";
        Box.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        Box.FindChild("Cost_Text").GetComponent<Text>().text = gem.ToString("#,##0");

        Box.FindChild("Ok").FindChild("Text").GetComponent<Text>().text = "구 매";

        Box.FindChild("Ok").GetComponent<Button>().onClick.RemoveAllListeners();
        Box.FindChild("Cancle").GetComponent<Button>().onClick.RemoveAllListeners();

        Box.FindChild("Ok").GetComponent<Button>().onClick.AddListener(() => { BuyCoin_Btn(coin, gem); });
        Box.FindChild("Cancle").GetComponent<Button>().onClick.AddListener(() => { Cancle_Btn(); });
    }

    private void Open_BuyItemPanel(ItemInfo.ItemInformation itemInfo)
    {
        foreach (RectTransform cts in ChildBuyItem)
            cts.gameObject.SetActive(true);

        Transform Box = BuyItemPanel.transform.FindChild("Box");
        NowValue = 1;

        Box.FindChild("Name").GetComponent<Text>().text = itemInfo.Name;
        if (itemInfo.Attribute == 0)
            Box.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Coin");
        else if (itemInfo.Attribute == 1)
            Box.FindChild("Cost").GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Gem");
        Box.FindChild("Cost_Text").GetComponent<Text>().text = itemInfo.cost.ToString("#,##0");

        Box.FindChild("Value").FindChild("Text").GetComponent<Text>().text = NowValue.ToString();

        Box.FindChild("Ok").FindChild("Text").GetComponent<Text>().text = "구 매";

        Box.FindChild("Value").FindChild("Foward").GetComponent<Button>().onClick.RemoveAllListeners();
        Box.FindChild("Value").FindChild("Foward2").GetComponent<Button>().onClick.RemoveAllListeners();
        Box.FindChild("Value").FindChild("Back").GetComponent<Button>().onClick.RemoveAllListeners();
        Box.FindChild("Value").FindChild("Back2").GetComponent<Button>().onClick.RemoveAllListeners();
        Box.FindChild("Ok").GetComponent<Button>().onClick.RemoveAllListeners();
        Box.FindChild("Cancle").GetComponent<Button>().onClick.RemoveAllListeners();

        Box.FindChild("Value").FindChild("Foward").GetComponent<Button>().onClick.AddListener(() => { ChanageValue(itemInfo, 1); });
        Box.FindChild("Value").FindChild("Foward2").GetComponent<Button>().onClick.AddListener(() => { ChanageValue(itemInfo, 10); });
        Box.FindChild("Value").FindChild("Back").GetComponent<Button>().onClick.AddListener(() => { ChanageValue(itemInfo, -1); });
        Box.FindChild("Value").FindChild("Back2").GetComponent<Button>().onClick.AddListener(() => { ChanageValue(itemInfo, -10); });
        Box.FindChild("Ok").GetComponent<Button>().onClick.AddListener(() => { BuyItem_Btn(itemInfo); });
        Box.FindChild("Cancle").GetComponent<Button>().onClick.AddListener(() => { Cancle_Btn(); });
    }

    private void ChanageValue(ItemInfo.ItemInformation itemInfo, int num)
    {
        NowValue += num;

        if (NowValue > 100)
            NowValue = 1;
        if (NowValue < 1)
            NowValue = 100;

        Transform Box = BuyItemPanel.transform.FindChild("Box");
        Box.FindChild("Value").FindChild("Text").GetComponent<Text>().text = NowValue.ToString();
        Box.FindChild("Cost_Text").GetComponent<Text>().text = (itemInfo.cost * NowValue).ToString("#,##0");
    }    

    private void BuyCoin_Btn(int coin, int gem)
    {
        if (Cloud_Mgr.GetInstance.UserData.Gem < gem)
        {
            foreach (RectTransform cts in ChildWarining)
                cts.gameObject.SetActive(true);

            WarningPanel.transform.FindChild("Box").FindChild("Text").GetComponent<Text>().text = "보석이 부족합니다.";
            return;
        }

        Cloud_Mgr.GetInstance.UserData.Gem -= gem;
        Cloud_Mgr.GetInstance.UserData.Coin += coin;

        Cloud_Mgr.GetInstance.SaveToCloud();

        Cancle_Btn();
    }

    private void BuyItem_Btn(ItemInfo.ItemInformation itemInfo)
    {
        int empty = -1;
        int[] Item = new int[90];
        int ItemPos = 0;

        for (int i = 0; i < 90; i++)
            Item[i] = -1;

        for (int i = 0; i < 90; i++)
        {
            if (Cloud_Mgr.GetInstance.UserData.HoldItem[i].StartsWith(itemInfo.Code) && itemInfo.Code[0] == 'I')
                Item[ItemPos++] = i;
            if (Cloud_Mgr.GetInstance.UserData.HoldItem[i][0] == '$' && empty == -1)
                empty = i;
        }

        if(itemInfo.Code[0] == 'I')
        {
            int tempValue = NowValue;
            for (int i = 0; i < ItemPos; i++)
            {
                if (Item[i] != -1)
                {
                    string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[Item[i]].Split(new char[] { ':' });
                    int value = Convert.ToInt32(p[1]);
                    value = 100 - value;
                    if (value > 0)
                        tempValue -= value;
                }
            }
            if (tempValue > 0 && empty == -1)
            {
                foreach (RectTransform cts in ChildWarining)
                    cts.gameObject.SetActive(true);

                WarningPanel.transform.FindChild("Box").FindChild("Text").GetComponent<Text>().text = "가방의 공간이 부족합니다.";
                return;
            }
        }
        else
        {
            if (empty == -1)
            {
                foreach (RectTransform cts in ChildWarining)
                    cts.gameObject.SetActive(true);
                WarningPanel.transform.FindChild("Box").FindChild("Text").GetComponent<Text>().text = "가방의 공간이 부족합니다.";
                return;
            }
        }

        if (itemInfo.Attribute == 1 && itemInfo.Code[0] == 'I')
        {
            if (itemInfo.cost * NowValue > Cloud_Mgr.GetInstance.UserData.Gem)
            {
                foreach (RectTransform cts in ChildWarining)
                    cts.gameObject.SetActive(true);

                WarningPanel.transform.FindChild("Box").FindChild("Text").GetComponent<Text>().text = "보석이 부족합니다.";
                return;
            }
            Cloud_Mgr.GetInstance.UserData.Gem -= itemInfo.cost * NowValue;
        }
        else
        {
            if (itemInfo.cost * NowValue > Cloud_Mgr.GetInstance.UserData.Coin)
            {
                foreach (RectTransform cts in ChildWarining)
                    cts.gameObject.SetActive(true);

                WarningPanel.transform.FindChild("Box").FindChild("Text").GetComponent<Text>().text = "금화가 부족합니다.";
                return;
            }
            Cloud_Mgr.GetInstance.UserData.Coin -= itemInfo.cost * NowValue;
        }

        if(itemInfo.Code[0] == 'I')
        {
            while (NowValue > 0)
            {
                int maxPos = -1;
                int maxValue = 0;

                for (int i = 0; i < ItemPos; i++)
                {
                    if (Item[i] != -1)
                    {
                        string[] p = Cloud_Mgr.GetInstance.UserData.HoldItem[Item[i]].Split(new char[] { ':' });
                        int value = Convert.ToInt32(p[1]);
                        if (maxValue < value)
                        {
                            maxPos = i;
                            maxValue = value;
                        }
                    }
                }

                if (maxPos == -1)
                    break;

                if (NowValue + maxValue > 100)
                {
                    Cloud_Mgr.GetInstance.UserData.HoldItem[Item[maxPos]] = itemInfo.Code + ":100";
                    NowValue -= 100 - maxValue;
                }
                else
                {
                    Cloud_Mgr.GetInstance.UserData.HoldItem[Item[maxPos]] = itemInfo.Code + ":" + (NowValue + maxValue).ToString();
                    NowValue = 0;
                }
                Item[maxPos] = -1;
            }
        }    
            
        if (NowValue > 0)
        {
            Cloud_Mgr.GetInstance.UserData.HoldItem[empty] = itemInfo.Code + ":" + NowValue.ToString();
        }

        Cloud_Mgr.GetInstance.SaveToCloud();

        Cancle_Btn();
    }

    public void warningOK_Btn()
    {        
        foreach (RectTransform cts in ChildWarining)
            cts.gameObject.SetActive(false);
    }

    private void Cancle_Btn()
    {
        foreach (RectTransform cts in ChildBuyItem)
            cts.gameObject.SetActive(false);
        foreach (RectTransform cts in ChildBuy)
            cts.gameObject.SetActive(false);
    }

    public void BacktoLobbyBtn()
    {
        StartCoroutine(Scene_Mgr.GetInstance.LoadLobby());
        Scene_Mgr.GetInstance.UnLoadStore();
    }

    public void ClickSellPanel()
    {
        foreach (RectTransform cts in ChildStore)
            cts.gameObject.SetActive(false);
    }

    public void ClickBuyPanel()
    {
        foreach (RectTransform cts in ChildStore)
            cts.gameObject.SetActive(true);
        ClickWeapon();
    }
}
