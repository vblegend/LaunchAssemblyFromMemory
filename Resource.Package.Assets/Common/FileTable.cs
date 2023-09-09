using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Package.Assets.Common
{
    internal enum FileType : Byte{ 
        Text = 0,
        Json = 1,
        Xaml = 2,
        Image = 3,
        Binary = 4,
        
    }






    internal struct FileTable
    {
        /// <summary>
        /// 文件名
        /// 32字节
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 文件类型
        /// 1字节
        /// </summary>
        public FileType type { get; set; }

        /// <summary>
        /// 是否被压缩过
        /// 1字节
        /// </summary>
        public Byte isCompressed { get; set; }

        /// <summary>
        /// 数据地址
        /// 4字节
        /// </summary>
        public UInt32 lpData { get; set; }

        /// <summary>
        /// 数据大小
        /// 4字节
        /// </summary>
        public UInt32 lpSize { get; set; }

    }
}
