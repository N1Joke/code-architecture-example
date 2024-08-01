using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using Core;

public class BootstrapInstaller : MonoInstaller
{
    public enum LocalStorageType
    {
        JsonFile,
    }

    [SerializeField] private LocalStorageType _localStorageType;
    [SerializeField] private CubePreset _cubePreset;

    private List<IDisposable> _disposables = new List<IDisposable>();
    private IStorageService _storageService;
    private ISceneLoader _sceneLoader;

    public override void InstallBindings()
    {
        Container.Bind<CubePreset>().FromScriptableObject(_cubePreset).AsSingle();

        _storageService = CreateStorageService(_localStorageType);
        Container.Bind<IStorageService>().FromInstance(_storageService).AsSingle();

        _disposables.Add(_sceneLoader);
        _sceneLoader = CreateSceneLoader();
        _sceneLoader.LoadScene((int)Scenes.GameplayScene, null, null);
    }

    private ISceneLoader CreateSceneLoader()
    {
        return new SceneLoader(new SceneLoader.Ctx
        {

        });
    }

    private IStorageService CreateStorageService(LocalStorageType type)
    {
        switch (type)
        {
            case LocalStorageType.JsonFile:
                return new JsonToFileStorageService(new JsonToFileStorageService.Ctx());
        }

        Debug.LogError($"storage Service type {type} is not found");
        return null;
    }

    private void OnDestroy()
    {
        List<IDisposable> disposables = _disposables;
        for (int i = _disposables.Count - 1; i >= 0; i--)
            _disposables[i]?.Dispose();
        disposables.Clear();
        _disposables.Clear();
    }
}