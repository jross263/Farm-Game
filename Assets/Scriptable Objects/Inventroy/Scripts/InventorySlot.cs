[System.Serializable]
public class InventorySlot {
  public ItemObject item;
  public int amount;

  public InventorySlot(ItemObject item, int amount) {
    this.item = item;
    this.amount = amount;
  }

  public void AddAmount(int amount) {
    this.amount += amount;
  }

}
