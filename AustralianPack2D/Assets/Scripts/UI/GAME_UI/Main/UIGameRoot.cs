using System;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private MainFooterPanel_Game mainFooterPanel;
    [SerializeField] private MemorizeFooterPanel_Game memorizeFooterPanel;

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
    }

    public void Activate()
    {
        
    }

    public void Deactivate()
    {
        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        mainFooterPanel.Dispose();
        memorizeFooterPanel.Dispose();
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

    #endregion




    #region Output



    #endregion
}
