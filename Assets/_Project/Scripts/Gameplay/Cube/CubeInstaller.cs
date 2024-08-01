using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

public class CubeInstaller : MonoInstaller
{
    [SerializeField] private Transform _spawnPoint;

    private List<IDisposable> _disposables = new List<IDisposable>();

    public override void InstallBindings()
    {
        
    }

    [Inject]
    private void Construct(CubePreset cubePreset)
    {
        _disposables.Add(new CubePm(new CubePm.Ctx
        {
            cubeViewPrefab = cubePreset.CubePrefab,
            distance = cubePreset.Distance,
            speed = cubePreset.Speed,
            spawnPoint = _spawnPoint.position
        }));
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