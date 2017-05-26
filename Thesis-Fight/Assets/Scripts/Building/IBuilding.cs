public interface IBuilding
{
    int Cost { get; set; }
    int IncomeValue { get; set; }
    Stat Health { get; set; }
    Stat Armor { get; set; }
    ArmorType buildingArmorType { get; set; }
    string Owner { get; set; }
    bool IsSpecial { get; set; }
}
