using Student.DAL.Interfaces;
using Student.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.DAL.Repository
{
    public class SystemReposytory<T> : ISaveStrategy<T> where T : Student.DAL.Model.Student
    {
        private SaveContext<T> context;

        public SystemReposytory(SaveType type)
        {
            if (type == SaveType.xml)
                context = new SaveContext<T>(new XmlStrategy<T>());
            else
                context = new SaveContext<T>(new JsonStrategy<T>());
        }

        public async Task<bool> AddNewStudent(T value)
        {
            var result = await context.WriteToFileEntity(value);
            return result;
        }

        public async Task<T> Delete(int id)
        {
            var entities = await context.ReadFromFile();
            var entity = entities.Where(x => x.Id == id);
            if (entity is null)
                return null;
            else
            {
                var newCollection = entities.Where(x => x.Id != id).ToList();
                await context.WriteToFileCollection(newCollection);
                return entity as T;

            }
        }

        public async Task<List<T>> GetAll()
        {
            return await context.ReadFromFile();
        }

        public async Task<List<T>> GetByGroupName(string groupName)
        {
            var entyties = await context.ReadFromFile();
            var entitiesGroup = entyties.Where(x => x.Group == groupName).ToList();
            return entitiesGroup;
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetByUniversity(string university)
        {
            var entities = await context.ReadFromFile();
            var entitiesGroup = entities
                                                .Where(u => u.University == university)
                                                .ToList();
            return entitiesGroup;
        }

        public async Task<T> UpdateValue(T entity)
        {
            var entities = await context.ReadFromFile();
            var searchEntity = entities.Where(item => item.Id == entity.Id);
            if (searchEntity is null)
                return null;
            else
            {
                await context.WriteToFileEntity(entity);
                return searchEntity as T;
            }
        }
    }

    public class SaveContext<T> where T : class
    {
        private IFileStrategy<T> _fileSatrategy;

        public SaveContext() { }

        public SaveContext(IFileStrategy<T> fileStrategy)
        {
            this._fileSatrategy = fileStrategy;
        }

        public void ChangeSaveStrategy(IFileStrategy<T> fileStrategy)
        {
            this._fileSatrategy = fileStrategy;
        }

        public async Task<bool> WriteToFileEntity(T data)
        {
            var result = await _fileSatrategy.WriteToFileAsync(data);
            return result;
        }

        public async Task<bool> WriteToFileCollection(List<T> data)
        {
            var result = await _fileSatrategy.WriteToFileCollectionAsync(data);
            return result;
        }

        public async Task<List<T>> ReadFromFile()
        {
            var result = await _fileSatrategy.ReadFromFileAsync();
            return result;
        }
    }
}
