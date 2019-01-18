#if GleyIAPiOS || GleyIAPGooglePlay
#define GleyIAPEnabled
#endif


public enum ProductType
{
    Consumable = 0,
    NonConsumable = 1,
    Subscription = 2
}

public enum IAPOperationStatus
{
    Success,
    Fail
}

[System.Serializable]
public class StoreProduct
{
    public string productName;
    public ProductType productType;
    public string idGooglePlay;
    public string idIOS;
    public int value;
    public string localizedPriceString = "-";
    public int price;
    public string isoCurrencyCode;
    internal string localizedDescription;
    internal string localizedTitle;
    public bool active;


    public StoreProduct(string productName, ProductType productType, int value, string idGooglePlay, string idIOS)
    {
        this.productName = productName;
        this.productType = productType;
        this.value = value;
        this.idGooglePlay = idGooglePlay;
        this.idIOS = idIOS;
    }


    public StoreProduct()
    {
        productName = "";
        idGooglePlay = "";
        idIOS = "";
        productType = ProductType.Consumable;
    }

#if GleyIAPEnabled
    internal UnityEngine.Purchasing.ProductType GetProductType()
    {
        return (UnityEngine.Purchasing.ProductType)(int)productType;
    }
#endif

    internal string GetStoreID()
    {
#if GleyIAPiOS
        return idIOS;
#elif GleyIAPGooglePlay
        return idGooglePlay;
#else
        return "";
#endif
    }
}
