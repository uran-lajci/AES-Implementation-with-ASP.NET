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
         private static string DecryptDataWithAes(string cipherText, string keyBase64, string vectorBase64) {

            try {
                using(Aes aesAlgorithm = Aes.Create()) {
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
                    using(MemoryStream ms = new MemoryStream(cipher)) {
                        using(CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)) {
                            using(StreamReader sr = new StreamReader(cs)) {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine("Wrong input!" + ex.Message);
                return "";
            }

        }

  // Encryption validation and print methods

        private static string readPlaintext() {
            Console.WriteLine("Please enter the text that you want to encrypt:");
            string plainText = "";
            bool isPlainTextValid = true;
            while (isPlainTextValid) {
                plainText = Console.ReadLine();
                if (plainText.Length == 0) {
                    Console.WriteLine("Invalid plaintext, it should not be empty");
                } else {
                    isPlainTextValid = false;
                }
            }
            Console.WriteLine("--------------------------------------------------------------");
            return plainText;
        }
private static void printEncryptionData(string cipherText, string keyBase64, string vectorBase64) {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Here is the cipher text:");
            Console.WriteLine(cipherText);

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Here is the Aes key in Base64:");
            Console.WriteLine(keyBase64);

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Here is the Aes IV in Base64:");
            Console.WriteLine(vectorBase64);
        }

        // Decryption validation and print methods

        private static string readCiphertext() {
            Console.WriteLine("Please enter the text that you want to decrypt:");
            string cipherText = "";
            bool isCipherTextValid = true;
            while (isCipherTextValid) {
                cipherText = Console.ReadLine();
                if (cipherText.Length == 0) {
                    Console.WriteLine("Invalid ciphertext, it should not be empty");
                } else {
                    isCipherTextValid = false;
                }
            }
            Console.WriteLine("--------------------------------------------------------------");
            return cipherText;
        }

        private static string readAesKey() {
            Console.WriteLine("Provide the Aes Key:");
            string keyBase64 = "";
            bool isKeyBase64Valid = true;
            while (isKeyBase64Valid) {
                keyBase64 = Console.ReadLine();
                if (keyBase64.Length != 44) {
                    Console.WriteLine("Invalid Aes Key, it should be 44 characters long");
                } else {
                    isKeyBase64Valid = false;
                }
            }
            Console.WriteLine("--------------------------------------------------------------");
            return keyBase64;
        }

        private static string readInitializationVector() {
            Console.WriteLine("Provide the initialization vector:");
            string vectorBase64 = "";
            bool isVectorBase64Valid = true;
            while (isVectorBase64Valid) {
                vectorBase64 = Console.ReadLine();
                if (vectorBase64.Length != 24) {
                    Console.WriteLine("Invalid initialization vector, it should be 24 characters long");
                } else {
                    isVectorBase64Valid = false;
                }
            }
            Console.WriteLine("--------------------------------------------------------------");
            return vectorBase64;
        }

        private static void printDecryptionData(string plainText) {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Here is the decrypted data:");
            Console.WriteLine(plainText);
        }
    }
}
