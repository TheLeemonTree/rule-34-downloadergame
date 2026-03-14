using Godot;
using System;

namespace R34.BooruDownloader
{
    public class PostBody
    {
        public string preview_url;
        public string sample_url;
        public string file_url;
        public float directory;
        public string hash;
        public int width;
        public int height;
        public int id;
        public string image;
        public int change;
        public string owner;
        public int parent_id;
        public string rating;
        public bool sample;
        public int sample_height;
        public int sample_width;
        public int score;
        public string tags;
        public string source;
        public string status;
        public bool has_notes;
        public int comment_count;

        public PostBody(Godot.Collections.Dictionary dict)
        {
            preview_url = dict["preview_url"].ToString();
            sample_url = dict["sample_url"].ToString();
            file_url = dict["file_url"].ToString();
            directory = float.Parse(dict["directory"].ToString());
            hash = dict["hash"].ToString();
            width = (int)float.Parse(dict["width"].ToString());
            height = (int)float.Parse(dict["height"].ToString());
            id = (int)float.Parse(dict["id"].ToString());
            image = dict["image"].ToString();
            change = (int)float.Parse(dict["change"].ToString());
            owner = dict["owner"].ToString();
            parent_id = (int)float.Parse(dict["parent_id"].ToString());
            rating = dict["rating"].ToString();
            sample = bool.Parse(dict["sample"].ToString());
            sample_height = (int)float.Parse(dict["sample_height"].ToString());
            sample_width = (int)float.Parse(dict["sample_width"].ToString());
            score = (int)float.Parse(dict["score"].ToString());
            tags = dict["tags"].ToString();
            source = dict["source"].ToString();
            status = dict["status"].ToString();
            has_notes = bool.Parse(dict["has_notes"].ToString());
            comment_count = (int)float.Parse(dict["comment_count"].ToString());
        }
    }
}
