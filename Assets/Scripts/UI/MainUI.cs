using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;

        [SerializeField] private GameObject roleInfoPrefab;

        private List<RoleInfo> _items = new List<RoleInfo>();

        private void Awake()
        {
            _gameController.OnGameStart += Init;
        }

        private void Init()
        {
            for (int i = 1; i <= 24; i++)
            {
                var gun = _gameController.GetGun(i);
                
                var itemObj = Instantiate(roleInfoPrefab,transform);
                if (!itemObj) break;
                
                var item = itemObj.GetComponent<RoleInfo>();
                if (!item) break;

                var follow = itemObj.GetComponent<FollowObj>();
                if (!follow) break;

                item.BindData(gun.Data);
                follow.Target = gun.transform;
                
                _items.Add(item);
            }
        }
    }
}