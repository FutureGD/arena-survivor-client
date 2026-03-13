using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
public class WaveManager : MonoBehaviour
{
    public enum State
    {
        InBetweenWaves,
        WaveInProgress,
        Win,
        Lose
    }

    public State CurrentState { get; private set; } = State.InBetweenWaves;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private WaveData[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Text waveLabel;
    private int currentWave;
    private int enemiesAlive;

    void Start()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(3f);
        if (currentWave >= waves.Length) { Win(); yield break; }


        CurrentState = State.WaveInProgress;

        WaveData data = waves[currentWave];
        if (waveLabel) waveLabel.text = "Wave: " + (currentWave + 1);
        for (int i = 0; i < data.enemyCount; i++)
        {
            SpawnEnemy(data);
            yield return new WaitForSeconds(data.spawnInterval);
        }
    }

    void SpawnEnemy(WaveData data)
    {
        Transform sp = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject e = Instantiate(enemyPrefab, sp.position, Quaternion.identity);
        e.GetComponent<EnemyChase>().SetSpeed(data.enemySpeed);
        e.GetComponent<Health>().OnDeath += OnEnemyDied;
        enemiesAlive++;
    }

    void OnEnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0 && CurrentState == State.WaveInProgress)
        {
            currentWave++;

            StartCoroutine(StartWave());
        }

    }
    void Win()
    {
        CurrentState = State.Win;
        if (winPanel) winPanel.SetActive(true);
        StartCoroutine(RestartAfterDelay(2f));
    }

    public void PlayerDied()
    {
        Instantiate(deathEffectPrefab, transform.position, quaternion.identity);
        CurrentState = State.Lose;
        if (losePanel) losePanel.SetActive(true);
        StartCoroutine(RestartAfterDelay(2f));
    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
