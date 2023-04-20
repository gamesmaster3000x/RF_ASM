using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Kyber;
using Org.BouncyCastle.Security;
using System.Text;

namespace Berry.src
{
	public class Berry
	{
		public static int Main (string[] args)
		{
			Crypto();

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

		public static void Generate ()
		{
			DilithiumKeyPairGenerator generator = new DilithiumKeyPairGenerator();
			generator.Init(new DilithiumKeyGenerationParameters(
				new SecureRandom(),
				DilithiumParameters.Dilithium5Aes
				));

			AsymmetricCipherKeyPair keyPair = generator.GenerateKeyPair();

			// All the later methods need bytes of these key - how do I get them?
			AsymmetricKeyParameter publicKey = keyPair.Public;
			AsymmetricKeyParameter privateKey = keyPair.Private;
		}

		public static void Sign ()
		{
			DilithiumSigner signer = new DilithiumSigner();

			// What do I put in all these parameters?
			signer.Init(true, new DilithiumPrivateKeyParameters(
				DilithiumParameters.Dilithium5Aes,
						// byte[] rho,
						// byte[] K,
						// byte[] tr,
						// byte[] s1,
						// byte[] s2,
						// byte[] t1,
						// byte[] t2,
				));

			string messageText = "Important Message!";

			// Send the message and signature
			byte[] message = Encoding.UTF8.GetBytes(messageText);
			byte[] signature = signer.GenerateSignature(message);
		}

		public void Verify (byte[] publicKey, byte[] message, byte[] signature)
		{
			DilithiumSigner verifier = new DilithiumSigner();

			// The constructor asks for "pkEncoded" - how should the bytes be encoded?
			verifier.Init(false, new DilithiumPublicKeyParameters(publicKey));

			bool good = verifier.VerifySignature(message, signature);
		}
	}
}