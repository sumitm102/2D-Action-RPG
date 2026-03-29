using TMPro;
using UnityEngine;

public class UIStatTooltip : UITooltip
{
    private PlayerStats _playerStats;
    private TextMeshProUGUI _statTooltipText;

    protected override void Awake() {
        base.Awake();

        _playerStats = FindFirstObjectByType<PlayerStats>();
        _statTooltipText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowTooltip(bool show, RectTransform targetRect, E_StatType statType) {
        base.ShowTooltip(show, targetRect);
        _statTooltipText.text = GetStatTextByType(statType);
    }

    public string GetStatTextByType(E_StatType statType) {
        switch (statType) {

            // Major Stats
            case E_StatType.Strength:
                return "Increases physical damage by 1 per point." +
                       "\nIncreases critical power by 0.5% per point.";
            case E_StatType.Agility:
                return "Increases critical chance by 0.3% per point." +
                       "\nIncreases evasion by 0.5% per point.";
            case E_StatType.Intelligence:
                return "Increases elemental resistances by 0.5% per point." +
                       "\nAdds 1 elemental damage per point as a bonus." +
                       "\nIf all elements have 0 damage, the bonus will not be applied.";
            case E_StatType.Vitality:
                return "Increases maximum health by 5 per point." +
                       "\nIncreases armor by 1 per point.";

            // Physical Damage Stats
            case E_StatType.Damage:
                return "Determines the physical damage of your attacks.";
            case E_StatType.CritChance:
                return "Chance for your attacks to critically strike.";
            case E_StatType.CritPower:
                return "Increases the damage dealt by critical stikes.";
            case E_StatType.ArmorReduction:
                return "Percent of armor that will be ignored by your attacks.";
            case E_StatType.AttackSpeed:
                return "Determines how quickly you can attack.";

            // Defense Stats
            case E_StatType.MaxHealth:
                return "Determines how much total health you have.";
            case E_StatType.HealthRegen:
                return "Amount of health restored per second.";
            case E_StatType.Armor:
                return "Reduces incoming physical damage." + 
                    "\nArmor mitigation is limited at 85%." +
                    "Current mitigation is: " + _playerStats.GetArmorMitigation(0) * 100 + "%.";
            case E_StatType.Evasion:
                return "Chance to completely avoid attacks." + 
                    "\nEvasion is limited at 85%.";

            // Elemental Damage
            case E_StatType.IceDamage:
                return "Determines ice damage of your attacks.";
            case E_StatType.FireDamage:
                return "Determines fire damage of your attacks.";
            case E_StatType.LightningDamage:
                return "Determines lightning damage of your attacks.";
            case E_StatType.ElementalDamage:
                return "Elemental damage combines all three elements. " + 
                "\nThe highest element applies the corresponding element status effect and full damage. " +
                "\nThe other two elements contributes 50% of their damage as a bonus.";

            // Elemental Resistances
            case E_StatType.IceResistance:
                return "Reduces ice damage taken.";
            case E_StatType.FireResistance:
                return "Reduces fire damage taken.";
            case E_StatType.LightningResistance:
                return "Reduces lightning damage taken.";

            default:
                return "No tooltip available for this stat.";

        }
    }
}
