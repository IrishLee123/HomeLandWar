using System.Collections;
using System.Collections.Generic;
using Role;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class RoleInfo : MonoBehaviour
{
    [SerializeField] private Text bulletCount;

    private RoleData _roleData;

    public void BindData(RoleData roleData)
    {
        _roleData = roleData;

        OnBulletCountChange(roleData.BulletCount.Value);
        
        _roleData.BulletCount.RegisterOnValueChanged(OnBulletCountChange).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void OnBulletCountChange(int v)
    {
        bulletCount.text = string.Format(v + "");
    }
}