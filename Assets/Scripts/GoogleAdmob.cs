using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
/* Change Log
	- 1.0.0 (04/05/2017)
	Google Admob 
	Support Skype: duc6365691
	
	*/
public class GoogleAdmob {

    public static BannerView bannerView;
    public static InterstitialAd interstitial;
	public static bool isBannerLoaded=false;
    public static void CallBigBanner()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
    void OnDestroy()
    {
        bannerView.Destroy();
    }

    public static void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-2888488436369352/8207840824";
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-8688439802773899/7789995162";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        // Register for ad events.
		bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdOpening += HandleAdOpened;
        bannerView.OnAdClosed += HandleAdClosed;
        // Load a banner ad.
        bannerView.LoadAd(createAdRequest());
    }

    public static void RequestInterstitial()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-8688439802773899/3267460362";
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-8688439802773899/9266728366";
#else
            string adUnitId = "unexpected_platform";
#endif
        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);
        // Register for ad events.
		interstitial.OnAdLoaded += HandleInterstitialLoaded;
		interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.OnAdOpening += HandleInterstitialOpened;
		interstitial.OnAdClosed += HandleInterstitialClosed;

        // Load an interstitial ad.
        interstitial.LoadAd(createAdRequest());
    }

    // Returns an ad request with custom ad targeting.
    public static AdRequest createAdRequest()
    {
		return new AdRequest.Builder()  .AddTestDevice("0A614690E909F32A8798D3BA16A0774A").Build();
    }

	public static void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    
    }

    #region Banner callback handlers

    public static void HandleAdLoaded(object sender, EventArgs args)
    {
		isBannerLoaded=true;
    }

    public static void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    }

    public static void HandleAdOpened(object sender, EventArgs args)
    {
    }

    public static void HandleAdClosing(object sender, EventArgs args)
    {
    }

    public static void HandleAdClosed(object sender, EventArgs args)
    {
    }

    public static void HandleAdLeftApplication(object sender, EventArgs args)
    {
    }

    #endregion

    #region Interstitial callback handlers

    public static void HandleInterstitialLoaded(object sender, EventArgs args)
    {
	
    }

    public static void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    }

    public static void HandleInterstitialOpened(object sender, EventArgs args)
    {
    }

    public static void HandleInterstitialClosing(object sender, EventArgs args)
    {
    }

    public static void HandleInterstitialClosed(object sender, EventArgs args)
    {
    }

    public static void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
    }

    #endregion
}
