using System;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class ValuesService: IValuesService
    {
        public ValuesService()
        {
        }

        public SomeDto GetSomeDto()
        {
            return new SomeDto { NumberParam = 1, StringParam = "str" };
        }
    }
}
