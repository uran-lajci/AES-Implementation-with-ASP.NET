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
