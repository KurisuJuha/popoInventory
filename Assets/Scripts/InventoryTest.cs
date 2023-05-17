using JuhaKurisu.PopoTools.InventorySystem;
using JuhaKurisu.PopoTools.InventorySystem.Extentions;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    private InventoryGrid<string> grid;

    private void Start()
    {
        var settings = new InventorySettings<string>(
            item => item,
            (itemA, itemB) => itemA == itemB,
            item => item == "",
            item => 100,
            () => ""
        );
        grid = new InventoryGrid<string>(50, settings);
        grid.SetItems("popo", 30);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) grid.SubtractItem();
        if (Input.GetKeyDown(KeyCode.RightShift)) grid.AddItem();
        Debug.Log(grid.amount);
    }
}