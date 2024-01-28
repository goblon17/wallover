using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WallManager : Singleton<WallManager>
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private WallList walls;
    [SerializeField]
    private Transform spawnTransform;
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private float moveDuration;
    [SerializeField]
    private float waitDuration;
    [SerializeField]
    private float ragdollEnablePoint;

    public event Action EnableRagdollEvent;
    public event Action WallEndedEvent;
    public event Action WallStartedEvent;
    public event Action WallSpawned;

    public WallList.MeshMeta MeshMeta => meshMeta;
    public WallList.MaterialMeta MaterialMeta => materialMeta;

    private GameObject currentWall;
    private WallList.MeshMeta meshMeta;
    private WallList.MaterialMeta materialMeta;

    private Coroutine currentCoroutine = null;

    protected override void Awake()
    {
        gameManager.GameStateChangedEvent += OnGameStateChanged;
        base.Awake();
    }

    private void OnGameStateChanged(GameManager.State newState)
    {
        switch (newState)
        {
            case GameManager.State.Game:
                StartWalling(); 
                break;
            case GameManager.State.End:
                EndWalling();
                break;
        }
    }

    private void StartWalling()
    {
        currentCoroutine = StartCoroutine(WallCoroutine());
    }

    private IEnumerator WallCoroutine()
    {
        while (true)
        {
            (currentWall, meshMeta, materialMeta) = walls.GetRandomWall();
            currentWall.transform.position = spawnTransform.position;
            WallSpawned?.Invoke();

			yield return new WaitForSeconds(waitDuration);
            Debug.Log("Wall started");
            WallStartedEvent?.Invoke();

            float counter = 0;
            bool enabledRagdoll = false;
            while ((counter += Time.deltaTime) <= moveDuration)
            {
                float t = counter / moveDuration;
                currentWall.transform.position = Vector3.Lerp(spawnTransform.position, targetTransform.position, t);
                if(!enabledRagdoll && counter > ragdollEnablePoint)
                {
                    enabledRagdoll = true;
                    EnableRagdollEvent?.Invoke();
                }
                yield return null;
            }

            Debug.Log("Wall ended");
            WallEndedEvent?.Invoke();
            Destroy(currentWall);
        }
    }

    private void EndWalling()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }
}
