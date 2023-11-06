namespace E2EChat;

public class Message
{
    private RsaEncryption rsa = new RsaEncryption();
    private string _publicKey;

    public Message()
    {
        _publicKey = rsa.getPublicKey();
    }

    public string PublicKey
    {
        get { return _publicKey; }
    }

    public string Encrypt(string message, string receiverPublicKey)
    {
        return rsa.Encrypt(message, receiverPublicKey, "AQAB");
    }

    public string Decrypt(string cypherText)
    {
        return rsa.Decrypt(cypherText);
    }

}
