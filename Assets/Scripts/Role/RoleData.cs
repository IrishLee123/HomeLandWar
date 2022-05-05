using UnityEngine;
using Utils;

namespace Role
{
    public enum RoleState
    {
        Idle,
        Attacking,
        Dead
    }

    public class RoleData
    {
        private int _id;

        public int Id
        {
            get { return _id; }
        }

        public int GovernId { get; set; }
        public Color OriginColor { get; set; }

        public BindableProperty<Color> GovernColor { get; }

        public BindableProperty<int> BulletCount { get; }

        public BindableProperty<RoleState> State { get; }

        public RoleData(Vector3 color, int id)
        {
            _id = id;
            GovernId = id;

            OriginColor = new Color(color.x, color.y, color.z);

            GovernColor = new BindableProperty<Color>()
            {
                Value = new Color(color.x, color.y, color.z)
            };

            BulletCount = new BindableProperty<int>()
            {
                Value = 1
            };

            State = new BindableProperty<RoleState>()
            {
                Value = RoleState.Idle
            };
        }

        public void ChangeGovern(int governId, Color governColor)
        {
            State.Value = RoleState.Idle;
            BulletCount.Value = 1;
            GovernId = governId;
            GovernColor.Value = governColor;
        }
    }
}