using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apicalldemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static readonly List<Student> _students = new()
        {
            new Student { Id = 1, Name = "Remya", Email = "remya@mail.com", Department = "IT", Marks = 85 },
            new Student { Id = 2, Name = "Soumya", Email = "soumya@mail.com", Department = "CSE", Marks = 90 }
        };
        // GET: api/student
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            return Ok(_students);
        }
        // GET: api/student/1
        [HttpGet("{id:int}")]
        public ActionResult<Student> GetById(int id)
        {
            var s = _students.FirstOrDefault(x => x.Id == id);
            if (s == null)
                return NotFound($"Student with ID {id} not found.");

            return Ok(s);
        }
        // POST: api/student
        [HttpPost]
        public ActionResult<Student> Create(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
                return BadRequest("Name is required.");

            student.Id = _students.Any() ? _students.Max(x => x.Id) + 1 : 1;
            _students.Add(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        // PUT: api/student/2
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Student student)
        {
            var existing = _students.FirstOrDefault(x => x.Id == id);
            if (existing == null)
                return NotFound();

            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Department = student.Department;
            existing.Marks = student.Marks;

            return NoContent();  // success, no body
        }

        // DELETE: api/student/2
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existing = _students.FirstOrDefault(x => x.Id == id);
            if (existing == null)
                return NotFound();

            _students.Remove(existing);
            return NoContent();
        }
    }
}
