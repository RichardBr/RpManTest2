using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RpMan.Application.Customers.Queries.GetCustomersList;
using RpMan.Application.Customers.Queries.GetCustomerDetail;
using RpMan.Application.Customers.Commands.UpdateCustomer;
using RpMan.Application.Customers.Commands.CreateCustomer;
using RpMan.Application.Customers.Commands.DeleteCustomer;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace RpMan.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CustomersController : RpManControllerBase
    {
        // GET api/customers
        [HttpGet]
        public async Task<ActionResult<CustomersListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCustomersListQuery()));
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetailModel>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetCustomerDetailQuery { Id = id }));
        }

        // POST api/customers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreateCustomerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(string id, [FromBody]UpdateCustomerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteCustomerCommand { Id = id });

            return NoContent();
        }
    }
}