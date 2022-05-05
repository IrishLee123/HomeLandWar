using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Utils
{
    public class Trigger2DUtil : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private UnityEvent<Collider2D> onTriggerEnter;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (IsInLayerMask(col.gameObject, targetLayer))
            {
                onTriggerEnter.Invoke(col);
            }
        }

        private bool IsInLayerMask(GameObject obj, LayerMask mask)
        {
            // TODO: 这是什么写法？
            var objectLayer = 1 << obj.layer;
            // TODO: 这是什么写法？
            return (mask.value & objectLayer) > 0;
        }
    }
}