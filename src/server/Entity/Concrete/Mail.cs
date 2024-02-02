using Entity.Concrete.Common;

namespace Entity.Concrete
{
    public class Mail : BaseEntity
    {
        public string For { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
