using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OA.Basis.Extentions
{
   public class EnDecryption
    {
        private static string EncryptKey = "_plmokn_";
        private EnDecryption()
        { }

        //AES：更快，兼容设备，安全级别高；            有key    可逆

        //SHA1：公钥后处理回传                                  不可逆

        //DES：本地数据，安全级别低                    有key    可逆

        //RSA：非对称加密，有公钥和私钥                有key    可逆  公、私Key采用不同的加密算法

        //MD5：防篡改                                  没有key   不可逆

        //MD5 SHA 是哈希函数 不是加密算法

        //加密输入的字符串 要转换成 byte[]=Encoding.Default.GetBytes(EncryptString)
        //    输入的字符串 要转换成 string=Convert.ToBase64String(m_stream.ToArray())
        //解密输入的字符串 要转换成 byte[]=Convert.FromBase64String(DecryptString)
        //    输入的字符串 要转换成 string=Encoding.Default.GetString(m_stream.ToArray())


        #region DES加密解密
        //DES加密 (数据加密标准，速度较快，适用于加密大量数据的场合)
        public static string DESEncrypt(string EncryptString)
        {
            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("密文不得为空")); }
            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }
            if (EncryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }
            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF }; //初始化向量
            string m_strEncrypt = "";
            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider(); //using System.Security.Cryptography;
            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
                MemoryStream m_stream = new MemoryStream(); //创建一个流，其后备存储为内存。实现对内存进行数据读写的功能，而不是对持久性存储器进行读写
                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write); //定义将数据流链接到加密转换的流。
                m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);
                m_cstream.FlushFinalBlock();
                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
                m_stream.Close();
                m_stream.Dispose();
                m_cstream.Close();
                m_cstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch(CryptographicException ex) { throw ex; }
            catch(ArgumentException ex) { throw ex; }
            catch(Exception ex) { throw ex; }
            finally { m_DESProvider.Clear(); }
            return m_strEncrypt;
        }
        //DES解密 (数据加密标准，速度较快，适用于加密大量数据的场合)
        public static string DESDecrypt(string DecryptString)
        {
            if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("密文不得为空")); }
            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }
            if (EncryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }
            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF }; //初始化向量
            string m_strDecrypt = "";
            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider(); //using System.Security.Cryptography;
            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
                MemoryStream m_stream = new MemoryStream(); //创建一个流，其后备存储为内存。实现对内存进行数据读写的功能，而不是对持久性存储器进行读写
                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write); //定义将数据流链接到加密转换的流。
                m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);
                m_cstream.FlushFinalBlock();
                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());
                m_stream.Close();
                m_stream.Dispose();
                m_cstream.Close();
                m_cstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_DESProvider.Clear(); }
            return m_strDecrypt;
        }

        #endregion

        #region 3DES加密解密
        //3DES加密
        public static string Encrypt3Des(string encryStr,string  key)
        {
            try
            {
                var inputArray = Encoding.Default.GetBytes(encryStr);
                var hashmd5 = new MD5CryptoServiceProvider();
                var byKey = hashmd5.ComputeHash(Encoding.Default.GetBytes(key));
                var byIv = byKey;
                var ms = new MemoryStream();
                using (var tDescryptProvider = new TripleDESCryptoServiceProvider())
                {
                    tDescryptProvider.Mode = CipherMode.ECB;
                    using (var cs = new CryptoStream(ms, tDescryptProvider.CreateEncryptor(byKey, byIv), CryptoStreamMode.Write))
                    {
                        cs.Write(inputArray, 0, inputArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                }
                var str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
            catch(Exception)
            {
                return encryStr;
            }
        }

        //3DES解密
        public static string Decrypt3Des(string decryStr, string key)
        {
            try
            {
                var inputArray = Convert.FromBase64String(decryStr);
                var hashmd5 = new MD5CryptoServiceProvider();
                var byKey = hashmd5.ComputeHash(Encoding.Default.GetBytes(key));
                var byIv = byKey;
                var ms = new MemoryStream();
                using (var tDescryptProvider = new TripleDESCryptoServiceProvider())
                {
                    tDescryptProvider.Mode = CipherMode.ECB;
                    using (var cs = new CryptoStream(ms, tDescryptProvider.CreateEncryptor(byKey, byIv), CryptoStreamMode.Write))
                    {
                        cs.Write(inputArray, 0, inputArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                }
                var str = Encoding.Default.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
            catch (Exception)
            {
                return decryStr;
            }
        }
        #endregion

        #region MD5加密解密

        public static string MD5Encrypt(string inputStr)
        {
            try
            {
                string output = "";
                MD5 hasher = MD5Cng.Create();
                byte[] data = hasher.ComputeHash(Encoding.Default.GetBytes(inputStr));
                output = BitConverter.ToString(data);
                return output;
            }
            catch(Exception e)
            {
                File.WriteAllText("D:\\err.log.text", e.Message);
                throw;
            }
        }
        public static string MD5Decrypt(string str,int code)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] buffer = md.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder builder = new StringBuilder(code);
            for(int i=0;i<buffer.Length;i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }
        #endregion
    }
}
