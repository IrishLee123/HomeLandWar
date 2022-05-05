using System.Collections.Generic;
using Attack;
using Role;
using UnityEngine;

namespace Map
{
    public class MapField : MonoBehaviour
    {
        [SerializeField] private GameObject blockPrefab;
        [SerializeField] private GameObject gunPrefab;

        [SerializeField] private Vector2 mapSize;

        [SerializeField] private List<Border> borderList;

        [SerializeField] private Transform blockRoot;
        [SerializeField] private Transform gunRoot;

        private List<Block> _blockList = new List<Block>();
        public List<Gun> GunList { get; set; } = new List<Gun>();

        public void InitMap(List<RoleData> roleList)
        {
            foreach (var border in borderList)
            {
                border.OnBallIn += OnBorderTrigger;
            }

            //将方块添加到节点中
            for (int i = 1; i < mapSize.y; i++)
            {
                for (int j = 1; j < mapSize.x; j++)
                {
                    var blockObj = Instantiate(blockPrefab, blockRoot, true);
                    if (!blockObj) break;

                    blockObj.transform.localPosition = new Vector3(j, i, 0);

                    var block = blockObj.GetComponent<Block>();
                    if (!block) break;

                    _blockList.Add(block);
                }
            }

            //添加炮台
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int x = 6 + j * 11;
                    int y = 6 + i * 11;
                    var block = GetBlockWithXY(x, y);

                    var gunObj = Instantiate(gunPrefab, gunRoot);
                    if (!gunObj) break;

                    gunObj.name = $"Gun_({j + 1},{i + 1})";
                    var position = block.transform.position;
                    position.z = -1;
                    gunObj.transform.position = position;

                    var gun = gunObj.GetComponent<Gun>();
                    if (!gun) break;

                    var roleData = roleList[i * 6 + j];
                    gun.BindData(roleData);
                    GunList.Add(gun);

                    SetAroundBlock(new Vector2(x, y), roleData.GovernId, roleData.GovernColor.Value);
                }
            }
        }

        private void SetAroundBlock(Vector2 center, int ownerId, Color ownerColor)
        {
            for (int j = (int) center.y - 5; j <= (int) center.y + 5; j++)
            {
                for (int i = (int) center.x - 5; i <= (int) center.x + 5; i++)
                {
                    var block = GetBlockWithXY(i, j);
                    if (!block) continue;

                    block.Init(ownerId, ownerColor);
                }
            }
        }

        private Block GetBlockWithXY(int x, int y)
        {
            return _blockList[(y - 1) * 66 + (x - 1)];
        }

        private void OnBorderTrigger(Transform tran, BorderType type)
        {
            var position = tran.position;
            switch (type)
            {
                case BorderType.LeftBorder:
                    position.x += mapSize.x - 1;
                    tran.position = position;
                    break;
                case BorderType.RightBorder:
                    position.x -= mapSize.x - 1;
                    tran.position = position;
                    break;
                case BorderType.TopBorder:
                    position.y -= mapSize.y - 1;
                    tran.position = position;
                    break;
                case BorderType.BottomBorder:
                    position.y += mapSize.y - 1;
                    tran.position = position;
                    break;
            }
        }
    }
}