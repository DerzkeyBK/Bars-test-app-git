using System;
using System.Collections.Generic;
using System.Text;

namespace Bars_test_app
{
    public static class StringExtension
    {
        /// <summary>
        /// Переводит строку из формата x TB/GB/MB/KB/B к размеру в гигабайтах
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double ToGB(this string str)
        {
            //Поскольку мы знаем,что к нам придёт строка формата x TB/GB/MB/KB/B мы можем разделить её на 2 части
            var array =str.Split(' ');
            double size = 0;
            switch (array[1])
            {
                case "TB":
                    {
                        size = Convert.ToDouble(array[0])*1024;
                        break;
                    }
                case "GB":
                    {
                        size = Convert.ToDouble(array[0]);
                        break;
                    }
                case "MB":
                    {
                        size = Convert.ToDouble(array[0])/1024;
                        break;
                    }
                case "KB":
                    {
                        size = Convert.ToDouble(array[0]) / Math.Pow(1024,2);
                        break;
                    }
                case "B":
                    {
                        size = Convert.ToDouble(array[0]) / Math.Pow(1024, 3);
                        break;
                    }
            }
            return size;
        }
    }
}
