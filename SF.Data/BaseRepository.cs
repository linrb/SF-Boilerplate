using Microsoft.EntityFrameworkCore;
using SF.Core.EFCore.UoW;
using SF.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Data
{
    /// <summary>
    /// 基础仓储接口
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public interface IBaseRepository<Entity> : IEFCoreQueryableRepository<Entity, long> where Entity : BaseEntity
    {
    }
    /// <summary>
    /// 基础仓库类
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class BaseRepository<Entity> : EFCoreQueryableRepository<Entity, long>, IBaseRepository<Entity> where Entity : BaseEntity
    {
        public BaseRepository(DbContext context) : base(context)
        {
        }
        public override IQueryable<Entity> QueryById(long id)
        {
            return Query().Where(e => e.Id == id);
        }
    }

}
