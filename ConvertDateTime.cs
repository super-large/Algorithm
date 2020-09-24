using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HT.Algorithm
{
    public class ConvertDateTime
    {
        #region [构造及基本功能]
        /// <summary>
        /// 日期格式由“yyyyMMddHHmmss”转换为“MM-dd HH:mm”
        /// </summary>
        ///  /// <param name="strDatetime">日期字符串</param>
        /// <returns>返回日期格式“MM-dd HH:mm”字符串</returns>
        public string GetDateTimeByString(string strDatetime)
        {
            string NewDateTime = ""; //格式“MM-dd HH:mm”
            NewDateTime = strDatetime.Substring(4, 8);
            NewDateTime = NewDateTime.Insert(6, ":");
            NewDateTime = NewDateTime.Insert(4, " ");
            NewDateTime = NewDateTime.Insert(2, "-");
            return NewDateTime;
        }
        #endregion
    }
}
