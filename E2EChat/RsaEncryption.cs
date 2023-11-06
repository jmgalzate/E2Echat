using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace E2EChat;

public class RsaEncryption
{
    private static RSACryptoServiceProvider _rsa = new RSACryptoServiceProvider(2048);
    private RSAParameters _privateKey;
    private RSAParameters _publicKey;

    public RsaEncryption()
    {
        _privateKey = _rsa.ExportParameters(true);
        _publicKey = _rsa.ExportParameters(false);
    }

    public string getPublicKey()
    {
        var sw = new StringWriter();
        var xs = new XmlSerializer(typeof(RSAParameters));
        xs.Serialize(sw, _publicKey);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(sw.ToString());

        XmlNode modulusNode = doc.DocumentElement!.SelectSingleNode("/RSAParameters/Modulus")!;
        return modulusNode.InnerText;
    }

    public string Encrypt(string plaintext, string publickeyString, string exponentString)
    {
        // Create a new RSAParameters object and set its Modulus and Exponent properties
        RSAParameters publickey = new RSAParameters();
        publickey.Modulus = Convert.FromBase64String(publickeyString);
        publickey.Exponent = Convert.FromBase64String(exponentString);

        // Serialize the RSAParameters object to XML
        var xs = new XmlSerializer(typeof(RSAParameters));
        var sw = new StringWriter();
        xs.Serialize(sw, publickey);
        string publickeyXml = sw.ToString();

        // Use the XML to import the parameters into the RSA crypto service provider
        _rsa = new RSACryptoServiceProvider();
        _rsa.FromXmlString(publickeyXml);

        var data = Encoding.Unicode.GetBytes(plaintext);
        var cypher = _rsa.Encrypt(data, false);

        return Convert.ToBase64String(cypher);
    }

    public string Decrypt(string cypherText)
    {
        _rsa = new RSACryptoServiceProvider();
        _rsa.ImportParameters(_privateKey);
        var dataBytes = Convert.FromBase64String(cypherText);
        var plainText = _rsa.Decrypt(dataBytes, false);

        return Encoding.Unicode.GetString(plainText);
    }
}
