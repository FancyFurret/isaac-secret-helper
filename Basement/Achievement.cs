using System.Text.Json.Serialization;

namespace Basement
{
    public record Achievement
    {
        public int Id { get; init; }
        public Isaac.Dlc Dlc { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Unlock { get; init; }
        public AchievementRequirement Requirement { get; init; }
        public AchievementReward Reward { get; init; }

        public bool Unlocked { get; internal set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AchievementRequirement
    {
        MomsHeartHard,
        Isaac,
        QQQ,
        Satan,
        TheLamb,
        BossRush,
        Hush,
        MegaSatan,
        Delirium,
        Mother,
        TheBeast,
        Greed,
        Greedier,
        Completion,
        Challenge,
        Misc
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AchievementReward
    {
        Item,
        Trinket,
        Consumable,
        Character,
        Challenge,
        Baby,
        Stage,
        Boss,
        Misc,
        None
    }
}