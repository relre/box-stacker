//using GoogleMobileAds.Api;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class RewardAds : MonoBehaviour
//{
//    private RewardedAd myRewardAds;
//    private CrateSpawner crateSpawner;
//    public string AndroidAdId;
//    public string IosAdId;
//    string AdsId;
//    void Start()
//    {
//        crateSpawner = GameObject.Find("GameManager").GetComponent<CrateSpawner>();
//        RequestRewardAds();
//    }

//    void RequestRewardAds()
//    {

//#if UNITY_ANDROID
//        AdsId = AndroidAdId;
//#elif UNITY_IPHONE
//        AdsId = IosAdId;
//#else
//        AdsId = "Unknown Platform";
//        #endif

//        myRewardAds = new RewardedAd(AdsId);

//        myRewardAds.OnAdLoaded += yuklendimi;
//        myRewardAds.OnAdOpening += acildi;
//        myRewardAds.OnAdFailedToShow += acildimi;
//        myRewardAds.OnUserEarnedReward += videoyuizlediOduluHaketti;

//        myRewardAds.OnAdClosed += kapatildi;


//        AdRequest request = new AdRequest.Builder().Build();
//        myRewardAds.LoadAd(request);
//    }

//    public void yuklendimi(object sender, System.EventArgs args)
//    {

//        Debug.Log("Reklam yüklendi\n");

//    }
//    public void yuklemedesorunvar(object sender, AdErrorEventArgs args)
//    {
//        Debug.Log("Reklam yüklenemedi\n");

//    }
//    public void acildi(object sender, EventArgs args)
//    {
//        Debug.Log("Reklam Açýldý\n");

//    }
//    public void kapatildi(object sender, EventArgs args)
//    {
//        Debug.Log("Reklam kapatýldý\n");
//        myRewardAds.Destroy();
//    }

//    public void acildimi(object sender, AdErrorEventArgs args)
//    {
//        Debug.Log("Reklam açýlamadý\n");

//    }
//    public void videoyuizlediOduluHaketti(object sender, Reward args)
//    {

//        string reklaminadi = args.Type;
//        double miktar = args.Amount;

//        crateSpawner.spawnableCrate += Mathf.RoundToInt((float)miktar);
       
//        Debug.Log(crateSpawner.spawnableCrate);

//        Debug.Log("Ödülün türü " + reklaminadi + " Ödülün Miktarý " + miktar + " \n");
        
//    }





//    public void OdulluReklamgoster()
//    {

//        if (myRewardAds.IsLoaded())
//        {
//            myRewardAds.Show();
//        }
//    }
//    void Update()
//    {
        
//    }
//}
