using System;
using UnityEngine;

namespace UI
{
    public class FollowObj : MonoBehaviour
    {
        public Transform Target { get; set; }
        private RectTransform _rect;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (!Target) return;
            
            //将 position 从世界空间变换为屏幕空间
            Vector3 screenPos = Camera.main.WorldToScreenPoint(Target.position);

            //此 RectTransform 的轴心相对于锚点参考点的位置
            _rect.position = screenPos;
        }
    }
}