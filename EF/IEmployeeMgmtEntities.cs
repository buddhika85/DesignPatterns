using System.Data.Entity;

namespace EF
{
    public interface IEmployeeMgmtEntities
    {
        DbSet<TBL_DEPARTMENT> TBL_DEPARTMENT { get; set; }
        DbSet<TBL_EMPLOYEE> TBL_EMPLOYEE { get; set; }
    }
}