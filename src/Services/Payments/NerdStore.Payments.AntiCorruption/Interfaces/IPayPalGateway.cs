namespace NerdStore.Payments.AntiCorruption.Interfaces;

public interface IPayPalGateway
{
    string GetPayPalServiceKey(string apiKey, string encryptionKey);
    string GetCardHashKey(string serviceKey, string creditCard);
    bool CommitTransaction(string cardHashKey, string orderId, decimal amount);
}