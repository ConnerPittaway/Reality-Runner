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
    public string coins1000ID = "com.BaconGames.RealityRunner.coins1000";

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
        if (product.definition.id == coins1000ID)
        {
            GlobalDataManager.Instance.AlterCoins(1000);
            EventManager.OnCoinPurchase();
        }

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    //Purchases
    public void Coins1000()
    {
        m_StoreController.InitiatePurchase(coins1000ID);
    }
}
