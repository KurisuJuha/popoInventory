using System;
using UnityEngine;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    [Serializable]
    public class InventoryGrid<ItemType>
    {
        [SerializeField] private ItemType _item;
        [SerializeField] private int _amount;
        private InventorySettings<ItemType> _settings;

        /// <summary>
        /// 中身のItem
        /// </summary>
        public ItemType item => _item;

        /// <summary>
        /// 個数
        /// </summary>
        public int amount => _amount;

        /// <summary>
        /// 参考にするSettings
        /// </summary>
        public InventorySettings<ItemType> settings => _settings;

        /// <summary>
        /// settingsをもとに空のGridを生成する
        /// </summary>
        /// <param name="settings">参考にするsettings</param>
        public InventoryGrid(InventorySettings<ItemType> settings)
        {
            this._settings = settings;
            this._item = settings.getEmptyItem();
            this._amount = 0;

            MaintainConsistency();
        }

        /// <summary>
        /// 指定されたアイテムと個数でGridを生成する
        /// </summary>
        /// <param name="item">Gridの中身のItem</param>
        /// <param name="amount">個数</param>
        /// <param name="settings">参考にするSettings</param>
        public InventoryGrid(ItemType item, int amount, InventorySettings<ItemType> settings)
        {
            this._settings = settings;
            this._item = item;
            this._amount = amount;

            MaintainConsistency();
        }

        /// <summary>
        /// 指定されたアイテムをもとにGridを生成する
        /// </summary>
        /// <param name="item">Gridの中身のItem</param>
        /// <param name="setting">参考にするSettings</param>
        /// <param name="toMaximize">個数を上限いっぱいにするかどうか</param>
        public InventoryGrid(ItemType item, InventorySettings<ItemType> setting, bool toMaximize = false)
        {
            this._settings = setting;
            this._item = item;
            this._amount = toMaximize ? setting.getMaxAmount(item) : 1;

            MaintainConsistency();
        }

        /// <summary>
        /// このGridにアイテムを可能な限り全て追加する。
        /// </summary>
        /// <param name="otherGrid">追加するアイテムの供給元</param>
        public void AddAll(InventoryGrid<ItemType> otherGrid)
        {
            bool isSameItem = item.Equals(otherGrid.item);

            if (!isSameItem && amount != 0) return;

            int p = Math.Clamp(otherGrid.amount, 0, settings.getMaxAmount.Invoke(otherGrid.item) - amount);
            _amount += p;
            otherGrid._amount -= p;
            _item = otherGrid.item;

            MaintainConsistency();
            otherGrid.MaintainConsistency();
        }

        /// <summary>
        /// このGridにアイテムを追加する。
        /// </summary>
        /// <param name="otherGrid">追加するアイテムの供給元</param>
        /// <param name="addAmount">追加する数</param>
        public void Add(InventoryGrid<ItemType> otherGrid, int addAmount)
        {
            bool isSameItem = item.Equals(otherGrid.item);

            if (!isSameItem && amount != 0) return;

            int p = Math.Clamp(addAmount, 0, settings.getMaxAmount.Invoke(otherGrid.item) - amount);
            _amount += p;
            otherGrid._amount -= p;
            _item = otherGrid.item;

            MaintainConsistency();
            otherGrid.MaintainConsistency();
        }

        /// <summary>
        /// このGridの中身を交換する
        /// </summary>
        /// <param name="otherGrid">中身の交換対象</param>
        public void Exchange(InventoryGrid<ItemType> otherGrid)
        {
            (_item, otherGrid._item) = (otherGrid._item, _item);
            (_amount, otherGrid._amount) = (otherGrid._amount, _amount);

            MaintainConsistency();
            otherGrid.MaintainConsistency();
        }

        /// <summary>
        /// 整合性をチェックする
        /// </summary>
        public void MaintainConsistency()
        {
            // 数が0ならemptyアイテムに
            if (amount <= 0) _item = settings.getEmptyItem();

            // 数が0以下なら0に
            if (amount <= 0) _amount = 0;

            // 数が最大値よりも大きいなら最大値に
            int maxAmount = settings.getMaxAmount(item);
            if (amount > maxAmount) _amount = maxAmount;
        }
    }
}