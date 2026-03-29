using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIStatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _rect;
    private UI _ui;
    private PlayerStats _playerStats;

    [SerializeField] private E_StatType _statType;
    [SerializeField] private TextMeshProUGUI _statName;
    [SerializeField] private TextMeshProUGUI _statValue;

    private void OnValidate() {
        gameObject.name = "UIStat - " + GetStatNameByType(_statType);
        _statName.text = GetStatNameByType(_statType);
    }

    private void Awake() {
        _ui = GetComponentInParent<UI>();
        _rect = GetComponent<RectTransform>();
        _playerStats = FindFirstObjectByType<PlayerStats>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _ui.statTooltip.ShowTooltip(true, _rect, _statType);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _ui.statTooltip.ShowTooltip(false, null);
    }


    public void UpdateStatValue() {
        Stat statToUpdate = _playerStats.GetStatByType(_statType);

        if (statToUpdate == null && _statType != E_StatType.ElementalDamage) {
            Debug.Log($"You do not have {statToUpdate} implemented on the player!");
            return;
        }

        float value = 0f;

        switch(_statType) {

            // Major stats
            case E_StatType.Strength:
                value = _playerStats.majorStats.strength.GetValue();
                break; 
            case E_StatType.Agility:
                value = _playerStats.majorStats.agility.GetValue();
                break; 
            case E_StatType.Intelligence:
                value = _playerStats.majorStats.intelligence.GetValue();
                break; 
            case E_StatType.Vitality:
                value = _playerStats.majorStats.vitality.GetValue();
                break;

            // Offense stats
            case E_StatType.Damage:
                value = _playerStats.GetBaseDamage();
                break;
            case E_StatType.CritChance:
                value = _playerStats.GetCritChance();
                break;
            case E_StatType.CritPower:
                value = _playerStats.GetCritPower();
                break;
            case E_StatType.ArmorReduction:
                value = _playerStats.GetArmorReduction() * 100f;
                break;
            case E_StatType.AttackSpeed:
                value = _playerStats.offenseStats.attackSpeed.GetValue() * 100f;
                break;

            // Defense stats
            case E_StatType.MaxHealth:
                value = _playerStats.GetMaxHealth();
                break;
            case E_StatType.HealthRegen:
                value = _playerStats.resourceStats.healthRegen.GetValue();
                break;
            case E_StatType.Evasion:
                value = _playerStats.GetEvasion();
                break;
            case E_StatType.Armor:
                value = _playerStats.GetBaseArmor();
                break;

            // Elemental damage stats
            case E_StatType.FireDamage:
                value = _playerStats.offenseStats.fireDamage.GetValue();
                break;
            case E_StatType.IceDamage:
                value = _playerStats.offenseStats.iceDamage.GetValue();
                break;
            case E_StatType.LightningDamage:
                value = _playerStats.offenseStats.lightningDamage.GetValue();
                break;
            case E_StatType.ElementalDamage:
                value = _playerStats.GetElementalDamage(out E_ElementType element, 1);
                break;

            // Elemental resistance stats
            case E_StatType.FireResistance:
                value = _playerStats.GetElementalResistance(E_ElementType.Fire) * 100f;
                break;
            case E_StatType.IceResistance:
                value = _playerStats.GetElementalResistance(E_ElementType.Ice) * 100f;
                break;
            case E_StatType.LightningResistance:
                value = _playerStats.GetElementalResistance(E_ElementType.Lightning) * 100f;
                break;
        }

        _statValue.text = IsPercentageStat(_statType) ? value + "%" : value.ToString();
    }


    private string GetStatNameByType(E_StatType statType) {
        switch (statType) {
            case E_StatType.MaxHealth: return "Max Health";
            case E_StatType.HealthRegen: return "Health Regeneration";
            case E_StatType.Strength: return "Strength";
            case E_StatType.Agility: return "Agility";
            case E_StatType.Intelligence: return "Intelligence";
            case E_StatType.Vitality: return "Vitality";
            case E_StatType.AttackSpeed: return "Attack Speed";
            case E_StatType.Damage: return "Damage";
            case E_StatType.CritChance: return "Critical Chance";
            case E_StatType.CritPower: return "Critical Power";
            case E_StatType.ArmorReduction: return "Armor Reduction";
            case E_StatType.FireDamage: return "Fire Damage";
            case E_StatType.IceDamage: return "Ice Damage";
            case E_StatType.LightningDamage: return "Lightning Damage";
            case E_StatType.ElementalDamage: return "Elemental Damage";
            case E_StatType.Armor: return "Armor";
            case E_StatType.Evasion: return "Evasion";
            case E_StatType.FireResistance: return "Fire Resistance";
            case E_StatType.IceResistance: return "Ice Resistance";
            case E_StatType.LightningResistance: return "Lightning Resistance";
            default: return "Unknown Stat";
        }
    }

    private bool IsPercentageStat(E_StatType statType) {
        switch (statType) {
            case E_StatType.CritChance:
            case E_StatType.CritPower:
            case E_StatType.ArmorReduction:
            case E_StatType.FireResistance:
            case E_StatType.IceResistance:
            case E_StatType.LightningResistance:
            case E_StatType.Evasion:
            case E_StatType.AttackSpeed:
                return true;
            default:
                return false;
        }
    }

}
