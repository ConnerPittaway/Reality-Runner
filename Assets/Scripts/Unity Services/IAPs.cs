using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Serialization;

public class IAPs : MonoBehaviour, IStoreListener
{
    public IStoreController m_StoreController; // The Unity Purchasing system.

    public static IAPs Instance;

    //Your products IDs. They should match the ids of your products in your store.
    //Coin Purchases
    public string coins1000ID = "com.BaconGames.RealityRunner.coins1000";
    public string coins5000ID = "com.BaconGames.RealityRunner.coins5000";
    public string coins10000ID = "com.BaconGames.RealityRunner.coins10000";
    public string coins40000ID = "com.BaconGames.RealityRunner.coins40000";

    //Other Purchases
    public string premiumID = "com.BaconGames.RealityRunner.premium";
    public string allCharactersID = "com.BaconGames.RealityRunner.allCharacters";

    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializePurchasing();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Add products that will be purchasable and indicate its type.
        builder.AddProduct(coins1000ID, ProductType.Consumable);
        builder.AddProduct(coins5000ID, ProductType.Consumable);
        builder.AddProduct(coins10000ID, ProductType.Consumable);
        builder.AddProduct(coins40000ID, ProductType.Consumable);
        builder.AddProduct(premiumID, ProductType.NonConsumable);
        builder.AddProduct(allCharactersID, ProductType.NonConsumable);

        Debug.Log("In App Product Added");

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"In-App Purchasing initialize failed: {error}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        //Retrieve the purchased product
        var product = args.purchasedProduct;

        //Add the purchased product to the players inventory
        //Coins
        if (product.definition.id == coins1000ID)
        {
            GlobalDataManager.Instance.AlterCoins(1000);
            EventManager.OnCoinPurchase();
        }

        if (product.definition.id == coins5000ID)
        {
            GlobalDataManager.Instance.AlterCoins(5000);
            EventManager.OnCoinPurchase();
        }

        if (product.definition.id == coins10000ID)
        {
            GlobalDataManager.Instance.AlterCoins(10000);
            EventManager.OnCoinPurchase();
        }

        if (product.definition.id == coins40000ID)
        {
            GlobalDataManager.Instance.AlterCoins(40000);
            EventManager.OnCoinPurchase();
        }

        if (product.definition.id == premiumID)
        {
            GlobalDataManager.Instance.SetPremiumStatus(true);
            EventManager.OnPremiumPurchase();
        }

        if (product.definition.id == allCharactersID)
        {
            GlobalDataManager.Instance.UnlockAllCharacters();
            EventManager.OnAllCharactersPurchase();
        }

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    //Purchase Functions
    public void Coins1000()
    {
        m_StoreController.InitiatePurchase(coins1000ID);
    }

    public void Coins5000()
    {
        m_StoreController.InitiatePurchase(coins5000ID);
    }

    public void Coins10000()
    {
        m_StoreController.InitiatePurchase(coins10000ID);
    }

    public void Coins40000()
    {
        m_StoreController.InitiatePurchase(coins40000ID);
    }

    public void Premium()
    {
        m_StoreController.InitiatePurchase(premiumID);
    }

    public void AllCharacters()
    {
        m_StoreController.InitiatePurchase(allCharactersID);
    }
}
