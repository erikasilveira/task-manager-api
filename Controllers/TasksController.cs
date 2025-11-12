using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        // Lista de tarefas fake (temporária)
        private static List<string> tasks = new List<string> { "Estudar C#", "Criar API", "Dominar Backend" };

        // GET: api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetTasks()
        {
            return Ok(tasks);
        }

        // POST: api/tasks
        [HttpPost]
        public ActionResult AddTask([FromBody] string newTask)
        {
            if (string.IsNullOrWhiteSpace(newTask))
                return BadRequest("A tarefa não pode ser vazia.");

            tasks.Add(newTask);
            return Ok($"Tarefa '{newTask}' adicionada com sucesso!");
        }

        // DELETE: api/tasks/{index}
        [HttpDelete("{index}")]
        public ActionResult DeleteTask(int index)
        {
            if (index < 0 || index >= tasks.Count)
                return NotFound("Tarefa não encontrada.");

            string removed = tasks[index];
            tasks.RemoveAt(index);
            return Ok($"Tarefa '{removed}' removida com sucesso!");
        }
    }
}
