using System;
using System.Collections.Generic;
using UnityEngine;

namespace Role
{
    public class Rolling : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private List<Transform> _blocks = new List<Transform>();

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                _blocks.Add(child);
            }
        }

        private void FixedUpdate()
        {
            foreach (var tran in _blocks)
            {
                tran.Translate(0, -moveSpeed * Time.deltaTime, 0);

                if (tran.localPosition.y < -0.5)
                {
                    var localPosition = tran.localPosition;
                    localPosition.y += 1.2f;
                    tran.localPosition = localPosition;
                }
            }
        }
    }
}