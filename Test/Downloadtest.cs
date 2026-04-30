using Godot;
using System;

public partial class Downloadtest : Control
{

    private HttpRequest httpBody;
    private HttpRequest httpData;
    private TextureRect portrait;

    private string imageUrl = "";
    public override void _Ready()
    {
        // Create an HTTP request node and connect its completion signal.
        httpBody = GetNode<HttpRequest>("GetBody");
        httpData = GetNode<HttpRequest>("GetData");
        portrait = GetNode<TextureRect>("TextureRect");

        // Perform the HTTP request. The URL below returns a PNG image as of writing.
        Error error = httpBody.Request("");
        if (error != Error.Ok)
        {
            GD.PushError("An error occurred in the HTTP request.");
        }
    }
    private void OnGetBodyRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
    {
        if (result != (long)HttpRequest.Result.Success)
        {
            GD.PushError("Image couldn't be downloaded. Try a different image.");
        }
        var json = new Json();
        json.Parse(body.GetStringFromUtf8());
        var responseArr = json.GetData().AsGodotArray();
        var response = responseArr[0].AsGodotDictionary();
        imageUrl = response["file_url"].ToString();
        Error error = httpData.Request(imageUrl);
        if (error != Error.Ok)
        {
            GD.PushError("An error occurred in the HTTP request.");
        }
    }
    private void OnGetDataRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
    {
        var image = new Image();
        Error error = image.LoadJpgFromBuffer(body);
        if (error != Error.Ok)
        {
            GD.PushError("Couldn't load the image.");
        }
        string path = "res://SaveTest/test.jpeg";

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        file.StoreBuffer(body);
        var texture = ImageTexture.CreateFromImage(image);
        //string path = "user://downloaded_image.jpg";

        //using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        //file.StoreBuffer(body);

        //GD.Print("Saved to: " + path);
        portrait.Texture = texture;
    }


    //// Called when the HTTP request is completed.
    //private void HttpRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
    //{
    //    if (result != (long)HttpRequest.Result.Success)
    //    {
    //        GD.PushError("Image couldn't be downloaded. Try a different image.");
    //    }
    //    var json = new Json();
    //    json.Parse(body.GetStringFromUtf8());
    //    var responseArr = json.GetData().AsGodotArray();
    //    var response = responseArr[0].AsGodotDictionary();
    //    GD.Print(response["file_url"]);

    //    var image = new Image();
    //    Error error = image.LoadJpgFromBuffer(GD.VarToBytes(response["file_url"]));
    //    if (error != Error.Ok)
    //    {
    //        GD.PushError("Couldn't load the image.");
    //    }

    //    var texture = ImageTexture.CreateFromImage(image);

    //    // Display the image in a TextureRect node.
    //    var textureRect = new TextureRect();
    //    AddChild(textureRect);
    //    textureRect.Texture = texture;
    //}
}
