using UnityEngine;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class InventoryGrid<ItemType> : IInventoryGrid<ItemType>
    {
        public readonly IInventorySettings<ItemType> inventorySettings;
        public InventoryItem<ItemType> inventoryItem { get; private set; }
        public int amount { get; private set; }
        public readonly int gridMaxAmount;
        public int itemMaxAmount => inventorySettings.ItemMaxAmount(inventoryItem.item);
        public int maxAmount => Mathf.Min(gridMaxAmount, itemMaxAmount);

        public InventoryGrid(int gridMaxAmount, IInventorySettings<ItemType> inventorySettings, InventoryItem<ItemType> inventoryItem)
        {
            this.gridMaxAmount = gridMaxAmount;
            this.inventorySettings = inventorySettings;
            this.inventoryItem = inventoryItem;
        }

        public InventoryGrid(int gridMaxAmount, IInventorySettings<ItemType> inventorySettings)
        {
            this.gridMaxAmount = gridMaxAmount;
            this.inventorySettings = inventorySettings;
            this.inventoryItem = new InventoryItem<ItemType>(
                inventorySettings,
                this.inventorySettings.GenerateEmptyItem()
            );
        }

        /// <summary>
        /// グリッドにアイテムを足す
        /// </summary>
        /// <param name="addAmount">足す数</param>
        /// <returns>足せなかった数</returns>
        public int AddItems(int addAmount)
        {
            // 許容値
            int limit = gridMaxAmount - addAmount;

            if (addAmount > limit)
            {
                // 足す方が大きい場合
                amount = maxAmount;
                MaintainConsistency();
                return addAmount - limit;
            }
            else
            {
                // 許容値の方が大きい場合
                amount += limit;
                MaintainConsistency();
                return limit - addAmount;
            }
        }

        /// <summary>
        /// グリッドからアイテムを引く
        /// </summary>
        /// <param name="subtractAmount">引く数</param>
        /// <returns>引けなかった数</returns>
        public int SubtractItems(int subtractAmount)
        {
            if (amount < subtractAmount)
            {
                int ret = subtractAmount - amount;
                amount = 0;
                MaintainConsistency();
                return ret;
            }
            else
            {
                amount -= subtractAmount;
                MaintainConsistency();
                return 0;
            }
        }

        public void MaintainConsistency()
        {
            // 数が0より小さい、もしくは同じ場合Defaultにする
            if (amount <= 0)
            {
                inventoryItem = new InventoryItem<ItemType>(
                    inventorySettings,
                    inventorySettings.GenerateEmptyItem()
                );
            }

            // デフォルトアイテムなら数を0に
            if (inventorySettings.IsEmptyItem(inventoryItem.item))
            {
                amount = 0;
            }
        }
    }
}