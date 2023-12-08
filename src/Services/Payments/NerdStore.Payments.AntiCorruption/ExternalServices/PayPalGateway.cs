using NerdStore.Payments.AntiCorruption.Interfaces;

namespace NerdStore.Payments.AntiCorruption.ExternalServices;

public class PayPalGateway : IPayPalGateway
{
    public string GetPayPalServiceKey(string apiKey, string encryptionKey) =>
        new(Enumerable
            .Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());

    public string GetCardHashKey(string serviceKey, string creditCard) =>
        new(Enumerable
            .Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());

    public bool CommitTransaction(string cardHashKey, string orderId, decimal amount) =>
        new Random().Next(2) == 0;
}