using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstDotNet8API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private static List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Item1" },
            new Item { Id = 2, Name = "Item2" },
            new Item { Id = 3, Name = "Item3" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get()
        {
            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<Item> Post([FromBody] Item newItem)
        {
            newItem.Id = items.Max(i => i.Id) + 1;
            items.Add(newItem);
            return CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Item updatedItem)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = updatedItem.Name;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            items.Remove(item);
            return NoContent();
        }
    }
}
