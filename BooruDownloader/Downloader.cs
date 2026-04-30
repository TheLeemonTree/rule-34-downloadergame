using Commons;
using Godot;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace R34.BooruDownloader
{
    public partial class Downloader : Node
    {
        [Export]
        private HttpRequest _bodyRequest;
        [Export]
        private HttpRequest _dataRequest;
        [Export]
        private HttpRequest _tagRequest;
        [Export]
        private TextEdit _tagsText;
        [Export]
        private TextureRect _imagePreview;
        [Export]
        private Button _downloadButton;

        private string _tags = "";
        private string _mainTag = "";
        private int _pageNum = 0;
        private int _tagCount = 0;
        private Godot.Collections.Array _postsArr;

        public string PostURL;
        public string TagURL;

        private PostBody _currentPost;
        private void SetPostURL()
        {
            PostURL = $"{Shareware.Instance.currentServerURL}&tags={_tags}&limit=1&pid={_pageNum}{Shareware.Instance.currentApiKey}";
            GD.Print(PostURL);
        }
        private void SetTagURL()
        {
            TagURL = $"{Shareware.Instance.currentServerTagURL}&name={_mainTag}{Shareware.Instance.currentApiKey}";
        }
        private string GetMainTag()
        {
            if (string.IsNullOrEmpty(_tags)) return "";
            var tagsArr = _tags.Split(' ');
            return tagsArr[0];
        }
        public override void _Ready()
        {
            SetPostURL();
            SetTagURL();
        }
        public void OnTagsTextTextChanged()
        {
            _tags = _tagsText.Text;
        }
        public async void OnButtonButtonDown()
        {
            _downloadButton.Disabled = true;
            _mainTag = GetMainTag();
            SetTagURL();
            StartTagRequest();
            await ToSignal(_tagRequest, "request_completed");
            GD.Print("Tag request done");
            int currentPageID = _pageNum * 42;
            while (_pageNum <= ((int)(_tagCount / 42) + 1))
            {
                GD.Print($"Page: {_pageNum}, ID: {currentPageID}");
                SetPostURL();
                GetBodyRequest();
                await ToSignal(_bodyRequest, "request_completed");
                GD.Print("Body request done");
                //await StartDownload(_postsArr);
                _pageNum++;
                await ToSignal(GetTree().CreateTimer(.25f), "timeout");
            }
            _downloadButton.Disabled = false;
            GD.Print("Download finished!");
        }
        private void StartTagRequest()
        {
            Error error = _tagRequest.Request(TagURL);
            if (error != Error.Ok)
            {
                GD.PushError("An error occurred while trying to get tag data");
            }
        }
        public void OnHttpTagRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
        {
            var xml = new XmlParser();

            if (xml.OpenBuffer(body) != Error.Ok)
            {
                GD.Print("Failed to open XML");
                return;
            }

            while (xml.Read() == Error.Ok)
            {
                if (xml.GetNodeType() == XmlParser.NodeType.Element && xml.GetNodeName() == "tag")
                {
                    _tagCount = xml.GetNamedAttributeValue("count").ToInt();
                    GD.Print($"Tag: {_mainTag}, Count: {_tagCount}");
                    break;
                }
            }
        }
        private void GetBodyRequest()
        {
            Error error = _bodyRequest.Request(PostURL);
            if (error != Error.Ok)
            {
                GD.PushError("An error occurred trying to get the body data.");
            }
        }
        public void OnHttpBodyRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
        {
            var json = new Json();
            json.Parse(body.GetStringFromUtf8());
            _postsArr = json.GetData().AsGodotArray();
            GD.Print(_postsArr);
            GD.Print(headers);
            //PostBody postBody = new PostBody(responseArr[0].AsGodotDictionary());
            //await StartDownload(responseArr);

        }
        private async Task StartDownload(Godot.Collections.Array arr)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                _currentPost = new PostBody(arr[i].AsGodotDictionary());
                Error error = _dataRequest.Request(_currentPost.file_url);

                GD.Print("making request");
                await ToSignal(_dataRequest, "request_completed");
                if (error != Error.Ok)
                {
                    GD.PushError("An error occurred trying to download image.");
                    GD.PushError(error);
                }
                GD.Print("Image downloaded");
            }
        }
        public void OnHttpDataRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
        {
            var image = new Image();
            if(_currentPost.file_url.EndsWith(".jpg") || _currentPost.file_url.EndsWith(".jpeg"))
            {
                Error error = image.LoadJpgFromBuffer(body);
                if (error != Error.Ok)
                {
                    GD.PushError("Couldn't load the image.");
                }
            }
            else if(_currentPost.file_url.EndsWith(".png"))
            {
                Error error = image.LoadPngFromBuffer(body);
                if (error != Error.Ok)
                {
                    GD.PushError("Couldn't load the image.");
                }
            }
            else if( (_currentPost.file_url.EndsWith(".gif") || _currentPost.file_url.EndsWith(".mp4")))
            {
                if (Shareware.Instance.ignoreVideos)
                {
                    GD.PushWarning("Video format detected, skipping download.");
                    return;
                }
                GD.Print("Video format detected, skipping preview.");
            }
            else
            {
                GD.PushWarning("Unsupported file format.");
                return;
            }
            string pathSave = $"{SaveSystem.Instance.saveData.SavePath}{_currentPost.hash}{Path.GetExtension(_currentPost.file_url)}";
            using var file = Godot.FileAccess.Open(pathSave, Godot.FileAccess.ModeFlags.Write);
            file.StoreBuffer(body);
            var texture = ImageTexture.CreateFromImage(image);
            _imagePreview.Texture = texture;
        }
    }
}
