﻿using Chinook.Domain.Entities;
using Chinook.Domain.Supervisor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class PlaylistsController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public PlaylistsController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    //[HttpGet("odata/Playlists")]
    public async Task<ActionResult<List<Playlist>>> GetAll()
    {
        try
        {
            var playlists = await _chinookSupervisor.GetAllPlaylist();
            return Ok(playlists);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [EnableQuery]
    [HttpGet("odata/Playlists({id})")]
    public async Task<ActionResult<Playlist>> Get([FromRoute] int id)
    {
        try
        {
            var playlist = await _chinookSupervisor.GetPlaylistById(id);
            return Ok(playlist);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPost("odata/Playlists")]
    public async Task<ActionResult<Playlist>> Post([FromBody] Playlist input)
    {
        try
        {
            var playlist = await _chinookSupervisor.AddPlaylist(input);
            return Ok(playlist);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPut("odata/Playlists({id})")]
    public async Task<ActionResult<Playlist>> Put([FromRoute] int id, [FromBody] Playlist input)
    {
        try
        {
            var playlist = await _chinookSupervisor.UpdatePlaylist(input);
            return Ok(playlist);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("odata/Playlists({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeletePlaylist(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}