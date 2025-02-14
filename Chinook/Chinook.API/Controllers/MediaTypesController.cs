﻿using Chinook.Domain.Entities;
using Chinook.Domain.Supervisor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class MediaTypesController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public MediaTypesController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    [HttpGet("odata/MediaTypes")]
    public async Task<ActionResult<List<MediaType>>> Get()
    {
        try
        {
            var mediaTypes = await _chinookSupervisor.GetAllMediaType();
            return Ok(mediaTypes);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [EnableQuery]
    [HttpGet("odata/MediaTypes({id})")]
    public async Task<ActionResult<MediaType>> Get([FromRoute] int id)
    {
        try
        {
            var mediaType = await _chinookSupervisor.GetMediaTypeById(id);
            return Ok(mediaType);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPost("odata/MediaTypes")]
    public async Task<ActionResult<MediaType>> Post([FromBody] MediaType input)
    {
        try
        {
            var mediaType = await _chinookSupervisor.AddMediaType(input);
            return Ok(mediaType);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPut("odata/MediaTypes({id})")]
    public async Task<ActionResult<MediaType>> Put([FromRoute] int id, [FromBody] MediaType input)
    {
        try
        {
            var mediaType = await _chinookSupervisor.UpdateMediaType(input);
            return Ok(mediaType);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("odata/MediaTypes({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteMediaType(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}