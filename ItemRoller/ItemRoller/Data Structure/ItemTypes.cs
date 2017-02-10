using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace ItemRoller.Data_Structure
{
    [Flags()]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ItemTypes
    {
        None = 0,
        Magic = 1,
        Armor = 1 << 1,
        Shield = 1 << 2 | Armor,
        Melee_Weapon = 1 << 3,
        Ranged_Weapon = 1 << 4,
        Weapon = Melee_Weapon | Ranged_Weapon,
        Potion = 1 << 5,
        Ring = 1 << 6,
        Rod = 1 << 7,
        Scroll = 1 << 8,
        Staff = 1 << 9,
        Wand = 1 << 10,
        Wondrous = 1 << 11,
        Salvage = 1 << 12,
        Alchemical = 1 << 13,
        Alcohol = 1 << 14,
        Tool = 1 << 15,
        Gem = 1 << 16,
        Art = 1 << 17,
        Ammunition = 1 << 18,
        Ability = 1 << 19,
        Magic_Minor = 1 << 20,
        Magic_Medium = 1 << 21,
        Magic_Major = 1 << 22,
        Table = 1 << 23 //24 bits
    }
}
