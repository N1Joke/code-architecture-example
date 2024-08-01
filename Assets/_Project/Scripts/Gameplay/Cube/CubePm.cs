using Core;
using Tools.Extensions;
using UnityEngine;

public class CubePm : BaseDisposable
{
    public struct Ctx
    {
        public float speed;
        public float distance;
        public CubeView cubeViewPrefab;
        public Vector3 spawnPoint;
    }

    private readonly Ctx _ctx;
    private Vector3 _startPos;
    private CubeView _cubeViewInstance;

    public CubePm(Ctx ctx)
    {
        _ctx = ctx;

        Initialize();
    }

    private void Initialize()
    {
        AddDispose(ReactiveExtensions.StartFixedUpdate(Update));

        _cubeViewInstance = GameObject.Instantiate(_ctx.cubeViewPrefab);
        _cubeViewInstance.transform.position =_ctx.spawnPoint;
    }

    private void Update()
    {
        _cubeViewInstance.transform.position = new Vector3(
            _startPos.x + Mathf.Sin(Time.time * _ctx.speed) * _ctx.distance,
            _startPos.y,
            _startPos.z);
    }
}