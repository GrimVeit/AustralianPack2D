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
    [SerializeField] private MovePanel backgroundPanel_Green;

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

    [Header("SETTINGS")]
    [SerializeField] private LeadersHeaderPanel_Menu leadersHeaderPanel;
    [SerializeField] private LeadersMiddlePanel_Menu leadersMiddlePanel;

    [Header("SHOP")]
    [SerializeField] private ShopHeaderPanel_Menu shopHeaderPanel;
    [SerializeField] private ShopMiddlePanel_Menu shopMiddlePanel;

    [SerializeField] private ShopCoverTablePanel_Menu shopCoverTablePanel;
    [SerializeField] private ShopCoverTableFooterPanel_Menu shopCoverTableFooterPanel;

    [SerializeField] private ShopChoosePackPanel_Menu shopChoosePackPanel;
    [SerializeField] private ShopOpenPackPanel_Menu shopOpenPackPanel;
    [SerializeField] private ShopOpenPackFooterPanel_Menu shopOpenPackFooterPanel;
    [SerializeField] private CardPresentationPanel_Menu shopCardPresentationPanel;

    [Header("ALBUM")]
    [SerializeField] private AlbumHeaderPanel_Menu albumHeaderPanel;
    [SerializeField] private AlbumMiddlePanel_Menu albumChooseMiddlePanel;
    [SerializeField] private AlbumTablePanel_Menu albumTablePanel;
    [SerializeField] private AlbumTableFooterPanel_Menu albumTableFooterPanel;

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
        backgroundPanel_Green.Initialize();

        mainHeaderPanel.Initialize();
        mainMiddlePanel.Initialize();
        playFooterPanel.Initialize();

        levelHeaderPanel.Initialize();
        levelMiddlePanel.Initialize();

        settingsHeaderPanel.Initialize();
        settingsMiddlePanel.Initialize();

        leadersHeaderPanel.Initialize();
        leadersMiddlePanel.Initialize();

        shopHeaderPanel.Initialize();
        shopMiddlePanel.Initialize();
        shopCoverTablePanel.Initialize();
        shopCoverTableFooterPanel.Initialize();

        shopChoosePackPanel.Initialize();
        shopOpenPackPanel.Initialize();
        shopOpenPackFooterPanel.Initialize();
        shopCardPresentationPanel.Initialize();

        albumHeaderPanel.Initialize();
        albumChooseMiddlePanel.Initialize();
        albumTablePanel.Initialize();
        albumTableFooterPanel.Initialize();
    }

    public void Activate()
    {
        playFooterPanel.OnClickToPlay += ClickToPlay_PlayFooter;
        mainMiddlePanel.OnClickLevel += ClickToLevel_MainMiddle;
        mainMiddlePanel.OnClickSettings += ClickToSettings_MainMiddle;
        mainMiddlePanel.OnClickStore += ClickToStore_MainMiddle;
        mainMiddlePanel.OnClickAlbum += ClickToAlbum_MainMiddle;
        mainMiddlePanel.OnClickLeaders += ClickToLeaders_MainMiddle;


        levelHeaderPanel.OnClickBack += ClickToExit_LevelHeader;
        levelMiddlePanel.OnClickLevel1 += ClickToLevel1_LevelHeader;
        levelMiddlePanel.OnClickLevel2 += ClickToLevel2_LevelHeader;
        levelMiddlePanel.OnClickLevel3 += ClickToLevel3_LevelHeader;
        levelMiddlePanel.OnClickLevel4 += ClickToLevel4_LevelHeader;
        levelMiddlePanel.OnClickLevel5 += ClickToLevel5_LevelHeader;


        settingsHeaderPanel.OnClickBack += ClickToExit_SettingsHeader;

        leadersHeaderPanel.OnClickBack += ClickToExit_LeadersHeader;


        shopHeaderPanel.OnClickBack += ClickToExit_ShopHeader;
        shopMiddlePanel.OnClickCover += ClickToCover_ShopMiddle;
        shopMiddlePanel.OnClickCardPack += ClickToCardPack_ShopMiddle;
        shopChoosePackPanel.OnClickStandard += ClickToStandard_ShopChoosePack;
        shopChoosePackPanel.OnClickPriority += ClickToPriority_ShopChoosePack;
        shopOpenPackFooterPanel.OnClickToAlbum += ClickToAlbum_ShopOpenPackFooter;
        shopCardPresentationPanel.OnClickToBack += ClickToBack_ShopCardPresentation;



        albumHeaderPanel.OnClickBack += ClickToExit_AlbumHeader;
        albumChooseMiddlePanel.OnClickCommon += ClickToCommon_AlbumChoose;
        albumChooseMiddlePanel.OnClickUncommon += ClickToUncommon_AlbumChoose;
        albumChooseMiddlePanel.OnClickRare += ClickToRare_AlbumChoose;
        albumChooseMiddlePanel.OnClickEpic += ClickToEpic_AlbumChoose;
        albumChooseMiddlePanel.OnClickMythical += ClickToMythical_AlbumChoose;
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
        mainMiddlePanel.OnClickLeaders -= ClickToLeaders_MainMiddle;


        levelHeaderPanel.OnClickBack -= ClickToExit_LevelHeader;
        levelMiddlePanel.OnClickLevel1 -= ClickToLevel1_LevelHeader;
        levelMiddlePanel.OnClickLevel2 -= ClickToLevel2_LevelHeader;
        levelMiddlePanel.OnClickLevel3 -= ClickToLevel3_LevelHeader;
        levelMiddlePanel.OnClickLevel4 -= ClickToLevel4_LevelHeader;
        levelMiddlePanel.OnClickLevel5 -= ClickToLevel5_LevelHeader;


        settingsHeaderPanel.OnClickBack -= ClickToExit_SettingsHeader;

        leadersHeaderPanel.OnClickBack -= ClickToExit_LeadersHeader;


        shopHeaderPanel.OnClickBack -= ClickToExit_ShopHeader;
        shopMiddlePanel.OnClickCover -= ClickToCover_ShopMiddle;
        shopMiddlePanel.OnClickCardPack -= ClickToCardPack_ShopMiddle;
        shopChoosePackPanel.OnClickStandard -= ClickToStandard_ShopChoosePack;
        shopChoosePackPanel.OnClickPriority -= ClickToPriority_ShopChoosePack;
        shopOpenPackFooterPanel.OnClickToAlbum -= ClickToAlbum_ShopOpenPackFooter;
        shopCardPresentationPanel.OnClickToBack -= ClickToBack_ShopCardPresentation;


        albumHeaderPanel.OnClickBack -= ClickToExit_AlbumHeader;
        albumChooseMiddlePanel.OnClickCommon -= ClickToCommon_AlbumChoose;
        albumChooseMiddlePanel.OnClickUncommon -= ClickToUncommon_AlbumChoose;
        albumChooseMiddlePanel.OnClickRare -= ClickToRare_AlbumChoose;
        albumChooseMiddlePanel.OnClickEpic -= ClickToEpic_AlbumChoose;
        albumChooseMiddlePanel.OnClickMythical -= ClickToMythical_AlbumChoose;
    }

    public void Dispose()
    {
        backgroundPanel_Main.Dispose();
        backgroundPanel_Level.Dispose();
        backgroundPanel_Settings.Dispose();
        backgroundPanel_Shop.Dispose();
        backgroundPanel_Album.Dispose();
        backgroundPanel_Green.Dispose();

        mainHeaderPanel.Dispose();
        mainMiddlePanel.Dispose();
        playFooterPanel.Dispose();

        levelHeaderPanel.Dispose();
        levelMiddlePanel.Dispose();

        settingsHeaderPanel.Dispose();
        settingsMiddlePanel.Dispose();

        leadersHeaderPanel.Dispose();
        leadersMiddlePanel.Dispose();

        shopHeaderPanel.Dispose();
        shopMiddlePanel.Dispose();
        shopCoverTablePanel.Dispose();
        shopCoverTableFooterPanel.Dispose();

        shopChoosePackPanel.Dispose();
        shopOpenPackPanel.Dispose();
        shopOpenPackFooterPanel.Dispose();
        shopCardPresentationPanel.Dispose();

        albumHeaderPanel.Dispose();
        albumChooseMiddlePanel.Dispose();
        albumTablePanel.Dispose();
        albumTableFooterPanel.Dispose();
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



    public void OpenBackgroundPanel_Green()
    {
        if (backgroundPanel_Green.IsActive) return;

        OpenOtherPanel(backgroundPanel_Green);
    }

    public void CloseBackgroundPanel_Green()
    {
        if (!backgroundPanel_Green.IsActive) return;

        CloseOtherPanel(backgroundPanel_Green);
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



    public void OpenShopCoverTablePanel()
    {
        if (shopCoverTablePanel.IsActive) return;

        OpenOtherPanel(shopCoverTablePanel);
    }

    public void CloseShopCoverTablePanel()
    {
        if (!shopCoverTablePanel.IsActive) return;

        CloseOtherPanel(shopCoverTablePanel);
    }



    public void OpenShopCoverTableFooterPanel()
    {
        if (shopCoverTableFooterPanel.IsActive) return;

        OpenOtherPanel(shopCoverTableFooterPanel);
    }

    public void CloseShopCoverTableFooterPanel()
    {
        if (!shopCoverTableFooterPanel.IsActive) return;

        CloseOtherPanel(shopCoverTableFooterPanel);
    }




    public void OpenShopChoosePackPanel()
    {
        if (shopChoosePackPanel.IsActive) return;

        OpenOtherPanel(shopChoosePackPanel);
    }

    public void CloseShopChoosePackPanel()
    {
        if (!shopChoosePackPanel.IsActive) return;

        CloseOtherPanel(shopChoosePackPanel);
    }


    public void OpenShopOpenPackPanel()
    {
        if (shopOpenPackPanel.IsActive) return;

        OpenOtherPanel(shopOpenPackPanel);
    }

    public void CloseShopOpenPackPanel()
    {
        if (!shopOpenPackPanel.IsActive) return;

        CloseOtherPanel(shopOpenPackPanel);
    }



    public void OpenShopOpenFooterPackPanel()
    {
        if (shopOpenPackFooterPanel.IsActive) return;

        OpenOtherPanel(shopOpenPackFooterPanel);
    }

    public void CloseShopOpenFooterPackPanel()
    {
        if (!shopOpenPackFooterPanel.IsActive) return;

        CloseOtherPanel(shopOpenPackFooterPanel);
    }


    public void OpenShopCardPresentationPanel()
    {
        if (shopCardPresentationPanel.IsActive) return;

        OpenOtherPanel(shopCardPresentationPanel);
    }

    public void CloseShopCardPresentationPanel()
    {
        if (!shopCardPresentationPanel.IsActive) return;

        CloseOtherPanel(shopCardPresentationPanel);
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
        if (albumChooseMiddlePanel.IsActive) return;

        OpenOtherPanel(albumChooseMiddlePanel);
    }

    public void CloseAlbumMiddlePanel()
    {
        if (!albumChooseMiddlePanel.IsActive) return;

        CloseOtherPanel(albumChooseMiddlePanel);
    }



    public void OpenAlbumTablePanel()
    {
        if(albumTablePanel.IsActive) return;

        OpenOtherPanel(albumTablePanel);
    }

    public void CloseAlbumTablePanel()
    {
        if (!albumTablePanel.IsActive) return;

        CloseOtherPanel(albumTablePanel);
    }



    public void OpenAlbumTableFooterPanel()
    {
        if (albumTableFooterPanel.IsActive) return;

        OpenOtherPanel(albumTableFooterPanel);
    }

    public void CloseAlbumTableFooterPanel()
    {
        if (!albumTableFooterPanel.IsActive) return;

        CloseOtherPanel(albumTableFooterPanel);
    }

    #endregion

    #region LEADERS

    public void OpenLeadersHeaderPanel()
    {
        if(leadersHeaderPanel.IsActive) return;

        OpenOtherPanel(leadersHeaderPanel);
    }

    public void CloseLeadersHeaderPanel()
    {
        if (!leadersHeaderPanel.IsActive) return;

        CloseOtherPanel(leadersHeaderPanel);
    }

    public void OpenLeadersMiddlePanel()
    {
        if (leadersMiddlePanel.IsActive) return;

        OpenOtherPanel(leadersMiddlePanel);
    }

    public void CloseLeadersMiddlePanel()
    {
        if (!leadersMiddlePanel.IsActive) return;

        CloseOtherPanel(leadersMiddlePanel);
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
    public event Action OnClickToLeaders_MainMiddle;

    private void ClickToPlay_PlayFooter()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToPlay_PlayFooter?.Invoke();
    }


    private void ClickToLevel_MainMiddle()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToLevel_MainMiddle?.Invoke();
    }

    private void ClickToSettings_MainMiddle()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToSettings_MainMiddle?.Invoke();
    }

    private void ClickToStore_MainMiddle()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToShop_MainMiddle?.Invoke();
    }

    private void ClickToAlbum_MainMiddle()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToAlbum_MainMiddle?.Invoke();
    }

    private void ClickToLeaders_MainMiddle()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToLeaders_MainMiddle?.Invoke();
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
        _soundProvider.PlayOneShot("PanelClose");

        OnClickToExit_LevelHeader?.Invoke();
    }

    private void ClickToLevel1_LevelHeader()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToLevel1_LevelMiddle?.Invoke();
    }

    private void ClickToLevel2_LevelHeader()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToLevel2_LevelMiddle?.Invoke();
    }

    private void ClickToLevel3_LevelHeader()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToLevel3_LevelMiddle?.Invoke();
    }

    private void ClickToLevel4_LevelHeader()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToLevel4_LevelMiddle?.Invoke();
    }

    private void ClickToLevel5_LevelHeader()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToLevel5_LevelMiddle?.Invoke();
    }

    #endregion

    #region SETTINGS

    public event Action OnClickToExit_SettingsHeader;

    private void ClickToExit_SettingsHeader()
    {
        _soundProvider.PlayOneShot("PanelClose");

        OnClickToExit_SettingsHeader?.Invoke();
    }

    #endregion

    #region SHOP

    public event Action OnClickToExit_ShopHeader;

    public event Action OnClickToCover_ShopMiddle;
    public event Action OnClickToCardPack_ShopMiddle;

    public event Action OnClickToStandard_ShopChoosePack;
    public event Action OnClickToPriority_ShopChoosePack;

    public event Action OnClickToAlbum_ShopOpenPackFooter;
    public event Action OnClickToBack_ShopCardPresentation;

    private void ClickToExit_ShopHeader()
    {
        _soundProvider.PlayOneShot("PanelClose");

        OnClickToExit_ShopHeader?.Invoke();
    }

    private void ClickToCover_ShopMiddle()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToCover_ShopMiddle?.Invoke();
    }

    private void ClickToCardPack_ShopMiddle()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToCardPack_ShopMiddle?.Invoke();
    }


    private void ClickToStandard_ShopChoosePack()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToStandard_ShopChoosePack?.Invoke();
    }

    private void ClickToPriority_ShopChoosePack()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToPriority_ShopChoosePack?.Invoke();
    }

    private void ClickToAlbum_ShopOpenPackFooter()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToAlbum_ShopOpenPackFooter?.Invoke();
    }

    private void ClickToBack_ShopCardPresentation()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToBack_ShopCardPresentation?.Invoke();
    }

    #endregion

    #region ALBUM

    public event Action OnClickToExit_AlbumHeader;

    public event Action OnClickToCommon_AlbumChoose;
    public event Action OnClickToUncommon_AlbumChoose;
    public event Action OnClickToRare_AlbumChoose;
    public event Action OnClickToEpic_AlbumChoose;
    public event Action OnClickToMythical_AlbumChoose;

    private void ClickToExit_AlbumHeader()
    {
        _soundProvider.PlayOneShot("PanelClose");

        OnClickToExit_AlbumHeader?.Invoke();
    }

    private void ClickToCommon_AlbumChoose()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToCommon_AlbumChoose?.Invoke();
    }

    private void ClickToUncommon_AlbumChoose()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToUncommon_AlbumChoose?.Invoke();
    }

    private void ClickToRare_AlbumChoose()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToRare_AlbumChoose?.Invoke();
    }

    private void ClickToEpic_AlbumChoose()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToEpic_AlbumChoose?.Invoke();
    }

    private void ClickToMythical_AlbumChoose()
    {
        _soundProvider.PlayOneShot("PanelOpen");

        OnClickToMythical_AlbumChoose?.Invoke();
    }

    #endregion

    #region SETTINGS

    public event Action OnClickToExit_LeadersHeader;

    private void ClickToExit_LeadersHeader()
    {
        _soundProvider.PlayOneShot("PanelClose");

        OnClickToExit_LeadersHeader?.Invoke();
    }

    #endregion

    #endregion

}
