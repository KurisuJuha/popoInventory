using UnityEngine;
using JuhaKurisu.PopoTools.InventorySystem;

public class InventoryTest : MonoBehaviour
{
    public Inventory<string> inventory;

    [SerializeReference] ITestInterface obj1;
    [SerializeField] TestClass testClass;
    [SerializeField] TestClass2 testClass2;

    private void Reset()
    {
        testClass = new TestClass();
        obj1 = testClass;
    }

    // Start is called before the first frame update
    void Start()
    {
        InventorySetting<string> setting = new InventorySetting<string>(
            s => s.Length,
            () => ""
        );
        inventory = new Inventory<string>(9, setting);
        for (int i = 0; i < 9; i++)
        {
            inventory.grids[i].AddAll(setting.CreateGrid("popoInventory"[0..i]));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public interface ITestInterface { }

public class TestClass : ITestInterface, ISerializationCallbackReceiver
{
    public string hoge;

    public void OnAfterDeserialize()
    {
        throw new System.NotImplementedException();
    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class TestClass2
{
    [SerializeField]
    private string hoge;
}