using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace JuhaKurisu.PopoTools.InventorySystem
{
    [Serializable]
    public class Inventory<ItemType>
    {
        public ReadOnlyCollection<Grid<ItemType>> grids => Array.AsReadOnly(_grids);
        [SerializeField] private Grid<ItemType>[] _grids;
        private InventorySetting<ItemType> setting;

        public Inventory(int size, InventorySetting<ItemType> setting)
        {
            _grids = new Grid<ItemType>[size];
            this.setting = setting;

            for (int i = 0; i < size; i++) _grids[i] = setting.CreateEmptyGrid();
        }

        public bool TryAddItem(Grid<ItemType> grid)
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