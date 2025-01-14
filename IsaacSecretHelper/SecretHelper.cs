using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Basement;

namespace IsaacSecretHelper
{
    public class SecretHelper
    {
        private readonly IshOptions _options;
        private readonly List<AchievementRequirement> _requirements;
        private readonly List<AchievementReward> _rewards;

        public SecretHelper(IshOptions options)
        {
            _options = options;
            _requirements = new List<AchievementRequirement>();
            _rewards = new List<AchievementReward>();
        }

        public void Run()
        {
            if (!ValidateOptions())
                return;

            var saveFilePath = FindSaveFile();
            Console.WriteLine($"Using save file {saveFilePath}");
            var saveFile = new SaveFile(FindSaveFile());
            PrintSecrets(saveFile);
        }

        private bool ValidateOptions()
        {
            if (!string.IsNullOrEmpty(_options.SaveFile) && !File.Exists(_options.SaveFile))
                Console.WriteLine("The provided save file path doesn't exist!");
            else if (string.IsNullOrEmpty(_options.SaveFile) &&
                     !Directory.Exists(Environment.ExpandEnvironmentVariables(_options.SavesFolder)))
                Console.WriteLine(
                    "The provided saves folder path doesn't exist! Try setting a different one using the --saves-folder option");
            else if (_options.SaveNumber < 1 || _options.SaveNumber > 3)
                Console.WriteLine("The provided save number is not 1, 2, or 3!");
            else if (!ValidateEnumList(_requirements, _options.Requirements))
                Console.WriteLine("Invalid requirements!");
            else if (!ValidateEnumList(_rewards, _options.Rewards))
                Console.WriteLine("Invalid rewards!");
            else if (FindSaveFile() == null)
                Console.WriteLine("Could not find your save file!");
            else
                return true;

            return false;
        }

        private bool ValidateEnumList<T>(List<T> values, IEnumerable<string> stringValues)
        {
            values.Clear();
            var valid = true;
            foreach (var stringValue in stringValues)
            {
                if (!Enum.TryParse(typeof(T), stringValue, out var enumValue))
                {
                    Console.WriteLine($"{stringValue} is not a valid option!");
                    valid = false;
                }
                else
                {
                    values.Add((T) enumValue);
                }
            }

            return valid;
        }

        private string FindSaveFile()
        {
            if (!string.IsNullOrEmpty(_options.SaveFile))
                return _options.SaveFile;
            return Directory.GetFiles(Environment.ExpandEnvironmentVariables(_options.SavesFolder),
                    $"*persistentgamedata{_options.SaveNumber}.dat")
                .OrderByDescending(File.GetLastWriteTime)
                .FirstOrDefault();
        }

        private void PrintSecrets(SaveFile file)
        {
            var secrets = file.Secrets.Where(secret =>
            {
                if (!_options.ShowUnlocked && secret.Unlocked)
                    return false;
                if (_requirements.Any() && _requirements.All(r => secret.Requirement != r))
                    return false;
                if (_rewards.Any() && _rewards.All(r => secret.Reward != r))
                    return false;
                return true;
            });

            TerminalUtils.WriteTable(
                secrets,
                4,
                s => $"{(s.Unlocked ? " X" : " -")}",
                s => s.Id.ToString(),
                s => s.Dlc switch
                {
                    Isaac.Dlc.Afterbirth => "AB",
                    Isaac.Dlc.AfterbirthPlus => "AB+",
                    Isaac.Dlc.Repentance => "Rep",
                    Isaac.Dlc.RepentancePlus => "Rp+",
                    _ => ""
                },
                s => s.Name,
                s => s.Unlock,
                s => s.Description
            );
        }
    }
}