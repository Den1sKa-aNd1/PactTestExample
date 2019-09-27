using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Fakes
{
    public class FakeValueService: IValuesService
    {
        public FakeValueService()
        {
        }

        public SomeDto GetSomeDto()
        {
            return new SomeDto { NumberParam = 999, StringParam = "fake dto" };
        }
    }
}
