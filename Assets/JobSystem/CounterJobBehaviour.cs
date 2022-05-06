using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JobSystem
{
    public class CounterJobBehaviour : MonoBehaviour
    {
        public struct CounterJob : IJob
        {
            public NativeArray<int> Numbers;
            public NativeArray<int> Result;

            public void Execute()
            {
                for (int i = 0; i < Numbers.Length; i++)
                {
                    Result[0] += Numbers[i];
                }
            }
        }

        private void Start()
        {
            var nunberCount = 10;
            NativeArray<int> numbers = new NativeArray<int>(nunberCount, Allocator.TempJob);
            NativeArray<int> result = new NativeArray<int>(1, Allocator.TempJob);

            for (int i = 0; i < nunberCount; i++)
            {
                numbers[i] = i + 1;
            }

            var jobData = new CounterJob()
            {
                Numbers = numbers,
                Result = result
            };

            var handle = jobData.Schedule();
            handle.Complete();
            Debug.Log($"result = {result[0]}");
            numbers.Dispose();
            result.Dispose();
        }
    }
}