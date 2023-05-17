namespace JuhaKurisu.PopoTools.InventorySystem.Extentions
{
    public static class InventoryGridExtention
    {
        public static int AddItem<TItem>(this IInventoryGrid<TItem> self)
        {
            return self.AddItems(1);
        }

        public static int SubtractItem<TItem>(this IInventoryGrid<TItem> self)
        {
            return self.SubtractItems(1);
        }

        public static bool TryAddItem<TItem>(this IInventoryGrid<TItem> self)
        {
            return self.AddItem() == 0;
        }

        public static bool TrySubtractItem<TItem>(this IInventoryGrid<TItem> self)
        {
            return self.SubtractItem() == 0;
        }
    }
}