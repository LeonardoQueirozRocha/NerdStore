using NerdStore.Payments.AntiCorruption.Interfaces;

namespace NerdStore.Payments.AntiCorruption.ConfigurationManagements;

public class ConfigurationManagement : IConfigurationManagement
{
    public string GetValue(string node) =>
        new(Enumerable
            .Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
}