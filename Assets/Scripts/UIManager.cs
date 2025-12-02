using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public TMP_Text fpsText;

    private void Start()
    {
        WaveManager.Instance.OnWaveChanged += UpdateWaveUI;
        WaveManager.Instance.OnEnemyCountChanged += UpdateEnemyUI;
        StartCoroutine(FPSCounter());
    }

    private void UpdateWaveUI(int wave)
    {
        waveText.text = "Wave: " + wave;
    }

    private void UpdateEnemyUI(int count)
    {
        enemyCountText.text = "Enemies: " + count;
    }

    private IEnumerator FPSCounter()
    {
        while (true)
        {
            fpsText.text = "FPS: " + (int)(1f / Time.unscaledDeltaTime);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void OnClick_ToggleAuto()
    {
        GameManager.Instance.ToggleAutoCycle();
    }

    public void OnClick_NextWave()
    {
        WaveManager.Instance.ForceNextWave();
    }

    public void OnClick_DestroyWave()
    {
        WaveManager.Instance.DestroyCurrentWave();
    }
}
