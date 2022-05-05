using UnityEngine;

namespace Role
{
    public class RoleBall : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer originRender;
        [SerializeField] private SpriteRenderer governRenderer;

        private RoleData _roleData;

        public RoleData RoleData
        {
            get => _roleData;
        }

        public void BindData(RoleData roleData)
        {
            _roleData = roleData;

            originRender.color = _roleData.OriginColor;

            governRenderer.color = _roleData.GovernColor.Value;
            _roleData.GovernColor.RegisterOnValueChanged(OnGovernColorChange);
        }

        private void OnGovernColorChange(Color v)
        {
            governRenderer.color = v;
        }
    }
}