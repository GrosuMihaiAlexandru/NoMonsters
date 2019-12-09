using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class IAPHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        InAppPurchasing.PurchaseCompleted += InAppPurchasing_PurchaseCompleted;
        InAppPurchasing.PurchaseFailed += InAppPurchasing_PurchaseFailed;
    }

    private void OnDisable()
    {
        InAppPurchasing.PurchaseCompleted -= InAppPurchasing_PurchaseCompleted;
        InAppPurchasing.PurchaseFailed -= InAppPurchasing_PurchaseFailed;
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
