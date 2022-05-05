using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class GameObjPool : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        private Stack<GameObject> _pool = new Stack<GameObject>();

        public GameObject Get()
        {
            if (_pool.Count > 0) return _pool.Pop();

            return Instantiate(bulletPrefab);
        }

        public GameObject Get(Transform parent)
        {
            if (_pool.Count > 0)
            {
                var result = _pool.Pop();
                result.transform.SetParent(parent);
                result.SetActive(true);
                return result;
            }

            return Instantiate(bulletPrefab, parent);
        }

        public void Put(GameObject obj)
        {
            obj.transform.SetParent(transform);
            
            var position = obj.transform.position;
            position.y = 10000;
            obj.transform.position = position;

            obj.SetActive(false);
            
            _pool.Push(obj);
        }
    }
}