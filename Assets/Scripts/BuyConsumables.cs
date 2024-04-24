using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing;
using TMPro;

public class BuyConsumables : MonoBehaviour, IDetailedStoreListener
{
    IStoreController m_StoreController; // The Unity Purchasing system.

    //Your products IDs. They should match the ids of your products in your store.
    public string coinProductId = "com.bitterballengames.dozen.coins";

    public GameObject CoinCount;
    private TextMeshProUGUI CoinCountText;

    int m_CoinsCount = 0;

    void Start()
    {
        InitializePurchasing();
        CoinCountText = CoinCount.GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }


    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Add products that will be purchasable and indicate its type.
        builder.AddProduct(coinProductId, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyCoins()
    {
        m_StoreController.InitiatePurchase(coinProductId);
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        OnInitializeFailed(error, null);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

        if (message != null)
        {
            errorMessage += $" More details: {message}";
        }

        Debug.Log(errorMessage);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        //Retrieve the purchased product
        var product = args.purchasedProduct;

        //Add the purchased product to the players inventory
        if (product.definition.id == coinProductId)
        {
            AddCoin();
        }


        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
            $" Purchase failure reason: {failureDescription.reason}," +
            $" Purchase failure details: {failureDescription.message}");
    }

    void AddCoin()
    {
        m_CoinsCount++;
        UpdateUI();
    }



    void UpdateUI()
    {
        //CoinCountText.text = $"My coins : {m_CoinsCount}";
        CoinCountText.text = m_CoinsCount.ToString();
    }
}

