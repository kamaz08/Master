﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgorytmyOnline.L1
{
    public class MoveToFronAssembler<T> : AbstractSelfAssembler<T>, ISelfAssembler<T> where T : Item
    {
        public T[] Assmebly(T[] array, int index)
        {
            for (int i = index; i > 0; i--)
                array = Swap(array, index, index - 1);
            return array;
        }
    }
}