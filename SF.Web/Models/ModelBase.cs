﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SF.Web.Models
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public abstract partial class ModelBase
    {
        public ModelBase()
        {

        }

        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }


    }
    /// <summary>
    /// 模型基类
    /// </summary>
    public abstract partial class EntityModelBase : ModelBase
    {
        public virtual long Id { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public int SortIndex { get; set; } = 1;
    }
    public abstract partial class TabbableModel : EntityModelBase
    {

        public virtual string[] LoadedTabs { get; set; }
    }

}
