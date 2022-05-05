using System;
using UnityEngine;

namespace UI
{
    public class TriggerProgress : MonoBehaviour
    {
        [SerializeField] private Transform leftObj;
        [SerializeField] private Transform rightObj;
        [SerializeField] private Transform arrow;
        [SerializeField] private Vector2 range;

        private float _progress;

        public float Progress
        {
            get => _progress;
            set
            {
                _progress = Mathf.Max(0f, Mathf.Min(value, 1f));
                Refresh();
            }
        }

        private void Refresh()
        {
            //设置左边scale
            var leftScale = leftObj.transform.localScale;
            leftScale.x = _progress * (range.y - range.x);
            leftObj.transform.localScale = leftScale;

            var rightTransfrom = rightObj.transform;

            //设置右边position和scale
            var rightPosition = rightTransfrom.localPosition;
            rightPosition.x = range.x + _progress * (range.y - range.x);
            rightTransfrom.localPosition = rightPosition;

            var rightScale = rightTransfrom.localScale;
            rightScale.x = (1 - _progress) * (range.y - range.x);
            rightTransfrom.localScale = rightScale;

            var arrowPosition = arrow.transform.localPosition;
            arrowPosition.x = range.x + _progress * (range.y - range.x);
            arrow.transform.localPosition = arrowPosition;
        }
    }
}