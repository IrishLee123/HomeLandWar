using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JobSystem
{
    /**
     * C# Job System是Unity从2018开始提供给用户的一个非常强大的功能，它允许用户以一种低成本的方式书写高效的多线程代码。
     *
     * 总结一下声明一个Job的要点：
     *     创建一个实现了IJob接口的struct。
     *     在struct中声明blittable types或者NativeContainer的变量。
     *     在Execute()方法中实现Job的逻辑。
     */
    public struct AddJob : IJob
    {
        public float a;
        public float b;
        public NativeArray<float> result;

        public void Execute()
        {
            result[0] = a + b;
        }
    }

    public class JobSystemDemo : MonoBehaviour
    {
        public bool longRunningJob;
        private NativeArray<float> _result;
        private JobHandle _handle;

        private void Start()
        {
            //NativeContainer是需要显式管理内存的
            _result = new NativeArray<float>(1, Allocator.Persistent);

            var job = new AddJob()
            {
                a = 1,
                b = 2,
                result = _result
            };

            //调度（Schedule）一个Job是比较简单的，只需要调用Schedule()方法就可以了
            _handle = job.Schedule();
            
            if (!longRunningJob)
            {
                //这里比较有意思的是Complete()方法，在我们需要读取执行结果之前需要调用Complete()方法。
                _handle.Complete();
                Debug.Log($"result = {_result[0]}");
            }
        }

        private void Update()
        {
            // 但是Complete()不一定在Schedule()之后立即调用，也不一定在当前帧必须调用
            // 也就是说一个Job本身不受Update()限制可以跨帧运行
            // 当一个Job需要跨帧运行的时候，我们需要使用IsCompleted属性来判断Job是否执行完毕。
            if (_handle.IsCompleted)
            {
                _handle.Complete();
                Debug.Log($"result = {_result[0]}");
            }
        }

        private void OnDestroy()
        {
            //回收NativeContainer的内存
            if (_result.IsCreated)
                _result.Dispose();
        }
    }
}