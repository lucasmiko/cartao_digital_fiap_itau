using DigitalBank.Api.Application.DTOs;
using DigitalBank.Api.Domain.Models;
using DigitalBank.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomersController(ICustomerRepository _customerRepository)
    {
        this._customerRepository = _customerRepository;
    }

    [HttpPost]
    public ActionResult<Customer> CreateCustomer([FromBody] CreateCustomerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Cpf))
        {
            return BadRequest("Nome e CPF são obrigatórios.");
        }

        if (!CpfUtils.IsValid(dto.Cpf))
        {
            return BadRequest("CPF inválido.");
        }

        var newCustomer = new Customer
        {
            Name = dto.Name,
            Cpf = dto.Cpf
        };

        var createdCustomer = _customerRepository.CreateCustomer(newCustomer);
        return CreatedAtAction(nameof(GetById), new { id = createdCustomer.Id }, createdCustomer);
    }

    [HttpGet]
    public ActionResult<List<Customer>> GetAllCustomers()
    {
        var customers = _customerRepository.GetAll();
        return Ok(customers);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Customer> GetById([FromRoute] int id)
    {
        var customer = _customerRepository.GetById(id);
        if (customer is null) return NotFound(new { Message = "Cliente não encontrado." });
        return Ok(customer);
    }
}
