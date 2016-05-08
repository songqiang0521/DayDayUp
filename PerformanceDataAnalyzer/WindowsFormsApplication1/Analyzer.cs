using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hollysys.LeakAnalyzer
{
    public class Analyzer
    {
        /// <summary>
        /// 获取最大最小值
        /// </summary>
        /// <param name="values"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        public void GetMaxMinValue(double[] values, ref double max, ref double min)
        {
            double maxValue = values[0];
            double minValue = values[0];


            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] > maxValue)
                {
                    maxValue = values[i];
                }

                if (values[i] < minValue)
                {
                    minValue = values[i];
                }
            }

            max = maxValue;
            min = minValue;

        }


        /// <summary>
        /// 获取平均值
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public double GetMeanValue(double[] values)
        {
            double sum = 0.0;

            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }

            return sum / values.Length;
        }



        //获取数组中后者大于前者的百分比
        public unsafe double GetPercentOfLatterBigThanFormer(double* values, int countOfValues)
        {
            int count = 0;

            for (int i = 0; i < countOfValues - 1; i++)
            {
                //if (values[i + 1] >= values[i])
                if (values[i + 1] > values[i])
                {
                    count += 1;
                }
            }
            return ((double)count / (double)(countOfValues-1));

        }


        public unsafe double GetMeanValue(double* values, int countOfValues)
        {

            double sum = 0.0;

            for (int i = 0; i < countOfValues; i++)
            {
                sum += values[i];
            }

            return sum / countOfValues;
        }

        //获取数组中后者大于前者的百分比
        public unsafe double GetPercentOfLatterBigThanFormerBySections(double* values, int countOfValues, int countOfSections)
        {

            int countInOneSection = countOfValues / countOfSections;
            bool append = (countOfValues % countOfSections) > 0;
            double[] values2 = new double[countOfSections + (append ? 1 : 0)];

            fixed (double* pValues2 = values2)
            {
                int i = 0;
                for (i = 0; i < countOfSections; i++)
                {
                    values2[i] = GetMeanValue(values + i * countInOneSection, countInOneSection);
                }
                if (append)
                {
                    values2[countOfSections] = GetMeanValue(values + i * countInOneSection, countOfValues % countInOneSection);
                }


                double percent = GetPercentOfLatterBigThanFormer(pValues2, countOfSections + (append ? 1 : 0));
                return percent;
            }
        }

    }
}
