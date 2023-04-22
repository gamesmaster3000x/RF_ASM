using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Kyber;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Tls;
using System.Text;

namespace Berry.src
{
	public class Berry
	{
		public static int Main (string[] args)
		{
			Test();

			Run();
			return 0;
		}

		public static void Run ()
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder();

			_ = builder.WebHost.ConfigureKestrel(serverOptions =>
			{
				serverOptions.ConfigureEndpointDefaults(listenOptions =>
				{

				});

				serverOptions.ConfigureHttpsDefaults(listenOptions => { });
			});

			// Add services to the container.
			_ = builder.Services.AddRazorPages();

			_ = builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

			var app = builder.Build();

			string expression = @".+exp.+";
			_ = app.MapGet("/get/{id:required:regex(" + expression + ")}", (string id) => Get(id));


			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				_ = app.UseExceptionHandler("/Error");
			}
			_ = app.UseStaticFiles();

			_ = app.UseRouting();

			_ = app.UseAuthorization();

			_ = app.UseHsts();

			_ = app.UseHttpsRedirection();

			_ = app.MapRazorPages();

			app.Run();
		}

		public static string Get (string id)
		{
			return $"Hello, World! {id}";
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
	}
}