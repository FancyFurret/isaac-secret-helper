using System.Collections.Generic;
using CommandLine;

namespace IsaacSecretHelper
{
    public record IshOptions
    {
        [Option('s', "save", Default = 1, HelpText = "Which save file should be used? (1, 2, or 3)")]
        public int SaveNumber { get; init; }

        [Option('f', "save-file", Required = false,
            HelpText = "Path to a specific save file. Overrides --saves-folder and --save")]
        public string SaveFile { get; init; }

        [Option('d', "saves-folder", Default = "%UserProfile%\\Documents\\My Games\\Binding of Isaac Repentance+\\save_backups",
            HelpText = "Folder where your saves are located")]
        public string SavesFolder { get; init; }

        [Option('u', "unlocked", Required = false, HelpText = "Show already unlocked achievements")]
        public bool ShowUnlocked { get; init; }

        [Option('q', "requirements", Required = false, Separator = ',',
            HelpText = "Comma separated list of achievement requirement types: " +
                       "\n\t * MomsHeartHard (Achievements for beating Mom's Heart or It Lives! on Hard mode)" +
                       "\n\t * Isaac (Achievements for beating Isaac)" +
                       "\n\t * QQQ (Achievements for beating ???)" +
                       "\n\t * Satan (Achievements for beating Satan)" +
                       "\n\t * TheLamb (Achievements for beating The Lamb)" +
                       "\n\t * BossRush (Achievements for beating Boss Rush)" +
                       "\n\t * Hush (Achievements for beating Hush)" +
                       "\n\t * Delirium (Achievements for beating Delirium)" +
                       "\n\t * Mother (Achievements for beating Mother)" +
                       "\n\t * TheBeast (Achievements for beating The Beast)" +
                       "\n\t * Greed (Achievements for beating Greed mode)" +
                       "\n\t * Greedier (Achievements for beating Greedier mode)" +
                       "\n\t * Completion (Achievements for beating multiple bosses)" +
                       "\n\t * Challenge (Achievements for beating Challenges)" +
                       "\n\t * Misc (Achievements that aren't tied to Challenges/Completion Marks)"
        )]
        public IEnumerable<string> Requirements { get; init; }

        [Option('r', "rewards", Required = false, Separator = ',',
            HelpText = "Comma separated list of achievement reward types: " +
                       "\n\t * Item (Passive or Active items)" +
                       "\n\t * Trinket (Trinkets)" +
                       "\n\t * Consumable (Cards, Runs, Pills, Keys, Chests, etc.)" +
                       "\n\t * Character (A Character or upgrade to a Character)" +
                       "\n\t * Challenge (Challenges)" +
                       "\n\t * Baby (Co-Op Babies)" +
                       "\n\t * Stage (New chapters or alt-floors)" +
                       "\n\t * Boss (Bosses)" +
                       "\n\t * Misc (Rocks, Machines, and anything else not in an above category)" +
                       "\n\t * None (Doesn't unlock anything)"
        )]
        public IEnumerable<string> Rewards { get; init; }
    }
}