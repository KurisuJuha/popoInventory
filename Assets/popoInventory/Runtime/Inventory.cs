using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    [Serializable]
    public class Inventory<ItemType>
    {
        public ReadOnlyCollection<InventoryGrid<ItemType>> grids => Array.AsReadOnly(_grids);
        [SerializeField] private InventoryGrid<ItemType>[] _grids;
        private InventorySettings<ItemType> setting;

        public Inventory(int size, InventorySettings<ItemType> setting)
        {
            _grids = new InventoryGrid<ItemType>[size];
            this.setting = setting;

            for (int i = 0; i < size; i++) _grids[i] = setting.CreateEmptyGrid();
        }

        public bool TryAddItem(InventoryGrid<ItemType> grid)
        {
            // 同じアイテムに出来るだけ入れてみる
            foreach (var inventoryGrid in grids)
            {
                // 同じアイテムなら入れる
                if (inventoryGrid.item.Equals(grid.item))
                    inventoryGrid.AddAll(grid);
            }
            // 空のgridに入れる
            foreach (var inventoryGrid in grids)
            {
                // 空のgridなら入れる
                if (inventoryGrid.item.Equals(setting.getEmptyItem()))
                    inventoryGrid.AddAll(grid);
            }

            return grid.amount == 0;
        }
    }
}