using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;


// 
[System.Serializable]
public class ConsumableItem
{
    public string Name;
    public string Id;
    public string desc;
    public float price;
}

[System.Serializable]
public class NonConsumableItem
{
    public string Name;
    public string Id;
    public string desc;
    public float price;
}

[System.Serializable]
public class SubscriptionItem
{
    public string Name;
    public string Id;
    public string desc;
    public float price;
    public int timeDuration;
}

public class TestShop : MonoBehaviour, IStoreListener
{
    // Start is called before the first frame

    public ConsumableItem cItem;
    public NonConsumableItem nItem;
    public SubscriptionItem sItem;

    IStoreController m_StoreController;

    public TextMeshProUGUI coinsTxt;

    public GameObject gameObj;
   


    void Start()
    {
        int coins = PlayerPrefs.GetInt("totalCoins");
        coinsTxt.text = coins.ToString();
       SetupBuilder();
    }

    void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(cItem.Id, ProductType.Consumable);
        builder.AddProduct(nItem.Id, ProductType.NonConsumable);
        builder.AddProduct(sItem.Id, ProductType.Subscription);

        UnityPurchasing.Initialize(this, builder);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        // retrive purchased product
        var product = purchaseEvent.purchasedProduct;
        print("Purchase complete " + product.definition.id);
        if (product.definition.id == cItem.Id)
        {
            AddCoins(50);
        }
        if (product.definition.id == nItem.Id)
        {
            RemoveAds();
        }
        if (product.definition.id == sItem.Id)
        {
            ActivateMembership();
        }

        return PurchaseProcessingResult.Complete;
    }


    public void AddCoins(int amount)
    {
        int coins = PlayerPrefs.GetInt("totalCoins") + amount;
        coinsTxt.text = coins.ToString();
    }
    void RemoveAds()
    {

    }

    void ActivateMembership()
    {

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Succes");
        m_StoreController = controller;
    }

    public void ConsumableBtnPressed()
    {
        //AddCoins(50);
        m_StoreController.InitiatePurchase(cItem.Id);
    }
    public void NonConsumableBtnPressed()
    {
        //AddCoins(50);
        m_StoreController.InitiatePurchase(nItem.Id);
    }

    public void SubscriptionBtnPressed()
    {
        //AddCoins(50);
        m_StoreController.InitiatePurchase(sItem.Id);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("OnInitializeFailed " + error.ToString());
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("OnInitializeFailed " + error.ToString());
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Purchase Failed");
    }

}
