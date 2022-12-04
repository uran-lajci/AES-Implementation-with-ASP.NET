using System;
using System.IO;
using System.Security.Cryptography;

namespace AesEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Aes Encryption Test tool");
            Console.WriteLine("Type 'encrypt' key for encryption, type any key for decryption!");
						string EncDEC = Console.ReadLine();
												Console.WriteLine(EncDEC);
						if(EncDEC=="encrypt"){

						Console.WriteLine("Please enter the text that you want to encrypt:");
            
						string plainText = Console.ReadLine();
            string cipherText = EncryptDataWithAes(plainText, out string keyBase64, out string vectorBase64);

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Here is the cipher text:");
            Console.WriteLine(cipherText);

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Here is the Aes key in Base64:");
            Console.WriteLine(keyBase64);

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Here is the Aes IV in Base64:");
            Console.WriteLine(vectorBase64);

			}else {
                Console.WriteLine("Please enter the text that you want to decrypt:");
            string cipherText = Console.ReadLine();
            Console.WriteLine("--------------------------------------------------------------");

            Console.WriteLine("Provide the Aes Key:");
            string keyBase64 = Console.ReadLine();
            Console.WriteLine("--------------------------------------------------------------");

            Console.WriteLine("Provide the initialization vector:");
            string vectorBase64 = Console.ReadLine();
            Console.WriteLine("--------------------------------------------------------------");

                        
            string plainText = DecryptDataWithAes(cipherText, keyBase64, vectorBase64);

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Here is the decrypted data:");
            Console.WriteLine(plainText);
                        
		}

        }

        private static string EncryptDataWithAes(string plainText, out string keyBase64, out string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
                Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
                Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");
                Console.WriteLine($"Aes Block Size : {aesAlgorithm.BlockSize}");

                //set the parameters with out keyword
                keyBase64 = Convert.ToBase64String(aesAlgorithm.Key);
                vectorBase64 = Convert.ToBase64String(aesAlgorithm.IV);

                // Create encryptor object
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                byte[] encryptedData;

                //Encryption will be done in a memory stream through a CryptoStream object
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        encryptedData = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedData);
            }
        }
	    
	private static string DecryptDataWithAes(string cipherText, string keyBase64, string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);

                Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
                Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
                Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");
                Console.WriteLine($"Aes Block Size : {aesAlgorithm.BlockSize}");

                // Create decryptor object
                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

                byte[] cipher = Convert.FromBase64String(cipherText);

                //Decryption will be done in a memory stream through a CryptoStream object
                using (MemoryStream ms = new MemoryStream(cipher))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

		
    }
}
