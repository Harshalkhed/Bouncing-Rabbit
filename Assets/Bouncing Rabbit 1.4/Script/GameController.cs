using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GoogleMobileAds.Api;

public class GameController : MonoBehaviour {

	private int score; //For count score
	public Text scoreText; //For show score
	public Text newHighScoreText; //For show when player got high score
	public Text gameoverScoreText; //For show score when gameover
	public Text gameoverHighscoreText; //For show highscore when gameover
	public Canvas gameoverGUI; //For show GUI of gameover
	public Canvas ingameGUI;  //For show GUI when play (pause button ,scoreText)
	public Canvas pauseGUI; //For show GUI when pause
	public static GameController instance; //Instance
    public string appId = "ca-app-pub-8475061783830254~1011896044";
    InterstitialAd interstitialAd;

#if UNITY_ANDROID
    //string bannerId = "ca-app-pub-1385093244148841/2952458907";
    string interId = "ca-app-pub-8475061783830254/2847721897";
    //string rewardedId = "ca-app-pub-3940256099942544/5224354917";

#endif
    void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => {

            print("Ads Initialised !!");

        });
        LoadInterstitialAd();
    }

    void Awake(){
		instance = this;
        

    }

	public void addScore(){
		score++; //Plus score
		scoreText.text = score.ToString (); //Change scoreText to current score
	}

	public void GameOver(){
        ShowInterstitialAd();
        CheckHighScore ();
		gameoverScoreText.text = score.ToString(); //Set score to gameoverScoreText
		gameoverHighscoreText.text = PlayerPrefs.GetInt ("highscore", 0).ToString(); //Set high score to gameoverHighscoreText
		gameoverGUI.gameObject.SetActive(true); //Show gameover's GUI
		ingameGUI.gameObject.SetActive(true); //Hide ingame's GUI
	}

	public void CheckHighScore(){
		if( score > PlayerPrefs.GetInt("highscore",0) ){ //If score > highscore
			PlayerPrefs.SetInt("highscore",score); //Save a new highscore
			newHighScoreText.gameObject.SetActive(true); //Enable newHighScoreText
		}
	}

	public void Pause(){
         // Prevent multiple pauses
        Time.timeScale = 0;
        pauseGUI.gameObject.SetActive(true);
       

    }

	public void Resume(){
		Time.timeScale = 1; //Change timeScale to 1
		pauseGUI.gameObject.SetActive (false); //Hide pauseGUI
        LoadInterstitialAd();
    }

    public void showpausedAd()
    {
        ShowInterstitialAd();
    }

    public void LoadInterstitialAd()
    {

        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(interId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                print("Interstitial ad failed to load" + error);
                return;
            }

            print("Interstitial ad loaded !!" + ad.GetResponseInfo());

            interstitialAd = ad;
            InterstitialEvent(interstitialAd);
        });

    }
    public void ShowInterstitialAd()
    {

        if (interstitialAd != null && interstitialAd.CanShowAd() )
        {
            
            interstitialAd.Show();
            
        }
        else
        {
            print("Intersititial ad not ready!!");
        }
    }
    public void InterstitialEvent(InterstitialAd ad)
    {

        
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Interstitial ad paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
            Pause();
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            Pause();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }







}
