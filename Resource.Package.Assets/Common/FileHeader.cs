using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Package.Assets.Common
{
    internal enum CompressionOption : Byte
    {
        /// <summary>
        /// 永远不会压缩
        /// </summary>
        NeverCompress = 1,

        /// <summary>
        /// 尽可能压缩
        /// </summary>
        MuchPossibleCompress = 2,

        /// <summary>
        /// 必须压缩
        /// </summary>
        MustCompressed = 3
    }





    internal struct FileHeader
    {
        public UInt32 Magic { get; set; }

        public CompressionOption CompressOption { get; set; }

        public UInt64 TableAddress { get; set; }

        public UInt64 TableSize { get; set; }

        public UInt64 DataAddress { get; set; }



    }
}
