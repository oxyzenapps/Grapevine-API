using grapevineServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Data;

namespace grapevineApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class musicController : ControllerBase
	{
		private readonly UtilityService _utilityService;

		public musicController(UtilityService utilityService)
		{
			_utilityService = utilityService;
		}

		public class MediaHomePageRequest
		{
			public int LoginFeedChannelID { get; set; } = 0;
			public int PageID { get; set; } = 0;
			public int PageTypeID { get; set; } = 0;
			public int PageCategoryID { get; set; } = 0;
		}

		[HttpPost("mediaHomePage")]
		public async Task<IActionResult> mediaHomePage([FromBody] MediaHomePageRequest request)
		{
			int LoginFeedChannelID = request.LoginFeedChannelID;
			int PageID = request.PageID;
			int PageTypeID = request.PageTypeID;
			int PageCategoryID = request.PageCategoryID;

			string sqlQuery =
				"exec glivebooks.dbo.crm_get_feed_media_homepage " +
				$"@LoginFeedChannelID='{LoginFeedChannelID}'," +
				$"@PageID='{PageID}'," +
				$"@PageTypeID='{PageTypeID}'," +
				$"@PageCategoryID='{PageCategoryID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class MediaSearchRequest
		{
			public int LoginFeedChannelID { get; set; } = 0;
			public int PageID { get; set; } = 0;
			public string SearchString { get; set; } = "";
			public int OnlySearch { get; set; } = 0;
			public int SearchID { get; set; } = 0;
			public int ResultTypeID { get; set; } = 0;
			public int ID { get; set; } = 0;
			public int PageTypeID { get; set; } = 0;
			public int PageCategoryID { get; set; } = 0;
			public int PageNo { get; set; } = 0;
			public int PageSize { get; set; } = 20;
		}

		[HttpPost("mediaSearch")]
		public async Task<IActionResult> mediaSearch([FromBody] MediaSearchRequest request)
		{
			int LoginFeedChannelID = request.LoginFeedChannelID;
			int PageID = request.PageID;
			string SearchString = request.SearchString;
			int OnlySearch = request.OnlySearch;
			int SearchID = request.SearchID;
			int ResultTypeID = request.ResultTypeID;
			int ID = request.ID;
			int PageTypeID = request.PageTypeID;
			int PageCategoryID = request.PageCategoryID;
			int PageNo = request.PageNo;
			int PageSize = request.PageSize;

			string sqlQuery =
				"exec glivebooks.dbo.crm_get_feed_media_search " +
				$"@LoginFeedChannelID='{LoginFeedChannelID}'," +
				$"@PageID='{PageID}'," +
				$"@SearchString='{SearchString}'," +
				$"@OnlySearch='{OnlySearch}'," +
				$"@SearchID='{SearchID}'," +
				$"@ResultTypeID='{ResultTypeID}'," +
				$"@ID='{ID}'," +
				$"@PageTypeID='{PageTypeID}'," +
				$"@PageCategoryID='{PageCategoryID}'," +
				$"@PageNo='{PageNo}'," +
				$"@PageSize='{PageSize}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class GetFeedAlbumTypesRequest
		{
			public int AlbumTypeID { get; set; } = 0;
			public int FeedObjectTypeID { get; set; } = 0;
		}

		[HttpPost("getAlbumTypes")]
		public async Task<IActionResult> getAlbumTypes([FromBody] GetFeedAlbumTypesRequest request)
		{
			int AlbumTypeID = request.AlbumTypeID;
			int FeedObjectTypeID = request.FeedObjectTypeID;

			string sqlQuery =
				"exec glivebooks.dbo.crm_get_feed_album_types " +
				$"@AlbumTypeID='{AlbumTypeID}'," +
				$"@FeedObjectTypeiD='{FeedObjectTypeID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}
		public class InsertFeedLikesRequest
		{
			public int Website_ID { get; set; } = 0;
			public int FeedID { get; set; } = 0;
			public int CommentID { get; set; } = 0;
			public int ObjectID { get; set; } = 0;
			public int FeedLikeTypeID { get; set; } = 0;
			public int FeedLikedByContactID { get; set; } = 0;
		}

		[HttpPost("insertFeedLike")]
		public async Task<IActionResult> insertFeedLike([FromBody] InsertFeedLikesRequest request)
		{
			int Website_ID = request.Website_ID;
			int FeedID = request.FeedID;
			int CommentID = request.CommentID;
			int ObjectID = request.ObjectID;
			int FeedLikeTypeID = request.FeedLikeTypeID;
			int FeedLikedByContactID = request.FeedLikedByContactID;

			string sqlQuery =
				"exec glivebooks.dbo.crm_Insert_Feed_Likes " +
				$"@Website_ID='{Website_ID}'," +
				$"@FeedID='{FeedID}'," +
				$"@CommentID='{CommentID}'," +
				$"@ObjectID='{ObjectID}'," +
				$"@FeedLikeTypeID='{FeedLikeTypeID}'," +
				$"@FeedLikedByContactID='{FeedLikedByContactID}'";

			var result = await _utilityService.GetDataResultAsync(sqlQuery);

			if (result.errors.Any())
				return BadRequest(result.errors);

			return Ok(result.result);
		}

	}
}