using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private ChickenAllPicturesSO chickenAllPicturesSO;
    [SerializeField] private UIGameRoot menuRootPrefab;

    private UIGameRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private VideoPresenter videoPresenter;

    private CustomSliderPresenter customSliderPresenter_Sound;
    private CustomSliderPresenter customSliderPresenter_Music;
    private VolumeSettingsPresenter volumeSettingsPresenter;

    private StoreChickenPresenter storeChickenPresenter;
    private ChooseChickenPresenter chooseChickenPresenter;
    private SpawnerChickenPresenter spawnerChickenPresenter;
    private ChickenBattlePresenter chickenBattlePresenter;
    private CameraFollowPresenter cameraFollowPresenter;
    private SlotMachinePresenter slotMachinePresenter;
    private VisualChickenEffectPresenter visualChickenEffectPresenter;
    private ChickenRaceLeaderPresenter chickenRaceLeaderPresenter;

    private MaskEffectPresenter maskEffectPresenter;

    private ChooseBuyBoxPresenter chooseBuyBoxPresenter;
    private BuyBoxPresenter buyBoxPresenter;
    private RaceDesignPresenter raceDesignPresenter;

    private TimerPresenter timerPresenter_Start;
    private TimerPresenter timerPresenter_Game;

    private CountChickenPicturePresenter countChickenPicturePresenter;
    private BuyPiecesPresenter buyPiecesPresenter;
    private ShowChickenPicturePresenter showChickenPicturePresenter;
    private VisualChickenPicturePresenter visualChickenPicturePresenter;
    private VisualHintPicturePiecePresenter visualHintPicturePresenter;
    private VisualPseudoPicturePiecePresenter visualPseudoPicturePiecePresenter;
    private StoreChickenPicturePiecePresenter storeChickenPicturePiecePresenter;
    private StoreChickenPicturePresenter storeChickenPicturePresenter;

    private StateMachine_Game stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS, PlayerPrefsKeys.KEY_VOLUME_SOUND, PlayerPrefsKeys.KEY_VOLUME_MUSIC),
                    viewContainer.GetView<SoundView>());

        videoPresenter = new VideoPresenter(new VideoModel(), viewContainer.GetView<VideoView>());

        customSliderPresenter_Music = new CustomSliderPresenter(new CustomSliderModel(soundPresenter), viewContainer.GetView<CustomSliderView>("Music"));
        customSliderPresenter_Sound = new CustomSliderPresenter(new CustomSliderModel(soundPresenter), viewContainer.GetView<CustomSliderView>("Sound"));
        volumeSettingsPresenter = new VolumeSettingsPresenter(new VolumeSettingsModel(soundPresenter, customSliderPresenter_Sound, customSliderPresenter_Music));

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        
        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        storeChickenPresenter = new StoreChickenPresenter(new StoreChickenModel());
        chooseChickenPresenter = new ChooseChickenPresenter(new ChooseChickenModel(storeChickenPresenter, soundPresenter), viewContainer.GetView<ChooseChickenView>());
        spawnerChickenPresenter = new SpawnerChickenPresenter(new SpawnerChickenModel(storeChickenPresenter, soundPresenter), viewContainer.GetView<SpawnerChickenView>()); ;
        chickenBattlePresenter = new ChickenBattlePresenter(new ChickenBattleModel(spawnerChickenPresenter, chooseChickenPresenter));
        cameraFollowPresenter = new CameraFollowPresenter(new CameraFollowModel(spawnerChickenPresenter), viewContainer.GetView<CameraFollowView>());
        visualChickenEffectPresenter = new VisualChickenEffectPresenter(new VisualChickenEffectModel(chooseChickenPresenter, storeChickenPresenter), viewContainer.GetView<VisualChickenEffectView>());
        chickenRaceLeaderPresenter = new ChickenRaceLeaderPresenter(new ChickenRaceLeaderModel(chickenBattlePresenter), viewContainer.GetView<ChickenRaceLeaderView>()); ;

        slotMachinePresenter = new SlotMachinePresenter(new SlotMachineModel(soundPresenter), viewContainer.GetView<SlotMachineView>());

        maskEffectPresenter = new MaskEffectPresenter(new MaskEffectModel(), viewContainer.GetView<MaskEffectView>());

        chooseBuyBoxPresenter = new ChooseBuyBoxPresenter(new ChooseBuyBoxModel(soundPresenter), viewContainer.GetView<ChooseBuyBoxView>());
        buyBoxPresenter = new BuyBoxPresenter(new BuyBoxModel(chooseBuyBoxPresenter), viewContainer.GetView<BuyBoxView>());
        raceDesignPresenter = new RaceDesignPresenter(new RaceDesignModel(slotMachinePresenter), viewContainer.GetView<RaceDesignView>());

        timerPresenter_Start = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView_Mapped>("Start"));
        timerPresenter_Game = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView_Formatted>("Game"));

        storeChickenPicturePresenter = new StoreChickenPicturePresenter(new StoreChickenPictureModel("CHICKEN_PICTURES", chickenAllPicturesSO));
        storeChickenPicturePiecePresenter = new StoreChickenPicturePiecePresenter(new StoreChickenPicturePieceModel(storeChickenPicturePresenter, storeChickenPicturePresenter));
        visualPseudoPicturePiecePresenter = new VisualPseudoPicturePiecePresenter(new VisualPseudoPicturePieceModel(storeChickenPicturePiecePresenter, storeChickenPicturePiecePresenter, soundPresenter), viewContainer.GetView<VisualPseudoPicturePieceView>());
        visualHintPicturePresenter = new VisualHintPicturePiecePresenter(new VisualHintPicturePieceModel(visualPseudoPicturePiecePresenter), viewContainer.GetView<VisualHintPicturePieceView>());
        visualChickenPicturePresenter = new VisualChickenPicturePresenter(new VisualChickenPictureModel(storeChickenPicturePresenter, storeChickenPicturePresenter, soundPresenter), viewContainer.GetView<VisualChickenPictureView>());
        showChickenPicturePresenter = new ShowChickenPicturePresenter(new ShowChickenPictureModel(visualChickenPicturePresenter), viewContainer.GetView<ShowChickenPictureView>());
        buyPiecesPresenter = new BuyPiecesPresenter(new BuyPiecesModel(storeChickenPicturePresenter), viewContainer.GetView<BuyPiecesView>());
        countChickenPicturePresenter = new CountChickenPicturePresenter(new CountChickenPictureModel(storeChickenPicturePresenter), viewContainer.GetView<CountChickenPictureView>());
        
        stateMachine = new StateMachine_Game(
            storeChickenPresenter, 
            spawnerChickenPresenter, 
            chooseChickenPresenter, 
            sceneRoot, 
            chickenBattlePresenter, 
            chickenBattlePresenter, 
            chooseBuyBoxPresenter, 
            videoPresenter, 
            maskEffectPresenter, 
            slotMachinePresenter, 
            slotMachinePresenter,
            visualChickenEffectPresenter,
            timerPresenter_Start,
            timerPresenter_Start,
            timerPresenter_Game,
            visualChickenPicturePresenter,
            showChickenPicturePresenter,
            buyBoxPresenter,
            buyPiecesPresenter,
            countChickenPicturePresenter,
            bankPresenter,
            chickenRaceLeaderPresenter,
            cameraFollowPresenter,
            soundPresenter);

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        videoPresenter.Initialize();

        customSliderPresenter_Music.Initialize();
        customSliderPresenter_Sound.Initialize();
        volumeSettingsPresenter.Initialize();

        particleEffectPresenter.Initialize();
        sceneRoot.Initialize();
        bankPresenter.Initialize();
        
        chooseChickenPresenter.Initialize();
        chickenBattlePresenter.Initialize();
        cameraFollowPresenter.Initialize();
        chickenRaceLeaderPresenter.Initialize();
        visualChickenEffectPresenter.Initialize();
        slotMachinePresenter.Initialize();
        maskEffectPresenter.Initialize();
        spawnerChickenPresenter.Initialize();

        chooseBuyBoxPresenter.Initialize();
        buyBoxPresenter.Initialize();
        raceDesignPresenter.Initialize();

        timerPresenter_Start.Initialize();
        timerPresenter_Game.Initialize();

        countChickenPicturePresenter.Initialize();
        buyPiecesPresenter.Initialize();
        showChickenPicturePresenter.Initialize();
        visualChickenPicturePresenter.Initialize();
        visualHintPicturePresenter.Initialize();
        visualPseudoPicturePiecePresenter.Initialize();
        storeChickenPicturePiecePresenter.Initialize();
        storeChickenPicturePresenter.Initialize();

        stateMachine.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var data = storeChickenPicturePresenter.GetRandomAvailablePiece();

            if(data != null) storeChickenPicturePresenter.OwnedPiece(data.Type, data.IdPicture, data.IdPiece);
        }
    }

    private void ActivateEvents()
    {
        ActivateTransitions();
    }

    private void DeactivateEvents()
    {
        DeactivateTransitions();
    }

    private void ActivateTransitions()
    {

    }

    private void DeactivateTransitions()
    {

    }

    private void Deactivate()
    {
        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        Deactivate();

        DeactivateEvents();

        bankPresenter?.Dispose();
        videoPresenter?.Dispose();

        customSliderPresenter_Music?.Dispose();
        customSliderPresenter_Sound?.Dispose();
        volumeSettingsPresenter?.Dispose();

        chooseChickenPresenter?.Dispose();
        chickenBattlePresenter?.Dispose();
        cameraFollowPresenter?.Dispose();
        chickenRaceLeaderPresenter?.Dispose();
        visualChickenEffectPresenter?.Dispose();
        slotMachinePresenter?.Dispose();
        maskEffectPresenter?.Dispose();
        spawnerChickenPresenter?.Dispose();

        chooseBuyBoxPresenter?.Dispose();
        buyBoxPresenter?.Dispose();
        raceDesignPresenter?.Dispose();

        timerPresenter_Start?.Dispose();
        timerPresenter_Game?.Dispose();

        countChickenPicturePresenter?.Dispose();
        buyPiecesPresenter?.Dispose();
        showChickenPicturePresenter?.Dispose();
        visualChickenPicturePresenter?.Dispose();
        visualHintPicturePresenter?.Dispose();
        visualPseudoPicturePiecePresenter?.Dispose();
        storeChickenPicturePiecePresenter?.Dispose();
        storeChickenPicturePresenter?.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void OnApplicationQuit()
    {
        Dispose();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            bankPresenter?.Save();
        }
    }

    void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
            bankPresenter?.Save();
        }
    }

    #region Output


    public event Action OnClickToMenu;
    public event Action OnClickToGame;

    private void HandleClickToMenu()
    {
        Deactivate();

        OnClickToMenu?.Invoke();
    }

    private void HandleClickToGame()
    {
        Deactivate();

        OnClickToGame?.Invoke();
    }

    #endregion
}
