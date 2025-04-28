using LibraryManagement.Domain.Enums;
using LibraryManagement.Service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class ReservationsController
{
    [HttpGet]
    [Authorize(Roles = $"{Roles.Member},{Roles.SupperAdmin}")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
    {
        await Task.CompletedTask; // Simulate async work
        throw new NotImplementedException();
    }

    // GET /api/reservations/{id} - Consider if Member should see ANY reservation by ID.
    // Usually, only their own or requires Admin.
    [HttpGet("{id}")]
    [Authorize(Roles = $"{Roles.Member},{Roles.SupperAdmin}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(Guid id)
    {
        await Task.CompletedTask; // Simulate async work
        throw new NotImplementedException();
    }

    // POST - As per request, only Admin creates. This is unusual for a library system.
    // A Member typically creates their own reservation.
    // Consider adding logic here to check role or a separate Member endpoint.
    [HttpPost]
    [Authorize(Roles = Roles.SupperAdmin)]
    public async Task<ActionResult<ReservationDto>> PostReservation(CreateReservationDto createReservationDto)
    {
        await Task.CompletedTask; // Simulate async work
        throw new NotImplementedException(); 
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = Roles.SupperAdmin)]
    public async Task<IActionResult> PutReservation(Guid id, UpdateReservationDto updateReservationDto)
    {
        await Task.CompletedTask; // Simulate async work
        throw new NotImplementedException(); 
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Roles.SupperAdmin)]
    public async Task<IActionResult> DeleteReservation(Guid id)
    {
        await Task.CompletedTask; // Simulate async work
        throw new NotImplementedException(); 
    }
}