
using SF.Web.Models;
using System.Collections.Generic;
using AutoMapper;
using SF.Entitys.Abstraction;

namespace SF.Web.Base.DataContractMapper
{
    /// <summary>
    /// ����ӳ���Զ�ӳ������ID���������޸�����
    /// </summary>
    public class BaseCrudDtoMapper<TEntity, TDto, Tkey> : CrudDtoMapper<TEntity, TDto, Tkey>
       where TEntity : IEntityWithTypedId<Tkey>, new()
       where TDto : EntityModelBase<Tkey>, new()
    {
        /// <summary>
        /// DTOת�������ʵ��ӳ��
        /// </summary>
        /// <param name="dto">DTOʵ��ӳ��</param>
        /// <param name="entity">ʵ��ӳ��DTO</param>
        /// <returns>The entity</returns>
        protected override TEntity OnMapDtoToEntity(TDto dto, TEntity entity)
        {
            Mapper.Map<TDto, TEntity>(dto, entity);
            return entity;
        }
        /// <summary>
        /// �����ʵ��ת��DTOӳ��
        /// </summary>
        /// <param name="entity">ʵ��ӳ��</param>
        /// <param name="dto">DTOӳ��ʵ��</param>
        /// <returns>The dto</returns>
        protected override TDto OnMapEntityToDto(TEntity entity, TDto dto)
        {
            Mapper.Map<TEntity, TDto>(entity, dto);
            return dto;
        }
        /// <summary>
        /// �����ʵ��ת��List<dto>ӳ��
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override IEnumerable<TDto> OnMapEntityToDtos(IEnumerable<TEntity> entitys)
        {
            var dtos = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(entitys);
            return dtos;
        }
    }
}
