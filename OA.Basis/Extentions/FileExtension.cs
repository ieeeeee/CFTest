using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace OA.Basis.Extentions
{
   public  class FileExtension
    {
        /// <summary>
        /// 生成文件名（按日期随机生成）
        /// </summary>
        /// <param name="postfile"></param>
        /// <returns></returns>
        public static string GetFileName(HttpPostedFile postfile)
        {
            /* 当前时间加随机数 */
            string _FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Math.Abs(postfile.FileName.GetHashCode());
            /* 得到文件扩展名 */
            string FileEx = Path.GetExtension(postfile.FileName);
            _FileName += FileEx;
            return _FileName;
        }

        /// <summary>
        /// 生成文件名（按日期生成）
        /// </summary>
        /// <param name="postfile"></param>
        /// <returns></returns>
        public static string GetFileName(string fileName)
        {
            /* 当前时间加随机数 */
            string _FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            /* 得到文件扩展名 */
            string FileEx = Path.GetExtension(fileName);
            _FileName += FileEx;
            return _FileName;
        }

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void FileDel(string filePath)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// 文件拷贝
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="destFilePath"></param>
        public static void FileCopy(string filePath,string destFilePath)
        {
            if(File.Exists(filePath))
            {
                File.Copy(filePath, destFilePath, true);
            }
        }

        /// <summary>
        /// 判断文件名是否为浏览器可以直接显示的图片文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsImgFileName(string fileName)
        {
            fileName = fileName.Trim();
            if(fileName.EndsWith(".")||fileName.IndexOf(".")==-1)
            {
                return false;
            }
            string extName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
            return (extName == "jpg" || extName == "jpeg" || extName == "png" || extName == "bmp" || extName == "gif");
        }

        /// <summary>
        /// 读取指定文件的指定编码内容
        /// </summary>
        /// <param name="_Path"></param>
        /// <param name="_Charset"></param>
        /// <returns></returns>
        public static string ReadFileValue(string _Path,string _Charset)
        {
            using (StreamReader sr = new StreamReader(_Path, Encoding.GetEncoding(_Charset.ToLower())))
            {
                return sr.ReadToEnd();
            }
        }
        /// <summary>
        /// 向指定的文件写入指定编码的内容(如已存在的文件会被替换)
        /// </summary>
        /// <param name="_Path">文件路径</param>
        /// <param name="_Value">内容</param>
        /// <param name="_Charset">编码</param>
        /// <returns></returns>
        public static bool WriterFile(string Path, string _Value, string _Charset)
        {

            try
            {
                string _Path = AppDomain.CurrentDomain.BaseDirectory + Path;
                int fd = _Path.LastIndexOf(@"\");
                string dr = _Path.Substring(0, fd);
                if (!Directory.Exists(dr))
                {
                    Directory.CreateDirectory(dr);
                }
                using (FileStream fs = File.Create(_Path))
                {
                    byte[] bytes;

                    bytes = System.Text.Encoding.GetEncoding(_Charset.ToLower()).GetBytes(_Value);


                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                string sed = ex.Message;
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// 在当前文件夹下写下内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileValue"></param>
        public static void WriteFileValue(string fileName,string fileValue)
        {
            try
            {
                fileName = fileName.Replace(".txt", "").Replace("/Log/", "").Replace(DateTime.Now.ToString("yyyyMMdd"), "");
                string path = AppDomain.CurrentDomain.BaseDirectory;
                if(!Directory.Exists(path+"Log/"))
                {
                    Directory.CreateDirectory(path + "Log/");
                }
                FileStream fs = new FileStream(path + "\\Log/" + fileName + DateTime.Now.ToString("yyyyMMdd") + ".txt", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(fs);
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                streamWriter.WriteLine(fileValue);
                streamWriter.Flush();
                streamWriter.Close();
                fs.Close();
                path = path + "\\Log/" + fileName + DateTime.Now.AddDays(-50).ToString("yyyyMMdd") + ".txt";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch(Exception exp)
            {

            }
        }

        /// <summary>
        /// 向指定文件追加写入文本内容（追加）
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="filevalue">写入内容</param>
        public static void WriterFileAppendValue(string _FilePath, string _Value, string _Charset)
        {
            System.Text.Encoding tmpEncoding = System.Text.Encoding.Default;
            if (!string.IsNullOrEmpty(_Charset))
            {
                try
                {
                    tmpEncoding = System.Text.Encoding.GetEncoding(_Charset.ToLower());
                }
                catch
                {

                }
            }
            try
            {
                StreamWriter fs = new StreamWriter(_FilePath, true, tmpEncoding);
                fs.Write(_Value);
                fs.Close();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 防止对同一文件并发操作
        /// </summary>
        public static object lockObj = new object();
        static string LogFolder = ConfigExtension.GetConfigString("LogFolder", "Log");
        #region 静态属性

        /// <summary>
        /// 运行的路径根目录
        /// </summary>
        public static string GetStartupPath
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory;
            }
        }


        #endregion


        #region  公共方法

        #region  获取文件或目录的最后修改日期
        /// <summary>
        /// 获取文件或目录的最后修改日期
        /// </summary>
        /// <param name="argPath">完整路径</param>
        /// <param name="argFileName">文件名</param>
        /// <returns></returns>
        public static DateTime GetLastWriteTime(string argFilePath)
        {
            FileInfo myFile = new FileInfo(argFilePath);
            DateTime dtLastWrite = myFile.LastWriteTime;
            return dtLastWrite;
        }
        #endregion

        #region 记录日志
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="argLog">日志内容</param>
        /// <param name="argFolderName">文件夹名</param>
        /// <param name="argPath">路径</param>
        /// <param name="argFileName">文件名</param>
        public static void WriteLog(string argLog, string argFolderName = null, string argPath = null, string argFileName = null)
        {
            string TodayStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            string fileName = (argFileName == null ? TodayStr : (argFileName + TodayStr));
            string path = string.Format("{0}\\{1}", (string.IsNullOrEmpty(argPath) ? GetStartupPath : argPath), LogFolder);
            string logPath = "";
            if (argFolderName == null)
            {
                if (!Directory.Exists(path))
                {
                    new DirectoryInfo(path).Create();
                }
                logPath = string.Format("{0}\\{1}.log", path, fileName);
            }
            else
            {
                string directory = string.Format("{0}\\{1}", path, argFolderName);
                if (!Directory.Exists(directory))
                {
                    new DirectoryInfo(directory).Create();
                }
                logPath = string.Format("{0}\\{1}.log", directory, fileName);
            }

            StreamWriter sw = null;
            if (!File.Exists(logPath))
            {
                lock (lockObj)
                {
                    sw = new StreamWriter(logPath);
                    sw.Write(DateTime.Now.ToString() + "：" + argLog);
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                lock (lockObj)
                {
                    sw = new StreamWriter(logPath, true);
                    sw.Write(string.Format("{0}--------------------------------------------------{0}{0}{0}{1}：{2}", System.Environment.NewLine, DateTime.Now.ToString(), argLog));
                    sw.Flush();
                    sw.Close();
                }
            }
        }
        #endregion

        #endregion

        #region 压缩图片
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dHeight">高度</param>
        /// <param name="dWidth">宽度</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        public static bool CompressImage(string sFile, string dFile, int dHeight, int dWidth, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }
            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }

        }
        #endregion
    }
}
