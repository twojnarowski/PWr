using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmGenetyczny2
{
    class ModyfikujPlecak
    {
        public static Plecak modify_sack(Plecak sack, string mode)
        {
            Random rnd = new Random();
            if (mode == "weight-to-profit")
            {
                int index = sack.indices.Max();
                while (sack.weight > sack.capacity)
                {
                    if (sack.selection_list[sack.indices[index]] == 0)
                    {
                        sack.selection_list[sack.indices[index]] = 0;
                    }
                    sack.weight = sack.cal_weight();
                    index = index - 1;
                }
                sack.cost = sack.cal_cost();
            }
            if (mode == "random")
            {
                int index = rnd.Next(0, sack.capacity);
                while (sack.weight > sack.capacity)
                {
                    if (sack.selection_list[index] == 0)
                    {
                        sack.selection_list[index] = 0;
                    }
                    sack.weight = sack.cal_weight();
                    index = index - 1;
                }
                sack.cost = sack.cal_cost();
            }
            return sack;
        }

    }
}
