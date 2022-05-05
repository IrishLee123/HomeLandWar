using System;
using System.Collections;
using System.Collections.Generic;
using Attack;
using Map;
using Role;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private MapField mapField;

    [SerializeField] private RandomArea randomArea;

    [Header("子弹相关")] [SerializeField] private bool useObjectPool;

    [Header("子弹相关")] [SerializeField] private GameObject bulletPrefab;

    [Header("子弹相关")] [SerializeField] private GameObjPool bulletPool;

    [Header("子弹相关")] [SerializeField] private Transform bulletRoot;

    private static GameController _instance;

    public static GameController Instance
    {
        get => _instance;
    }

    private List<RoleData> _roleDatas = new List<RoleData>();

    public List<RoleData> RoleDatas
    {
        get => _roleDatas;
    }

    public Action OnGameStart = () => { };

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 24; i++)
        {
            float r = (float) Random.Range(2, 24) * 10f / 255f;
            float g = (float) Random.Range(2, 24) * 10f / 255f;
            float b = (float) Random.Range(2, 24
            ) * 10f / 255f;
            _roleDatas.Add(new RoleData(new Vector3(r, g, b), i + 1));
        }

        mapField.InitMap(_roleDatas);

        StartCoroutine(randomArea.Init());

        OnGameStart();
    }

    public Gun GetGun(int id)
    {
        return mapField.GunList[id - 1];
    }

    public Bullet SpwanBullet()
    {
        GameObject bulletObj;
        if (useObjectPool)
            bulletObj = bulletPool.Get(bulletRoot);
        else
            bulletObj = Instantiate(bulletPrefab);

        if (!bulletObj) return null;

        var bullet = bulletObj.GetComponent<Bullet>();
        if (!bullet) return null;

        return bullet;
    }

    public void RecycleBullet(GameObject bullet)
    {
        if (useObjectPool)
            bulletPool.Put(bullet);
        else
            Destroy(bullet);
    }
}