namespace JuhaKurisu.PopoTools.InventorySystem
{
    public interface IInventorySettings<ItemType>
    {
        public ItemType CopyItem();

        public bool IsSameItem(ItemType itemA, ItemType itemB);

        public bool IsEmptyItem(ItemType item);

        public int ItemMaxAmount(ItemType item);

        public ItemType GenerateEmptyItem();
    }
}