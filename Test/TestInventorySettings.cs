using JuhaKurisu.PopoTools.InventorySystem;

namespace Test;

public class TestInventorySettings : IInventorySettings<TestInventorySettings, TestItem>
{
    private readonly TestItem _emptyItem;
    private readonly int _maxItemAmountInItem;

    public TestInventorySettings(int maxItemAmountInItem, TestItem emptyItem)
    {
        _maxItemAmountInItem = maxItemAmountInItem;
        _emptyItem = emptyItem;
    }

    public int GetMaxItemAmountInItem(TestItem item)
    {
        return _maxItemAmountInItem;
    }

    public TestItem GetEmptyItem()
    {
        return _emptyItem;
    }

    public bool AreSameItem(TestItem item1, TestItem item2)
    {
        return item1.Equals(item2);
    }
}