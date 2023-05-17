using UnityEngine;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class InventoryGrid<TItem> : IInventoryGrid<TItem>
    {
        public readonly int gridMaxAmount;
        public readonly IInventorySettings<TItem> inventorySettings;

        public InventoryGrid(int gridMaxAmount, IInventorySettings<TItem> inventorySettings,
            InventoryItem<TItem> inventoryItem)
        {
            this.gridMaxAmount = gridMaxAmount;
            this.inventorySettings = inventorySettings;
            this.inventoryItem = inventoryItem;
        }

        public InventoryGrid(int gridMaxAmount, IInventorySettings<TItem> inventorySettings)
        {
            this.gridMaxAmount = gridMaxAmount;
            this.inventorySettings = inventorySettings;
            inventoryItem = new InventoryItem<TItem>(
                inventorySettings,
                this.inventorySettings.GenerateEmptyItem()
            );
        }

        public int itemMaxAmount => inventorySettings.ItemMaxAmount(inventoryItem.item);
        public InventoryItem<TItem> inventoryItem { get; private set; }
        public int amount { get; private set; }
        public int maxAmount => Mathf.Min(gridMaxAmount, itemMaxAmount);

        /// <summary>
        ///     グリッドにアイテムを足す
        /// </summary>
        /// <param name="addAmount">足す数</param>
        /// <returns>足せなかった余った数</returns>
        public int AddItems(int addAmount)
        {
            // 許容値
            var limit = maxAmount - amount;

            if (addAmount > limit)
            {
                // 足す方が大きい場合
                amount = maxAmount;
                MaintainConsistency();
                return addAmount - limit;
            }

            // 許容値の方が大きい場合
            amount += addAmount;
            MaintainConsistency();
            return 0;
        }

        /// <summary>
        ///     グリッドからアイテムを引く
        /// </summary>
        /// <param name="subtractAmount">引く数</param>
        /// <returns>引けなかった余った数</returns>
        public int SubtractItems(int subtractAmount)
        {
            if (amount < subtractAmount)
            {
                var ret = subtractAmount - amount;
                amount = 0;
                MaintainConsistency();
                return ret;
            }

            amount -= subtractAmount;
            MaintainConsistency();
            return 0;
        }

        public void SetItems(TItem item, int itemAmount)
        {
            inventoryItem = new InventoryItem<TItem>(inventorySettings, item);
            amount = itemAmount;
            MaintainConsistency();
        }

        public void MaintainConsistency()
        {
            // 量が0より小さい、もしくは同じ場合Defaultにする
            if (amount <= 0)
            {
                inventoryItem = new InventoryItem<TItem>(
                    inventorySettings,
                    inventorySettings.GenerateEmptyItem()
                );
                amount = 0;
            }

            // デフォルトアイテムなら数を0に
            if (inventorySettings.IsEmptyItem(inventoryItem.item)) amount = 0;

            // 量が0より大きい場合は量をmaxAmountにする
            if (amount > maxAmount) amount = maxAmount;
        }
    }
}