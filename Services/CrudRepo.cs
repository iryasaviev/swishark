using Infrastructure;

namespace Services
{
    public class CrudRepo : Crud
    {
        public CrudRepo() : base(new SwisharkContext()) { }
    }
}