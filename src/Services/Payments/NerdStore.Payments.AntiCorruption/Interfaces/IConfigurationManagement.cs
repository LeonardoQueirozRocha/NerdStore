namespace NerdStore.Payments.AntiCorruption.Interfaces;

public interface IConfigurationManagement
{
    string GetValue(string node);
}