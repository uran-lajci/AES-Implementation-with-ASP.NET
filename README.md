# Information Security
# AES Encryption with ASP.NET

# AES (Advanced Encryption Standard)

The Advanced Encryption Standard (AES) is a symmetric block cipher.

•	AES-128 uses a 128-bit key length to encrypt and decrypt a block of messages.

•	AES-192 uses a 192-bit key length to encrypt and decrypt a block of messages.

•	AES-256 uses a 256-bit key length to encrypt and decrypt a block of messages.

Each cipher encrypts and decrypts data in blocks of 128 bits using cryptographic keys of 128, 192 and 256 bits, respectively.

AES operates on a 4 × 4 column-major order array of 16 bytes b0,b1,...,b15 termed the state

b0	b4	b8	b12

b1	b5	b9	b13

b2	b6	b10	b14

b3	b7	b11	b15

The number of rounds are as follows:

•	10 rounds for 128-bit keys.

•	12 rounds for 192-bit keys.

•	14 rounds for 256-bit keys.

![image](https://user-images.githubusercontent.com/117693854/205474985-ae64ec4c-f619-468e-b48e-79192cb03077.png)

![image](https://user-images.githubusercontent.com/117693854/205474990-b5aacb0c-c9c1-4943-bc45-3091c5d138e9.png)

In the SubBytes step, each byte in the state is replaced with its entry in a fixed 8-bit lookup table, S; bij = S(aij).

![image](https://user-images.githubusercontent.com/117693854/205475003-581c50f8-243f-41b8-9352-a7c670a524db.png)

In the ShiftRows step, bytes in each row of the state are shifted cyclically to the left. The number of places each byte is shifted differs incrementally for each row.

![image](https://user-images.githubusercontent.com/117693854/205475011-5ecb0aa4-6fb6-4380-995f-e56cf25975d3.png)

In the MixColumns step, each column of the state is multiplied with a fixed polynomial c(x).


# Tests
![317794271_537298731346915_4040557537840596551_n](https://user-images.githubusercontent.com/58117020/205487675-f0911e1b-9b5c-4ac1-bd53-d8f4f8825b10.png)

![318053349_1443376229814265_8592709450762097204_n](https://user-images.githubusercontent.com/58117020/205487725-7d89a2f9-ccd7-42be-ab3b-404e159c28d1.png)

![318298564_2910831972545724_9218231228228026869_n](https://user-images.githubusercontent.com/58117020/205487741-a7c17b65-d040-43c8-a2b4-855acacb9fa9.png)

![317951125_985779916144328_236201654989166453_n](https://user-images.githubusercontent.com/58117020/205487752-a0c6b5c8-f437-4958-84c5-266e81c34223.png)

