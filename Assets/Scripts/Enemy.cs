using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int typeId;

    public void ResetEnemy()
    {
        // return to default state (HP, color, behavior, etc.)
    }

    public void SpawnRandomPosition()
    {
        Vector3 pos = new Vector3(
            Random.Range(-8f, 8f),
            0f,
            Random.Range(-8f, 8f)
        );

        transform.position = pos;
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
