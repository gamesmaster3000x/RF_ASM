using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using Org.BouncyCastle.Security;
using System.Text;

namespace Berry.src
{
    internal static class Crypto
    {

        public static AsymmetricCipherKeyPair Generate ()
        {
            DilithiumKeyPairGenerator generator = new DilithiumKeyPairGenerator();
            generator.Init(new DilithiumKeyGenerationParameters(
                new SecureRandom(),
                DilithiumParameters.Dilithium5Aes
                ));

            AsymmetricCipherKeyPair keyPair = generator.GenerateKeyPair();

            return keyPair;
        }

        public static byte[] Sign (DilithiumPrivateKeyParameters privateKey, byte[] message)
        {
            DilithiumSigner signer = new DilithiumSigner();

            // What do I put in all these parameters?
            signer.Init(true, new DilithiumPrivateKeyParameters(
                DilithiumParameters.Dilithium5Aes,
                        privateKey.Rho,
                        privateKey.K,
                        privateKey.Tr,
                        privateKey.S1,
                        privateKey.S2,
                        privateKey.T0,
                        privateKey.T1
                ));

            byte[] signature = signer.GenerateSignature(message);
            return signature;
        }

        public static void Test ()
        {
            AsymmetricCipherKeyPair keyPair1 = Generate();
            AsymmetricCipherKeyPair keyPair2 = Generate();

            byte[] message = Encoding.UTF8.GetBytes("Important Message!");
            byte[] signature = Sign((DilithiumPrivateKeyParameters) keyPair1.Private, message);

            bool good1 = Verify((DilithiumPublicKeyParameters) keyPair1.Public, message, signature);
            bool good2 = Verify((DilithiumPublicKeyParameters) keyPair2.Public, message, signature);
        }

        public static bool Verify (DilithiumPublicKeyParameters publicKey, byte[] message, byte[] signature)
        {
            DilithiumSigner verifier = new DilithiumSigner();

            // The constructor asks for "pkEncoded" - how should the bytes be encoded?
            verifier.Init(false, new DilithiumPublicKeyParameters(
                DilithiumParameters.Dilithium5Aes,
                publicKey.GetEncoded()
                ));

            return verifier.VerifySignature(message, signature);
        }

        internal static string GetPassword ()
        {
            throw new NotImplementedException();
        }
    }
}