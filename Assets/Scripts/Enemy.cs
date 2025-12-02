using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public int typeId;

    public void ResetEnemy()
    {
        // return to default state (HP, color, behavior, etc.)
    }

    public void SpawnAt(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void Die()
    {
        WaveManager.Instance.NotifyEnemyDead(this);
        ObjectPool.Instance.ReturnToPool(this, typeId);
    }

    public void DieImmediate()
    {
        ObjectPool.Instance.ReturnToPool(this, typeId);
    }
}
