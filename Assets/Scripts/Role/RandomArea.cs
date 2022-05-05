using System.Collections;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Role
{
    public class RandomArea : MonoBehaviour
    {
        [SerializeField] private TriggerProgress progress;

        private float _timer;
        [SerializeField] private float duration;

        [SerializeField] private GameObject ballPrefab;

        [SerializeField] private Transform ballRoot;

        public IEnumerator Init()
        {
            for (int i = 0; i < 24; i++)
            {
                yield return new WaitForSeconds(0.1f);

                var ballObj = Instantiate(ballPrefab, ballRoot);
                var ball = ballObj.GetComponent<RoleBall>();
                ball.BindData(GameController.Instance.RoleDatas[i]);
                RandomPos(ball.transform);
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > duration) return;

            float t = 0.3f + 0.25f * _timer / duration;
            progress.Progress = t;
        }

        public void OnDoubleTrigger(Collider2D ballCol)
        {
            if (!ballCol) return;

            var ball = ballCol.GetComponent<RoleBall>();
            RandomPos(ball.transform);

            var data = ball.RoleData;
            if (data.State.Value == RoleState.Idle && data.BulletCount.Value < 2048)
                data.BulletCount.Value *= 2;
        }

        public void OnFireTrigger(Collider2D ballCol)
        {
            if (!ballCol) return;

            var ball = ballCol.GetComponent<RoleBall>();
            RandomPos(ball.transform);

            var data = ball.RoleData;
            if (data.State.Value == RoleState.Idle)
                data.State.Value = RoleState.Attacking;
        }

        private Vector4 _range = new Vector4(-2.5f, 2.5f, 17f, 22f);

        private void RandomPos(Transform tran)
        {
            float x = Random.Range(_range.x, _range.y);
            float y = Random.Range(_range.z, _range.w);

            var localPosition = tran.localPosition;
            localPosition.x = x;
            localPosition.y = y;
            tran.localPosition = localPosition;
        }
    }
}