using UnityEngine;
using System;
using System.Collections;
public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    private int currentWave = 0;
    private int activeEnemies = 0;
    private bool waveRunning = false;

    public event Action<int> OnWaveChanged;
    public event Action<int> OnEnemyCountChanged;

    public EnemySpawnArea spawnArea;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            if (!GameManager.Instance.autoCycle)
            {
                yield return null;
                continue;
            }

            yield return StartCoroutine(StartNextWaveAuto());
        }
    }

    private IEnumerator StartNextWaveAuto()
    {
        StartWave();
        while (activeEnemies > 0)
            yield return null;

        yield return new WaitForSeconds(5f);
    }

    public void StartWave(bool force = false)
    {
        if (waveRunning && !force) return;

        currentWave++;
        waveRunning = true;

        int enemyCount = GetEnemyCountForWave(currentWave);
        SpawnWave(enemyCount);

        OnWaveChanged?.Invoke(currentWave);
    }

    private int GetEnemyCountForWave(int wave)
    {
        if (wave == 1) return 30;
        if (wave == 2) return 50;
        if (wave == 3) return 70;
        return 70 + (wave - 3) * 10;
    }

    private void SpawnWave(int count)
    {
        spawnArea.gameObject.SetActive(true);

        activeEnemies = count;

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = ObjectPool.Instance.GetRandomEnemy();
            enemy.SpawnAt(spawnArea.GetValidSpawnPoint());
        }

        spawnArea.gameObject.SetActive(false);

        OnEnemyCountChanged?.Invoke(activeEnemies);
    }


    public void NotifyEnemyDead(Enemy enemy)
    {
        activeEnemies--;
        OnEnemyCountChanged?.Invoke(activeEnemies);

        if (activeEnemies <= 0)
        {
            waveRunning = false;
        }
    }

    public void DestroyCurrentWave()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
            enemy.DieImmediate();
    }

    public void ForceNextWave()
    {
        StartWave(force: true);
    }
}
