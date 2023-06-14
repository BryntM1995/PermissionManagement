using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PermissionManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace PermissionManagement.Model.Extension
{
    public static class SoftDeleteQueryExtension
    {
        public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
        {
            var filter = typeof(SoftDeleteQueryExtension)
                .GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(entityData.ClrType)
                .Invoke(null, new object[] { });

            entityData.SetQueryFilter((LambdaExpression)filter);
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity : class, ISoftDeleteEntity
        {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }
    }
}
