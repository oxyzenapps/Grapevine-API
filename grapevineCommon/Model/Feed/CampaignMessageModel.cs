using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model.Feed
{
    public class CampaignMessageModel
    {
        public string EntityFeedChannelID { get; set; }
        public string ProjectID { get; set; }
        public string ListCampaignID { get; set; }
        public string MessagePurposeID { get; set; }
        public string LanguageID { get; set; }
        public string HeaderText { get; set; }
        public string MessageFeedID { get; set; }
        public string Message { get; set; }
        public string FooterText { get; set; }
        public List<ReplyButtonText> _ReplyButtonText { get; set; }
        public string URLText { get; set; }
        public string URLToOpen { get; set; }
        public string CallBackText { get; set; }
        public string CallBackNumber { get; set; }
        public string ViewLocationText { get; set; }
        public string vlLat { get; set; }
        public string vlLng { get; set; }
        public string ShareLocationText { get; set; }
        public string slLat { get; set; }
        public string slLng { get; set; }
        public string CreateCalEventText { get; set; }
        public string ActivityID { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public string EventStartDateTime { get; set; }
        public string EventEndDateTime { get; set; }
        public string MessageTemplateID { get; set; }
        public string FormName { get; set; }
        public string OptionsText { get; set; }
        public string OptionsTextList { get; set; }
        public string EmbeddedFormURL { get; set; }
        public string WorkteamID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateStatusID { get; set; }
        public string TemplateID { get; set; }
        public string MediaType { get; set; }
        public string MediaID { get; set; }
        public string ActivityChannelID { get; set; }
        public string Channel_Description { get; set; }
        public string ChannelLogo { get; set; }
        public string AppAccountID { get; set; }
        public string AppListingID { get; set; }
        public string LanguageDescription { get; set; }
        public string ShortCode { get; set; }
        public string Description { get; set; }
        public string App_ProjectID { get; set; }
        public string AppProject { get; set; }
        public string MessagePurpose { get; set; }
        public string ProjectName { get; set; }
        public string ProjectLogo { get; set; }
        public string Title { get; set; }
        public string PageFeedchannelID { get; set; }
        public string statusdatetime { get; set; }
        public List<Feed_ObjectFilePath> _FeedObjectFilePath { get; set; }
    }
    public class ReplyButtonText
    {
        public string type { get; set; }
        public RepliesButton reply { get; set; }
    }
    public class RepliesButton
    {
        public string id { get; set; }
        public string title { get; set; }
    }
}
