
using SF.Entitys.Abstraction;
using SF.Web.Models;
using System.Collections.Generic;

namespace SF.Web.Base.DataContractMapper
{
    /// <summary>
    /// ����ӳ���Զ�ӳ������ID���������޸�����
    /// </summary>
    public abstract class CrudDtoMapper<TEntity, TDto, Tkey> : ICrudDtoMapper<TEntity, TDto, Tkey>
    where TEntity : IEntityWithTypedId<Tkey>, new()
    where TDto : EntityModelBase<Tkey>, new()
    {
        /// <summary>
        /// DTOת�������ʵ��
        /// </summary>
        /// <param name="dto">DTO����Դ</param>
        /// <returns></returns>
        public TEntity MapDtoToEntity(TDto dto)
        {
            var entity = OnMapDtoToEntity(dto, new TEntity());

            return entity;
        }

        /// <summary>
        /// �����ʵ��ת��DTO
        /// </summary>
        /// <param name="entity">�����ʵ��</param>
        /// <returns></returns>
        public TDto MapEntityToDto(TEntity entity)
        {
            var dto = OnMapEntityToDto(entity, new TDto());

            return dto;
        }

        /// <summary>
        /// �����ʵ��ת��DTO
        /// </summary>
        /// <param name="entity">�����ʵ��</param>
        /// <param name="existingDto">��ʵ������DTO</param>
        /// <returns></returns>
        public TDto MapEntityToDto(TEntity entity, TDto existingDto)
        {
            var dto = OnMapEntityToDto(entity, existingDto);

            return dto;
        }

        /// <summary>
        /// DTOת�������ʵ��
        /// </summary>
        /// <param name="dto">DTO����Դ</param>
        /// <param name="existingEntity">��ʵ�����������ʵ��</param>
        /// <returns></returns>
        public TEntity MapDtoToEntity(TDto dto, TEntity existingEntity)
        {
            var entity = OnMapDtoToEntity(dto, existingEntity);

            return entity;
        }

        /// <summary>
        /// �����ʵ��ת��DTO
        /// </summary>
        /// <param name="entity">�����ʵ��</param>
        /// <param name="existingDto">��ʵ������DTO</param>
        /// <returns></returns>
        public IEnumerable<TDto> MapEntityToDtos(IEnumerable<TEntity> entitys)
        {
            var dtos = OnMapEntityToDtos(entitys);

            return dtos;
        }

        /// <summary>
        /// �����ʵ��ת��DTOӳ��
        /// </summary>
        /// <param name="entity">ʵ��ӳ��</param>
        /// <param name="dto">DTOӳ��ʵ��</param>
        /// <returns>The dto</returns>
        protected abstract TDto OnMapEntityToDto(TEntity entity, TDto dto);
        /// <summary>
        /// �����ʵ��ת��DTOӳ��
        /// </summary>
        /// <param name="entity">ʵ��ӳ��</param>
        /// <param name="dto">DTOӳ��ʵ��</param>
        /// <returns>The dto</returns>
        protected abstract IEnumerable<TDto> OnMapEntityToDtos(IEnumerable<TEntity> entitys);
        /// <summary>
        /// DTOת�������ʵ��ӳ��
        /// </summary>
        /// <param name="dto">DTOʵ��ӳ��</param>
        /// <param name="entity">ʵ��ӳ��DTO</param>
        /// <returns>The entity</returns>
        protected abstract TEntity OnMapDtoToEntity(TDto dto, TEntity entity);


    }
}
