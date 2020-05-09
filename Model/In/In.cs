using System;
using System.Collections.Generic;
using System.Text;

namespace Model.In
{
    public class In
    {
    }

    public class In<T> : In
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }
    }
}
