using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmGenetyczny2
{
    class Kolonia
    {
        int colonysize;
        int dimension;
        int[] colony;
        int employed;
        int onlookers;
        int scout;
        Random rnd = new Random();

        public Kolonia(Plecak instance, int rozmiarKolonii)
        {
            this.colonysize = rozmiarKolonii;
            int sz = instance.selection_list.Length; //list.length?
            dimension = sz;
            colony = new int[sz];

            for(int m = 1; m <= colonysize; m++)
            {
                int[] x = { 0, 1 };// wygenerowanie losowego chromosomu tu przekopiować
                Plecak sack_obj = new Plecak(x, instance.capacity, instance.items_weight, instance.items_cost, instance.indices);
                sack_obj = ModyfikujPlecak.modify_sack(sack_obj, "weight-to-profit");
                // TODO - obj.colony = [obj.colony sack_obj];
            }

        }
    }
}
