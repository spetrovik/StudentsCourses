using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;
        public StudentService(ISchoolDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentsCollectionName);
        }
        public async Task<List<Student>> GetAllAsync()
        {
            return await _students.Find(s => true).ToListAsync();
        }
        public async Task<Student> GetByIdAsync(string id)
        {
            return await _students.Find<Student>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Student> CreateAsync(Student student)
        {
            await _students.InsertOneAsync(student);
            return student;
        }
        public async Task UpdateAsync(string id, Student student)
        {
            await _students.ReplaceOneAsync(s => s.Id == id, student);
        }
        public async Task DeleteAsync(string id)
        {
            await _students.DeleteOneAsync(s => s.Id == id);
        }
    }
}
