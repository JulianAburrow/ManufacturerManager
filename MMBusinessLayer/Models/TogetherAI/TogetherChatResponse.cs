namespace MMBusinessLayer.Models.TogetherAI;

public class TogetherChatResponse
{
    public List<TogetherChoice> Choices { get; set; } = [];
}

public class TogetherChoice
{
    public TogetherMessage Message { get; set; } = new TogetherMessage();
}   