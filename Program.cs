using System;
using System.IO;
using System.Security.Cryptography;

namespace AesEncryption {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Welcome to the Aes Encryption Test tool");

            bool testAes = true;
            while (testAes) {
                Console.WriteLine("Type e key for encryption, type d key for decryption!");
                string aesAction = Console.ReadLine();
                if (aesAction == "e") {
                    string plainText = readPlaintext();
                    string cipherText = EncryptDataWithAes(plainText, out string keyBase64, out string vectorBase64);
                    printEncryptionData(cipherText, keyBase64, vectorBase64);
                    testAes = false;
                } else if (aesAction == "d") {
                    string cipherText = readCiphertext();
                    string keyBase64 = readAesKey();
                    string vectorBase64 = readInitializationVector();
                    string plainText = DecryptDataWithAes(cipherText, keyBase64, vectorBase64);
                    if (plainText.Length != 0) {
                        printDecryptionData(plainText);
                    }
                    testAes = false;
                } else {
                    Console.WriteLine("Wrong input!");
                }
            }
        }
  private static string EncryptDataWithAes(string plainText, out string keyBase64, out string vectorBase64) {
            using(Aes aesAlgorithm = Aes.Create()) {
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
                using(MemoryStream ms = new MemoryStream()) {
                    using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
                        using(StreamWriter sw = new StreamWriter(cs)) {
                            sw.Write(plainText);
                        }
                        encryptedData = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedData);
            }
        }
