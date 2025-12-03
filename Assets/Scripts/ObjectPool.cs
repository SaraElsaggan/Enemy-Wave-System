using UnityEngine;
using System.Collections.Generic;
public class ObjectPool : MonoBehaviour
{
    
    public static ObjectPool Instance;

    public List<Enemy> enemyPrefabs;
    public int poolSize = 50;

    private Dictionary<int, Queue<Enemy>> pools;

    private void Awake()
    {
        Instance = this;
        CreatePools();
    }

    private void CreatePools()
    {
        pools = new Dictionary<int, Queue<Enemy>>();

        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            Queue<Enemy> queue = new Queue<Enemy>();

            for (int j = 0; j < poolSize; j++)
            {
                Enemy obj = Instantiate(enemyPrefabs[i]);
                obj.gameObject.SetActive(false);
                queue.Enqueue(obj);
            }

            pools.Add(i, queue);
        }
    }

    public Enemy GetRandomEnemy()
    {
        int id = Random.Range(0, enemyPrefabs.Count);
        return GetFromPool(id);
    }

    private Enemy GetFromPool(int id)
    {
        if (pools[id].Count == 0)
            CreateOneMore(id);

        Enemy obj = pools[id].Dequeue();
        obj.ResetEnemy();
        obj.gameObject.SetActive(true);
        return obj;
    }

    private void CreateOneMore(int id)
    {
        Enemy newObj = Instantiate(enemyPrefabs[id]);
        newObj.gameObject.SetActive(false);
        pools[id].Enqueue(newObj);
    }

    public void ReturnToPool(Enemy enemy, int id)
    {
        enemy.gameObject.SetActive(false);
        pools[id].Enqueue(enemy);
    }
}
