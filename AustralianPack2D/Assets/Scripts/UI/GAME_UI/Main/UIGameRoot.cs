using System;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private MainFooterPanel_Game mainFooterPanel;
    [SerializeField] private MemorizeFooterPanel_Game memorizeFooterPanel;

    [Header("WIN")]
    [SerializeField] private WinStartPanel_Game winStartPanel;
    [SerializeField] private WinPanel_Game winPanel;

    [Header("GIFT")]
    [SerializeField] private OpenPackPanel_Game openPackPanel;
    [SerializeField] private CardPresentationPanel_Game cardPresentationPanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        mainFooterPanel.Initialize();
        memorizeFooterPanel.Initialize();

        winStartPanel.Initialize();
        winPanel.Initialize();

        openPackPanel.Initialize();
        cardPresentationPanel.Initialize();
    }

    public void Activate()
    {
        winPanel.OnClickToMenu += ClickToMenu_Win;
        winPanel.OnClickToGame += ClickToGame_Win;

        cardPresentationPanel.OnClickToBack += ClickToBack_CardPresentation;
    }

    public void Deactivate()
    {
        winPanel.OnClickToMenu -= ClickToMenu_Win;
        winPanel.OnClickToGame -= ClickToGame_Win;

        cardPresentationPanel.OnClickToBack -= ClickToBack_CardPresentation;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseOpenPackPanel();
        CloseCardPresentationPanel();
        CloseMainFooterPanel();
        CloseMainPanel();
        CloseMemorizeFooterPanel();
        CloseWinPanel();
        CloseWinStartPanel();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        mainFooterPanel.Dispose();
        memorizeFooterPanel.Dispose();

        winStartPanel.Dispose();
        winPanel.Dispose();

        openPackPanel.Dispose();
        cardPresentationPanel.Dispose();
    }

    #region Input

    public void OpenMainPanel()
    {
        if(mainPanel.IsActive) return;

        OpenOtherPanel(mainPanel);
    }

    public void CloseMainPanel()
    {
        if(!mainPanel.IsActive) return;

        CloseOtherPanel(mainPanel);
    }



    public void OpenMainFooterPanel()
    {
        if (mainFooterPanel.IsActive) return;

        OpenOtherPanel(mainFooterPanel);
    }

    public void CloseMainFooterPanel()
    {
        if (!mainFooterPanel.IsActive) return;

        CloseOtherPanel(mainFooterPanel);
    }



    public void OpenMemorizeFooterPanel()
    {
        if (memorizeFooterPanel.IsActive) return;

        OpenOtherPanel(memorizeFooterPanel);
    }

    public void CloseMemorizeFooterPanel()
    {
        if (!memorizeFooterPanel.IsActive) return;

        CloseOtherPanel(memorizeFooterPanel);
    }




    public void OpenWinStartPanel()
    {
        if (winStartPanel.IsActive) return;

        OpenOtherPanel(winStartPanel);
    }

    public void CloseWinStartPanel()
    {
        if (!winStartPanel.IsActive) return;

        CloseOtherPanel(winStartPanel);
    }

    public void OpenWinPanel()
    {
        if (winPanel.IsActive) return;

        OpenOtherPanel(winPanel);
    }

    public void CloseWinPanel()
    {
        if (!winPanel.IsActive) return;

        CloseOtherPanel(winPanel);
    }



    public void OpenOpenPackPanel()
    {
        if(openPackPanel.IsActive) return;

        OpenOtherPanel(openPackPanel);
    }

    public void CloseOpenPackPanel()
    {
        if(!openPackPanel.IsActive) return;

        CloseOtherPanel(openPackPanel);
    }


    public void OpenCardPresentationPanel()
    {
        if (cardPresentationPanel.IsActive) return;

        OpenOtherPanel(cardPresentationPanel);
    }

    public void CloseCardPresentationPanel()
    {
        if (!cardPresentationPanel.IsActive) return;

        CloseOtherPanel(cardPresentationPanel);
    }

    #endregion




    #region Output

    #region WIN

    public event Action OnClickToMenu_Win;
    public event Action OnClickToGame_Win;

    private void ClickToMenu_Win()
    {
        OnClickToMenu_Win?.Invoke();
    }

    private void ClickToGame_Win()
    {
        OnClickToGame_Win?.Invoke();
    }

    #endregion

    #region CARD PRESENTATION

    public event Action OnClickToBack_CardPresentation;

    private void ClickToBack_CardPresentation()
    {
        OnClickToBack_CardPresentation?.Invoke();
    }

    #endregion

    #endregion
}
