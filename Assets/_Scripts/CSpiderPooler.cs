using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]
internal struct SpiderToPool
{
    public CEnemyAgentSpider spiderPrefab;
    public int amount;
}


public class CSpiderPooler : MonoBehaviour
{
    [SerializeField] List<SpiderToPool> spidersToPool;
    [SerializeField] List<Transform> spawnPoints;
    private List<CEnemyAgentSpider> pooledSpiders;


    public void Init()
    {
        pooledSpiders = new List<CEnemyAgentSpider>();
        CEnemyAgentSpider tmp;
        GameObject spiders = new GameObject("Spiders");
        spiders.transform.SetParent(transform, false);
        int spawnPointsCount = spawnPoints.Count;
        foreach(SpiderToPool spider in spidersToPool)
        {
            for (int i = 0; i < spider.amount; i++)
            {
                tmp = Instantiate(spider.spiderPrefab);
                tmp.transform.SetParent(spiders.transform, false);
                tmp.transform.position = spawnPoints[Random.Range(0, spawnPointsCount)].position;
                pooledSpiders.Add(tmp);
            }
        }
    }
}
