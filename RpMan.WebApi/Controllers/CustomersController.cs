using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RpMan.Application.Customers.Queries.GetCustomersList;
using RpMan.Application.Customers.Queries.GetCustomerDetail;
using RpMan.Application.Customers.Commands.UpdateCustomer;
using RpMan.Application.Customers.Commands.CreateCustomer;
using RpMan.Application.Customers.Commands.DeleteCustomer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RpMan.Application.CQRS.Customers.Queries.GetCustomersPagedList;
using RpMan.WebApi.Helpers;

namespace RpMan.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Member,Admin,Moderator,VIP")]
    public class CustomersController : RpManControllerBase
    {
        // GET api/customers
        [HttpGet]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<CustomersListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCustomersListQuery()));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<CustomersPagedListViewModel>> GetAllPagedList([FromQuery]GetCustomersPagedListQuery request)
        {
            var response = await Mediator.Send(request);

            Response.AddPagination( response.Customers.CurrentPage
                                  , response.Customers.PageSize
                                  , response.Customers.TotalCount
                                  , response.Customers.TotalPages
                                  );

            return Ok(response);
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
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