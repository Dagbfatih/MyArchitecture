﻿using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.API
{
    public interface IControllerRepository<TEntity, TResult> where TEntity : class, IEntity, new()
    {
        TResult Add(TEntity entity);
        TResult Delete(TEntity entity);
        TResult Update(TEntity entity);
        TResult GetById(int id);
        TResult GetAll();
    }
}
