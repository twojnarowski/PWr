using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmGenetyczny2
{
    class Plecak
    {
        public int capacity;
        public int weight;
        public int cost;
        public int[] selection_list;
        public int items_weight;
        public int items_cost;
        public int[] indices;

        public Plecak(int[] selection_list, int capacity, int items_weight, int items_cost, int[] indices)
        {
            this.capacity = capacity;
            this.selection_list = selection_list;
            this.items_weight = items_weight;
            this.items_cost = items_cost;
            this.indices = indices;
            this.weight = cal_weight();
            this.cost = cal_cost();
        }

        public int cal_cost()
        {
            throw new NotImplementedException();
        }

        public int cal_weight()
        {
            throw new NotImplementedException();
        }
    }
}
