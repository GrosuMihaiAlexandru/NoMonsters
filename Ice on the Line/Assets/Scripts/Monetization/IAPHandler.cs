using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class IAPHandler : MonoBehaviour
{
    public Text gFishText;

    public Text pileOfFishPrice;
    public Text bucketOfFishPrice;
    public Text barrelOfFishPrice;
    public Text bathtubOfFishPrice;
    public Text boatloadOfFishPrice;

    // Start is called before the first frame update
    void Start()
    {
        LoadGFishPrices();
    }

    public void LoadGFishPrices()
    {
        Product pile = InAppPurchasing.GetProduct(EM_IAPConstants.Product_A_pile_of_fish);
        if (pile != null)
            pileOfFishPrice.text = pile.metadata.localizedPriceString;

        Product bucket = InAppPurchasing.GetProduct(EM_IAPConstants.Product_A_large_bucket_of_fish);
        if (bucket != null)
            bucketOfFishPrice.text = bucket.metadata.localizedPriceString;

        Product barrel = InAppPurchasing.GetProduct(EM_IAPConstants.Product_A_barrel_of_fish);
        if (barrel != null)
            barrelOfFishPrice.text = barrel.metadata.localizedPriceString;

        Product bathtub = InAppPurchasing.GetProduct(EM_IAPConstants.Product_A_bathtub_of_fish);
        if (bathtub != null)
            bathtubOfFishPrice.text = bathtub.metadata.localizedPriceString;

        Product boatload = InAppPurchasing.GetProduct(EM_IAPConstants.Product_A_boatload_of_fish);
        if (boatload != null)
            boatloadOfFishPrice.text = boatload.metadata.localizedPriceString;
    }

    private void OnEnable()
    {
        InAppPurchasing.PurchaseCompleted += InAppPurchasing_PurchaseCompleted;
        InAppPurchasing.PurchaseFailed += InAppPurchasing_PurchaseFailed;

        Advertising.RewardedAdCompleted += Advertising_RewardedAdCompleted;
        Advertising.RewardedAdSkipped += Advertising_RewardedAdSkipped;
    }

    private void Advertising_RewardedAdSkipped(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        NativeUI.ShowToast("Ad canceled.");
    }

    private void Advertising_RewardedAdCompleted(RewardedAdNetwork arg1, AdPlacement arg2)
    {
        GameManager.instance.AddGfish(5);
        gFishText.text = GameManager.instance.GFish.ToString();
    }

    private void OnDisable()
    {
        InAppPurchasing.PurchaseCompleted -= InAppPurchasing_PurchaseCompleted;
        InAppPurchasing.PurchaseFailed -= InAppPurchasing_PurchaseFailed;

        Advertising.RewardedAdCompleted -= Advertising_RewardedAdCompleted;
        Advertising.RewardedAdSkipped -= Advertising_RewardedAdSkipped;
    }

    private void InAppPurchasing_PurchaseFailed(IAPProduct product)
    {
        NativeUI.Alert("Error", "The purchase of product " + product.Name + " has failed.");
    }

    private void InAppPurchasing_PurchaseCompleted(IAPProduct product)
    {
        switch(product.Name)
        {
            case EM_IAPConstants.Product_A_pile_of_fish:
                NativeUI.ShowToast("Bought " + product.Name);
                GameManager.instance.AddGfish(45);
                break;

            case EM_IAPConstants.Product_A_large_bucket_of_fish:
                NativeUI.ShowToast("Bought " + product.Name);
                GameManager.instance.AddGfish(160);
                break;

            case EM_IAPConstants.Product_A_barrel_of_fish:
                NativeUI.ShowToast("Bought " + product.Name);
                GameManager.instance.AddGfish(410);
                break;

            case EM_IAPConstants.Product_A_bathtub_of_fish:
                NativeUI.ShowToast("Bought " + product.Name);
                GameManager.instance.AddGfish(960);
                break;

            case EM_IAPConstants.Product_A_boatload_of_fish:
                NativeUI.ShowToast("Bought " + product.Name);
                GameManager.instance.AddGfish(2120);
                break;

            default:
                break;
        }

        gFishText.text = GameManager.instance.GFish.ToString();
    }

    public void GetAvailable()
    {
        // EM_IAPConstants.Sample_Product is the generated name constant of a product named "Sample Product"
        Product sampleProduct = InAppPurchasing.GetProduct(EM_IAPConstants.Product_A_barrel_of_fish);

        if (sampleProduct != null)
        {
            NativeUI.ShowToast("Available To Purchase: " + sampleProduct.availableToPurchase.ToString());
            if (sampleProduct.hasReceipt)
            {
                // NativeUI.ShowToast("Receipt: " + sampleProduct.receipt);
            }
            // NativeUI.ShowToast("Price: " + sampleProduct.metadata.localizedPrice);
        }
    }

    public void WatchAdForFreeFish()
    {
        if (Advertising.IsRewardedAdReady())
            Advertising.ShowRewardedAd();
    }

    public void PurchasePileOfFish()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_A_pile_of_fish);
    }

    public void PurchaseBucketOfFish()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_A_large_bucket_of_fish);
    }

    public void PurchaseBarrelOfFish()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_A_barrel_of_fish);
    }

    public void PurchaseBathtubOfFish()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_A_bathtub_of_fish);
    }

    public void PurchaseBoatloadOfFish()
    {
        InAppPurchasing.Purchase(EM_IAPConstants.Product_A_boatload_of_fish);
    }
}
