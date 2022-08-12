using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;
using SgLib;




public class UIManager : MonoBehaviour
{
    [Header("UI Config")]
    public bool showDailyRewardAtStart = true;
    public bool showWatchForCoinsAtStart = true;

    [Header("Object References")]
    public GameManager gameManager;
    public GameObject header;
    public Text score;
    public Text bestScore;
    public Text coinText;
    public GameObject tapToStart;
    public GameObject characterSelectBtn;
    public GameObject menuButtons;
    public GameObject dailyRewardBtn;
    public Text dailyRewardBtnText;
    public GameObject toastMsg;
    public GameObject rewardUI;
    public GameObject settingsUI;
    public GameObject soundOnBtn;
    public GameObject soundOffBtn;
    public GameObject musicOnBtn;
    public GameObject musicOffBtn;

    [Header("Premium Features Buttons")]
    public GameObject watchForCoinsBtn;
    public GameObject leaderboardBtn;
    public GameObject shareBtn;
    public GameObject iapPurchaseBtn;
    public GameObject removeAdsBtn;
    public GameObject restorePurchaseBtn;

    [Header("In-App Purchase Store")]
    public GameObject storeUI;

    [Header("Sharing-Specific")]
    public GameObject shareUI;
   // public ShareUIController shareUIController;

    Animator scoreAnimator;
    Animator dailyRewardAnimator;
    bool isWatchAdsForCoinBtnActive;
    bool isShowShareUI;

    void OnEnable()
    {
        GameManager.GameStateChanged += GameManager_GameStateChanged;
        ScoreManager.ScoreUpdated += OnScoreUpdated;
    }

    void OnDisable()
    {
        GameManager.GameStateChanged -= GameManager_GameStateChanged;
        ScoreManager.ScoreUpdated -= OnScoreUpdated;
    }

    // Use this for initialization
    void Start()
    {
        scoreAnimator = score.GetComponent<Animator>();
        dailyRewardAnimator = dailyRewardBtn.GetComponent<Animator>();

        Reset();
        ShowStartUI();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = ScoreManager.Instance.Score.ToString();
        bestScore.text = ScoreManager.Instance.HighScore.ToString();
        coinText.text = CoinManager.Instance.Coins.ToString();

        if (!DailyRewardController.Instance.disable && dailyRewardBtn.gameObject.activeSelf)
        {
            if (DailyRewardController.Instance.CanRewardNow())
            {
                dailyRewardBtnText.text = "GRAB YOUR REWARD!";
                dailyRewardAnimator.SetTrigger("activate");
            }
            else
            {
                TimeSpan timeToReward = DailyRewardController.Instance.TimeUntilReward;
                dailyRewardBtnText.text = string.Format("REWARD IN {0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
                dailyRewardAnimator.SetTrigger("deactivate");
            }
        }

        if (settingsUI.activeSelf)
        {
            UpdateSoundButtons();
            UpdateMusicButtons();
        }
    }

    void GameManager_GameStateChanged(GameState newState, GameState oldState)
    {
        if (newState == GameState.Playing)
        {              
            ShowGameUI();
        }
        else if (newState == GameState.PreGameOver)
        {
            // Before game over, i.e. game potentially will be recovered
        }
        else if (newState == GameState.GameOver)
        {
            Invoke("ShowGameOverUI", 1f);
        }
    }

    void OnScoreUpdated(int newScore)
    {
        scoreAnimator.Play("NewScore");
    }

    void Reset()
    {
        settingsUI.SetActive(false);

        header.SetActive(false);
        score.gameObject.SetActive(false);
        tapToStart.SetActive(false);
        characterSelectBtn.SetActive(false);
        menuButtons.SetActive(false);
        dailyRewardBtn.SetActive(false);
        settingsUI.SetActive(false);
        toastMsg.SetActive(false);

        // Enable or disable premium stuff
        //bool enablePremium = PremiumFeaturesManager.Instance.enablePremiumFeatures;
        bool enablePremium = false;
        leaderboardBtn.SetActive(enablePremium);
        shareBtn.SetActive(enablePremium);
        iapPurchaseBtn.SetActive(enablePremium);
        removeAdsBtn.SetActive(enablePremium);
        restorePurchaseBtn.SetActive(enablePremium);

        // Hidden by default
        storeUI.SetActive(false);
        settingsUI.SetActive(false);
        shareUI.SetActive(false);

        // These premium feature buttons are hidden by default
        // and shown when certain criteria are met (e.g. rewarded ad is loaded)
        watchForCoinsBtn.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        gameManager.StartGame();
    }

    public void EndGame()
    {
        gameManager.GameOver();
    }

    public void RestartGame()
    {
        gameManager.RestartGame(0.2f);
    }

    public void StopGame()
    {
        gameManager.StopGame();
    }

    void ShowStartUI()
    {
        settingsUI.SetActive(false);

        header.SetActive(true);
        tapToStart.SetActive(true);
        characterSelectBtn.SetActive(true);  

        // If first launch: show "WatchForCoins" and "DailyReward" buttons if the conditions are met
        if (GameManager.GameCount == 0)
        {

            if (showDailyRewardAtStart)
                ShowDailyRewardBtn();
        }
    }

    void ShowGameUI()
    {
        header.SetActive(true);
        score.gameObject.SetActive(true);
        tapToStart.SetActive(false);
        characterSelectBtn.SetActive(false);
        dailyRewardBtn.SetActive(false);
        watchForCoinsBtn.SetActive(false);
    }

    void ShowGameOverUI()
    {
        header.SetActive(true);
        score.gameObject.SetActive(true);
        tapToStart.SetActive(false);
        menuButtons.SetActive(true);

        watchForCoinsBtn.gameObject.SetActive(false);
        settingsUI.SetActive(false);

        // Show "WatchForCoins" and "DailyReward" buttons if the conditions are met     
        ShowDailyRewardBtn();


    }


    void ShowDailyRewardBtn()
    {
        // Not showing the daily reward button if the feature is disabled
        if (!DailyRewardController.Instance.disable)
        {
            dailyRewardBtn.SetActive(true);
        }
    }

    public void ShowSettingsUI()
    {
        settingsUI.SetActive(true);
    }

    public void HideSettingsUI()
    {
        settingsUI.SetActive(false);
    }

    public void ShowStoreUI()
    {
        storeUI.SetActive(true);
    }

    public void HideStoreUI()
    {
        storeUI.SetActive(false);
    }





    public void GrabDailyReward()
    {
        if (DailyRewardController.Instance.CanRewardNow())
        {
            int reward = DailyRewardController.Instance.GetRandomReward();

            // Round the number and make it mutiplies of 5 only.
            int roundedReward = (reward / 5) * 5;

            // Show the reward UI
            ShowRewardUI(roundedReward);

            // Update next time for the reward
            DailyRewardController.Instance.ResetNextRewardTime();
        }
    }

    public void ShowRewardUI(int reward)
    {
        rewardUI.SetActive(true);
        rewardUI.GetComponent<RewardUIController>().Reward(reward);
    }

    public void HideRewardUI()
    {
        rewardUI.GetComponent<RewardUIController>().Close();
    }




    public void ShowShareUI()
    {
        isShowShareUI = !isShowShareUI;
        shareUI.SetActive(isShowShareUI);
    }

  

    public void HideShareUI()
    {
        shareUI.SetActive(false);
    }

    public void ShowCharacterSelectionScene()
    {
        SoundManager.Instance.StopMusic();
        SceneManager.LoadScene("CharacterSelection");
    }

    public void ToggleSound()
    {
        SoundManager.Instance.ToggleMute();
    }

    public void ToggleMusic()
    {
        SoundManager.Instance.ToggleMusic();
    }

    public void RateApp()
    {
        Utilities.RateApp();
    }

    public void OpenTwitterPage()
    {
        Utilities.OpenTwitterPage();
    }

    public void OpenFacebookPage()
    {
        Utilities.OpenFacebookPage();
    }

    public void ButtonClickSound()
    {
        Utilities.ButtonClickSound();
    }

    void UpdateSoundButtons()
    {
        if (SoundManager.Instance.IsMuted())
        {
            soundOnBtn.gameObject.SetActive(false);
            soundOffBtn.gameObject.SetActive(true);
        }
        else
        {
            soundOnBtn.gameObject.SetActive(true);
            soundOffBtn.gameObject.SetActive(false);
        }
    }

    void UpdateMusicButtons()
    {
        if (SoundManager.Instance.IsMusicOff())
        {
            musicOffBtn.gameObject.SetActive(true);
            musicOnBtn.gameObject.SetActive(false);
        }
        else
        {
            musicOffBtn.gameObject.SetActive(false);
            musicOnBtn.gameObject.SetActive(true);
        }
    }

    IEnumerator CRIncreaseCoins(int amount)
    {
        float playTime = 1f;
        float waitTime = playTime / (float)amount;
    
        for (int i = 0; i < amount; i++)
        {
            CoinManager.Instance.AddCoins(1);
    
            yield return new WaitForSeconds(waitTime);
        }
    
        ShowToastMsg("You have earn " + amount);
    }

    public void ShowToastMsg(string msg, float hideAfter = 4f)
    {
        toastMsg.transform.Find("Text").GetComponent<Text>().text = msg;
        toastMsg.SetActive(true);
        Invoke("HideToastMsg", hideAfter);
    }

    public void HideToastMsg()
    {
        toastMsg.SetActive(false);
    }



}
