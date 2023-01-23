using Chinook.Domain.Extensions;
using FluentValidation;
using Chinook.Domain.ApiModels;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<TrackApiModel>> GetAllTrack()
    {
        var tracks = await _trackRepository.GetAll();
        var trackApiModels = tracks.ConvertAll();
        var newPagedList = new List<TrackApiModel>(trackApiModels.ToList());
        return newPagedList;
    }

    public async Task<TrackApiModel?> GetTrackById(int id)
    {
        var track = await _trackRepository.GetById(id);
        if (track == null) return null;
        var trackApiModel = track.Convert();
        trackApiModel.Genre = await GetGenreById(trackApiModel.GenreId);
        trackApiModel.Album = await GetAlbumById(trackApiModel.AlbumId);
        trackApiModel.MediaType = await GetMediaTypeById(trackApiModel.MediaTypeId);
        if (trackApiModel.Album != null) trackApiModel.AlbumName = trackApiModel.Album.Title;

        if (trackApiModel.MediaType != null) trackApiModel.MediaTypeName = trackApiModel.MediaType.Name;
        if (trackApiModel.Genre != null) trackApiModel.GenreName = trackApiModel.Genre.Name;
        return trackApiModel;
    }

    public async Task<TrackApiModel> AddTrack(TrackApiModel newTrackApiModel)
    {
        await _trackValidator.ValidateAndThrowAsync(newTrackApiModel);

        var track = newTrackApiModel.Convert();

        await _trackRepository.Add(track);
        newTrackApiModel.Id = track.Id;
        return newTrackApiModel;
    }

    public async Task<bool> UpdateTrack(TrackApiModel trackApiModel)
    {
        await _trackValidator.ValidateAndThrowAsync(trackApiModel);

        var track = await _trackRepository.GetById(trackApiModel.Id);

        if (track == null) return false;
        track.Id = trackApiModel.Id;
        track.Name = trackApiModel.Name;
        track.AlbumId = trackApiModel.AlbumId;
        track.MediaTypeId = trackApiModel.MediaTypeId;
        track.GenreId = trackApiModel.GenreId;
        track.Composer = trackApiModel.Composer ?? string.Empty;
        track.Milliseconds = trackApiModel.Milliseconds;
        track.Bytes = trackApiModel.Bytes;
        track.UnitPrice = trackApiModel.UnitPrice;

        return await _trackRepository.Update(track);
    }

    public Task<bool> DeleteTrack(int id)
        => _trackRepository.Delete(id);
    
    public async Task<List<TrackApiModel>?> GetTrackByAlbumId(int id)
    {
        var tracks = await _trackRepository.GetByAlbumId(id);
        var trackApiModels = tracks.ConvertAll();
        var newPagedList = new List<TrackApiModel>(trackApiModels.ToList());
        return newPagedList;
    }

    public async Task<List<TrackApiModel>> GetTrackByGenreId(int id)
    {
        var tracks = await _trackRepository.GetByGenreId(id);
        var trackApiModels = tracks.ConvertAll();
        var newPagedList = new List<TrackApiModel>(trackApiModels.ToList());
        return newPagedList;
    }

    public async Task<List<TrackApiModel>> GetTrackByMediaTypeId(int id)
    {
        var tracks = await _trackRepository.GetByMediaTypeId(id);
        var trackApiModels = tracks.ConvertAll();
        var newPagedList = new List<TrackApiModel>(trackApiModels.ToList());
        return newPagedList;
    }

    public async Task<List<TrackApiModel>> GetTrackByPlaylistId(int id)
    {
        var tracks = await _trackRepository.GetByPlaylistId(id);
        var trackApiModels = tracks.ConvertAll();
        var newPagedList = new List<TrackApiModel>(trackApiModels.ToList());
        return newPagedList;
    }

    public async Task<List<TrackApiModel>> GetTrackByArtistId(int id)
    {
        var tracks = await _trackRepository.GetByArtistId(id);
        var trackApiModels = tracks.ConvertAll();
        var newPagedList = new List<TrackApiModel>(trackApiModels.ToList());
        return newPagedList;
    }

    public async Task<List<TrackApiModel>> GetTrackByInvoiceId(int id)
    {
        var tracks = await _trackRepository.GetByInvoiceId(id);
        var trackApiModels = tracks.ConvertAll();
        var newPagedList = new List<TrackApiModel>(trackApiModels.ToList());
        return newPagedList;
    }
}