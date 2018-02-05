using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Math
{
    public class MathOnList : IBlock
    {
        static Random rnd = new Random();
        public override object Evaluate(Context context)
        {
            var op = this.Fields.Get("OP");
            var list =  this.Values.Evaluate("LIST", context) as IEnumerable<object>;

            var doubleList = list.Select(x => (double)x).ToArray();

            switch (op)
            {
                case "SUM": return doubleList.Sum();
                case "MIN": return doubleList.Min();
                case "MAX": return doubleList.Max();
                case "AVERAGE": return doubleList.Average();
                case "MEDIAN": return Median(doubleList);
                case "RANDOM": return doubleList.Any() ? doubleList[rnd.Next(doubleList.Count())] as object : null;
                
                case "MODE":
                case "STD_DEV":
                    throw new NotImplementedException($"OP {op} not implemented");

                default: throw new ApplicationException($"Unknown OP {op}");
            }
        }

        object Median(IEnumerable<double> values) 
        {
            if (!values.Any()) return null;
            var sortedValues = values.OrderBy(x => x).ToArray();
            double mid = (sortedValues.Length - 1) / 2.0;
            return (sortedValues[(int)(mid)] + sortedValues[(int)(mid + 0.5)]) / 2;
        }

    }




}