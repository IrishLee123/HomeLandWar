using System;
using System.Collections.Generic;
using Role;
using UnityEngine;

namespace Attack
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;

        [SerializeField] private List<SpriteRenderer> originChangable = new List<SpriteRenderer>();
        [SerializeField] private List<SpriteRenderer> governChangable = new List<SpriteRenderer>();

        private RoleData _roleData; //角色数据

        public RoleData Data
        {
            get => _roleData;
        }

        [SerializeField] private float attackDuration = 0.1f;
        private float _attackTimer;

        public void BindData(RoleData originRole)
        {
            //绑定数据
            _roleData = originRole;

            //初始化填充颜色
            foreach (var sprite in originChangable)
            {
                sprite.color = _roleData.OriginColor;
            }

            foreach (var sprite in governChangable)
            {
                sprite.color = _roleData.GovernColor.Value;
            }
        }

        private void Update()
        {
            if (_roleData == null) return;

            transform.Rotate(0, 0, 20 * Time.deltaTime);

            if (_roleData.State.Value == RoleState.Attacking)
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer > attackDuration)
                {
                    ShootOneBullet();
                    _attackTimer = 0;
                }

                if (_roleData.BulletCount.Value == 0)
                {
                    _roleData.BulletCount.Value = 1;
                    _roleData.State.Value = RoleState.Idle;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var bullet = other.gameObject.GetComponent<Bullet>();
            var bulletData = bullet.Data;
            if (_roleData.GovernId != bulletData.OwnerId)
            {
                //炮塔被占领
                _roleData.ChangeGovern(bulletData.OwnerId, bulletData.OwnerColor);

                //换颜色
                foreach (var sprite in governChangable)
                {
                    sprite.color = _roleData.GovernColor.Value;
                }

                //销毁子弹
                GameController.Instance.RecycleBullet(other.gameObject);
            }
        }

        private void ShootOneBullet()
        {
            var bullet = GameController.Instance.SpwanBullet();
            if (!bullet) return;

            var position = firePoint.position;
            bullet.transform.position = position;
            bullet.transform.rotation = firePoint.rotation;

            var bulletData = new BulletData()
            {
                Direction = firePoint.up,
                OwnerColor = _roleData.GovernColor.Value,
                OwnerId = _roleData.GovernId
            };

            bullet.BindData(bulletData);

            _roleData.BulletCount.Value--;
        }
    }
}