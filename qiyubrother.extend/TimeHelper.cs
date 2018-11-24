using System;
using System.Collections.Generic;
using System.Text;

namespace qiyubrother.extend
{
    public class TimeHelper
    {
        public static long DateTimeToTimeStamp(DateTime dateTime)
        {
            var startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
            //var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            return (long)(dateTime - startTime).TotalSeconds; // 相差秒数
        }
    }
}
