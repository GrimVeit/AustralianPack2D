using System;
using UnityEngine;

public class UIMainMenuRoot : UIRoot
{
    private ISoundProvider _soundProvider;

    [Header("BACKGROUNDS")]
    [SerializeField] private MovePanel backgroundPanel_Main;
    [SerializeField] private MovePanel backgroundPanel_Level;
    [SerializeField] private MovePanel backgroundPanel_Settings;
    [SerializeField] private MovePanel backgroundPanel_Shop;
    [SerializeField] private MovePanel backgroundPanel_Album;

    [Header("MAIN")]
    [SerializeField] private MainHeaderPanel_Menu mainHeaderPanel;
    [SerializeField] private MainMiddlePanel_Menu mainMiddlePanel;
    [SerializeField] private PlayFooterPanel_Menu playFooterPanel;

    [Header("LEVEL")]
    [SerializeField] private LevelHeaderPanel_Game levelHeaderPanel;
    [SerializeField] private LevelMiddlePanel_Game levelMiddlePanel;

    [Header("SETTINGS")]
    [SerializeField] private SettingsHeaderPanel_Menu settingsHeaderPanel;
    [SerializeField] private SettingsMiddlePanel_Menu settingsMiddlePanel;

    [Header("SHOP")]
    [SerializeField] private ShopHeaderPanel_Menu shopHeaderPanel;
    [SerializeField] private ShopMiddlePanel_Menu shopMiddlePanel;

    [Header("ALBUM")]
    [SerializeField] private AlbumHeaderPanel_Menu albumHeaderPanel;
    [SerializeField] private AlbumMiddlePanel_Menu albumMiddlePanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        backgroundPanel_Main.Initialize();
        backgroundPanel_Level.Initialize();
        backgroundPanel_Settings.Initialize();
        backgroundPanel_Shop.Initialize();
        backgroundPanel_Album.Initialize();

        mainHeaderPanel.Initialize();
        mainMiddlePanel.Initialize();
        playFooterPanel.Initialize();

        levelHeaderPanel.Initialize();
        levelMiddlePanel.Initialize();

        settingsHeaderPanel.Initialize();
        settingsMiddlePanel.Initialize();

        shopHeaderPanel.Initialize();
        shopMiddlePanel.Initialize();

        albumHeaderPanel.Initialize();
        albumMiddlePanel.Initialize();
    }

    public void Activate()
    {
        playFooterPanel.OnClickToPlay += ClickToPlay_PlayFooter;
        mainMiddlePanel.OnClickLevel += ClickToLevel_MainMiddle;
        mainMiddlePanel.OnClickSettings += ClickToSettings_MainMiddle;
        mainMiddlePanel.OnClickStore += ClickToStore_MainMiddle;
        mainMiddlePanel.OnClickAlbum += ClickToAlbum_MainMiddle;


        levelHeaderPanel.OnClickBack += ClickToExit_LevelHeader;
        levelMiddlePanel.OnClickLevel1 += ClickToLevel1_LevelHeader;
        levelMiddlePanel.OnClickLevel2 += ClickToLevel2_LevelHeader;
        levelMiddlePanel.OnClickLevel3 += ClickToLevel3_LevelHeader;
        levelMiddlePanel.OnClickLevel4 += ClickToLevel4_LevelHeader;
        levelMiddlePanel.OnClickLevel5 += ClickToLevel5_LevelHeader;


        settingsHeaderPanel.OnClickBack += ClickToExit_SettingsHeader;


        shopHeaderPanel.OnClickBack += ClickToExit_ShopHeader;
        shopMiddlePanel.OnClickCover += ClickToCover_ShopMiddle;
        shopMiddlePanel.OnClickCardPack += ClickToCardPack_ShopMiddle;



        albumHeaderPanel.OnClickBack += ClickToExit_AlbumHeader;
    }


    public void Deactivate()
    {
        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        playFooterPanel.OnClickToPlay -= ClickToPlay_PlayFooter;
        mainMiddlePanel.OnClickLevel -= ClickToLevel_MainMiddle;
        mainMiddlePanel.OnClickSettings -= ClickToSettings_MainMiddle;
        mainMiddlePanel.OnClickStore -= ClickToStore_MainMiddle;
        mainMiddlePanel.OnClickAlbum -= ClickToAlbum_MainMiddle;


        levelHeaderPanel.OnClickBack -= ClickToExit_LevelHeader;
        levelMiddlePanel.OnClickLevel1 -= ClickToLevel1_LevelHeader;
        levelMiddlePanel.OnClickLevel2 -= ClickToLevel2_LevelHeader;
        levelMiddlePanel.OnClickLevel3 -= ClickToLevel3_LevelHeader;
        levelMiddlePanel.OnClickLevel4 -= ClickToLevel4_LevelHeader;
        levelMiddlePanel.OnClickLevel5 -= ClickToLevel5_LevelHeader;


        settingsHeaderPanel.OnClickBack -= ClickToExit_SettingsHeader;


        shopHeaderPanel.OnClickBack -= ClickToExit_ShopHeader;
        shopMiddlePanel.OnClickCover -= ClickToCover_ShopMiddle;
        shopMiddlePanel.OnClickCardPack -= ClickToCardPack_ShopMiddle;


        albumHeaderPanel.OnClickBack -= ClickToExit_AlbumHeader;
    }

    public void Dispose()
    {
        backgroundPanel_Main.Dispose();
        backgroundPanel_Level.Dispose();
        backgroundPanel_Settings.Dispose();
        backgroundPanel_Shop.Dispose();
        backgroundPanel_Album.Dispose();

        mainHeaderPanel.Dispose();
        mainMiddlePanel.Dispose();
        playFooterPanel.Dispose();

        levelHeaderPanel.Dispose();
        levelMiddlePanel.Dispose();

        settingsHeaderPanel.Dispose();
        settingsMiddlePanel.Dispose();

        shopHeaderPanel.Dispose();
        shopMiddlePanel.Dispose();

        albumHeaderPanel.Dispose();
        albumMiddlePanel.Dispose();
    }

    #region INPUT

    #region BACKGROUND

    public void OpenBackgroundPanel_Main()
    {
        if(backgroundPanel_Main.IsActive) return;

        OpenOtherPanel(backgroundPanel_Main);
    }

    public void CloseBackgroundPanel_Main()
    {
        if(!backgroundPanel_Main.IsActive) return;

        CloseOtherPanel(backgroundPanel_Main);
    }




    public void OpenBackgroundPanel_Level()
    {
        if (backgroundPanel_Level.IsActive) return;

        OpenOtherPanel(backgroundPanel_Level);
    }

    public void CloseBackgroundPanel_Level()
    {
        if (!backgroundPanel_Level.IsActive) return;

        CloseOtherPanel(backgroundPanel_Level);
    }



    public void OpenBackgroundPanel_Settings()
    {
        if (backgroundPanel_Settings.IsActive) return;

        OpenOtherPanel(backgroundPanel_Settings);
    }

    public void CloseBackgroundPanel_Settings()
    {
        if (!backgroundPanel_Settings.IsActive) return;

        CloseOtherPanel(backgroundPanel_Settings);
    }




    public void OpenBackgroundPanel_Shop()
    {
        if (backgroundPanel_Shop.IsActive) return;

        OpenOtherPanel(backgroundPanel_Shop);
    }

    public void CloseBackgroundPanel_Shop()
    {
        if (!backgroundPanel_Shop.IsActive) return;

        CloseOtherPanel(backgroundPanel_Shop);
    }



    public void OpenBackgroundPanel_Album()
    {
        if (backgroundPanel_Album.IsActive) return;

        OpenOtherPanel(backgroundPanel_Album);
    }

    public void CloseBackgroundPanel_Album()
    {
        if (!backgroundPanel_Album.IsActive) return;

        CloseOtherPanel(backgroundPanel_Album);
    }

    #endregion

    #region MAIN

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



    public void OpenMainMiddlePanel()
    {
        if (mainMiddlePanel.IsActive) return;

        OpenOtherPanel(mainMiddlePanel);
    }

    public void CloseMainMiddlePanel()
    {
        if (!mainMiddlePanel.IsActive) return;

        CloseOtherPanel(mainMiddlePanel);
    }



    public void OpenPlayFooterPanel()
    {
        if(playFooterPanel.IsActive) return;

        OpenOtherPanel(playFooterPanel);
    }

    public void ClosePlayFooterPanel()
    {
        if(!playFooterPanel.IsActive) return;

        CloseOtherPanel(playFooterPanel);
    }

    #endregion

    #region LEVEL

    public void OpenLevelHeaderPanel()
    {
        if(levelHeaderPanel.IsActive) return;

        OpenOtherPanel(levelHeaderPanel);
    }

    public void CloseLevelHeaderPanel()
    {
        if(!levelHeaderPanel.IsActive) return;

        CloseOtherPanel(levelHeaderPanel);
    }



    public void OpenLevelMiddlePanel()
    {
        if(levelMiddlePanel.IsActive) return;

        OpenOtherPanel(levelMiddlePanel);
    }

    public void CloseLevelMiddlePanel()
    {
        if(!levelMiddlePanel.IsActive) return;

        CloseOtherPanel(levelMiddlePanel);
    }

    #endregion

    #region SETTINGS

    public void OpenSettingsHeaderPanel()
    {
        if (settingsHeaderPanel.IsActive) return;

        OpenOtherPanel(settingsHeaderPanel);
    }

    public void CloseSettingsHeaderPanel()
    {
        if (!settingsHeaderPanel.IsActive) return;

        CloseOtherPanel(settingsHeaderPanel);
    }



    public void OpenSettingsMiddlePanel()
    {
        if (settingsMiddlePanel.IsActive) return;

        OpenOtherPanel(settingsMiddlePanel);
    }

    public void CloseSettingsMiddlePanel()
    {
        if (!settingsMiddlePanel.IsActive) return;

        CloseOtherPanel(settingsMiddlePanel);
    }

    #endregion

    #region SHOP

    public void OpenShopHeaderPanel()
    {
        if (shopHeaderPanel.IsActive) return;

        OpenOtherPanel(shopHeaderPanel);
    }

    public void CloseShopHeaderPanel()
    {
        if (!shopHeaderPanel.IsActive) return;

        CloseOtherPanel(shopHeaderPanel);
    }



    public void OpenShopMiddlePanel()
    {
        if (shopMiddlePanel.IsActive) return;

        OpenOtherPanel(shopMiddlePanel);
    }

    public void CloseShopMiddlePanel()
    {
        if (!shopMiddlePanel.IsActive) return;

        CloseOtherPanel(shopMiddlePanel);
    }

    #endregion

    #region ALBUM

    public void OpenAlbumHeaderPanel()
    {
        if (albumHeaderPanel.IsActive) return;

        OpenOtherPanel(albumHeaderPanel);
    }

    public void CloseAlbumHeaderPanel()
    {
        if (!albumHeaderPanel.IsActive) return;

        CloseOtherPanel(albumHeaderPanel);
    }



    public void OpenAlbumMiddlePanel()
    {
        if (albumMiddlePanel.IsActive) return;

        OpenOtherPanel(albumMiddlePanel);
    }

    public void CloseAlbumMiddlePanel()
    {
        if (!albumMiddlePanel.IsActive) return;

        CloseOtherPanel(albumMiddlePanel);
    }

    #endregion

    #endregion


    #region OUTPUT

    #region MAIN

    public event Action OnClickToPlay_PlayFooter;

    public event Action OnClickToLevel_MainMiddle;
    public event Action OnClickToSettings_MainMiddle;
    public event Action OnClickToShop_MainMiddle;
    public event Action OnClickToAlbum_MainMiddle;

    private void ClickToPlay_PlayFooter()
    {
        OnClickToPlay_PlayFooter?.Invoke();
    }


    private void ClickToLevel_MainMiddle()
    {
        OnClickToLevel_MainMiddle?.Invoke();
    }

    private void ClickToSettings_MainMiddle()
    {
        OnClickToSettings_MainMiddle?.Invoke();
    }

    private void ClickToStore_MainMiddle()
    {
        OnClickToShop_MainMiddle?.Invoke();
    }

    private void ClickToAlbum_MainMiddle()
    {
        OnClickToAlbum_MainMiddle?.Invoke();
    }

    #endregion

    #region LEVEL

    public event Action OnClickToExit_LevelHeader;

    public event Action OnClickToLevel1_LevelMiddle;
    public event Action OnClickToLevel2_LevelMiddle;
    public event Action OnClickToLevel3_LevelMiddle;
    public event Action OnClickToLevel4_LevelMiddle;
    public event Action OnClickToLevel5_LevelMiddle;

    private void ClickToExit_LevelHeader()
    {
        OnClickToExit_LevelHeader?.Invoke();
    }

    private void ClickToLevel1_LevelHeader()
    {
        OnClickToLevel1_LevelMiddle?.Invoke();
    }

    private void ClickToLevel2_LevelHeader()
    {
        OnClickToLevel2_LevelMiddle?.Invoke();
    }

    private void ClickToLevel3_LevelHeader()
    {
        OnClickToLevel3_LevelMiddle?.Invoke();
    }

    private void ClickToLevel4_LevelHeader()
    {
        OnClickToLevel4_LevelMiddle?.Invoke();
    }

    private void ClickToLevel5_LevelHeader()
    {
        OnClickToLevel5_LevelMiddle?.Invoke();
    }

    #endregion

    #region SETTINGS

    public event Action OnClickToExit_SettingsHeader;

    private void ClickToExit_SettingsHeader()
    {
        OnClickToExit_SettingsHeader?.Invoke();
    }

    #endregion

    #region LEVEL

    public event Action OnClickToExit_ShopHeader;

    public event Action OnClickToCover_ShopMiddle;
    public event Action OnClickToCardPack_ShopMiddle;

    private void ClickToExit_ShopHeader()
    {
        OnClickToExit_ShopHeader?.Invoke();
    }

    private void ClickToCover_ShopMiddle()
    {
        OnClickToCover_ShopMiddle?.Invoke();
    }

    private void ClickToCardPack_ShopMiddle()
    {
        OnClickToCardPack_ShopMiddle?.Invoke();
    }

    #endregion

    #region ALBUM

    public event Action OnClickToExit_AlbumHeader;

    private void ClickToExit_AlbumHeader()
    {
        OnClickToExit_AlbumHeader?.Invoke();
    }

    #endregion

    #endregion

}
