// Decompiled with JetBrains decompiler
// Type: aes.AES128CTR
// Assembly: LeapPad Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Security.Cryptography;

namespace aes
{
  public class AES128CTR
  {
    private const int KEY_SIZE = 128;
    private const int BLOCK_SIZE = 16;
    public static readonly byte[] KEY_WII_COMMON = new byte[16]
    {
      (byte) 235,
      (byte) 228,
      (byte) 42,
      (byte) 34,
      (byte) 94,
      (byte) 133,
      (byte) 147,
      (byte) 228,
      (byte) 72,
      (byte) 217,
      (byte) 197,
      (byte) 69,
      (byte) 115,
      (byte) 129,
      (byte) 170,
      (byte) 247
    };
    public static readonly byte[] KEY_WII_SD = new byte[16]
    {
      (byte) 171,
      (byte) 1,
      (byte) 185,
      (byte) 216,
      (byte) 225,
      (byte) 98,
      (byte) 43,
      (byte) 8,
      (byte) 175,
      (byte) 186,
      (byte) 216,
      (byte) 77,
      (byte) 191,
      (byte) 194,
      (byte) 165,
      (byte) 93
    };
    public static readonly byte[] IV_WII_SD = new byte[16]
    {
      (byte) 33,
      (byte) 103,
      (byte) 18,
      (byte) 230,
      (byte) 170,
      (byte) 31,
      (byte) 104,
      (byte) 159,
      (byte) 149,
      (byte) 197,
      (byte) 162,
      (byte) 35,
      (byte) 36,
      (byte) 220,
      (byte) 106,
      (byte) 152
    };
    private byte[] key;
    private byte[] iv;
    private AesManaged am;

    public AES128CTR(byte[] key)
    {
      this.iv = new byte[16];
      for (int index = 0; index < 16; ++index)
        this.iv[index] = (byte) 0;
      this.key = key;
      this.am = new AesManaged();
      this.am.KeySize = 128;
      this.am.Key = key;
      this.am.IV = this.iv;
      this.am.Mode = CipherMode.ECB;
      this.am.Padding = PaddingMode.None;
    }

    private void XorBlock(byte[] dst, int offset, byte[] ctrBlock)
    {
      for (int index = 0; index < this.key.Length; ++index)
        dst[index + offset] ^= ctrBlock[index];
    }

    public byte[] Decrypt(byte[] input)
    {
      ICryptoTransform encryptor = this.am.CreateEncryptor();
      byte[] numArray1 = new byte[16];
      byte[] numArray2 = new byte[16];
      Buffer.BlockCopy((Array) input, 0, (Array) numArray1, 0, 8);
      numArray1[8] = (byte) 0;
      numArray1[9] = (byte) 0;
      numArray1[10] = (byte) 0;
      numArray1[11] = (byte) 0;
      for (int offset = 0; offset < input.Length; offset += 16)
      {
        Buffer.BlockCopy((Array) BitConverter.GetBytes(offset), 0, (Array) numArray1, 12, 4);
        encryptor.TransformBlock(numArray1, 0, 16, numArray2, 0);
        this.XorBlock(input, offset, numArray2);
      }
      return input;
    }

    public byte[] Encrypt(byte[] input) => input;
  }
}
