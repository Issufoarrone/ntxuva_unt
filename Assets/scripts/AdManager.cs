using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {


	//public static AdManager instance{ set; get; }

	public string bannerId;
	public string videoId;
	private string appId = "ca-app-pub-1490353196093317~5332253350";
	private BannerView bannerView;

	private void Start()
	{
		//instance = this;
		//DontDestroyOnLoad (gameObject);

		#if UNITY_EDITOR
		Debug.Log("Editor nao mostrat pub");
		#elif UNITY_ANDROID
			MobileAds.Initialize(appId);
			this.ShowBanner ();
		#endif

	}


	public void ShowBanner()
	{


		#if UNITY_EDITOR
		Debug.Log("Editor nao mostrat pub");
		#elif UNITY_ANDROID
			bannerView = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Bottom);
			// Create an empty ad request.
			// Called when an ad request has successfully loaded.
			//bannerView.OnAdLoaded += HandleOnAdLoaded;
			// Called when an ad request failed to load.
			bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;

			AdRequest request = new AdRequest.Builder().Build();

			// Load the banner with the request.
			bannerView.LoadAd(request);
		#endif	
	}
	/*
	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		//MonoBehaviour.print("HandleAdLoaded event received");
		Alerta ("HandleAdLoaded event received");
	}
	*/

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		//MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "+ args.Message);
		Alerta ("Failed: "+ args.Message);
	}

	public void ShowVideo()
	{
		/*
		#if UNITY_EDITOR
		Debug.Log("Editor nao mostrat pub");
		#elif UNITY_ANDROID
		if (MobileAds.Instance ().isInterstitialReady ()) 
		{
		MobileAds.Instance().showInterstitial ();
		}
		#endif

*/


	}

	public static void Alerta(string Texto)
	{
		GameObject.Find ("Alertas").GetComponent<Text> ().text = Texto;
	}

}
