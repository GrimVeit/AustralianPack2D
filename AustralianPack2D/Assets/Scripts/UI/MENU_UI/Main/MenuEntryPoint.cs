using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private ParticleEffectMaterialPresenter particleEffectMaterialPresenter;
    private SoundPresenter soundPresenter;

    private StoreLevelPresenter storeLevelPresenter;
    private ChooseLevelVisualPresenter chooseLevelVisualPresenter;

    private StateMachine_Menu stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
            (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS, PlayerPrefsKeys.KEY_VOLUME_SOUND, PlayerPrefsKeys.KEY_VOLUME_MUSIC),
            viewContainer.GetView<SoundView>());

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        particleEffectMaterialPresenter = new ParticleEffectMaterialPresenter(new ParticleEffectMaterialModel(), viewContainer.GetView<ParticleEffectMaterialView>());

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

        storeLevelPresenter = new StoreLevelPresenter(new StoreLevelModel(PlayerPrefsKeys.LEVEL_NUMBER));
        chooseLevelVisualPresenter = new ChooseLevelVisualPresenter(new ChooseLevelVisualModel(storeLevelPresenter, storeLevelPresenter), viewContainer.GetView<ChooseLevelVisualView>());

        stateMachine = new StateMachine_Menu(sceneRoot);

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        particleEffectMaterialPresenter.Initialize();
        particleEffectMaterialPresenter.Activate();
        sceneRoot.Initialize();

        bankPresenter.Initialize();

        chooseLevelVisualPresenter.Initialize();
        storeLevelPresenter.Initialize();

        stateMachine.Initialize();
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
        particleEffectMaterialPresenter.Deactivate();

        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    private void Dispose()
    {
        DeactivateEvents();

        soundPresenter?.Dispose();
        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        particleEffectMaterialPresenter?.Dispose();
        bankPresenter?.Dispose();

        chooseLevelVisualPresenter.Dispose();
        storeLevelPresenter.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Output

    public event Action OnClickToGame;

    private void HandleClickToGame()
    {
        Deactivate();

        OnClickToGame?.Invoke();
    }

    #endregion
}
