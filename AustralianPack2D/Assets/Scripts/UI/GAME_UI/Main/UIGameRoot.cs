using System;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private MovePanel introVideoPanel;
    [SerializeField] private PlayVideoPanel_Game playVideoPanel;
    [SerializeField] private PlayPanel_Game playPanel;
    [SerializeField] private MovePanel winVideoPanel;
    [SerializeField] private MovePanel loseVideoPanel;
    [SerializeField] private ChooseLocationPanel_Game chooseLocationPanel;
    [SerializeField] private SettingsPanel_Game settingsPanel;

    [SerializeField] private MovePanel backgroundBrownPanel;
    [SerializeField] private MovePanel backgroundBarnPanel;
    [SerializeField] private MovePanel backgroundBlackPanel;

    [SerializeField] private MainHeaderPanel_Game mainHeaderPanel;
    [SerializeField] private MainPanel_Game mainPanel;

    [SerializeField] private ChoosePanel_Game choosePanel;

    [SerializeField] private LosePanel_Game losePanel;
    [SerializeField] private WinPanel_Game winPanel;

    [SerializeField] private CostBoxPanel_Game costBoxPanel;
    [SerializeField] private Panel buyBoxPanel;




    [SerializeField] private PiecesPanel_Game piecesPanel;
    [SerializeField] private CardsHeaderPanel_Game cardsHeaderPanel;
    [SerializeField] private CardsPanel_Game cardsPanel;
    [SerializeField] private CardsTypePanel_Game cardsTypePanel;


    [SerializeField] private NotFullPicturePanel_Game notFullPicturePanel;
    [SerializeField] private FullPicturePanel_Game fullPicturePanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        introVideoPanel.Initialize();
        playVideoPanel.Initialize();
        playPanel.Initialize();
        winVideoPanel.Initialize();
        loseVideoPanel.Initialize();
        chooseLocationPanel.Initialize();
        settingsPanel.Initialize();

        backgroundBarnPanel.Initialize();
        backgroundBrownPanel.Initialize();
        backgroundBlackPanel.Initialize();

        mainHeaderPanel.Initialize();
        mainPanel.Initialize();
        choosePanel.Initialize();

        losePanel.Initialize();
        winPanel.Initialize();

        costBoxPanel.Initialize();
        buyBoxPanel.Initialize();



        piecesPanel.Initialize();
        cardsHeaderPanel.Initialize();
        cardsTypePanel.Initialize();
        cardsPanel.Initialize();

        notFullPicturePanel.Initialize();
        fullPicturePanel.Initialize();
    }

    public void Activate()
    {
        playVideoPanel.OnClickPlay += ClickToPlay_PLAY;
        playPanel.OnClickSettings += ClickToSettings_PLAY;
        playPanel.OnClickCollection += ClickToCollection_PLAY;

        settingsPanel.OnClickToExit += ClickToExit_Settings;

        mainHeaderPanel.OnClickToMenu += ClickToMenu_MainHeader;

        choosePanel.OnChoose += ClickToChoose_CHOOSE;

        losePanel.OnClickToRestart += ClickToRestart_LOSE;
        losePanel.OnClickToMenu += ClickToMenu_LOSE;
        losePanel.OnClickToExit += ClickToExit_LOSE;

        winPanel.OnClickToRestart += ClickToRestart_WIN;
        winPanel.OnClickToBuy += ClickToBuy_WIN;
        winPanel.OnClickToMenu += ClickToMenu_WIN;
        winPanel.OnClickToExit += ClickToExit_WIN;

        costBoxPanel.OnClickToBuy += ClickToBox_COSTBOX;
        costBoxPanel.OnClickToMenu += ClickToMenu_COSTBOX;
        costBoxPanel.OnClickToExit += ClickToExit_COSTBOX;


        cardsHeaderPanel.OnClickToExit += ClickToExit_CARDSHEADER;
        cardsHeaderPanel.OnClickToMenu += ClickToMenu_CARDSHEADER;

        fullPicturePanel.OnClickExit += ClickToExit_FullPicture;
    }

    public void Deactivate()
    {
        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        playVideoPanel.OnClickPlay -= ClickToPlay_PLAY;
        playPanel.OnClickSettings -= ClickToSettings_PLAY;
        playPanel.OnClickCollection -= ClickToCollection_PLAY;

        settingsPanel.OnClickToExit -= ClickToExit_Settings;

        mainHeaderPanel.OnClickToMenu -= ClickToMenu_MainHeader;

        choosePanel.OnChoose -= ClickToChoose_CHOOSE;

        losePanel.OnClickToRestart -= ClickToRestart_LOSE;
        losePanel.OnClickToMenu -= ClickToMenu_LOSE;
        losePanel.OnClickToExit -= ClickToExit_LOSE;

        winPanel.OnClickToRestart -= ClickToRestart_WIN;
        winPanel.OnClickToBuy -= ClickToBuy_WIN;
        winPanel.OnClickToMenu -= ClickToMenu_WIN;
        winPanel.OnClickToExit -= ClickToExit_WIN;

        costBoxPanel.OnClickToBuy -= ClickToBox_COSTBOX;
        costBoxPanel.OnClickToMenu -= ClickToMenu_COSTBOX;
        costBoxPanel.OnClickToExit -= ClickToExit_COSTBOX;


        cardsHeaderPanel.OnClickToExit -= ClickToExit_CARDSHEADER;
        cardsHeaderPanel.OnClickToMenu -= ClickToMenu_CARDSHEADER;

        fullPicturePanel.OnClickExit -= ClickToExit_FullPicture;
    }

    public void Dispose()
    {
        introVideoPanel.Dispose();
        playVideoPanel.Dispose();
        playPanel.Dispose();
        winVideoPanel.Dispose();
        loseVideoPanel.Dispose();
        chooseLocationPanel.Dispose();
        settingsPanel.Dispose();

        backgroundBarnPanel.Dispose();
        backgroundBrownPanel.Dispose();
        backgroundBlackPanel.Dispose();

        mainHeaderPanel.Dispose();
        mainPanel.Dispose();
        choosePanel.Dispose();

        losePanel.Dispose();
        winPanel.Dispose();

        costBoxPanel.Dispose();
        buyBoxPanel.Dispose();




        piecesPanel.Dispose();
        cardsHeaderPanel.Dispose();
        cardsTypePanel.Dispose();
        cardsPanel.Dispose();

        notFullPicturePanel.Dispose();
        fullPicturePanel.Dispose();
    }

    #region Input

    #region VIDEO

    public void OpenIntroVideoPanel()
    {
        if(introVideoPanel.IsActive) return;

        OpenOtherPanel(introVideoPanel);
    }

    public void CloseIntroVideoPanel()
    {
        if(!introVideoPanel.IsActive) return;

        CloseOtherPanel(introVideoPanel);
    }



    public void OpenPlayVideoPanel()
    {
        if(playVideoPanel.IsActive) return;

        OpenOtherPanel(playVideoPanel);
    }

    public void ClosePlayVideoPanel()
    {
        if(!playVideoPanel.IsActive) return;

        CloseOtherPanel(playVideoPanel);
    }



    public void OpenWinVideoPanel()
    {
        if(winVideoPanel.IsActive) return;

        OpenOtherPanel(winVideoPanel);
    }

    public void CloseWinVideoPanel()
    {
        if(!winVideoPanel.IsActive) return;

        CloseOtherPanel(winVideoPanel);
    }



    public void OpenLoseVideoPanel()
    {
        if(loseVideoPanel.IsActive) return;

        OpenOtherPanel(loseVideoPanel);
    }

    public void CloseLoseVideoPanel()
    {
        if(!loseVideoPanel.IsActive) return;

        CloseOtherPanel(loseVideoPanel);
    }

    #endregion

    public void OpenPlayPanel()
    {
        if(playPanel.IsActive) return;

        OpenOtherPanel(playPanel);
    }

    public void ClosePlayPanel()
    {
        if(!playPanel.IsActive) return;

        CloseOtherPanel(playPanel);
    }


    public void OpenMainPanel()
    {
        if(mainPanel.IsActive) return;

        OpenOtherPanel(mainPanel);
    }

    public void CLoseMainPanel()
    {
        if(!mainPanel.IsActive) return;

        CloseOtherPanel(mainPanel);
    }


    public void OpenMainHeaderPanel()
    {
        if(mainHeaderPanel.IsActive) return;

        OpenOtherPanel(mainHeaderPanel);
    }

    public void CloseMainHeaderPanel()
    {
        if(!mainHeaderPanel.IsActive) return;

        CloseOtherPanel(mainHeaderPanel);
    }



    public void OpenBackgroundBrownPanel()
    {
        if(backgroundBrownPanel.IsActive) return;

        OpenOtherPanel(backgroundBrownPanel);
    }

    public void CloseBackgroundBrownPanel()
    {
        if(!backgroundBrownPanel.IsActive) return;

        CloseOtherPanel(backgroundBrownPanel);
    }


    public void OpenBackgroundBarnPanel()
    {
        if(backgroundBarnPanel.IsActive) return;

        OpenOtherPanel(backgroundBarnPanel);
    }

    public void CloseBackgroundBarnPanel()
    {
        if(!backgroundBarnPanel.IsActive) return;

        CloseOtherPanel(backgroundBarnPanel);
    }


    public void OpenBackgroundBlackPanel()
    {
        if(backgroundBlackPanel.IsActive) return;

        OpenOtherPanel(backgroundBlackPanel);
    }

    public void CloseBackgroundBlackPanel()
    {
        if(!backgroundBlackPanel.IsActive) return;

        CloseOtherPanel(backgroundBlackPanel);
    }



    public void OpenChoosePanel()
    {
        if(choosePanel.IsActive) return;

        OpenOtherPanel(choosePanel);
    }

    public void CloseChoosePanel()
    {
        if(!choosePanel.IsActive) return;

        CloseOtherPanel(choosePanel);
    }




    public void OpenLosePanel()
    {
        if(losePanel.IsActive) return;

        OpenOtherPanel(losePanel);
    }

    public void CloseLosePanel()
    {
        if(!losePanel.IsActive) return;

        CloseOtherPanel(losePanel);
    }

    public void OpenWinPanel()
    {
        if(winPanel.IsActive) return;

        OpenOtherPanel(winPanel);
    }

    public void CloseWinPanel()
    {
        if(!winPanel.IsActive) return;

        CloseOtherPanel(winPanel);
    }



    public void OpenCostBoxPanel()
    {
        if(costBoxPanel.IsActive) return;

        OpenOtherPanel(costBoxPanel);
    }

    public void CloseCostBoxPanel()
    {
        if(!costBoxPanel.IsActive) return;

        CloseOtherPanel(costBoxPanel);
    }

    public void OpenBuyBoxPanel()
    {
        OpenOtherPanel(buyBoxPanel);
    }

    public void CloseBuyBoxPanel()
    {
        CloseOtherPanel(buyBoxPanel);
    }



    public void OpenChooseLocationPanel()
    {
        if(chooseLocationPanel.IsActive) return;

        OpenOtherPanel(chooseLocationPanel);
    }

    public void CloseChooseLocationPanel()
    {
        if(!chooseLocationPanel.IsActive) return;

        CloseOtherPanel(chooseLocationPanel);
    }




    public void OpenSettingsPanel()
    {
        if(settingsPanel.IsActive) return;

        OpenOtherPanel(settingsPanel);
    }

    public void CloseSettingsPanel()
    {
        if(!settingsPanel.IsActive) return;

        CloseOtherPanel(settingsPanel);
    }



    #region CARDS

    public void OpenPiecesPanel()
    {
        if(piecesPanel.IsActive) return;

        OpenOtherPanel(piecesPanel);
    }

    public void ClosePiecesPanel()
    {
        if(!piecesPanel.IsActive) return;

        CloseOtherPanel(piecesPanel);
    }


    public void OpenCardsHeaderPanel()
    {
        if(cardsHeaderPanel.IsActive) return;

        OpenOtherPanel(cardsHeaderPanel);
    }

    public void CloseCardsHeaderPanel()
    {
        if(!cardsHeaderPanel.IsActive) return;

        CloseOtherPanel(cardsHeaderPanel);
    }


    public void OpenCardsPanel()
    {
        if(cardsPanel.IsActive) return;

        OpenOtherPanel(cardsPanel);
    }

    public void CloseCardsPanel()
    {
        if(!cardsPanel.IsActive) return;

        CloseOtherPanel(cardsPanel);
    }


    public void OpenCardsTypePanel()
    {
        if(cardsTypePanel.IsActive) return;

        OpenOtherPanel(cardsTypePanel);
    } 

    public void CloseCardsTypePanel()
    {
        if(!cardsTypePanel.IsActive) return;

        CloseOtherPanel(cardsTypePanel);
    }

    #endregion



    #region SHOW PICTURE

    public void OpenNotFullPicturePanel()
    {
        if(notFullPicturePanel.IsActive) return;

        OpenOtherPanel(notFullPicturePanel);
    }

    public void CloseNotFullPicturePanel()
    {
        if(!notFullPicturePanel.IsActive) return;

        CloseOtherPanel(notFullPicturePanel);
    }



    public void OpenFullPicturePanel()
    {
        if(fullPicturePanel.IsActive) return;

        OpenOtherPanel(fullPicturePanel);
    }

    public void CloseFullPicturePanel()
    {
        if(!fullPicturePanel.IsActive) return;

        CloseOtherPanel(fullPicturePanel);
    }

    #endregion

    #endregion


    #region Output

    #region MAIN_HEADER

    public event Action OnClickToMenu_MAINHEADER;

    private void ClickToMenu_MainHeader()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToMenu_MAINHEADER?.Invoke();
    }

    #endregion

    #region PLAY

    public event Action OnClickToPlay_PLAY;
    public event Action OnClickToSettings_PLAY;
    public event Action OnClickToCollection_PLAY;

    private void ClickToPlay_PLAY()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToPlay_PLAY?.Invoke();
    }

    private void ClickToSettings_PLAY()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToSettings_PLAY?.Invoke();
    }

    private void ClickToCollection_PLAY()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToCollection_PLAY?.Invoke();
    }

    #endregion

    #region SETTINGS

    public event Action OnClickToExit_SETTINGS;

    private void ClickToExit_Settings()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToExit_SETTINGS?.Invoke();
    }

    #endregion


    #region CHOOSE

    public event Action OnClickToChoose_CHOOSE;

    private void ClickToChoose_CHOOSE()
    {
        //_soundProvider.PlayOneShot("PanelOpen");

        OnClickToChoose_CHOOSE?.Invoke();
    }

    #endregion

    #region LOSE

    public event Action OnClickToRestart_LOSE;
    public event Action OnClickToExit_LOSE;
    public event Action OnClickToMenu_LOSE;

    private void ClickToRestart_LOSE()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToRestart_LOSE?.Invoke();
    }

    private void ClickToExit_LOSE()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToExit_LOSE?.Invoke();
    }

    private void ClickToMenu_LOSE()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToMenu_LOSE?.Invoke();
    }

    #endregion

    #region WIN

    public event Action OnClickToRestart_WIN;
    public event Action OnClickToBuy_WIN;
    public event Action OnClickToMenu_WIN;
    public event Action OnClickToExit_WIN;

    private void ClickToRestart_WIN()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToRestart_WIN?.Invoke();
    }

    private void ClickToBuy_WIN()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToBuy_WIN?.Invoke();
    }

    private void ClickToMenu_WIN()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToMenu_WIN?.Invoke();
    }

    private void ClickToExit_WIN()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToExit_WIN?.Invoke();
    }

    #endregion


    #region COST_BOX

    public event Action OnClickToBox_COSTBOX;
    public event Action OnClickToMenu_COSTBOX;
    public event Action OnClickToExit_COSTBOX;

    private void ClickToBox_COSTBOX()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToBox_COSTBOX?.Invoke();
    }

    private void ClickToMenu_COSTBOX()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToMenu_COSTBOX?.Invoke();
    }

    private void ClickToExit_COSTBOX()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToExit_COSTBOX?.Invoke();
    }

    #endregion

    #region COST_BOX

    public event Action OnClickToExit_CARDSHEADER;
    public event Action OnClickToMenu_CARDSHEADER;

    private void ClickToExit_CARDSHEADER()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToExit_CARDSHEADER?.Invoke();
    }

    private void ClickToMenu_CARDSHEADER()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToMenu_CARDSHEADER?.Invoke();
    }

    #endregion

    #region FULL_PICTURE

    public event Action OnClickToExit_FULLPICTURE;

    private void ClickToExit_FullPicture()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToExit_FULLPICTURE?.Invoke();
    }

    #endregion

    #endregion
}
